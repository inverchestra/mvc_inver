
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository.Create
{
    public class CreateTests
    {
        public static string InsertBloodPicture(BloodPicture es)
        {
            try
            {
                WebClient caseProxy1 = new WebClient();
                caseProxy1.Headers["Content-Type"] = "application/json";
                caseProxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer searialzeToInsert = new DataContractJsonSerializer((typeof(BloodPicture)));
                searialzeToInsert.WriteObject(ms, es);
                string ServiceURL = DomainServerPath.Service+"Vitals/InsertBloodPicture";
                byte[] data = caseProxy1.UploadData(ServiceURL, "POST", ms.ToArray());
                Stream strm = new MemoryStream(data);
                DataContractJsonSerializer searializeToResult = new DataContractJsonSerializer(typeof(string));
                string insertId = searializeToResult.ReadObject(strm) as string;
                return insertId;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static string InsertUrienePicture(UrinecompPicture es)
        {
            try
            {
                WebClient caseProxy1 = new WebClient();
                caseProxy1.Headers["Content-Type"] = "application/json";
                caseProxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer searialzeToInsert = new DataContractJsonSerializer((typeof(UrinecompPicture)));
                searialzeToInsert.WriteObject(ms, es);
                string ServiceURL = DomainServerPath.Service+"Vitals/InsertUrinePicture";
                byte[] data = caseProxy1.UploadData(ServiceURL, "POST", ms.ToArray());
                Stream strm = new MemoryStream(data);
                DataContractJsonSerializer searializeToResult = new DataContractJsonSerializer(typeof(string));
                string insertId = searializeToResult.ReadObject(strm) as string;
                return insertId;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public static string InsertSerumTest(SerumTest es)
        {
            try
            {
                WebClient caseProxy1 = new WebClient();
                caseProxy1.Headers["Content-Type"] = "application/json";
                caseProxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer searialzeToInsert = new DataContractJsonSerializer((typeof(SerumTest)));
                searialzeToInsert.WriteObject(ms, es);
                string ServiceURL = DomainServerPath.Service+"Vitals/InsertSerumTest";
                byte[] data = caseProxy1.UploadData(ServiceURL, "POST", ms.ToArray());
                Stream strm = new MemoryStream(data);
                DataContractJsonSerializer searializeToResult = new DataContractJsonSerializer(typeof(string));
                string insertId = searializeToResult.ReadObject(strm) as string;
                return insertId;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}