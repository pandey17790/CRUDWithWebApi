using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUDOperationWithWebApi.Models
{
    public class Employee
    {
        public Int32 EmpCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Designation { get; set; }
        public Int32 Salary { get; set; }
    }
}