using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace InnDocs.iHealth.Web.UI.Models
{
    public class ForgotPassword
    {

        [RegularExpression(@"(\w+([-+.’]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)|(\d{10})", ErrorMessage = "Enter a valid email address or phone number")]
        [Required(ErrorMessage = "Email id or phone number is required")]
        public string EmailId { get; set; }
    }
}