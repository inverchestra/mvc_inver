using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    public class CaseToLog
    {
        public string CaseId { get; set; }
        public string LogId { get; set; }
    }

    public class CreateCaseToLog
    {
        public static string InsertCaseToLogInfo(string logId, string caseId)
        {
            string insertId = string.Empty;
            try
            {

                WebClient caseProxy1 = new WebClient();
                caseProxy1.Headers["Content-Type"] = "application/json";
                caseProxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer searialzeToInsert = new DataContractJsonSerializer((typeof(string)));
                //searialzeToInsert.WriteObject(ms, Ctl);
                string ServiceURL = DomainServerPath.Service+"CaseManagement/InsertCaseToLog/" + logId + "/" + caseId;
                byte[] data = caseProxy1.UploadData(ServiceURL, "POST", ms.ToArray());
                Stream strm = new MemoryStream(data);
                DataContractJsonSerializer searializeToResult = new DataContractJsonSerializer(typeof(string));
                insertId = searializeToResult.ReadObject(strm) as string;
            }
            catch (Exception ex)
            {
                string s = Convert.ToString(ex);
            }
            return insertId;
        }


    }
}