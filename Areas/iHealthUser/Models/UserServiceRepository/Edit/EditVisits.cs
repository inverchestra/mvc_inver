using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository
{
    public class EditVisits
    {
        //public int UpdateVisit(Visits vt, int VsId)
        //{
        //    try
        //    {
        //        WebClient wc = new WebClient();
        //        wc.Headers["Content-type"] = "application/json";
        //        MemoryStream ms = new MemoryStream();
        //        DataContractJsonSerializer serializer = new DataContractJsonSerializer(vt.GetType());
        //        serializer.WriteObject(ms, vt);
        //        string url = DomainServerPath.Service+"CaseManagement/UpdateVisit/" + VsId.ToString() + "";
        //        byte[] data = wc.UploadData(url, "POST", ms.ToArray());
        //        Stream stream = new MemoryStream(data);
        //        serializer = new DataContractJsonSerializer(typeof(string));
        //        string code = serializer.ReadObject(stream) as string;
        //        return Convert.ToInt32(code);
        //    }
        //    catch (Exception e)
        //    {
        //        return 0;
        //    }
        //}

        public string  UpdateVisit(Visits vt)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Headers["Content-type"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(vt.GetType());
                serializer.WriteObject(ms, vt);
                string url = DomainServerPath.Service+"CaseManagement/UpdateVisit/" + vt.visitId + "";
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