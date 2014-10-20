using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models
{
    //[Serializable]
    public class OnlinePayments
    {
        public string CustomerId;
        public string EmailId;
        public string DomainId;
        public string TransactionId;
        public string TransactionDetails;
        public OnlinePayments TransactionDetailsInfo { get; set; }
        public DateTime? OnlinePaymentDate { get; set; }
       // public string OnlinePaymentDate1 { get { return Convert.ToDateTime(OnlinePaymentDate).ToLongDateString().Split(' ').ElementAt(1) + " " + OnlinePaymentDate.Value.Day + ", " + OnlinePaymentDate.Value.Year; } set { } }


        // storing hidden filed info from authorize.net
        private string _x_response_code;
        public string x_response_code
        {
            get { return _x_response_code; }
            set { _x_response_code = value; }
        }

        private string _x_response_reason_code;
        public string x_response_reason_code
        {
            get { return _x_response_reason_code; }
            set { _x_response_reason_code = value; }
        }

        private string _x_response_reason_text;
        public string x_response_reason_text
        {
            get { return _x_response_reason_text; }
            set { _x_response_reason_text = value; }
        }

        private string _x_avs_code;
        public string x_avs_code
        {
            get { return _x_avs_code; }
            set { _x_avs_code = value; }
        }

        private string _x_auth_code;
        public string x_auth_code
        {
            get { return _x_auth_code; }
            set { _x_auth_code = value; }
        }

        private string _x_trans_id;
        public string x_trans_id
        {
            get { return _x_trans_id; }
            set { _x_trans_id = value; }
        }

        private string _x_method;
        public string x_method
        {
            get { return _x_method; }
            set { _x_method = value; }
        }

        private string _x_card_type;
        public string x_card_type
        {
            get { return _x_card_type; }
            set { _x_card_type = value; }
        }

        private string _x_account_number;
        public string x_account_number
        {
            get { return _x_account_number; }
            set { _x_account_number = value; }
        }

        private string _x_first_name;
        public string x_first_name
        {
            get { return _x_first_name; }
            set { _x_first_name = value; }
        }

        private string _x_last_name;
        public string x_last_name
        {
            get { return _x_last_name; }
            set { _x_last_name = value; }
        }

        private string _x_company;
        public string x_company
        {
            get { return _x_company; }
            set { _x_company = value; }
        }

        private string _x_address;
        public string x_address
        {
            get { return _x_address; }
            set { _x_address = value; }
        }

        private string _x_city;
        public string x_city
        {
            get { return _x_city; }
            set { _x_city = value; }
        }

        private string _x_state;
        public string x_state
        {
            get { return _x_state; }
            set { _x_state = value; }
        }

        private string _x_zip;
        public string x_zip
        {
            get { return _x_zip; }
            set { _x_zip = value; }
        }

        private string _x_country;
        public string x_country
        {
            get { return _x_country; }
            set { _x_country = value; }
        }


        private string _x_phone;
        public string x_phone
        {
            get { return _x_phone; }
            set { _x_phone = value; }
        }

        private string _x_fax;
        public string x_fax
        {
            get { return _x_fax; }
            set { _x_fax = value; }
        }

        private string _x_email;
        public string x_email
        {
            get { return _x_email; }
            set { _x_email = value; }
        }

        private string _x_invoice_num;
        public string x_invoice_num
        {
            get { return _x_invoice_num; }
            set { _x_invoice_num = value; }
        }

        private string _x_description;
        public string x_description
        {
            get { return _x_description; }
            set { _x_description = value; }
        }

        private string _x_type;
        public string x_type
        {
            get { return _x_type; }
            set { _x_type = value; }
        }

        private string _x_cust_id;
        public string x_cust_id
        {
            get { return _x_cust_id; }
            set { _x_cust_id = value; }
        }

        private string _x_ship_to_first_name;
        public string x_ship_to_first_name
        {
            get { return _x_ship_to_first_name; }
            set { _x_ship_to_first_name = value; }
        }

        private string _x_ship_to_last_name;
        public string x_ship_to_last_name
        {
            get { return _x_ship_to_last_name; }
            set { _x_ship_to_last_name = value; }
        }

        private string _x_ship_to_company;
        public string x_ship_to_company
        {
            get { return _x_ship_to_company; }
            set { _x_ship_to_company = value; }
        }

        private string _x_ship_to_address;
        public string x_ship_to_address
        {
            get { return _x_ship_to_address; }
            set { _x_ship_to_address = value; }
        }

        private string _x_ship_to_city;
        public string x_ship_to_city
        {
            get { return _x_ship_to_city; }
            set { _x_ship_to_city = value; }
        }

        private string _x_ship_to_state;
        public string x_ship_to_state
        {
            get { return _x_ship_to_state; }
            set { _x_ship_to_state = value; }
        }

        private string _x_ship_to_zip;
        public string x_ship_to_zip
        {
            get { return _x_ship_to_zip; }
            set { _x_ship_to_zip = value; }
        }

        private string _x_ship_to_country;
        public string x_ship_to_country
        {
            get { return _x_ship_to_country; }
            set { _x_ship_to_country = value; }
        }

        private string _x_amount;
        public string x_amount
        {
            get { return _x_amount; }
            set { _x_amount = value; }
        }

        private string _x_tax;
        public string x_tax
        {
            get { return _x_tax; }
            set { _x_tax = value; }
        }

        private string _x_duty;
        public string x_duty
        {
            get { return _x_duty; }
            set { _x_duty = value; }
        }

        private string _x_freight;
        public string x_freight
        {
            get { return _x_freight; }
            set { _x_freight = value; }
        }

        private string _x_tax_exempt;
        public string x_tax_exempt
        {
            get { return _x_tax_exempt; }
            set { _x_tax_exempt = value; }
        }

        private string _x_po_num;
        public string x_po_num
        {
            get { return _x_po_num; }
            set { _x_po_num = value; }
        }

        private string _x_MD5_Hash;
        public string x_MD5_Hash
        {
            get { return _x_MD5_Hash; }
            set { _x_MD5_Hash = value; }
        }

        private string _x_cvv2_resp_code;
        public string x_cvv2_resp_code
        {
            get { return _x_cvv2_resp_code; }
            set { _x_cvv2_resp_code = value; }
        }

        private string _x_cavv_response;
        public string x_cavv_response
        {
            get { return _x_cavv_response; }
            set { _x_cavv_response = value; }
        }

        private string _x_test_request;
        public string x_test_request
        {
            get { return _x_test_request; }
            set { _x_test_request = value; }
        }

        private string _x_method_available;
        public string x_method_available
        {
            get { return _x_method_available; }
            set { _x_method_available = value; }
        }

        // extra fields

        private string _x_login;
        public string x_login
        {
            get { return _x_login; }
            set { _x_login = value; }
        }

        private string transactionKey;
        public string Transactionkey
        {
            get { return transactionKey; }
            set { transactionKey = value; }
        }

        private string _x_split_tender_status;
        public string x_split_tender_status
        {
            get { return _x_split_tender_status; }
            set { _x_split_tender_status = value; }
        }

        private string _x_line_item;
        public string x_line_item
        {
            get { return _x_line_item; }
            set { _x_line_item = value; }
        }

        private string _x_fp_sequence;
        public string x_fp_sequence
        {
            get { return _x_fp_sequence; }
            set { _x_fp_sequence = value; }
        }


        private string _x_fp_timestamp;
        public string x_fp_timestamp
        {
            get { return _x_fp_timestamp; }

            set { _x_fp_timestamp = value; }
        }


        private string _x_fp_hash;
        public string x_fp_hash
        {
            get { return _x_fp_hash; }
            set { _x_fp_hash = value; }
        }




        //Payment

        //private string _domainid;
        //public string DomainId
        //{
        //    get { return _domainid; }
        //    set { _domainid = value; }
        //}

        //private string _emailId;
        //public string EmailId
        //{
        //    get { return _emailId; }
        //    set { _emailId = value; }
        //}

        //private string _customerId;
        //public string CustomerId
        //{
        //    get { return _customerId; }
        //    set { _customerId = value; }
        //}

        //private string _transactionId;
        //public string TransactionId
        //{
        //    get { return _transactionId; }
        //    set { _transactionId = value; }
        //}

        //private string _transactionDetails;
        //public string TransactionDetails
        //{
        //    get { return _transactionDetails; }
        //    set { _transactionDetails = value; }
        //}

        //private string _x_cust_id;
        //public string x_cust_id
        //{
        //    get { return _x_cust_id; }
        //    set { _x_cust_id = value; }
        //}

        //private string _x_trans_id;
        //public string x_trans_id
        //{
        //    get { return _x_trans_id; }
        //    set { _x_trans_id = value; }
        //}


        //private string _x_email;
        //public string x_email
        //{
        //    get { return _x_email; }
        //    set { _x_email = value; }
        //}

        //private string _x_login;
        //public string x_login
        //{
        //    get { return _x_login; }
        //    set { _x_login = value; }
        //}

        //private string transactionKey;
        //public string Transactionkey
        //{
        //    get { return transactionKey; }
        //    set { transactionKey = value; }
        //}

        //private int _x_amount;
        //public int x_amount
        //{
        //    get { return _x_amount; }
        //    set { _x_amount = value; }
        //}


        //private string _x_description;
        //public string x_description
        //{
        //    get { return _x_description; }
        //    set { _x_description = value; }
        //}


        //private string _x_invoice_num;
        //public string x_invoice_num
        //{
        //    get { return _x_invoice_num; }
        //    set { _x_invoice_num = value; }
        //}


        //private string _x_fp_sequence;
        //public string x_fp_sequence
        //{
        //    get { return _x_fp_sequence; }
        //    set { _x_fp_sequence = value; }
        //}


        //private string _x_fp_timestamp;
        //public string x_fp_timestamp
        //{
        //    get { return _x_fp_timestamp; }

        //    set { _x_fp_timestamp = value; }
        //}


        //private string _x_fp_hash;
        //public string x_fp_hash
        //{
        //    get { return _x_fp_hash; }
        //    set { _x_fp_hash = value; }
        //}


        //private string _x_test_request;
        //public string x_test_request
        //{
        //    get { return _x_test_request; }
        //    set { _x_test_request = value; }
        //}



        //Payment

    }
}