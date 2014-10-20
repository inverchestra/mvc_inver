using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web.Optimization;
using System.ComponentModel.DataAnnotations;
using InnDocs.iHealth.Web.UI.Controllers;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;

namespace InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary.Edit
{
    public class EditDoctors
    {
        public static string UpdateDoctors(Doctors doc)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Headers["Content-type"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(doc.GetType());
                serializer.WriteObject(ms, doc);
                string url = DomainServerPath.Service+"HospitalInfo/UpdateDoctorById";
                byte[] data = wc.UploadData(url, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                serializer = new DataContractJsonSerializer(typeof(string));
                string code = serializer.ReadObject(stream) as string;
                return code;


            }
            catch (Exception e)
            {
                return "";
            }
        }


    }
}