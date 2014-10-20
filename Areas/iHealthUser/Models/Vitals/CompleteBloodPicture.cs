using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Web.Mvc;
using System.Configuration;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.Vitals
{
    public class CompleteBloodPicture
    {
        public string DomainId { get; set; }
        public string UserId { get; set; }
        //public string BloodGroup { get; set; }
        //public string BloodPressure { get; set; }
        public string HIV { get; set; }
        public string HemoglobinCount { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string BloodSugar { get; set; }
        public string HepatitisBVirus { get; set; }
        public string Rubella { get; set; }
        public string Herpes { get; set; }
        public string PlateletCount { get; set; }
        public string WhiteBloodCellCount { get; set; }
        public string RedBloodCellCount { get; set; }
        public string BloodTestName { get; set; }
        public string BloodTestValue { get; set; }

        public string dateBP
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

        public string BloodTests { get; set; }

        public List<SelectListItem> BloodTestList { get; set; }

        public List<SelectListItem> NoOfBloodTests()
        {
            
            BloodTestList = new List<SelectListItem>();
            /* static path*/
           // string path = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/BloodTestsList/Bloodtestslist.txt");
            string path = ConfigurationManager.AppSettings["BloodTestsList"];
            //string path = @"C:\InndocsiHealth\BloodTestsList\Bloodtestslist.txt";
            /* static path*/
            List<string> aList = new List<string>();
            using (StreamReader r = new StreamReader(path))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    aList.Add(line);
                }
                foreach (string a in aList)
                {
                    var splitingValueOfBT = a.Split('@');
                    var preV = splitingValueOfBT[0].ToString();
                    var nexV = splitingValueOfBT[1].ToString();

                    if (preV == "Hemoglobin")
                    {
                        BloodTestList.Add(new SelectListItem() { Text = preV, Value = nexV, Selected = true });

                    }
                    else
                    {

                        BloodTestList.Add(new SelectListItem() { Text = preV, Value = nexV, Selected = false });
                    }

                }
            }
            return BloodTestList;
        }



    }

    public class CompleteBPServiceCalls
    {
        public string InsertCompleteBPInfo(CompleteBloodPicture cbp)
        {
            //RegisteredUser ruser = null;
            //ruser = new RegisteredUser();
            WebClient Proxy1 = new WebClient();
            Proxy1.Headers["Content-type"] = "application/json";
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(CompleteBloodPicture));
            serializerToUplaod.WriteObject(ms, cbp);
            string ServiceURl = DomainServerPath.Service+"Vitals/InsertCBPInfo";
            byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
            Stream stream = new MemoryStream(data);

            DataContractJsonSerializer serializerToResult = new DataContractJsonSerializer(typeof(string));
            string InsertedID = serializerToResult.ReadObject(stream) as string;

            return InsertedID;
        } // not using

        public IList<CompleteBloodPicture> GetCompleteBPbyUserId(string userid)
        {
            try
            {
                IList<CompleteBloodPicture> userInfo = new List<CompleteBloodPicture>();
                //string UserID = Convert.ToString(userid);
                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"Vitals/GetCBPByUserId/" + userid;
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);

                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<CompleteBloodPicture>));

                userInfo = OutPut.ReadObject(stream) as List<CompleteBloodPicture>;
                return userInfo;
            }
            catch
            {
                throw;
            }
        }

        public string InsertBloodInfo(CompleteBloodPicture cbp)
        {
            //RegisteredUser ruser = null;
            //ruser = new RegisteredUser();
            WebClient Proxy1 = new WebClient();
            Proxy1.Headers["Content-type"] = "application/json";
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(CompleteBloodPicture));
            serializerToUplaod.WriteObject(ms, cbp);
            string ServiceURl = DomainServerPath.Service+"Vitals/InsertBloodInfo";
            byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
            Stream stream = new MemoryStream(data);

            DataContractJsonSerializer serializerToResult = new DataContractJsonSerializer(typeof(string));
            string InsertedID = serializerToResult.ReadObject(stream) as string;

            return InsertedID;
        }

        public IList<CompleteBloodPicture> GetBloodInfobyUserId(string userid)
        {
            try
            {
                IList<CompleteBloodPicture> userInfo = new List<CompleteBloodPicture>();
                //string UserID = Convert.ToString(userid);
                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"Vitals/GetBIByUserId/" + userid;
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);

                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<CompleteBloodPicture>));

                userInfo = OutPut.ReadObject(stream) as List<CompleteBloodPicture>;
                return userInfo;
            }
            catch
            {
                throw;
            }
        }


    }
}