//using DataModels.EntityModels.ERPModel;
//using DataModels.ViewModels;
//using Microsoft.Extensions.Configuration;
//using OpenHtmlToPdf;
//using Syncfusion.HtmlConverter;
//using Syncfusion.Pdf;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
//using Technofair.Model.ViewModel;

namespace Technofair.Lib.Utilities
{
    public static class Conversions
    {
        #region DataMapping
        public static List<T> DataReaderMapToList<T>(IDataReader reader)
        {
            var results = new List<T>();

            var columnCount = reader.FieldCount;
            while (reader.Read())
            {
                var item = Activator.CreateInstance<T>();
                try
                {
                    var rdrProperties = Enumerable.Range(0, columnCount).Select(i => reader.GetName(i)).ToArray();
                    foreach (var property in typeof(T).GetProperties())
                    {
                        if ((typeof(T).GetProperty(property.Name).GetGetMethod().IsVirtual) || (!rdrProperties.Contains(property.Name)))
                        {
                            continue;
                        }
                        else
                        {
                            if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
                            {
                                Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                                property.SetValue(item, Convert.ChangeType(reader[property.Name], convertTo), null);
                            }
                        }
                    }
                    results.Add(item);
                }
                catch (Exception)
                {

                }
            }
            return results;
        }

        public static List<T> ToCollection<T>(this DataTable dt)
        {
            List<T> lst = new System.Collections.Generic.List<T>();
            Type tClass = typeof(T);
            PropertyInfo[] pClass = tClass.GetProperties();
            List<DataColumn> dc = dt.Columns.Cast<DataColumn>().ToList();
            T cn;
            foreach (DataRow item in dt.Rows)
            {
                cn = (T)Activator.CreateInstance(tClass);
                foreach (PropertyInfo pc in pClass)
                {
                    // Can comment try catch block. 
                    try
                    {
                        DataColumn d = dc.Find(c => c.ColumnName == pc.Name);
                        if (d != null)
                            pc.SetValue(cn, item[pc.Name], null);
                    }
                    catch
                    {
                    }
                }
                lst.Add(cn);
            }
            return lst;
        }

        #endregion

        #region Pagination
        public static List<T> SkipTake<T>(List<T> model, vmCmnParameter cmncls)
        {
            List<T> lst = new List<T>();
            int skipnumber = 0;
            if (cmncls.pageNumber > 0)
            {
                skipnumber = ((int)cmncls.pageNumber - 1) * (int)cmncls.pageSize;
            }
            lst = model.Skip(skipnumber).Take((int)cmncls.pageSize).ToList();
            return lst;
        }

        public static int Skip(vmCmnParameter cmncls)
        {
            int skipnumber = 0;
            if (cmncls.pageNumber > 0)
            {
                skipnumber = ((int)cmncls.pageNumber - 1) * (int)cmncls.pageSize;
            }
            return skipnumber;

        }

        //public static int Skip(vmCmnParameters cmncls)
        //{
        //    int skipnumber = 0;
        //    if (cmncls.pageNumber > 0)
        //    {
        //        skipnumber = ((int)cmncls.pageNumber - 1) * (int)cmncls.pageSize;
        //    }
        //    return skipnumber;
        //}

        #endregion

        #region APIAuth
        public static string GetApiAuthenticationKey(string username, string password)
        {

            username += ":" + password;
            return "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(username));

        }


