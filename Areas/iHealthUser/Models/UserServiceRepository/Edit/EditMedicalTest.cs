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
    public class EditMedicalTest
    {
        public string  UpdateMedicalTest(MedicalTests mt)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Headers["Content-type"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(mt.GetType());
                serializer.WriteObject(ms, mt);
                string url = DomainServerPath.Service+"CaseManagement/UpdateMedicalTest/" + mt.MedicalTestId + "";
                byte[] data = wc.UploadData(url, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                serializer = new DataContractJsonSerializer(typeof(string));
                string code = serializer.ReadObject(stream) as string;
                return code;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

    }
}