using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    public class ResultList
    {
        public string Id;
        public string name;
        public bool IsChecked = false;
        public static IList<ResultList> GetResultLogList(IList<UserHealthLog> lstlogs, IList<CaseToLog> lstcasetoLog)
        {
            IList<ResultList> lstResult = null;
            lstResult = new List<ResultList>();
            ResultList result = null;
            
            foreach (UserHealthLog logs in lstlogs)
            {
                result = new ResultList();
                result.Id = logs.logId;
                result.name = logs.logName;
                foreach (CaseToLog casetoLog in lstcasetoLog)
                { 
                   
                    if (logs.logId == casetoLog.LogId)
                    {
                        result.IsChecked = true;                        
                    } 
                }
                lstResult.Add(result);
            }
            return lstResult;
        }


        public static IList<ResultList> GetResulCaseList(IList<Cases> lstCases, IList<CaseToCase> lstcasetoCase)
        {
            IList<ResultList> lstResult = null;
            lstResult = new List<ResultList>();
            ResultList result = null;
            
            foreach (Cases cases in lstCases)
            {
                result = new ResultList();
                result.Id = cases.CaseId;
                result.name = cases.CaseName;
                foreach (CaseToCase casetoCase in lstcasetoCase)
                {
                    if (cases.CaseId == casetoCase.RelatedCaseId)
                    {
                        result.IsChecked = true;
                    } 
                }
                lstResult.Add(result);
            }
            return lstResult;
        }

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
    
        //public static string TextBoxText(IList<ResultList> lstResult)
        //{
        //    string text = null;
        //    string value = null;
        //    foreach (ResultList list in lstResult)
        //    {
        //        if (list.IsChecked)
        //        {
        //            value = list.name;
        //            if (text != null)
        //            {
        //                text = text + "," + value;
        //            }
        //            else
        //            {
        //                text = value;
        //            }
        //        }

        //    }
        //    return text;
        //}
    }
}