        //public static string GetTenantCloudAccessToken(int? cloudUserId)
        //{
        //    dbRGLERPContext _ctx = new dbRGLERPContext(); string accessToken = string.Empty;
        //    if (cloudUserId > 0)
        //    {
        //        var cmnClouduser = _ctx.CmnUserCloud.Where(x => x.CloudUserId == cloudUserId).FirstOrDefault();
        //        if (cmnClouduser != null)
        //        {
        //            var subTenatInfo = _ctx.LookupCloudTenantSub.Where(x => x.CloudSubTenantId == Convert.ToInt32(cmnClouduser.TenantId)).SingleOrDefault();
        //            if (subTenatInfo != null)
        //            {
        //                var cmnCloudusersSubTNT = _ctx.CmnUserCloud.Where(x => x.CloudUserId == Convert.ToInt32(subTenatInfo.UserId)).FirstOrDefault();
        //                if (cmnCloudusersSubTNT != null)
        //                {
        //                    accessToken = GetApiAuthenticationKey(cmnCloudusersSubTNT.Username, cmnCloudusersSubTNT.AccessKeys);
        //                }
        //            }
        //            var TenatInfo = _ctx.LookupCloudTenant.Where(x => x.CloudTenantId == Convert.ToInt32(cmnClouduser.TenantId)).SingleOrDefault();
        //            if (TenatInfo != null)
        //            {
        //                accessToken = GetApiAuthenticationKey(TenatInfo.UserName, TenatInfo.Password);
        //            }
        //        }
        //    }
        //    return accessToken;
        //}
        //public static string GetUserCloudAccessToken(int? cloudUserId)
        //{
        //    dbRGLERPContext _ctx = new dbRGLERPContext(); string accessToken = string.Empty;
        //    if (cloudUserId > 0)
        //    {
        //        var cmnClouduser = _ctx.CmnUserCloud.Where(x => x.CloudUserId == cloudUserId).FirstOrDefault();
        //        if (cmnClouduser != null)
        //        {
        //            accessToken = GetApiAuthenticationKey(cmnClouduser.Username, cmnClouduser.AccessKeys);
        //        }
        //    }
        //    return accessToken;
        //}
        //public static int GetTenantIdAsInteger(int? cloudUserId)
        //{
        //    dbRGLERPContext _ctx = new dbRGLERPContext(); int tenantId = 0;
        //    if (cloudUserId > 0)
        //    {
        //        var cmnClouduser = _ctx.CmnUserCloud.Where(x => x.CloudUserId == cloudUserId).FirstOrDefault();
        //        if (cmnClouduser != null)
        //        {
        //            tenantId = Convert.ToInt32(cmnClouduser.TenantId);
        //        }
        //    }

        //    return tenantId;
        //}
        //public static string GetTenantIdAsString(int? cloudUserId)
        //{
        //    dbRGLERPContext _ctx = new dbRGLERPContext(); string tenantId = string.Empty;
        //    if (cloudUserId > 0)
        //    {
        //        var cmnClouduser = _ctx.CmnUserCloud.Where(x => x.CloudUserId == cloudUserId).FirstOrDefault();
        //        if (cmnClouduser != null)
        //        {
        //            tenantId = cmnClouduser.TenantId;
        //        }
        //    }

        //    return tenantId;
        //}

        #endregion

        #region DateTime
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static DateTime UnixTimeStampToDateTimeMiliSec(double unixTimeStamp)
        {
            string strTime = unixTimeStamp.ToString();
            double dTime = Convert.ToDouble(strTime.Substring(0, strTime.Length - 3));
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(dTime).ToLocalTime();
            return dtDateTime;
        }

