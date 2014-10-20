using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization.Json;
using System.Web.Optimization;
using InnDocs.iHealth.Web.UI.Controllers;
using System.Web.Mvc;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    public class ChangePassword
    {
        public string ihealthuserId { get; set; }
        [Required(ErrorMessage = "Old password is required")]
        public string oldPassword { get; set; }
        [Required(ErrorMessage = "New password is required")]
        [StringLength(15, ErrorMessage = "Minimum 10 characters", MinimumLength = 10)]

        [DataType(DataType.Password)]
        public string newPassword { get; set; }
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("newPassword", ErrorMessage = "Should match 'PASSWORD'")]
        public string confirmPassword { get; set; }
    }
}