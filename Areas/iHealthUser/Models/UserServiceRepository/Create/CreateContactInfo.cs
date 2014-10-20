using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository.Create
{
    public class CreateContactInfo
    {
        public static string InsertContactInfo(ContactUs vt)
        {
            try
            {

                WebClient caseProxy1 = new WebClient();
                caseProxy1.Headers["Content-Type"] = "application/json";
                caseProxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer searialzeToInsert = new DataContractJsonSerializer((typeof(ContactUs)));
                searialzeToInsert.WriteObject(ms, vt);
                string ServiceURL = DomainServerPath.Service+"Registration/InsertContactInfo";
                byte[] data = caseProxy1.UploadData(ServiceURL, "POST", ms.ToArray());
                ms = new MemoryStream(data);
                DataContractJsonSerializer searializeToResult = new DataContractJsonSerializer(typeof(string));
                string insertId = searializeToResult.ReadObject(ms) as string;
                return insertId;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }
    }
}