        public static double DateTimeToUnixTimestamp(DateTime dateTime)
        {
            return (TimeZoneInfo.ConvertTimeToUtc(dateTime) -
                   new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
        }

        public static double DateTimeToUnixTimestampExact(DateTime dateTime)
        {
            return (dateTime -
                   new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
        }

        public static double getMinutes(DateTime fromdate)
        {
            return DateTime.Now.Subtract(fromdate).TotalMinutes;
        }

        public static double getHour(DateTime fromdate)
        {
            return DateTime.Now.Subtract(fromdate).TotalHours;
        }

        public static double getDay(DateTime fromdate)
        {
            return DateTime.Now.Subtract(fromdate).TotalDays;
        }

        /// <summary>
        /// UniversalToLocal is a method to convert a universal time to local time. input a parameter as universal time
        /// </summary>
        /// <param name="universalTime"></param>
        /// <returns></returns>
        public static DateTime UniversalToLocal(DateTime universalTime)
        {
            DateTime convertedUtc = Convert.ToDateTime(universalTime).ToUniversalTime();
            return convertedUtc;
        }

        /// <summary>
        /// LocalToUniversal is a method to convert a local time to universal time. input a parameter as local time
        /// </summary>
        /// <param name="localTime"></param>
        /// <returns></returns>
        public static DateTime LocalToUniversal(DateTime localTime)
        {
            DateTime convertedLocal = Convert.ToDateTime(localTime).ToLocalTime();
            return convertedLocal;
        }
        #endregion

        #region Encrypt-Decrypt
        public static string Encryptdata(string inputString)
        {
            string strmsg = string.Empty;
            try
            {
                byte[] encode = new byte[inputString.Length];
                encode = Encoding.UTF8.GetBytes(inputString);
                strmsg = Convert.ToBase64String(encode);
            }
            catch (Exception) { }

            return strmsg;
        }

        public static string Decryptdata(string inputString)
        {
            string decryptpwd = string.Empty;
            try
            {
                UTF8Encoding encodepwd = new UTF8Encoding();
                Decoder Decode = encodepwd.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(inputString);
                int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                decryptpwd = new String(decoded_char);
            }
            catch (Exception) { }
            return decryptpwd;
        }

        private static Random random = new Random();
        public static string getRandomNumber()
        {
            return genRandomNumber(11);
        }

        public static string getRandomNumber(int length)
        {
            return genRandomNumber(length);
        }

        private static string genRandomNumber(int length)
        {
            string rndm = string.Empty;
            try
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                rndm = new string(Enumerable.Repeat(chars, length).Select(x => x[random.Next(x.Length)]).ToArray());
            }
            catch (Exception) { }
            return rndm;
        }

        public static string getJsonString(string bodys)
        {
            try
            {
                bodys = bodys.Remove(0, 0);
                bodys = bodys.Remove(bodys.Length - 1, 0);
            }
            catch (Exception) { }
            return bodys;
        }
        #endregion

        #region Validation
        /// <summary>
        /// ValidateRequiredField is a method to validate required field with method overloading parameters
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ValidateRequiredField(string value)
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(value))
            {
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(value))
            {
                isValid = false;
            }
            return isValid;
        }

        /// <summary>
        /// ValidateRequiredField is a method to validate required field with method overloading parameters
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ValidateRequiredField(int? value)
        {
            bool isValid = true;
            if (value == null)
            {
                isValid = false;
            }
            if (int.MinValue < value)
            {
                isValid = false;
            }
            if (int.MaxValue > value)
            {
                isValid = false;
            }
            return isValid;
        }

        /// <summary>
        /// ValidateRangField is a method to validate rang of field with method overloading parameters
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ValidateRangField(string value, int min, int max)
        {
            bool isValid = true;
            if (value.Length > max)
            {
                isValid = false;
            }
            if (value.Length < min)
            {
                isValid = false;
            }
            return isValid;
        }

        /// <summary>
        /// ValidateRangField is a method to validate rang of field with method overloading parameters
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ValidateRangField(int value, int min, int max)
        {
            bool isValid = true;
            if (value > max)
            {
                isValid = false;
            }
            if (value < min)
            {
                isValid = false;
            }
            return isValid;
        }
        #endregion
        public static string RemoveMultipleSpaceFromString(string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                data = string.Join(" ", data.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            }

            return data;
        }

        public class vmCmnParameter
        {
            public int? id { get; set; }
            public string strId { get; set; }
            public string strId2 { get; set; }
            public string strId3 { get; set; }
            public string strId4 { get; set; }

            public string strId5 { get; set; }
            public int? pageNumber { get; set; }
            public int? pageSize { get; set; }
            public bool IsPaging { get; set; }
            public string SearchVal { get; set; }
            public string Name { get; set; }
            public string userName { get; set; }
            public string userPass { get; set; }
            public string macAddress { get; set; }
            public string tablename { get; set; }
            public string property { get; set; }
            public string values { get; set; }
            public string UserID { get; set; }
            public string LoggedUserId { get; set; }
            public int? month { get; set; }
            public string ClientIP { get; set; }
            public bool IsLoggedIn { get; set; }
            public string Path { get; set; }
            public bool IsTrue { get; set; }
            public DateTime? SDate { get; set; }
            public DateTime? EDate { get; set; }
            public DateTime? CDate { get; set; }
            public bool IsEdit { get; set; }
            public string strYear { get; set; }
            public string strMonth { get; set; }
            public string strDay { get; set; }
            public string strTime { get; set; }
            public string Status { get; set; }
            public string Comments { get; set; }
            public bool IsSys { get; set; }
        }
    }
}
