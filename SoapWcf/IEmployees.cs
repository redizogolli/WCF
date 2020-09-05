using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Operations;

namespace SoapWcf
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEmployees" in both code and config file together.
    [ServiceContract]
    public interface IEmployees
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
