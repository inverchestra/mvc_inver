using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository
{
    public class CreateVisit
    {
        public static string CreateVisitInfo(Visits vt)
        {
            try
            {

                WebClient caseProxy1 = new WebClient();
                caseProxy1.Headers["Content-Type"] = "application/json";
                caseProxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer searialzeToInsert = new DataContractJsonSerializer((typeof(Visits)));
                searialzeToInsert.WriteObject(ms, vt);
                string ServiceURL = DomainServerPath.Service+"CaseManagement/InsertVisit";
                byte[] data = caseProxy1.UploadData(ServiceURL, "POST", ms.ToArray());
                ms = new MemoryStream(data);
                DataContractJsonSerializer searializeToResult = new DataContractJsonSerializer(typeof(string));
                string insertId = searializeToResult.ReadObject(ms) as string;
                return insertId;
            }
            catch(Exception e)
            {
                return  string.Empty;
            }
        }
    }
}