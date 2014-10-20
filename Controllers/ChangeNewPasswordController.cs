using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models;
using InnDocs.iHealth.Web.UI.Models;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Web.Security;
using InnDocs.iHealth.Web.UI.Utilities;
using log4net;
using System.Reflection;

namespace InnDocs.iHealth.Web.UI.Controllers
{
    public class ChangeNewPasswordController : Controller
    {
        private static ILog _log = LogManager.GetLogger("Web.UI");

        //
        // GET: /ChangeNewPassword/

        public ActionResult ChangeNewPassword(string MedId)
        {
            //LoginAndSignUp obj = new LoginAndSignUp();

            //obj.CountryList = obj.Countries();
            //obj.StypeList = obj.Stypes();
            ViewBag.UserId = MedId;
            return View();
        }

        [HttpPost]
        public ActionResult ChangeNewPassword(string UserId, string NewPassword)
        {
            JsonResult data = new JsonResult();
            UserInformation usr = new UserInformation();

            usr.CountryList = usr.Countries();

            usr.StypeList = usr.Stypes();

            Services1 UserService = null;
            UserService = new Services1();

            usr = UserService.ChangeNewPassworaByUserId(UserId, NewPassword);//GetUserbyId(UserId);

            if (usr.ResultCode == 1020)
            {
                data.Data = "You have successfully reset your password.";
            }
            else if (usr.ResultCode == 1022)
            {
                data.Data = "Choose a password you haven't previously used with this account.";
            }
            else
            {
                data.Data = "We are sorry, your password change attempt failed, Please try again.";
            }

            #region Dileep Commented
            //string Npassword = CryptorEngine.Encrypt(NewPassword, true);
            //if (usr.Password != Npassword)
            //{
            //    usr.Password = Npassword;
            //    // string success = UserService.UpdateUserDetails(usr, UserId);
            //    string success = UserService.UpdateUserPasswordDetails(usr, UserId);
            //    _log.Error("success :" + success);
            //    //if ((usr.EmailId != null) && (usr.IsVerified == true)) 


            //    if ((success == "1020") && (usr.EmailId != null) && (usr.IsVerified == true))
            //    {
            //        #region Mail Sending
            //        try
            //        {
            //            //MailMessage mail = new MailMessage();
            //            //mail.To.Add(usr.EmailId);
            //            //mail.From = new MailAddress("medlogx@gmail.com");
            //            //mail.Subject = " MedLogx : Password reset confirmation";
            //            string username = "";
            //            if (pinfo.LastName != null)
            //            {
            //                username = pinfo.FirstName + " " + pinfo.LastName;
            //            }
            //            else
            //            {
            //                username = pinfo.FirstName;
            //            }
            //            string Body = "Dear " + username + ", \n\n You have successfully reset your password.\nYou can start using your new password to login.\n\nThank you,\nMedLogx.";
            //            //mail.Body = Body;
            //            //mail.IsBodyHtml = false;
            //            //SmtpClient smtp = new SmtpClient();
            //            //smtp.Host = "smtp.gmail.com";
            //            //smtp.Port = 587;
            //            //smtp.UseDefaultCredentials = true;
            //            //smtp.Credentials = new System.Net.NetworkCredential("medlogx@gmail.com", "medlogx2013");

            //            //smtp.EnableSsl = true;
            //            //ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            //            //smtp.Send(mail);
            //            //IsSendMail = true;
            //            // ViewBag.message = "You have successfully reset your password.";


            //            SendMail _sendMail = new SendMail();

            //            _sendMail.To = usr.EmailId;

            //            _sendMail.Body = Body;
            //            _sendMail.Subject = "MedLogx : Password reset confirmation";
            //            IsSendMail = RegSerCalls.SendMail(_sendMail);
            //            if (IsSendMail)
            //            {
            //                //  ViewBag.message = "You have successfully reset your password.";
            //                data.Data = "You have successfully reset your password.";
            //            }
            //            else
            //            {
            //                // ViewBag.message = "Error in mail sending.";
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            IsSendMail = false;
            //            string message = ex.Message;
            //            //ViewBag.message = message;
            //            data.Data = message;
            //        }
            //        #endregion

            //    }
            //    else
            //    {
            //        #region message sending to phone
            //        try
            //        {
            //            string phnno = Convert.ToString(usr.PhoneNo);
            //            string Uid = "7386041104";
            //            string Password = "55312";
            //            string Number = phnno;
            //            string Message = "You have successfully reset your password";
            //            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://ubaid.tk/sms/sms.aspx?uid=" + Uid + "&pwd=" + Password +
            //       "&phone=" + Number + "&msg=" + Message + "&provider=Fullonsms");
            //            // Get the response.
            //            HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
            //            System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            //            string responseString = respStreamReader.ReadToEnd();
            //            // Close the Stream object.
            //            respStreamReader.Close();
            //            myResp.Close();


            //        }
            //        catch (Exception ex)
            //        {
            //            JsonResult ResultData = new JsonResult();
            //            ResultData.Data = ex.ToString();
            //            return ResultData;
            //        }
            //        #endregion
            //    }


            //}

            //else
            //{
            //    IsSendMail = false;
            //    // ViewBag.message = "Choose a password you haven't previously used with this account.";
            //    data.Data = "Choose a password you haven't previously used with this account.";
            //}

            #endregion


            return data;

        }

    }
}
