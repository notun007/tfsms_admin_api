
using System.Collections;

using System.Management;
using TFSMS.Admin.Model.TFAdmin;
using Technofair.Lib.Model;
using TFSMS.Admin.Model.ViewModel.TFAdmin;
using System.Net;
using TFSMS.Admin.Data.Repository.TFAdmin;
using TFSMS.Admin.Data.Infrastructure;
using Technofair.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Service.TFAdmin
{
  
    public interface ITFAClientServerInfoService
    {
        #region TFSMS
        Operation Save(TFAClientServerInfo obj);
        Operation Update(TFAClientServerInfo obj);
        Operation Delete(TFAClientServerInfo obj);
        List<TFAClientServerInfo> GetAll();
        TFAClientServerInfo GetById(int id);

        //Task<TFAClientServerInfo> GetStoredClientServerInfoByAppKey(string appKey);

        //Task<TFAClientServerInfo> GetClientServerInfoByCustomerCompanyId(int Id);
        //Task<bool> IsValidServer(TFAClientServerInfo obj);
        TFAClientServerInfoViewModel ReadServerInfo();
        string GetWin32_ProcessorId();
        string GetWin32_NetworkAdapterId();
        ArrayList GetWin32_NetworkAdapterList();
        string GetWin32_BaseBoardId();
        string GetWin32_BIOSId();
        #endregion
    }
    public class TFAClientServerInfoService : ITFAClientServerInfoService
    {
        private ITFAClientServerInfoRepository repository;
        private IAdminUnitOfWork _UnitOfWork;


        public TFAClientServerInfoService(ITFAClientServerInfoRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        #region TF Admin

        public Operation Save(TFAClientServerInfo obj)
        {
            Operation objOperation = new Operation { Success = false };
            try
            {
                objOperation.OperationId = repository.AddEntity(obj);
                _UnitOfWork.Commit();
                objOperation.Success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }
        public Operation Update(TFAClientServerInfo obj)
        {
            Operation objOperation = new Operation { Success = false, OperationId = obj.Id };
            try
            {
                repository.Update(obj);
                _UnitOfWork.Commit();
                objOperation.Success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }
        public Operation Delete(TFAClientServerInfo obj)
        {
            Operation objOperation = new Operation { Success = false, OperationId = obj.Id };
            try
            {
                repository.Delete(obj);
                _UnitOfWork.Commit();
                objOperation.Success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }

        public List<TFAClientServerInfo> GetAll()
        {
            try
            {
                return repository.GetAll().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFAClientServerInfo GetById(int Id)
        {
            try
            {
                return repository.GetById(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public async Task<TFAClientServerInfo> GetStoredClientServerInfoByAppKey(string appKey)
        //{
        //    try
        //    {
        //        return await repository.GetStoredClientServerInfoByAppKey(appKey);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task<TFAClientServerInfo> GetClientServerInfoByCustomerCompanyId(int Id)
        //{
        //    try
        //    {
        //        return await repository.GetClientServerInfoByCustomerCompanyId(Id);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public async Task<bool> IsValidServer(TFAClientServerInfo obj)
        //{
        //    try
        //    {
        //        //return await repository.GetClientServerInfoByCustomerCompanyId(obj.Id);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return false;
        //}

        #endregion

        #region TFSMS
        public string GetWin32_ProcessorId()
        {
            string processorId = "";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");

            foreach (ManagementObject mo in searcher.Get())
            {
                processorId = mo["ProcessorId"].ToString();

            }

            return processorId;
        }
        public string GetWin32_NetworkAdapterId()
        {

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapter WHERE NetConnectionStatus=2");
            string macAddress = "";
            foreach (ManagementObject mo in searcher.Get())
            {
                macAddress = mo["MACAddress"].ToString();
            }
            return macAddress;
        }
        public ArrayList GetWin32_NetworkAdapterList()
        {

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapter WHERE NetConnectionStatus=2");

            ArrayList macAddress = new ArrayList();
            // string[] macAddress = new string[100];
            // ArrayList arrayList = new ArrayList();



            foreach (ManagementObject mo in searcher.Get())
            {
                var address = mo["MACAddress"].ToString();
                macAddress.Add(address);

            }

            return macAddress;
        }
        public string GetWin32_BaseBoardId()
        {
            ManagementObjectSearcher SystemEnclosure = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
            string serialNumber = "";
            foreach (ManagementObject mo in SystemEnclosure.Get())
            {
                serialNumber = mo["SerialNumber"].ToString();
            }
            return serialNumber;
        }
        public string GetWin32_BIOSId()
        {
            ManagementObjectSearcher SystemEnclosure = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
            string serialNumber = "";
            foreach (ManagementObject mo in SystemEnclosure.Get())
            {
                serialNumber = mo["SerialNumber"].ToString();
            }
            return serialNumber;
        }

        public TFAClientServerInfoViewModel ReadServerInfo() 
        {
            TFAClientServerInfoViewModel objClientServerInfo = new TFAClientServerInfoViewModel();

            //Mother Board
            ArrayList motherBoardArrayList = new ArrayList();
            ManagementObjectSearcher SystemEnclosure = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
            string motherBoardId = "";
            foreach (ManagementObject mo in SystemEnclosure.Get())
            {
                motherBoardId = mo["SerialNumber"].ToString();
                motherBoardArrayList.Add(motherBoardId);
            }
            string[] motherBoardArray = new string[motherBoardArrayList.Count];
            for (int i = 0; i < motherBoardArrayList.Count; i++)
            {
                motherBoardArray[i] = motherBoardArrayList[i].ToString(); // Convert each element to string
            }
            objClientServerInfo.MotherBoardList = motherBoardArray;


            //Network Adapter
            ArrayList adapterArrayList = new ArrayList();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapter WHERE NetConnectionStatus=2");
            string networkAdapter = "";
            foreach (ManagementObject mo in searcher.Get())
            {
                networkAdapter = mo["MACAddress"].ToString();
                adapterArrayList.Add(networkAdapter);
            }
            string[] adapterArrayArray = new string[adapterArrayList.Count];
            for (int i = 0; i < adapterArrayList.Count; i++)
            {
                adapterArrayArray[i] = adapterArrayList[i].ToString(); // Convert each element to string
            }
            objClientServerInfo.NetworkAdapterList = adapterArrayArray;



            // Get the local machine's IP addresses
            ArrayList iPArrayList = new ArrayList();
            string hostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(hostName);

            foreach (var ipAddress in ipEntry.AddressList)
            {
                iPArrayList.Add(ipAddress);
            }
            
            // Convert ArrayList to string array
            string[] iPArray = new string[iPArrayList.Count];
            for (int i = 0; i < iPArrayList.Count; i++)
            {
                iPArray[i] = iPArrayList[i].ToString(); // Convert each element to string
            }

            objClientServerInfo.ServerIPList = iPArray;
            return objClientServerInfo;
        }

        #endregion
    }

}
