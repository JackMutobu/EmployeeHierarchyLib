using EmployeeHierarchyLib;
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


            Assert.Throws<ArgumentException>(() => new Employees(testData).LoadEmployees(testData));
        }
        [Fact]
        public void ShouldThrowExceptionOnEmployeeReportingToMultipleManager()
        {
            var testData = $"Emplyee4,Employee2,500{Environment.NewLine}Employee3,Employee1,800{Environment.NewLine}Employee3,Employee2,800{Environment.NewLine}Employee1,,1000{Environment.NewLine}Employee5,Employee1,500{Environment.NewLine}Employee2,Employee1,500{Environment.NewLine}";


            Assert.Throws<Exception>(() => new Employees(testData).CheckIfEmployeeHasMoreThanOneManager());
        }
        [Fact]
        public void ShouldThrowExceptionIfTheresMoreThanOneCEO()
        {
            var testData = $"Emplyee4,Employee2,500{Environment.NewLine}Employee3,,800{Environment.NewLine}Employee1,,1000{Environment.NewLine}Employee5,Employee1,500{Environment.NewLine}Employee2,Employee1,500{Environment.NewLine}";


            Assert.Throws<Exception>(() => new Employees(testData).CheckIfThereIsOnlyOneCEO());
        }
        [Fact]
        public void ShouldThrowExceptionIfTheresCircularReference()
        {
            var testData = $"Emplyee4,Employee2,500{Environment.NewLine}Employee3,,800{Environment.NewLine}Employee2,Employee4,1000{Environment.NewLine}Employee5,Employee1,500{Environment.NewLine}Employee2,Employee1,500{Environment.NewLine}";


            Assert.Throws<Exception>(() => new Employees(testData).CheckForCircularRedundancy());
        }
        [Fact]
        public void ShouldThrowExceptionIfThereIsManagerThatIsNotEmploye()
        {
            var testData = $"Emplyee4,Employee2,500{Environment.NewLine}Employee3,,800{Environment.NewLine}Employee1,Employee20,1000{Environment.NewLine}Employee5,Employee1,500{Environment.NewLine}Employee2,Employee1,500{Environment.NewLine}";


            Assert.Throws<Exception>(() => new Employees(testData));
        }
    }
}
