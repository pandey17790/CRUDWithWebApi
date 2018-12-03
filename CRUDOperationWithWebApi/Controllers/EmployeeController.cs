using CRUDOperationWithWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace CRUDOperationWithWebApi.Controllers
{
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        Connection con = new Connection();

        [Route("getall")]
        [HttpGet]
        public IEnumerable<Employee> GetEmployees()
        {
            IEnumerable<Employee> emp = con.GetEmployees().ToList();
            return emp;
        }

        [Route("find/{code}")]
        [HttpGet]
        public HttpResponseMessage GetEmployeeByCode(int code)
        {
            Employee emp = new Employee();
            emp = con.GetEmployeeByCode(code);
            if (emp != null & emp.EmpCode > 0)
                return Request.CreateResponse(HttpStatusCode.OK, emp);
            else
                return Request.CreateResponse(HttpStatusCode.NotFound, $"Employee code:{code} not found.");
        }

        [Route("add")]
        [HttpPost]
        public HttpResponseMessage AddEmployee(Employee emp)
        {
            if (ModelState.IsValid)
            {
                bool isSuccess = false;
                isSuccess = con.AddEmployee(emp);
                if (isSuccess)
                    return Request.CreateResponse(HttpStatusCode.OK, "Record created");
                else
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failed");
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid object");

        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage UpdateEmployee(Employee emp)
        {
            bool isSuccess = false;
            isSuccess = con.UpdateEmployee(emp);
            if (isSuccess)
                return Request.CreateResponse(HttpStatusCode.OK, "Record updated");
            else
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Failed");
        }
    }
}
