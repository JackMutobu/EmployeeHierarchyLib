using System;

namespace EmployeeHierarchyLib.Exceptions
{

    public class InvalidSalaryException : ArgumentException
    {
        public InvalidSalaryException(string exceptionMessage,string parameter) : base(exceptionMessage,parameter)
        {
        }
    }
    public class MoreThanOneManagerException : Exception
    {
        public MoreThanOneManagerException(string exceptionMessage) : base(exceptionMessage)
        {
        }
    }
    public class MoreThanOneCEOException : Exception
    {
        public MoreThanOneCEOException(string exceptionMessage) : base(exceptionMessage)
        {
        }
    }
    public class CircularReferenceException : Exception
    {
        public CircularReferenceException(string exceptionMessage) : base(exceptionMessage)
        {
        }
    }
    public class ManagerThatIsNotEmployeeException : Exception
    {
        public ManagerThatIsNotEmployeeException(string exceptionMessage) : base(exceptionMessage)
        {
        }
    }
}
