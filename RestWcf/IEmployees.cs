using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Operations;
using SwaggerWcf.Attributes;

namespace RestWcf
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEmployees" in both code and config file together.
    [ServiceContract]
    [SwaggerWcf("EmployeeService/v1/employees")]
    public interface IEmployees
    {
        [OperationContract]
        [SwaggerWcfTag("Employees")]
        [SwaggerWcfPath("Get Employees", "Get all available employees")]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "")]
        List<Employee> GetEmployees();

        [OperationContract]
        [SwaggerWcfTag("Employees")]
        [SwaggerWcfPath("Get Employee", "Get employee by no")]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{employeeNo}")]
        Employee GetEmployeeByNo(string employeeNo);

        [OperationContract]
        [SwaggerWcfTag("Employees")]
        [SwaggerWcfPath("Create Employee", "Create a new Employee")]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "")]
        void CreateEmployee(Employee employee);

        [OperationContract]
        [SwaggerWcfTag("Employees")]
        [SwaggerWcfPath("Update Employee", "Update an existing Employee")]
        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{employeeNo}")]
        void UpdateEmployee(string employeeNo, Employee employee);

        [OperationContract]
        [SwaggerWcfTag("Employees")]
        [SwaggerWcfPath("Delete Employee", "Delete an existing Employee")]
        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{employeeNo}")]
        void DeleteEmployee(string employeeNo);
    }
}
