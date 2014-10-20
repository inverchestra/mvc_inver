using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository
{
    public class EditUserCases
    {
        //public string UpdateCase(Cases c, int CaseId)
        //{
        //    WebClient Proxy1 = new WebClient();
        //    MemoryStream ms = new MemoryStream();
        //    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Cases));
        //    serializer.WriteObject(ms, c);
        //    string ServiceURl = DomainServerPath.Service+"CaseManagement/UpdateCase/" + CaseId.ToString();
        //    byte[] data = Proxy1.UploadData(ServiceURl, "PUT", ms.ToArray());
        //    ms = new MemoryStream(data);
        //    serializer = new DataContractJsonSerializer(typeof(string));
        //    string succode = serializer.ReadObject(ms) as string;

        //    return succode;
        //}

        public static string UpdateCase(Cases c, string GroupType)
        {
            WebClient Proxy1 = new WebClient();
            Proxy1.Headers["Content-type"] = "application/json";
            Proxy1.Headers["Accept"] = "application/json";


            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Cases));
            serializer.WriteObject(ms, c);
            string ServiceURl = DomainServerPath.Service+"CaseManagement/UpdateCase/" + c.CaseId.ToString() + "/" + GroupType + "";
            byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
            ms = new MemoryStream(data);
            serializer = new DataContractJsonSerializer(typeof(string));
            string succode = serializer.ReadObject(ms) as string;

            return succode;
        }
    }
}