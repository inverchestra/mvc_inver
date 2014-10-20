using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserUtility;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserUtility
{
    public class HealthLogToUserLog
    {
        public static UserHealthLog PopulateToUserLog(HealthLog log)
        {
            UserHealthLog uLog = new UserHealthLog();
            uLog.logId = log.logId;
            uLog.logName = log.logName;
            uLog.logDescription = log.logDescription;
            uLog.symptoms = XmlStringListSerializer.Deserialize<List<UserSymptom>>(log.symptoms);
            uLog.ihealthuserId = log.ihealthuserID;
            uLog.domainId = log.domainId;
            uLog.date = log.createdOn.ToString();
            uLog.symptoms = uLog.symptoms.OrderByDescending(x => x.DateTime).ToList();//reversing the list items
            uLog.FirstObserved = log.FirstObserved; // sandeep added
            uLog.CreatedOn = log.createdOn;
            return uLog;
        }
    }
}