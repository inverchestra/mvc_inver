using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Net;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository
{
    public class EditChart
    {
        public string UpdateChart(Charts ct)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Headers["Content-type"]="application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(ct.GetType());
                serializer.WriteObject(ms,ct);
                string url = DomainServerPath.Service+"CaseManagement/UpdateChart/" +ct.ChartId + "";
                byte[] data = wc.UploadData(url,"POST",ms.ToArray());
                Stream stream = new MemoryStream(data);
                serializer = new DataContractJsonSerializer(typeof(string));
                string code = serializer.ReadObject(stream) as string;
                return code;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

    }
}