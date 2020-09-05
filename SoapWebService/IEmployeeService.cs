using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Operations;

namespace SoapWebService
{
    [ServiceContract]
    public interface IEmployeeService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "")]
        List<Employee> GetEmployees();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Xml, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "{employeeNo}")]
        Employee GetEmployeeByNo(string employeeNo);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "")]
        void CreateEmployee(Employee employee);

        [OperationContract]
        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "{employeeNo}")]
        void UpdateEmployee(string employeeNo, Employee employee);

        [OperationContract]
        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "{employeeNo}")]
        void DeleteEmployee(string employeeNo);
    }
}