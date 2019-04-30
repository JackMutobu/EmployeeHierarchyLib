using EmployeeHierarchyLib;
using EmployeeHierarchyLib.Exceptions;
using System;
using Xunit;

namespace EmployeeHierarchy.Test
{
    public class EmployeesTest
    {
        [Fact]
        public void ShouldThrowInvalidArgumentExceptionOnInvalidSalary()
        {
            var testData =  $"Emplyee4,Employee2,500{Environment.NewLine}Employee3,Employee1,800{Environment.NewLine}Employee6,Employee1,invalidSalary{Environment.NewLine}Employee1,,1000{Environment.NewLine}Employee5,Employee1,500{Environment.NewLine}Employee2,Employee1,500{Environment.NewLine}";

            Assert.Throws<InvalidSalaryException>(() => new Employees(testData));
        }
        [Fact]
        public void ShouldThrowExceptionOnEmployeeReportingToMultipleManager()
        {
            var testData = $"Emplyee4,Employee2,500{Environment.NewLine}Employee3,Employee1,800{Environment.NewLine}Employee3,Employee2,800{Environment.NewLine}Employee1,,1000{Environment.NewLine}Employee5,Employee1,500{Environment.NewLine}Employee2,Employee1,500{Environment.NewLine}";


            Assert.Throws<MoreThanOneManagerException>(() => new Employees(testData));
        }
        [Fact]
        public void ShouldThrowExceptionIfTheresMoreThanOneCEO()
        {
            var testData = $"Emplyee4,Employee2,500{Environment.NewLine}Employee3,,800{Environment.NewLine}Employee1,,1000{Environment.NewLine}Employee5,Employee1,500{Environment.NewLine}Employee2,Employee1,500{Environment.NewLine}";


            Assert.Throws<MoreThanOneCEOException>(() => new Employees(testData));
        }
        [Fact]
        public void ShouldThrowExceptionIfTheresCircularReference()
        {
            var testData = $"Emplyee4,Employee2,500{Environment.NewLine}Employee3,,800{Environment.NewLine}Employee1,Employee5,1000{Environment.NewLine}Employee5,Employee1,500{Environment.NewLine}Employee2,Employee1,500{Environment.NewLine}";


            Assert.Throws<CircularReferenceException>(() => new Employees(testData));
        }        
        [Fact]
        public void ShouldThrowExceptionIfThereIsManagerThatIsNotEmploye()
        {
            var testData = $"Emplyee4,Employee2,500{Environment.NewLine}Employee3,,800{Environment.NewLine}Employee1,Employee20,1000{Environment.NewLine}Employee5,Employee1,500{Environment.NewLine}Employee2,Employee1,500{Environment.NewLine}";


            Assert.Throws<ManagerThatIsNotEmployeeException>(() => new Employees(testData));
        }
        [Theory]
        [InlineData(1800, "Employee2")]
        [InlineData(500,"Employee3")]
        [InlineData(3800, "Employee1")]
        public void GetSalaryBudget_ShouldReturnSumOfAllSalaries(int expectedSum, string managerId)
        {
            string input = $"Emplyee4,Employee2,500{Environment.NewLine}Employee3,Employee1,500{Environment.NewLine}Employee1,,1000{Environment.NewLine}Employee5,Employee1,500{Environment.NewLine}Employee2,Employee1,800{Environment.NewLine}Employee6,Employee2,500{Environment.NewLine}";
            Assert.Equal(expectedSum, new Employees(input).GetSalaryBudget(managerId));

        }
    }
}
