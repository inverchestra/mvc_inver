using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    public class Tips
    {

        public string TipId { get; set; }

        public string Tip { get; set; }

        public int Day { get; set; }

        public int Week { get; set; }

        public bool Status { get; set; }

    }
}