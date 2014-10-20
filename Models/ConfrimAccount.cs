using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace InnDocs.iHealth.Web.UI.Models
{
    public class ConfrimAccount
    {
        [Required(ErrorMessage = "Phone number is required")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid phone number")]
        [RegularExpression(@"\d{10}", ErrorMessage = "Invalid phone number(should not exceed 10 digits)")]
        public string MobileNumber { get; set; }
        [Required(ErrorMessage = "confirmation code is required")]
        public string OtaCode { get; set; }
    }
}