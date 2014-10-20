using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using System.Runtime.Serialization.Json;
using System.Net;
using System.IO;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository.Get
{
    public class GetLogToLog
    {
        public static IList<LogToLog> GetAllLogToLogByLogId(string LogId)
        {
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"HealthLogs/GetAllLogsToLogsByLogId/" + LogId + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<LogToLog>));
            IList<LogToLog> lstlogs = OutPut.ReadObject(stream) as List<LogToLog>;
            return lstlogs;
        }
    }
}