using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository
{
    public class EditMedicalSchedule
    {
        //public int UpdateMedicalSchedule(MedicalSchedule mes, int MsId)
        //{
        //    try
        //    {
        //        WebClient wc = new WebClient();
        //        wc.Headers["Content-type"] = "application/json";
        //        MemoryStream ms = new MemoryStream();
        //        DataContractJsonSerializer serializer = new DataContractJsonSerializer(mes.GetType());
        //        serializer.WriteObject(ms, mes);
        //        string url = DomainServerPath.Service+"CaseManagement/UpdateMedicalSchedule/" + MsId.ToString() + "";
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

        public string  UpdateMedicalSchedule(MedicalSchedule ms1)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Headers["Content-type"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(ms1.GetType());
                serializer.WriteObject(ms, ms1);
                string url = DomainServerPath.Service+"CaseManagement/UpdateMedicalSchedule/" + ms1.MedicalScheduleId + "";
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