using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository
{
    public class CreateUserCase
    {
        public static string CreateCase(Cases cf, string GroupType)
        {
            WebClient caseProxy1 = new WebClient();
            caseProxy1.Headers["Content-Type"] = "application/json";
            caseProxy1.Headers["Accept"] = "application/json";
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer searialzeToInsert = new DataContractJsonSerializer((typeof(Cases)));
            searialzeToInsert.WriteObject(ms, cf);
            string ServiceURL = DomainServerPath.Service+"CaseManagement/InsertCase/" + GroupType;
            byte[] data = caseProxy1.UploadData(ServiceURL, "POST", ms.ToArray());
            Stream strm = new MemoryStream(data);
            DataContractJsonSerializer searializeToResult = new DataContractJsonSerializer(typeof(string));
            string succode = searializeToResult.ReadObject(strm) as string;
            return succode;
        }


    }
}