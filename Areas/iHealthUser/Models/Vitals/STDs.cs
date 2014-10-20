using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.Vitals
{
    public class STDs
    {
        public bool Chlamydia { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string DomainId { get; set; }
        public bool Gonorrhea { get; set; }
        public bool HepatitisBnC { get; set; }
        public bool Herpes { get; set; }
        public bool Syphilis { get; set; }
        public string UserId { get; set; }
        public string dateSTDs
        {
            get
            {
                if (CreatedOn == null)
                {
                    return "";
                }
                return Convert.ToDateTime(CreatedOn).ToString("d");
            }
        }
        public string created { get; set; }
    }

    public class STDsServiceCalls
    {
        public string InsertSTDsInfo(STDs stds)
        {
            //RegisteredUser ruser = null;
            //ruser = new RegisteredUser();
            WebClient Proxy1 = new WebClient();
            Proxy1.Headers["Content-type"] = "application/json";
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(STDs));
            serializerToUplaod.WriteObject(ms, stds);
            string ServiceURl = DomainServerPath.Service+"Vitals/InsertSTDsInfo";
            byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
            Stream stream = new MemoryStream(data);

            DataContractJsonSerializer serializerToResult = new DataContractJsonSerializer(typeof(string));
            string InsertedID = serializerToResult.ReadObject(stream) as string;

            return InsertedID;
        }
        public IList<STDs> GetStdsbyUserId(string userid)
        {
            try
            {
                IList<STDs> userInfo = new List<STDs>();
                //string UserID = Convert.ToString(userid);
                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"Vitals/GetSTDsByUserId/" + userid;
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);

                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<STDs>));

                userInfo = OutPut.ReadObject(stream) as List<STDs>;
                return userInfo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }


}