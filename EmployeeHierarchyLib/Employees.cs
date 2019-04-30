using EmployeeHierarchyLib.Exceptions;
using EmployeeHierarchyLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeHierarchyLib
{
    public class Employees
    {
        private List<Employee> employees = new List<Employee>();
        public Employees(string csvInput)
        {
            LoadEmployees(csvInput);//1
            CheckIfEmployeeHasMoreThanOneManager();//2

            CheckIfThereIsOnlyOneCEO();//3
            CheckForCircularRedundancy();//4
            CheckIfAllManagersAreEmployee();//5
        }

        private void CheckIfEmployeeHasMoreThanOneManager()
        {
            employees.ForEach(emp =>
            {
                var managers = employees.Where(em => em.Id == emp.Id).Select(manager => manager.ManagerId);

                if (managers.Count() > 1)
                {
                    foreach (var manager in managers)
                    {
                        foreach (var manager1 in managers)
                        {
                            if (manager != manager1)
                            {
                                throw new MoreThanOneManagerException($"{emp.Id} has more than one manager");
                            }
                        }
                    }
                }
            });
        }

        private void CheckIfAllManagersAreEmployee()
        {
            employees.ForEach(emp =>
            {
                if (!String.IsNullOrEmpty(emp.ManagerId))
                {
                    var isManagerAnEmployee = employees.Any(m => m.Id == emp.ManagerId);
                    if (!isManagerAnEmployee)
                    {
                        throw new ManagerThatIsNotEmployeeException($"{emp.ManagerId} is not en employee");
                    }
                }
            });
        }

        private void CheckForCircularRedundancy()
        {
            employees.ForEach(emp =>
            {
                var managerOfActualEmployeeManager = employees.Where(em => em.Id == emp.ManagerId);
                
                if (managerOfActualEmployeeManager?.FirstOrDefault()?.ManagerId == emp.Id)
                {
                    throw new CircularReferenceException($"There is a circular redundacy.{emp.Id} is reporting to {managerOfActualEmployeeManager?.FirstOrDefault()?.Id} that is also under {emp.Id}");
                }
            });
        }

        private void CheckIfThereIsOnlyOneCEO()
        {
            var numberOfEmployeeWithoutManagers = employees.Select(m => m.ManagerId).Where(s => String.IsNullOrEmpty(s)).Count();
            if (numberOfEmployeeWithoutManagers > 1)
            {
                throw new MoreThanOneCEOException("There are more than one employee with no manager. We are only expecting one CEO.");
            }
        }

        private  void LoadEmployees(string csvInput)
        {
            int lineIndex = 1;
            foreach (var row in csvInput.Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
            {
                if (!String.IsNullOrEmpty(row))
                {
                    var line = row.Split(',');
                    var employee = new Employee
                    {
                        Id = line[0],
                        ManagerId = line[1],
                        Salary = CheckSalaryValidity(line[2],lineIndex)
                    };
                    employees.Add(employee);
                }
                lineIndex++;
            }

            int CheckSalaryValidity(string salary,int lineAtIndex)
            {
                int validSalary = default;

                if (int.TryParse(salary, out validSalary))
                {
                    return validSalary;
                }
                else
                {
                    throw new InvalidSalaryException($"{salary} is not a valid salary at line {lineAtIndex} in the csv string",nameof(salary));
                }
            }
        }

        public long GetSalaryBudget(string manager)
        {

            long GetSalaries(string managerId, long initialSalary)
            {
                long employeesSalary = initialSalary;
                List<Employee> employeesUnderThisManager = employees.Where(man => man.ManagerId == managerId).ToList();

                foreach (var employee in employeesUnderThisManager)
                {
                    employeesSalary += employee.Salary;
                    employeesSalary = GetSalaries(employee.Id, employeesSalary);
                }
                return employeesSalary;
            }
            long employeesSalaries = GetSalaries(manager, 0); 
            var managerSalary = employees.FirstOrDefault(man => man.Id == manager).Salary;
            return employeesSalaries + managerSalary;
        }
    }
}
