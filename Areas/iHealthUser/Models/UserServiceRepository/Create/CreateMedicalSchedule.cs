using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository
{
    public class CreateMedicalSchedule
    {
        public static string InsertMeidcalScheduleInfo(MedicalSchedule pr)
        {
            try
            {
                WebClient caseProxy1 = new WebClient();
                caseProxy1.Headers["Content-Type"] = "application/json";
                caseProxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer searialzeToInsert = new DataContractJsonSerializer((typeof(MedicalSchedule)));
                searialzeToInsert.WriteObject(ms, pr);
                string ServiceURL = DomainServerPath.Service+"CaseManagement/InsertMedicalSchedule";
                byte[] data = caseProxy1.UploadData(ServiceURL, "POST", ms.ToArray());
                Stream strm = new MemoryStream(data);
                DataContractJsonSerializer searializeToResult = new DataContractJsonSerializer(typeof(string));
                string insertId = searializeToResult.ReadObject(strm) as string;
                return insertId;
            }
            catch(Exception ex)
            {
                return string.Empty;
            }
        }
    }
}