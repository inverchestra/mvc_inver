using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    public class TipsAndQA
    {
        public IList<Tips> lstTips { get; set; }
        public IList<Questions> lstQues { get; set; }
    }
}