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


namespace InnDocs.iHealth.Web.UI.Controllers
{
    public class PaymentController : Controller
    {
        //
        // GET: /Payment/
        int Amount;
        public ActionResult PaymentNew()
        {
            //string id;
            //id =Convert.ToString(Session["dataPId"]);
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

            DomainUser = (SingleRegisterModel)TempData["UsrObj"];

            DateTime date1 = new DateTime();
            date1 = DateTime.Now;
            DateTime date2 = Convert.ToDateTime(DomainUser.ExpireOn);
            IsExpiry = DateTime.Compare(date1, date2);



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
                return View("Payment", user);
            }
            else if ((IsExpiry > 1 || IsExpiry == 1))
            {
                Amount = 10;

                //payment Code

                // start by setting the static values
                string loginID = "43G5WhXAcT2";
                string transactionKey = "73Wdsh9H49V5vF3m";
                //string amount = "19.99";
                string description = "BumpDocs Transaction";
                string label = "Submit Payment"; // The is the label on the 'submit' button
                string testMode = "false";

                // If an amount or description were posted to this page, the defaults are overidden

                string invoice = DateTime.Now.ToString("yyyyMMddhhmmss");

                Random random = new Random();
                string sequence = (random.Next(0, 1000)).ToString();

                string timeStamp = ((int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString();

                string fingerprint = HMAC_MD5(transactionKey, loginID + "^" + sequence + "^" + timeStamp + "^" + Amount + "^");



                l1.IHealthUser.x_login = loginID;
                l1.IHealthUser.x_amount = Amount;
                l1.IHealthUser.x_description = description;
                l1.IHealthUser.x_test_request = testMode;
                l1.IHealthUser.x_invoice_num = invoice;
                l1.IHealthUser.x_fp_sequence = sequence;
                l1.IHealthUser.x_fp_timestamp = timeStamp;
                l1.IHealthUser.x_fp_hash = fingerprint;
                ////Payment code



                return View("Payment", l1);

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



        public ActionResult ConfirmPayment(OnlinePayments value)
        {

            Services1 s = new Services1();

            RegistrationServiceCalls _regSerCalls = null;
            _regSerCalls = new RegistrationServiceCalls();

            SingleRegisterModel snglRegModel = null;
            snglRegModel = new SingleRegisterModel();

            OnlinePayments _op = null;
            _op = new OnlinePayments();

            _op.CustomerId = CryptorEngine.Decrypt(value.x_cust_id, true);
            snglRegModel = _regSerCalls.GetSingleRegUserbyCustomerId(_op.CustomerId);

            _op.OnlinePaymentDate = DateTime.Now;
            _op.EmailId = snglRegModel.EmailId;
            _op.DomainId = snglRegModel.RUserId;
            _op.TransactionId = value.x_trans_id;
            _op.TransactionDetails = XmlStringListSerializer.ToXmlString(value);

            string success = s.InsertOnlinePayments(_op);

            if (snglRegModel.UserPlan == "FreeUser")    // upgrading Free user to Paid user
            {
                string strDatePaidUser = DateTime.Now.ToString();
                DateTime datePaidUser = DateTime.Parse(strDatePaidUser);
                string expireDatePaidUser = datePaidUser.AddDays(30).ToShortDateString();
                snglRegModel.ExpireOn = Convert.ToDateTime(expireDatePaidUser);

                snglRegModel.UserPlan = "PaidUser";
                UserInformation userinfo=new UserInformation();

               
                int successocde = _regSerCalls.UpdateRUserDetails(snglRegModel, _op.DomainId);
                Session["RegUser"] = snglRegModel;
                return RedirectToAction("ConfirmUpdatePayment", "OnlinePaymentUpdate", new { area = "iHealthUser" });

            }

            else if (snglRegModel.ExpireOn == null && snglRegModel.IsFbVerified == false) // piad user from start
            {
                string strDatePaidUser = snglRegModel.CreatedOn.ToString();
                DateTime datePaidUser = DateTime.Parse(strDatePaidUser);
                string expireDatePaidUser = datePaidUser.AddDays(30).ToShortDateString();
                snglRegModel.ExpireOn = Convert.ToDateTime(expireDatePaidUser);

                int successcode = _regSerCalls.UpdateRUserDetails(snglRegModel, _op.DomainId);


                return View("Confirmpayment");
            }

            else if (snglRegModel.IsFbVerified == true)
            {
                Services3 pinfoService = new Services3();
                PersonalInformation pinfo = new PersonalInformation();
                UserInformation usr = null;
                usr = new UserInformation();

                usr = s.GetUserByEmail(snglRegModel.EmailId);
                pinfo = pinfoService.GetPersonalInfo(usr.UserId);
                string loginname = pinfo.FirstName;
                Session["LoginName"] = loginname;
                Session["LoginUser"] = usr;

                return RedirectToAction("DashBoard", "DashBoard", new { Area = "iHealthUser" });
            }

            else
            {       // renewal user plan 
                string strDatePaidUser = snglRegModel.ExpireOn.ToString();
                DateTime datePaidUser = DateTime.Parse(strDatePaidUser);
                string expireDatePaidUser = datePaidUser.AddDays(30).ToShortDateString();
                snglRegModel.ExpireOn = Convert.ToDateTime(expireDatePaidUser);

                int successcode = _regSerCalls.UpdateRUserDetails(snglRegModel, _op.DomainId);

                return RedirectToAction("ConfirmUpdatePayment", "OnlinePaymentUpdate", new { area = "iHealthUser" });

            }
        }
    }
}

