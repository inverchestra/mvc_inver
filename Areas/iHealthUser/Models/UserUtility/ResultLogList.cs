using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    public class ResultLogList
    {
        public int Id;
        public string name;
        public bool IsChecked = false;
        public static IList<ResultList> GetResultLogList(IList<UserHealthLog> lstlogs, IList<LogToLog> lstLogtoLog)
        {
            IList<ResultList> lstResult = null;
            lstResult = new List<ResultList>();
            ResultList result = null;

            foreach (UserHealthLog logs in lstlogs)
            {
                result = new ResultList();
                result.Id = logs.logId;
                result.name = logs.logName;
                foreach (LogToLog LogtoLog in lstLogtoLog)
                {

                    if (logs.logId == LogtoLog.RelatedLogId)
                    {
                        result.IsChecked = true;
                    }
                }
                lstResult.Add(result);
            }
            return lstResult;
        }





    }
}
