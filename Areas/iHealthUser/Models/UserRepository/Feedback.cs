using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    public class Feedback
    {
        [Required(ErrorMessage = "Please enter the subject")]

        public string FSubject { get; set; }

        [Required(ErrorMessage = "Please enter email id")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter valid email id")]
        [RegularExpression(@"\w+([-+.’]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Enter valid email id")]
        public string Email { get; set; }

        private string _fdescription;

        public string FDescription
        {
            get { return _fdescription; }
            set { _fdescription = value; }
        }
    }
}