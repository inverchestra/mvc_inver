using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    public class AlbumsUpload
    {
        public int CurrentDay { get; set; }
        public DateTime? CurrentDate { get; set; }
        public string PostComment { get; set; }
        public List<Comments> lstComments { get; set; }
        public string UserId { get; set; }
        public Comments Comment { get; set; }
        public int Week { get; set; }
        public int CurrentWeek { get; set; }

    }
}