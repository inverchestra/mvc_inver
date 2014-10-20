using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserUtility;
using InnDocs.iHealth.Web.UI.Models;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserUtility
{
    public class UserLogToHealthLog
    {
        public static HealthLog PopulateToHealthLog(UserHealthLog userLog)
        {
            HealthLog log = new HealthLog();

            log.createdOn = DateTime.Now; //Convert.ToDateTime(userLog.date + "" + userLog.time);//ToDateTime(userLog.date+" "+ userLog.time);
            log.logName = userLog.logName;
            log.logDescription = userLog.logDescription;
            log.symptoms = XmlStringListSerializer.ToXmlString(userLog.symptoms);
            log.FirstObserved = userLog.FirstObserved;
            log.logId = userLog.logId;
            return log;
        }
        public static HealthLog PopulateToHealthLog(UserSymptom symptom)
        {
            //string type = "Hospital";
           UserHealthLog h = GetHealthLogs.GetHealthLog(symptom.LogId);
            h.symptoms.Add(symptom);
            return PopulateToHealthLog(h);
        }



        public static DateTime ToDateTime(string dateTime)
        {
            return DateTime.ParseExact(dateTime, "g", new CultureInfo("fr-FR"));
        }
    }
}