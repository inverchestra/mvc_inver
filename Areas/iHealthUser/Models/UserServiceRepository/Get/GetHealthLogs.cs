using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserUtility;
using System.Runtime.Serialization.Json;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository
{
    public class GetHealthLogs
    {
        public static IList<UserHealthLog> GetAllHealthLogs(string UserId,string GroupType)
        {
            IList<HealthLog> lstEmp = null;
            lstEmp = new List<HealthLog>();
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"HealthLogs/GetAllLogs/" + UserId  +"/"+ GroupType + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<HealthLog>));
            lstEmp = OutPut.ReadObject(stream) as List<HealthLog>;

            //lstEmp = lstEmp.OrderByDescending(x => x.modifiedOn).ToList();//build it

            IList<UserHealthLog> uhlog = new List<UserHealthLog>();
            foreach (var item in lstEmp)
            {
                uhlog.Add(HealthLogToUserLog.PopulateToUserLog(item));
            }

            return uhlog;
        }
        //public static IList<UserHealthLog> GetAllHealthLogs(string UserId, string DoctorID, string stype)
        //{
        //    IList<HealthLog> lstEmp = null;
        //    lstEmp = new List<HealthLog>();
        //    WebClient PerInfoServiceProxy = new WebClient();
        //    string ServiceUrl = DomainServerPath.Service+"HealthLogs/GetAllLogs/" + UserId + "/" + DoctorID + "/" + stype + "";
        //    byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
        //    Stream stream = new MemoryStream(data);
        //    DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<HealthLog>));
        //    lstEmp = OutPut.ReadObject(stream) as List<HealthLog>;

        //    //lstEmp = lstEmp.OrderByDescending(x => x.modifiedOn).ToList();//build it

        //    IList<UserHealthLog> uhlog = new List<UserHealthLog>();
        //    foreach (var item in lstEmp)
        //    {
        //        uhlog.Add(HealthLogToUserLog.PopulateToUserLog(item));
        //    }

        //    return uhlog;
        //}

        public static UserHealthLog GetHealthLog(string logId)
        {
            HealthLog log = new HealthLog();
            WebClient LogServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"HealthLogs/GetLog/" + logId  + "";
            byte[] data = LogServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(HealthLog));
            log = OutPut.ReadObject(stream) as HealthLog;

            UserHealthLog ulog = HealthLogToUserLog.PopulateToUserLog(log);

            return ulog;
        }

        public static IList<UserHealthLog> GetAllSearchedLogs(SearchFields sFields)
        {
            IList<HealthLog> lstEmp = null;
            lstEmp = new List<HealthLog>();
            WebClient PerInfoServiceProxy = new WebClient();
            PerInfoServiceProxy.Headers["Content-Type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            //PerInfoServiceProxy.Headers.Add("Access-Control-Allow-Origin", "*");
            //PerInfoServiceProxy.Headers.Add("Access-Control-Allow-Methods", "POST");

            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer searialzeToInsert = new DataContractJsonSerializer((typeof(SearchFields)));
            searialzeToInsert.WriteObject(ms, sFields);
            string ServiceUrl = DomainServerPath.Service+"Search/GetLogsSearchList";
            byte[] data = PerInfoServiceProxy.UploadData(ServiceUrl, "POST", ms.ToArray());
            //   byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<HealthLog>));
            lstEmp = OutPut.ReadObject(stream) as List<HealthLog>;

            //lstEmp = lstEmp.OrderByDescending(x => x.modifiedOn).ToList();//build it

            IList<UserHealthLog> uhlog = new List<UserHealthLog>();
            foreach (var item in lstEmp)
            {
                uhlog.Add(HealthLogToUserLog.PopulateToUserLog(item));
            }
            return uhlog;

        }

        //public static IList<UserHealthLog> GetCustomHealthLog()
        //{
        //    IList<UserHealthLog> UserLogs = new List<UserHealthLog>();
        //    for (int i = 0; i < 10; i++)
        //    {
        //        UserLogs.Add(new UserHealthLog
        //        {
        //            logId = i,
        //            logName = "Log Name " + i,
        //            createdOn=DateTime.Now.Date.AddDays(-i),
        //            logDescription = "Log Description " + i,
        //            symptoms = GetHealthLogs.InsertSymptoms()
        //        });
        //    }
        //    return UserLogs;
        //}

        //public static IList<UserSymptom> InsertSymptoms()
        //{
        //    IList<UserSymptom> usym = new List<UserSymptom>();
        //    for (int i = 0; i < 5; i++)
        //    {
        //        usym.Add(new UserSymptom
        //        {
        //            Name = "Symptom Name " + i,
        //            Description = "Symptom Description " + i,
        //            Reasons = "Possible Reasons " + i,
        //            Medicines = "Medicines " + i,
        //            DateTime = DateTime.Now.AddDays(-i)
        //        });
        //    }
        //    return usym.OrderByDescending(x=>x.DateTime).ToList();
        //}

        //public static UserHealthLog GetCustomUserHealthLog(int logId)
        //{
        //    UserHealthLog hl = new UserHealthLog();
            
        //        hl.logId = logId;
        //        hl.logName = "Some Log Name";
        //        hl.createdOn = DateTime.Now;
        //        hl.symptoms = GetHealthLogs.InsertSymptoms();
        //        return hl;
            
        //}

    }
}