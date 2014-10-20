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
    public class EditProcedure
    {
        public string  UpdateProcedure(Procedure pt)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Headers["Content-type"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(pt.GetType());
                serializer.WriteObject(ms, pt);
                string url = DomainServerPath.Service+"CaseManagement/UpdateProcedure/" + pt.ProcedureId + "";
                byte[] data = wc.UploadData(url, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                serializer = new DataContractJsonSerializer(typeof(string));
                string code = serializer.ReadObject(stream) as string;
                return code;
            }
            catch (Exception e)
            {
                return String.Empty;
            }
        }

    }
}