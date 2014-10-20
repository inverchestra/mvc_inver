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
    public class LogToLog
    {
        public string LogId { get; set; }
        public string RelatedLogId { get; set; }

    }
    public class CreateLogToLog
    {
        public static string InsertLogToLogInfo(LogToLog ltl)
        {
            string insertId = "";
            WebClient caseProxy1 = new WebClient();
            caseProxy1.Headers["Content-Type"] = "application/json";
            caseProxy1.Headers["Accept"] = "application/json";
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer searialzeToInsert = new DataContractJsonSerializer((typeof(LogToLog)));
            searialzeToInsert.WriteObject(ms, ltl);
            string ServiceURL = DomainServerPath.Service+"HealthLogs/InsertLogToLog";
            byte[] data = caseProxy1.UploadData(ServiceURL, "POST", ms.ToArray());
            Stream strm = new MemoryStream(data);
            DataContractJsonSerializer searializeToResult = new DataContractJsonSerializer(typeof(string));
            insertId = searializeToResult.ReadObject(strm) as string;
            return insertId;
        }
    }
}