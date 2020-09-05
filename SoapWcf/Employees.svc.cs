using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Operations;

namespace SoapWcf
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Employees" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Employees.svc or Employees.svc.cs at the Solution Explorer and start debugging.
    public class Employees : IEmployees
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public List<Employee> GetEmployees()
        {
            try
            {
                var sql = new Sql(_connectionString);
                _logger.Info("Getting all employees");
                return sql.GetEmployees();
            }
            catch (Exception exception)
            {
                _logger.Error("Getting all employees Error", exception);
                WebOperationContext ctx = WebOperationContext.Current;
                ctx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return null;
            }
        }

        public Employee GetEmployeeByNo(string employeeNo)
        {
            try
            {
                var sql = new Sql(_connectionString);
                var employee = sql.GetEmployeeByNo(employeeNo);
                _logger.Info("Getting employee by no");
                return employee;
            }
            catch (Exception exception)
            {
                _logger.Error("Getting employee by no Error!", exception);
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
                    _logger.Info("Bad request during user creation");
                    ctx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return;
                }

                var sql = new Sql(_connectionString);
                sql.CreateEmployee(employee);
                _logger.Info("Created user");
                UriTemplate template = new UriTemplate("{employeeNo}");
                var location = template.BindByPosition(match.BaseUri, employee.EMPNO);
                ctx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.Created;
                ctx.OutgoingResponse.SetStatusAsCreated(location);
            }
            catch (Exception exception)
            {
                _logger.Error("Getting employee by no Error!", exception);
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
                    _logger.Info("Bad request during user update");
                    ctx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return;
                }

                // search for employee
                var sql = new Sql(_connectionString);

                var dbEmployee = sql.GetEmployeeByNo(employeeNo);

                if (dbEmployee == null)
                {
                    _logger.Info("Update: user not found");
                    ctx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return;
                }

                //update employee
                sql.UpdateEmployee(employeeNo, employee);
                _logger.Info("User updated");
                ctx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.NoContent;
            }
            catch (Exception exception)
            {
                _logger.Error("Update Error", exception);
                ctx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
        }

        public void DeleteEmployee(string employeeNo)
        {
            WebOperationContext ctx = WebOperationContext.Current;
            try
            {
                var sql = new Sql(_connectionString);
                Employee employee = sql.GetEmployeeByNo(employeeNo);

                if (employee == null)
                {
                    _logger.Info("User not found during delete operation");
                    ctx.OutgoingResponse.StatusCode = HttpStatusCode.NotFound;
                    return;
                }

                sql.DeleteEmployee(employeeNo);
                _logger.Info("User deleted");
                ctx.OutgoingResponse.StatusCode = HttpStatusCode.NoContent;
            }
            catch (Exception exception)
            {
                _logger.Error("Update Error", exception);
                ctx.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
        }
    }
}
