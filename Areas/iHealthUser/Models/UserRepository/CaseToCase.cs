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
    public class CaseToCase
    {
        public string CaseId { get; set; }
        public string RelatedCaseId { get; set; }
    }

    public class CreateCaseToCase
    {
        public static string InsertCaseToCaseInfo(CaseToCase Ctc)
        {
            string insertId = string.Empty;
            WebClient caseProxy1 = new WebClient();
            caseProxy1.Headers["Content-Type"] = "application/json";
            caseProxy1.Headers["Accept"] = "application/json";
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer searialzeToInsert = new DataContractJsonSerializer((typeof(CaseToCase)));
            searialzeToInsert.WriteObject(ms, Ctc);
            string ServiceURL = DomainServerPath.Service+"CaseManagement/InsertCaseToCase";
            byte[] data = caseProxy1.UploadData(ServiceURL, "POST", ms.ToArray());
            Stream strm = new MemoryStream(data);
            DataContractJsonSerializer searializeToResult = new DataContractJsonSerializer(typeof(string));
            insertId = searializeToResult.ReadObject(strm) as string;
            return insertId;
        }

    }
}