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
using InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary;

namespace InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary.Edit
{
    public class EditBranch
    {
        public static int UpdateBranch(Branch bran)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Headers["Content-type"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(bran.GetType());
                serializer.WriteObject(ms, bran);
                string url = DomainServerPath.Service+"HospitalInfo/UpdateBranch";
                byte[] data = wc.UploadData(url, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                serializer = new DataContractJsonSerializer(typeof(string));
                string code = serializer.ReadObject(stream) as string;
                return Convert.ToInt32(code);


            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public int DeleteBranch(string branchid)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Headers["Content-type"] = "application/json";
                MemoryStream ms = new MemoryStream();
                // DataContractJsonSerializer serializer = new DataContractJsonSerializer(bran.GetType());
                // serializer.WriteObject(ms, bran);
                string url = DomainServerPath.Service+"HospitalInfo/DeleteBranch/" + branchid + "";
                byte[] data = wc.UploadData(url, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(string));
                string code = serializer.ReadObject(stream) as string;
                return Convert.ToInt32(code);


            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}