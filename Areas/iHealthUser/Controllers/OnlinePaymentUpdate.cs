using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models;
using InnDocs.iHealth.Web.UI.Models;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using InnDocs.iHealth.Web.UI.Utilities;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserUtility;


namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Controllers
{
    public class OnlinePaymentUpdateController : Controller
    {
        //
        // GET: /iHealthUser/OnlinePaymentUpdate/

        int Amount;

        public ActionResult UpdatePayment()
        {
            // string id;
            //id =Convert.ToString(Session["dataPId"]);


            UserInformation LoginUser = null;
            string currentpage = "Administration";
            Session["CurrentPage"] = currentpage;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                LoginUser = Session["LoginUser"] as UserInformation;
            }

            // id = LoginUser.DomainId;

            Services1 s = new Services1();
            LoginAndSignUp l1 = new LoginAndSignUp();
            UserInformation user = new UserInformation();

            RegistrationServiceCalls _regServiceCalls = null;
            _regServiceCalls = new RegistrationServiceCalls();

            SingleRegisterModel DomainUser = null;
            DomainUser = new SingleRegisterModel();
            int IsExpiry = 0;

            l1.CountryList = l1.Countries();
            l1.StypeList = l1.Stypes();


            DomainUser = _regServiceCalls.GetSingleRegUserbyID(LoginUser.DomainId);
            DateTime currentDate = new DateTime();
            currentDate = DateTime.Now;
            DateTime UserExpireDate = Convert.ToDateTime(DomainUser.ExpireOn);
            IsExpiry = DateTime.Compare(currentDate, UserExpireDate);


            if (DomainUser.UserPlan == "PaidUser")
            {
                Amount = 10;

                //payment Code

                // start by setting the static values
                string loginID = "43G5WhXAcT2";
                string transactionKey = "73Wdsh9H49V5vF3m";
                //string amount = "19.99";
                string description = "BumpDocs Transaction";
                //   string label = "Submit Payment"; // The is the label on the 'submit' button
                string testMode = "false";

                // If an amount or description were posted to this page, the defaults are overidden

                string invoice = DateTime.Now.ToString("yyyyMMddhhmmss");

                Random random = new Random();
                string sequence = (random.Next(0, 1000)).ToString();

                string timeStamp = ((int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString();

                string fingerprint = HMAC_MD5(transactionKey, loginID + "^" + sequence + "^" + timeStamp + "^" + Amount + "^");

                SingleRegisterModel sRegUsrModel = _regServiceCalls.GetSingleRegUserbyID(DomainUser.RUserId);
                string custId = sRegUsrModel.CustomerId;
                user.x_cust_id = CryptorEngine.Encrypt(custId, true);

                user.x_login = loginID;
                user.x_amount = Amount;
                user.x_description = description;
                user.x_test_request = testMode;
                user.x_invoice_num = invoice;
                user.x_fp_sequence = sequence;
                user.x_fp_timestamp = timeStamp;
                user.x_fp_hash = fingerprint;
                ////Payment code
                l1.IHealthUser = user;
                return PartialView("UpdatePayment", user);
            }
            else if ((IsExpiry > 1))
            {
                Amount = 10;

                string loginID = "43G5WhXAcT2";
                string transactionKey = "73Wdsh9H49V5vF3m";
                //string amount = "19.99";
                string description = "BumpDocs Transaction";
                //   string label = "Submit Payment"; // The is the label on the 'submit' button
                string testMode = "false";

                // If an amount or description were posted to this page, the defaults are overidden

                string invoice = DateTime.Now.ToString("yyyyMMddhhmmss");

                Random random = new Random();
                string sequence = (random.Next(0, 1000)).ToString();

                string timeStamp = ((int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString();

                string fingerprint = HMAC_MD5(transactionKey, loginID + "^" + sequence + "^" + timeStamp + "^" + Amount + "^");

                SingleRegisterModel sRegUsrModel = _regServiceCalls.GetSingleRegUserbyID(DomainUser.RUserId);
                string custId = sRegUsrModel.CustomerId;
                user.x_cust_id = CryptorEngine.Encrypt(custId, true);

                user.x_login = loginID;
                user.x_amount = Amount;
                user.x_description = description;
                user.x_test_request = testMode;
                user.x_invoice_num = invoice;
                user.x_fp_sequence = sequence;
                user.x_fp_timestamp = timeStamp;
                user.x_fp_hash = fingerprint;
                ////Payment code
                l1.IHealthUser = user;
                return PartialView("UpdatePayment", user);

            }

            else if (DomainUser.UserPlan == "FreeUser")
            {
                Amount = 10;

                //payment Code

                // start by setting the static values
                string loginID = "43G5WhXAcT2";
                string transactionKey = "73Wdsh9H49V5vF3m";
                //string amount = "19.99";
                string description = "BumpDocs Transaction";
                //   string label = "Submit Payment"; // The is the label on the 'submit' button
                string testMode = "false";

                // If an amount or description were posted to this page, the defaults are overidden

                string invoice = DateTime.Now.ToString("yyyyMMddhhmmss");

                Random random = new Random();
                string sequence = (random.Next(0, 1000)).ToString();

                string timeStamp = ((int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString();

                string fingerprint = HMAC_MD5(transactionKey, loginID + "^" + sequence + "^" + timeStamp + "^" + Amount + "^");

                SingleRegisterModel sRegUsrModel = _regServiceCalls.GetSingleRegUserbyID(DomainUser.RUserId);
                string custId = sRegUsrModel.CustomerId;
                user.x_cust_id = CryptorEngine.Encrypt(custId, true);

                user.x_login = loginID;
                user.x_amount = Amount;
                user.x_description = description;
                user.x_test_request = testMode;
                user.x_invoice_num = invoice;
                user.x_fp_sequence = sequence;
                user.x_fp_timestamp = timeStamp;
                user.x_fp_hash = fingerprint;
                ////Payment code
                l1.IHealthUser = user;
                return PartialView("UpdatePayment", user);
            }
            else
            {
                return RedirectToAction("Home", "Home");
            }


        }

        string HMAC_MD5(string key, string value)
        {
            // The first two lines take the input values and convert them from strings to Byte arrays
            byte[] HMACkey = (new System.Text.ASCIIEncoding()).GetBytes(key);
            byte[] HMACdata = (new System.Text.ASCIIEncoding()).GetBytes(value);

            // create a HMACMD5 object with the key set
            HMACMD5 myhmacMD5 = new HMACMD5(HMACkey);

            //calculate the hash (returns a byte array)
            byte[] HMAChash = myhmacMD5.ComputeHash(HMACdata);

            //loop through the byte array and add append each piece to a string to obtain a hash string
            string fingerprint = "";
            for (int i = 0; i < HMAChash.Length; i++)
            {
                fingerprint += HMAChash[i].ToString("x").PadLeft(2, '0');
            }

            return fingerprint;
        }

        public ActionResult ConfirmUpdatePayment()
        {
            return RedirectToAction("PaymentManagement", "AccountSettings");
        }
    }
}
