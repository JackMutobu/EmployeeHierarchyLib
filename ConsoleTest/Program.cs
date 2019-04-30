using System;
using System.IO;

namespace ConsoleTest
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string input = $"Emplyee4,Employee2,500{Environment.NewLine}Employee3,Employee1,800{Environment.NewLine}Employee1,,1000{Environment.NewLine}Employee5,Employee1,500{Environment.NewLine}Employee2,Employee1,500{Environment.NewLine}";
            EmployeeHierarchyLib.Employees emp = new EmployeeHierarchyLib.Employees(input);
            Console.WriteLine(emp.GetSalaryBudget("Employee2"));
        }
    }
}
