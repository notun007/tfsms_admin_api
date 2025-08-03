using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Lib.Utilities
{
    public class GlobalConstants
    {
        public enum enumSureCashErrorCode
        {
            RUN_SUCCESS = 1,
            RUNERR_NOSUPPORT = 0,
            RUNERR_UNKNOWNERROR = -1,
            RUNERR_NO_OPEN_COMM = -2,
            RUNERR_WRITE_FAIL = -3,
            RUNERR_READ_FAIL = -4,
            RUNERR_INVALID_PARAM = -5,
            RUNERR_NON_CARRYOUT = -6,
            RUNERR_DATAARRAY_END = -7,
            RUNERR_DATAARRAY_NONE = -8,
            RUNERR_MEMORY = -9,
            RUNERR_MIS_PASSWORD = -10,
            RUNERR_MEMORYOVER = -11,
            RUNERR_DATADOUBLE = -12,
            RUNERR_MANAGEROVER = -14,
            RUNERR_FPDATAVERSION = -15,
        };

        public static string ReturnResultSureCash(long responseCode)
        {
            switch (responseCode)
            {
                case 200:
                    return "OK";
                case 9000:
                    return "Parameter Missing";
                case 9001:
                    return "Unauthorized Access";
                case 9002:
                    return "Request Failed";
                case 9003:
                    return "Account Missing";
                case 9004:
                    return "Invalid Account";
                case 9005:
                    return "Account Declined";
                case 9006:
                    return "In sufficient balance";
                case 9007:
                    return "Processing Error";
                case 9008:
                    return "Non-positive Amount";
                case 9009:
                    return "Rate Limit";
                case 9010:
                    return "Duplicate Bill No";
                case 9011:
                    return "Invalid Partner Code or Txn ID or mobileNumber";
                case 9012:
                    return "IP mismatch";
                case 9013:
                    return "Invalid PIN";
                case 9014:
                    return "PIN not entered";
                case 9015:
                    return "PUSH not sent";
                case 9016:
                    return "Invalid invoice number";
                case 9017:
                    return "Pending offline transaction";
                default:
                    return "Unknown error";
            }
        }

        public static string ReturnResultRocket(long responseCode)
        {
            switch (responseCode)
            {
                case 00:
                    return "Success";
                case 01:
                    return "Invalid Basic Authentication";
                case 02:
                    return "Invalid Host Authentication";
                case 03:
                    return "Invalid Authentication";
                case 04:
                    return "Invalid Operation Code";
                case 05:
                    return "Biller Short Code Missing";
                case 06:
                    return "User ID Missing";
                case 07:
                    return "Password Missing";
                case 08:
                    return "Operation Code Missing";
                case 09:
                    return "Invalid Bill Reference No";
                case 10:
                    return "Bill Amount Missing";
                case 11:
                    return "Invalid Bill Amount";
                case 13:
                    return "Txn Id not found in database";
                case 99:
                    return "Unable to process";
                default:
                    return "Unknown error";
            }
        }


        public static string ReturnResultNagad(string responseCode)
        {
            string retValue = "";
            if (responseCode == "XXX")
            {
                retValue = "The client is not allowed to connect. Need to whitelist Client’s IP";
            }
            else if (responseCode == "500")
            {
                retValue = "The client sent an invalid request: Such as lacking the required request body or parameter";
            }
            else if (responseCode == "401")
            {
                retValue = "The client failed to authenticate with the server";
            }
            else if (responseCode == "404")
            {
                retValue = "No Transaction Found";
            }
            else if (responseCode == "501")
            {
                retValue = "The Client sent an invalid request: Such as lacking the required parameter";
            }
            else if (responseCode == "406")
            {
                retValue = "GET Method not Support";
            }
            else
            {
                retValue = "Unknown error";
            }
            return retValue;
        }

        public static string ReturnResultBulkSMSBD(string responseCode)
        {
            string retValue = "";
            if (responseCode == "202")
            {
                retValue = "SMS Submitted Successfully";
            }
            else if (responseCode == "1002")
            {
                retValue = "sender id not correct/sender id is disabled";
            }
            else if (responseCode == "1003")
            {
                retValue = "Please Required all fields/Contact Your System Administrator";
            }
            else if (responseCode == "1005")
            {
                retValue = "Internal Error";
            }
            else if (responseCode == "1006")
            {
                retValue = "Balance Validity Not Available";
            }
            else if (responseCode == "1007")
            {
                retValue = "Balance Insufficient";
            }
            else if (responseCode == "1011")
            {
                retValue = "User Id not found";
            }
            else if (responseCode == "1012")
            {
                retValue = "Masking SMS must be sent in Bengali";
            }
            else if (responseCode == "1013")
            {
                retValue = "Sender Id has not found Gateway by api key";
            }
            else if (responseCode == "1014")
            {
                retValue = "Sender Type Name not found using this sender by api key";
            }
            else if (responseCode == "1015")
            {
                retValue = "Sender Id has not found Any Valid Gateway by api key";
            }
            else if (responseCode == "1016")
            {
                retValue = "Sender Type Name Active Price Info not found by this sender id";
            }
            else if (responseCode == "1017")
            {
                retValue = "Sender Type Name Price Info not found by this sender id";
            }
            else if (responseCode == "1018")
            {
                retValue = "Sender Type Name Active Price Info not found by this sender The Owner of this (username) Account is disabled";
            }
            else if (responseCode == "1019")
            {
                retValue = "The (sender type name) Price of this (username) Account is disabled";
            }
            else if (responseCode == "1020")
            {
                retValue = "Sender Type Name Active Price Info not found by this senderThe parent of this account is not found.";
            }
            else if (responseCode == "1021")
            {
                retValue = "The parent active (sender type name) price of this account is not found.";
            }
            else
            {
                retValue = "Unknown error";
            }
            return retValue;
        }

    }
}
