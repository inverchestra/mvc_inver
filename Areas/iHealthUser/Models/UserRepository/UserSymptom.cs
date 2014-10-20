using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    [Serializable()]
    public class UserSymptom
    {
        public string LogId { get; set; }
        [Required(ErrorMessage = "symptom is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public string Reasons { get; set; }
        public string Medicines { get; set; }
        public DateTime DateTime { get; set; }

    }
}