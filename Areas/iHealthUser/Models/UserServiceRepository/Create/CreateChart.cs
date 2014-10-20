using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository
{
    public class CreateChart
    {
        public static string InsertChartInfo(Charts ct)
        {
            try
            {
                WebClient caseProxy1 = new WebClient();
                caseProxy1.Headers["Content-Type"] = "application/json";
                caseProxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer searialzeToInsert = new DataContractJsonSerializer((typeof(Charts)));
                searialzeToInsert.WriteObject(ms, ct);
                string ServiceURL = DomainServerPath.Service+"CaseManagement/InsertChart";
                byte[] data = caseProxy1.UploadData(ServiceURL, "POST", ms.ToArray());
                Stream strm = new MemoryStream(data);
                DataContractJsonSerializer searializeToResult = new DataContractJsonSerializer(typeof(string));
                string insertId = searializeToResult.ReadObject(strm) as string;
                return insertId;
            }
            catch(Exception e)
            {
                return string.Empty;
            }
        }

    }
}