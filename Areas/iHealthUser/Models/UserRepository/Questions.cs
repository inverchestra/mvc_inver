using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    public class Questions
    {

        public string QuestionId { get; set; }

        public string Question { get; set; }

        public string ResponseType { get; set; }

        public int Day { get; set; }

        public int Week { get; set; }

        public string Response { get; set; }

        public string ResponseStatus { get; set; }

        public string UserId { get; set; }
    }
}