using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Operations;

namespace RestWebService
{
    [ServiceContract]
    public interface IEmployeeService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "")]
        List<Employee> GetEmployees();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "{employeeNo}")]
        Employee GetEmployeeByNo(string employeeNo);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "")]
        void CreateEmployee(Employee employee);

        [OperationContract]
        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{employeeNo}")]
        void UpdateEmployee(string employeeNo, Employee employee);

        [OperationContract]
        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{employeeNo}")]
        void DeleteEmployee(string employeeNo);
    }
}