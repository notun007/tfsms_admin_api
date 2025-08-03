using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.IO;
using System.Collections.Specialized;
using Newtonsoft.Json;
using TFSMS.Admin.Model.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace TFSMS.Admin.Models
{
    public class SSLCommerz
    {
         List<String> key_list;
         String generated_hash;
         string error;

         string Store_ID;
         string Store_Pass;
         bool Store_Test_Mode;

         string SSLCz_URL = "https://securepay.sslcommerz.com/";
         string Submit_URL = "gwprocess/v4/api.php";
         string Validation_URL = "validator/api/validationserverAPI.php";
         string Tran_Checking_URL = "validator/api/merchantTransIDvalidationAPI.php";

        //public SSLCommerz(AnFPaymentMethodCredential? objPaymentMethod, bool Store_Test_Mode = false)
        //{
        //    System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)0x00000C00;
        //    if (Store_ID == null && Store_Pass == null)
        //    {
        //        Store_ID = objPaymentMethod.UserID;
        //        Store_Pass = objPaymentMethod.AuthorizationCode;
        //        SSLCz_URL = objPaymentMethod.PaymentUrl;
        //        SetSSLCzTestMode(Store_Test_Mode);
        //    }
        //    else
        //    {
        //        throw new Exception("Please provide Store ID and Password to initialize SSLCommerz");
        //    }
        //}

        public SSLCommerzInitResponse InitiateTransaction(NameValueCollection postData, bool GetGateWayList = false)
        {
            SSLCommerzInitResponse objInitResponse = new SSLCommerzInitResponse();

            postData.Add("store_id", this.Store_ID);
            postData.Add("store_passwd", this.Store_Pass);
            string response = SendPost(postData);
            try
            {
                SSLCommerzInitResponse? resp = JsonConvert.DeserializeObject<SSLCommerzInitResponse>(response);
                if (resp != null && resp.status == "SUCCESS")
                {
                    objInitResponse = resp;

                    //if (GetGateWayList)
                    //{
                    //    // We will work on it!
                    //}
                    //else
                    //{
                    //    return resp.GatewayPageURL.ToString();
                    //}
                    
                }
                else
                {
                    objInitResponse.status = "FAILED";
                    objInitResponse.failedreason = "Unable to initiate SSL Commerz";
                    
                    //throw new Exception("Unable to get data from SSLCommerz. Please contact your manager!");
                }
            }
            catch (Exception e)
            {
                objInitResponse.status = "FAILED";
                objInitResponse.failedreason = "Unable to initiate SSL Commerz";
                return objInitResponse;
            }
            return objInitResponse;
        }



        public bool ValidateTransaction(string merchantTrxID, decimal merchantTrxAmount, string merchantTrxCurrency, string val_id)
        {
            //bool hash_verified = this.ipn_hash_verify(obj);
            //if (hash_verified)
            //{
            try
            {
                string jsonResponse = "";

                string validate_url = SSLCz_URL + Validation_URL + "?val_id=" + val_id + "&store_id=" + Store_ID + "&store_passwd=" + Store_Pass;

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(validate_url);
                request.PreAuthenticate = true;
                request.Headers.Add("responseType", "json");
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    jsonResponse = new StreamReader(response.GetResponseStream()).ReadToEnd();                    
                }

                if (jsonResponse != null && jsonResponse != "")
                {
                    SSLCommerzResponse? objResponse = JsonConvert.DeserializeObject<SSLCommerzResponse>(jsonResponse);
                    if (objResponse != null && (objResponse.status == "VALID" || objResponse.status== "VALIDATED"))
                    {
                        string transactionID = objResponse.tran_id.Trim();
                        decimal amount = objResponse.amount?.Trim() == null ? 0 : Convert.ToDecimal(objResponse.amount.Trim());
                        if (merchantTrxID != transactionID)
                        {
                            error = "Invalid trxID";
                            return false;
                        }
                        else if (merchantTrxAmount != Convert.ToDecimal(amount))
                        {
                            error = "Amount not matching";
                            return false;
                        }
                        else
                        {
                            return true;
                        }

                        //if (merchantTrxCurrency == "BDT")
                        //{
                        //    if (merchantTrxID == resp.tran_id && (Math.Abs(Convert.ToDecimal(merchantTrxAmount) - Convert.ToDecimal(resp.amount)) < 1) && merchantTrxCurrency == "BDT")
                        //    {
                        //        return true;
                        //    }
                        //    else
                        //    {
                        //        this.error = "Amount not matching";
                        //        return false;
                        //    }
                        //}
                        //else
                        //{
                        //    if (merchantTrxID == resp.tran_id && (Math.Abs(Convert.ToDecimal(merchantTrxAmount) - Convert.ToDecimal(resp.currency_amount)) < 1) && merchantTrxCurrency == resp.currency_type)
                        //    {
                        //        return true;
                        //    }
                        //    else
                        //    {
                        //        this.error = "Currency Amount not matching";
                        //        return false;
                        //    }

                        //}
                    }
                    else
                    {
                        this.error = "This transaction is either expired or fails";
                        return false;
                    }
                }
                else
                {
                    this.error = "Unable to get Transaction JSON status";
                    return false;

                }
                //}
                //else
                //{
                //    this.error = "Unable to verify hash";
                //    return false;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SSLCommerzResponse GetTransactionHistoryByTrxID(string trxID)
        {
            try
            {
                string jsonResponse = "";
                string validate_url = SSLCz_URL + Tran_Checking_URL + "?tran_id=" + trxID + "&store_id=" + Store_ID + "&store_passwd=" + Store_Pass;
                SSLCommerzResponse objResponse = new SSLCommerzResponse();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(validate_url);
                request.PreAuthenticate = true;
                request.Headers.Add("responseType", "json");
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    jsonResponse = new StreamReader(response.GetResponseStream()).ReadToEnd();
                }

                if (jsonResponse != "")
                {
                    SSLCommerzTransactionResponse? tranResponse = JsonConvert.DeserializeObject<SSLCommerzTransactionResponse>(jsonResponse);
                    if (tranResponse != null && tranResponse.element != null && tranResponse.element.Count > 0)
                    {
                        objResponse = tranResponse.element[0];
                        if (objResponse.status != "VALID" || objResponse.status != "VALIDATED")
                        {
                            objResponse.error = "This transaction is either expired or fails";
                        }
                    }
                }
                else
                {
                    objResponse.error = "Unable to get Transaction JSON status";
                }
                return objResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void SetSSLCzTestMode(bool mode)
        {
            this.Store_Test_Mode = mode;
            if (mode)
            {
                //this.Store_ID = "testbox";
                //this.Store_Pass = "qwerty";
                //this.SSLCz_URL = "https://sandbox.sslcommerz.com/";
            }
        }

        private string SendPost(NameValueCollection postData)
        {
            //Console.WriteLine(this.SSLCz_URL + this.Submit_URL);
            string url = this.SSLCz_URL + this.Submit_URL;
            string response = Post(url, postData);
            return response;
        }

        private string Post(string uri, NameValueCollection postData)
        {
            byte[] response = null;
            using (WebClient client = new WebClient())
            {
                response = client.UploadValues(uri, postData);
            }
            return System.Text.Encoding.UTF8.GetString(response);
        }
        /// <summary>
        /// SSLCommerz IPN Hash Verify method
        /// </summary>
        /// <param name="req"></param>
        /// <param name="pass"></param>
        /// <returns>Boolean - True or False</returns>
        //private Boolean ipn_hash_verify(SSLCommerzResponse obj)
        //{

        //    // Check For verify_sign and verify_key parameters
        //    if (obj.verify_sign != "" && obj.verify_sign != "")
        //    {
        //        // Get the verify key
        //        //String verify_key = verify_key;
        //        if (obj.verify_key != "")
        //        {

        //            // Split key string by comma to make a list array
        //            key_list = obj.verify_key.Split(',').ToList<String>();

        //            // Initiate a key value pair list array
        //            List<KeyValuePair<String, String>> data_array = new List<KeyValuePair<string, string>>();

        //            // Store key and value of post in a list
        //            foreach (String k in key_list)
        //            {
        //                data_array.Add(new KeyValuePair<string, string>(k, this.Store_Pass));
        //            }

        //            // Store Hashed Password in list
        //            String hashed_pass = this.MD5(this.Store_Pass);
        //            data_array.Add(new KeyValuePair<string, string>("store_passwd", hashed_pass));

        //            // Sort Array
        //            data_array.Sort(
        //                delegate (KeyValuePair<string, string> pair1,
        //                KeyValuePair<string, string> pair2)
        //                {
        //                    return pair1.Key.CompareTo(pair2.Key);
        //                }
        //            );


        //            // Concat and make String from array
        //            String hash_string = "";
        //            foreach (var kv in data_array)
        //            {
        //                hash_string += kv.Key + "=" + kv.Value + "&";
        //            }
        //            // Trim & from end of this string
        //            hash_string = hash_string.TrimEnd('&');

        //            // Make hash by hash_string and store
        //            generated_hash = this.MD5(hash_string);

        //            // Check if generated hash and verify_sign match or not
        //            if (generated_hash == obj.verify_sign)
        //            {
        //                return true; // Matched
        //            }
        //        }

        //        return false;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        /// </summary>
        /// <param name="s"></param>
        /// <returns>md5 Hashed String</returns>
        private String MD5(String s)
        {
            byte[] asciiBytes = ASCIIEncoding.ASCII.GetBytes(s);
            byte[] hashedBytes = System.Security.Cryptography.MD5CryptoServiceProvider.Create().ComputeHash(asciiBytes);
            string hashedString = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            return hashedString;
        }

        public class Gw
        {
            public string? visa { get; set; }
            public string? master { get; set; }
            public string? amex { get; set; }
            public string? othercards { get; set; }
            public string? internetbanking { get; set; }
            public string? mobilebanking { get; set; }
        }

        public class Desc
        {
            public string? name { get; set; }
            public string? type { get; set; }
            public string? logo { get; set; }
            public string? gw { get; set; }
            public string? r_flag { get; set; }
            public string? redirectGatewayURL { get; set; }
        }

        public class SSLCommerzInitResponse
        {
            public string? status { get; set; }
            public string? failedreason { get; set; }
            public string? sessionkey { get; set; }
            public Gw gw { get; set; }
            public string? redirectGatewayURL { get; set; }
            public string? redirectGatewayURLFailed { get; set; }
            public string? GatewayPageURL { get; set; }
            public string storeBanner { get; set; }
            public string storeLogo { get; set; }
            public List<Desc> desc { get; set; }
            public string is_direct_pay_enable { get; set; }
        }

        public class SSLCommerzResponse
        {
            public string? status { get; set; }
            public string? tran_date { get; set; }
            public string? tran_id { get; set; }
            public string? val_id { get; set; }
            public string? amount { get; set; }
            public string? store_amount { get; set; }
            public string? currency { get; set; }
            public string? bank_tran_id { get; set; }
            public string? card_type { get; set; }
            public string? card_no { get; set; }
            public string? card_issuer { get; set; }
            public string? card_brand { get; set; }
            public string? card_category { get; set; }
            public string? card_sub_brand { get; set; }
            public string? card_issuer_country { get; set; }
            public string? card_issuer_country_code { get; set; }
            public string? currency_type { get; set; }
            public string? currency_amount { get; set; }
            public string? currency_rate { get; set; }
            public string? base_fair { get; set; }
            public string? value_a { get; set; }
            public string? value_b { get; set; }
            public string? value_c { get; set; }
            public string? value_d { get; set; }
            public string? emi_instalment { get; set; }
            public string? emi_amount { get; set; }
            public string? emi_description { get; set; }
            public string? emi_issuer { get; set; }
            public string? account_details { get; set; }
            public string? risk_title { get; set; }
            public string? risk_level { get; set; }
            public string? discount_percentage { get; set; }
            public string? discount_amount { get; set; }
            public string? discount_remarks { get; set; }
            public string? APIConnect { get; set; }
            public string? validated_on { get; set; }
            public string? gw_version { get; set; }
            public int? offer_avail { get; set; }
            public string? card_ref_id { get; set; }
            public int? isTokeizeSuccess { get; set; }
            public string? campaign_code { get; set; }

            public string? token_key { get; set; }
            public string? shipping_method { get; set; }
            public string? num_of_item { get; set; }
            public string? product_name { get; set; }
            public string? product_profile { get; set; }
            public string? product_category { get; set; }

            public string? error { get; set; }
            //public string? verify_sign { get; set; }
            //public string? verify_key { get; set; }
        }

        public class SSLCommerzTransactionResponse
        {
            public string? APIConnect { get; set; }
            public string? no_of_trans_found { get; set; }
            public List<SSLCommerzResponse> element { get; set; }            
        }

    }
}