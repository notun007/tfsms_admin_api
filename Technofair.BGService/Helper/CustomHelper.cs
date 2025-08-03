
using TFSMS.Admin.Model.Common;
using System.Net;
using System.Text;
using TFSMS.Admin.Model.ViewModel.Common;
using System.Data;
using Newtonsoft.Json;


namespace TFSMS.Admin.Helper
{
    public class CustomHelper 
    {
        public CustomHelper()
        {

            //var dbfactory = new DatabaseFactory();
            //service = new CmnFinancialYearService(new CmnFinancialYearRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        #region Send SMS
        public static async Task<RequestResponseSMSSentViewModel> SendSMS(CmnSMSGateway objGateway, string recipient, string message,string messageType)
        {
            RequestResponseSMSSentViewModel objResponse = new RequestResponseSMSSentViewModel();
            try
            {
                if (objGateway.Provider == "Bulk SMS BD")
                {
                    String msg = System.Uri.EscapeUriString(message);
                    string url = objGateway.Url.Trim() + "api_key=" + objGateway.AuthorizationCode.Trim() + "&senderid=" + objGateway.UserID.Trim() + "&number=" + recipient + "&message=" + msg;
                    WebRequest request = HttpWebRequest.Create(url);
                    request.Method = "POST";
                    byte[] byteArray = Encoding.UTF8.GetBytes(url);
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.ContentLength = byteArray.Length;

                    // Get the request stream.  
                    Stream dataStream = request.GetRequestStream();
                    // Write the data to the request stream.  
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    // Close the Stream object.  
                    dataStream.Close();
                    // Get the response.  

                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        string responseData = new StreamReader(response.GetResponseStream()).ReadToEnd();
                        if (responseData != null && responseData != "")
                        {
                            objResponse = JsonConvert.DeserializeObject<RequestResponseSMSSentViewModel>(responseData);
                            if (objResponse.response_code == 202)
                            {
                                objResponse.status = "OK";
                            }
                            else
                            {
                                objResponse.error = objResponse.error_message;
                            }
                        }
                    }
                }
                else if (objGateway.Provider == "Alpha Net")
                {
                    string url = objGateway.Url.Trim() + "app=ws&u=" + objGateway.UserID.Trim() + "&h=" + objGateway.AuthorizationCode.Trim() + "&op=pv&to=" + recipient + "&nofooter=1" + "&msg=" + message + "&unicode=1";
                    WebRequest request = HttpWebRequest.Create(url);
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        string responseData = new StreamReader(response.GetResponseStream()).ReadToEnd();
                        if (responseData != null && responseData != "")
                        {
                            ResponseSMSSentViewModel objRes = JsonConvert.DeserializeObject<ResponseSMSSentViewModel>(responseData);
                            if (objRes != null && objRes.data != null && objRes.data.Count > 0)
                            {
                                objResponse = objRes.data[0];
                            }
                        }
                    }
                }
                else if (objGateway.Provider == "Grameenphone")
                {
                    try
                    {
                        string[] arr = recipient.Split(',');
                        var jsonBody = new
                        {
                            username = objGateway.UserID,
                            password = objGateway.AuthorizationCode,
                            apicode = "1",
                            msisdn = arr,
                            countrycode = "880",
                            cli = objGateway.MaskingTitle,
                            messagetype = messageType,
                            message = message,
                            clienttransid = "1311161161642629986567523",
                            bill_msisdn = objGateway.SenderID,
                            tran_type = "P",
                            request_type = "S",
                            rn_code = "71"
                        };

                        string jsonBodySerialize = JsonConvert.SerializeObject(jsonBody);
                        using (HttpClient client = new HttpClient())
                        {
                            // Convert the data to JSON format
                            var content = new StringContent(jsonBodySerialize, Encoding.UTF8, "application/json");

                            // Send the POST request
                            HttpResponseMessage response = await client.PostAsync(objGateway.Url.Trim(), content);

                            // Check if the request was successful
                            if (response.IsSuccessStatusCode)
                            {
                                // Read the response content
                                string responseData = await response.Content.ReadAsStringAsync();
                                if (responseData != null && responseData != "")
                                {
                                    RequestResponseSMSSentViewModel result = JsonConvert.DeserializeObject<RequestResponseSMSSentViewModel>(responseData);
                                    if (result != null && result.statusInfo != null && result.statusInfo.statusCode == "1000")
                                    {
                                        objResponse.status = "OK";
                                    }
                                    else
                                    {
                                        objResponse.error = result.statusInfo.errordescription;
                                    }
                                }
                            }
                            else
                            {
                                objResponse.error = response.StatusCode.ToString();
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        //Added: Commented On 05.05.2025
                        throw ex;

                        //Old: Commented On 05.05.2025
                        //if (ex.InnerException != null && ex.InnerException.Message != null)
                        //{
                        //    objResponse.error = ex.InnerException.Message;
                        //}
                        //else
                        //{
                        //    objResponse.error = ex.Message;
                        //}
                    }

                }

            }
            catch (Exception ex)
            {
                //Added: Commented On 05.05.2025
                throw ex;

                //Old: Commented On 05.05.2025
                //if (ex.InnerException != null && ex.InnerException.Message != null)
                //{
                //    objResponse.error = ex.InnerException.Message;
                //}
                //else
                //{
                //    objResponse.error = ex.Message;
                //}

            }
            return objResponse;
        }



        public static CmnClientSMSSendOption GetClientSMSSendOption(string hostName, string connectionStringPath)
        {
            CmnClientSMSSendOption obj = null;
            DataTable dt = Technofair.Lib.Utilities.Helper.GetClientSMSSendOption(hostName, connectionStringPath);
            if (dt != null && dt.Rows.Count > 0)
            {
                obj = new CmnClientSMSSendOption();
                foreach (DataRow row in dt.Rows)
                {
                    obj = ((CmnClientSMSSendOption)Technofair.Lib.Utilities.Helper.FillTo(row, typeof(CmnClientSMSSendOption)));
                }
            }
            return obj;
        }

        public static List<CmnSMSGateway> GetActiveSMSGateway(string connectionStringPath)
        {
            List<CmnSMSGateway> list = null;
            DataTable dt = Technofair.Lib.Utilities.Helper.GetActiveSMSGateway(connectionStringPath);
            if (dt != null && dt.Rows.Count > 0)
            {
                list = new List<CmnSMSGateway>();
                foreach (DataRow row in dt.Rows)
                {
                    list.Add((CmnSMSGateway)Technofair.Lib.Utilities.Helper.FillTo(row, typeof(CmnSMSGateway)));
                }
            }
            return list;
        }


        public static CmnClientSMSBalance GetClientSMSBalance(string hostName,string connectionStringPath)
        {
            CmnClientSMSBalance obj = null;
            int customerId = Technofair.Lib.Utilities.Helper.GetCompanyCustomerId(hostName, connectionStringPath);
            DataTable dt = Technofair.Lib.Utilities.Helper.GetClientSMSBalance(customerId, connectionStringPath);
            if (dt != null && dt.Rows.Count > 0)
            {
                obj = new CmnClientSMSBalance();
                foreach (DataRow row in dt.Rows)
                {
                    obj = ((CmnClientSMSBalance)Technofair.Lib.Utilities.Helper.FillTo(row, typeof(CmnClientSMSBalance)));
                }
            }
            return obj;
        }

        #endregion



    }
}