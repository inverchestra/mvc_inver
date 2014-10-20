using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.Vitals
{
    public class SkinTest
    {
        public bool Acne { get; set; }
        public bool ColourOfNipples { get; set; }
        public bool ComplexionOfSkin { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string DomainId { get; set; }
        public bool PigmentationSpotsOnSkin { get; set; }
        public bool StretchMarks { get; set; }
        public string UserId { get; set; }
        public string dateST
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

    public class SkinTestServiceCalls
    {
        public string InsertSkinTestInfo(SkinTest st)
        {
            //RegisteredUser ruser = null;
            //ruser = new RegisteredUser();
            WebClient Proxy1 = new WebClient();
            Proxy1.Headers["Content-type"] = "application/json";
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(SkinTest));
            serializerToUplaod.WriteObject(ms, st);
            string ServiceURl = DomainServerPath.Service+"Vitals/InsertSkinTestInfo";
            byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
            Stream stream = new MemoryStream(data);

            DataContractJsonSerializer serializerToResult = new DataContractJsonSerializer(typeof(string));
            string InsertedID = serializerToResult.ReadObject(stream) as string;

            return InsertedID;
        }
        public IList<SkinTest> GetSkintestbyUserId(string userid)
        {
            try
            {
                IList<SkinTest> userInfo = new List<SkinTest>();
                //string UserID = Convert.ToString(userid);
                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"Vitals/GetSkinTestByUserId/" + userid;
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);

                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<SkinTest>));

                userInfo = OutPut.ReadObject(stream) as List<SkinTest>;
                return userInfo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}