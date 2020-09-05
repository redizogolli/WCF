using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.ServiceModel.Web;
using Operations;

namespace SoapWebService
{
    public class EmployeeService : IEmployeeService
    {
        public List<Employee> GetEmployees()
        {
            try
            {
                var sql = new Sql();
                return sql.GetEmployees();
            }
            catch (Exception)
            {
                WebOperationContext ctx = WebOperationContext.Current;
                ctx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return null;
            }
        }

        public Employee GetEmployeeByNo(string employeeNo)
        {
            try
            {
                var sql = new Sql();
                var employee = sql.GetEmployeeByNo(employeeNo);
                return employee;
            }
            catch (Exception)
            {
                WebOperationContext ctx = WebOperationContext.Current;
                ctx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return null;
            }
        }

        public void CreateEmployee(Employee employee)
        {
            WebOperationContext ctx = WebOperationContext.Current;
            UriTemplateMatch match = ctx.IncomingRequest.UriTemplateMatch;
            try
            {
                var results = new List<ValidationResult>();
                var isValid = Validator.TryValidateObject(
                    employee,
                    new ValidationContext(employee ?? new Employee(), null, null),
                    results,
                    true);

                if (!isValid)
                {
                    ctx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return;
                }

                var sql = new Sql();
                sql.CreateEmployee(employee);

                UriTemplate template = new UriTemplate("{employeeNo}");
                var location = template.BindByPosition(match.BaseUri, employee.EMPNO);
                ctx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.Created;
                ctx.OutgoingResponse.SetStatusAsCreated(location);
            }
            catch (Exception)
            {
                ctx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
        }

        public void UpdateEmployee(string employeeNo, Employee employee)
        {
            WebOperationContext ctx = WebOperationContext.Current;
            try
            {
                //validate model
                var results = new List<ValidationResult>();
                var isValid = Validator.TryValidateObject(
                    employee,
                    new ValidationContext(employee ?? new Employee(), null, null),
                    results,
                    true);

                if (!isValid)
                {
                    ctx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return;
                }

                // search for employee
                var sql = new Sql();

                var dbEmployee = sql.GetEmployeeByNo(employeeNo);

                if (dbEmployee == null)
                {
                    ctx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return;
                }

                //update employee
                sql.UpdateEmployee(employeeNo, employee);
                ctx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.NoContent;
            }
            catch (Exception)
            {
                ctx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
        }

        public void DeleteEmployee(string employeeNo)
        {
            WebOperationContext ctx = WebOperationContext.Current;
            try
            {
                var sql = new Sql();
                var employee = sql.GetEmployeeByNo(employeeNo);

                if (employee == null)
                {
                    ctx.OutgoingResponse.StatusCode = HttpStatusCode.NotFound;
                    return;
                }

                sql.DeleteEmployee(employeeNo);

                ctx.OutgoingResponse.StatusCode = HttpStatusCode.NoContent;
            }
            catch (Exception)
            {
                ctx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
        }
    }
}