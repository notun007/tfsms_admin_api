using System.Data;
using System.Threading.Tasks;
using System.Web.Http;
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Lib.Model;
using Technofair.Lib.Utilities;
using TFSMS.Admin.Model.Common;

using TFSMS.Admin.Model.Utility;
using TFSMS.Admin.Model.ViewModel.Common;
using TFSMS.Admin.Model.ViewModel.Subscription;
using Technofair.Utiity.Http;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Repository.Common;

namespace TFSMS.Admin.Service.Common
{
    public interface ICmnCompanyService
    {
        Task<Operation> Save(CmnCompany obj);
        Task<Operation> AddWithNoCommit(CmnCompany obj);
        Operation Update(CmnCompany obj);
        Operation UpdateWithNoCommit(CmnCompany obj);
        Operation Delete(CmnCompany obj);
        CmnCompany GetById(int Id);
        List<CmnCompany> GetAll();
        List<CmnCompany> GetByCompanyId(int companyId);

        //New 23.09.2024
        List<CmnCompanyViewModel> GetSlsCustomerByCompanyId(int companyId);
        //New: 05.02.2024
        List<CmnCompanyViewModel> GetClientForQuickLoginByCompanyId(int cmnCompanyId, int userId); //New
        List<CmnCompanyViewModel> GetCompanyBySelfOrParentCompanyId(int companyId);
        List<SubscriptionStatisticsViewModel> GetSubscriptionStatistics();
        Task<List<CmnCompany>> GetSelfAndChildCompanyByCompanyId(int companyId);
        CmnCompanyViewModel GetCompanyByCompanyId(int companyId);
        Task<List<CmnCompany>> GetCompanyByCompanyTypeId(Int16 companyTypeId);
        Task<List<CmnCompany>> GetCompanyByCompanyTypeShortName(string shortName);
        List<CmnCompany> GetUpperLevelCompanyByCompanyTypeId(Int16 companyTypeId);
        Task<List<CmnCompany>> GetTopNLevelCompany(int level);
        Task<List<CmnCompany>> GetLoaneeByCompanyType(Int16 lnModelId, Int16 companyTypeId);
        string GetLastCode(Int16 companyTypeId);
        //Task<List<CmnCompanyType>> GetAllCompanyType()
        List<CmnCompanyType> GetSucceedingChildCompanyType(int? companyId);
        Task<List<CmnCompanyType>> GetSecondThirdLevelCompanyType();
        Task<List<CmnCompanyType>> GetChildCompanyType(int? companyId, int? userLevel);
        Task<List<CmnCompanyType>> GetAllCompanyType();
        Task<List<CmnCompanyTypeViewModel>> GetCompanyTypeByLoanModelId(Int16 loanModelId);
        Task<CmnCompanyType> GetCompanyTypeById(Int16 id);
        CmnCompanyType GetCompanyTypeByCompanyId(int? companyId);
        CmnCompany GetParentCompanyByCompanyId(int? companyId);
        List<CmnCompanyViewModel> GetChildCompanyByParentCompanyId( int? companyId);
        public List<CmnCompanyViewModel> GetChildCompanyUserByParentCompanyId(Int16? cmnCompanyTypeId, int? companyId, int? userLevel);
        CmnCompany GetByEmailID(string emailID);
        CmnCompany PrepareObject(CmnCompanyViewModel objView);
        CmnCompany GetByName(string name);
        List<CmnCompanyViewModel> GetClientByCompanyId(int companyId);
        //Added By Asad On 28.11.2023
        CmnCompany GetSolutionProvider();
        CmnCompany GetMainServiceOperator();
        Task<List<CmnCompany>> GetMainServiceOperators();
        List<CmnCompany> GetSecondLevelCompanies();
        Task<List<CmnCompany>> GetThirdLevelCompanies();
        List<CmnCompanyViewModel> cmnCompanyViewList();
        RevenueSubscription SetLoanCompany(RevenueSubscription objRevSubscription);
        Task<RevenueSubscription> SetLoanCompany(RevenueSubscription objRevSubscription, string baseUrl = "");
    }

    public class CmnCompanyService : ICmnCompanyService
    {
        private ICmnCompanyRepository repository;
        private IAdminUnitOfWork _UnitOfWork;

        public CmnCompanyService(ICmnCompanyRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        public CmnCompany PrepareObject(CmnCompanyViewModel objView)
        {
            return repository.PrepareObject(objView);
        }

        public string GetLastCode(Int16 companyTypeId)
        {
            return repository.GetLastCode(companyTypeId);
        }

        public async Task<Operation> Save(CmnCompany obj)
        {
            Operation objOperation = new Operation { Success = false };
            try
            {
                objOperation.OperationId = await repository.AddEntityAsync(obj);
                objOperation.Success = await _UnitOfWork.CommitAsync();
                return objOperation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //New: 20.01.2024
        public async Task<Operation> AddWithNoCommit(CmnCompany obj)
        {
            Operation objOperation = new Operation { Success = false };
            try
            {
                objOperation.OperationId = await repository.AddEntityAsync(obj);
                objOperation.Success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }

        public Operation Update(CmnCompany obj)
        {
            Operation objOperation = new Operation { Success = false, OperationId = obj.Id };
            try
            {
                repository.Update(obj);
                _UnitOfWork.Commit();
                objOperation.Success = true;
                return objOperation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //New: 20.01.2024
        public Operation UpdateWithNoCommit(CmnCompany obj)
        {
            Operation objOperation = new Operation { Success = false, OperationId = obj.Id };
            try
            {
                repository.UpdateEntity(obj);
                objOperation.Success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }

        public Operation Delete(CmnCompany obj)
        {
            Operation objOperation = new Operation { Success = false, OperationId = obj.Id };
            try
            {
                repository.Delete(obj);
                _UnitOfWork.Commit();
                objOperation.Success = true;
                return objOperation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public CmnCompany GetById(int Id)
        {
            return repository.GetById(Id);
        }

        public List<CmnCompany> GetAll()
        {
            return repository.GetAll().ToList();
        }
     
        public List<CmnCompanyViewModel> cmnCompanyViewList()
        {
            return repository.cmnCompanyViewList().ToList();
        }

        public List<CmnCompany> GetByCompanyId(int companyId)
        {
            try
            {
                DataTable dt = repository.GetByCompanyId(companyId);
                List<CmnCompany> list = new List<CmnCompany>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(((CmnCompany)Helper.FillTo(row, typeof(CmnCompany))));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //New: 05.02.2024
        public List<CmnCompanyViewModel> GetClientForQuickLoginByCompanyId(int cmnCompanyId, int userId) //New
        {
           return repository.GetClientForQuickLoginByCompanyId(cmnCompanyId, userId);
        }
        public List<CmnCompanyViewModel> GetCompanyBySelfOrParentCompanyId(int companyId)
        {
            try
            {
                DataTable dt = repository.GetCompanyBySelfOrParentCompanyId(companyId);
                List<CmnCompanyViewModel> list = new List<CmnCompanyViewModel>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(((CmnCompanyViewModel)Helper.FillTo(row, typeof(CmnCompanyViewModel))));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SubscriptionStatisticsViewModel> GetSubscriptionStatistics()
        {
            try
            {
                DataTable dt = repository.GetSubscriptionStatistics();
                List<SubscriptionStatisticsViewModel> list = new List<SubscriptionStatisticsViewModel>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(((SubscriptionStatisticsViewModel)Helper.FillTo(row, typeof(SubscriptionStatisticsViewModel))));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<CmnCompany>> GetSelfAndChildCompanyByCompanyId(int companyId)
        {
            return await repository.GetSelfAndChildCompanyByCompanyId(companyId); 
        }

        public CmnCompanyViewModel GetCompanyByCompanyId(int companyId)
        {
            try
            {
                //New
                DataTable dt = repository.GetCompanyByCompanyId(companyId);
                //Old
                //DataTable dt = repository.GetCompanyBySelfOrParentCompanyId(companyId);
                CmnCompanyViewModel objCompany = new CmnCompanyViewModel();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        objCompany = (CmnCompanyViewModel)Helper.FillTo(row, typeof(CmnCompanyViewModel));
                    }
                }
                return objCompany;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<CmnCompany>> GetCompanyByCompanyTypeId(Int16 companyTypeId)
        {
            return await repository.GetCompanyByCompanyTypeId(companyTypeId);
        }

        public async Task<List<CmnCompany>> GetCompanyByCompanyTypeShortName(string shortName)
        {
            return await repository.GetCompanyByCompanyTypeShortName(shortName);
        }
        public List<CmnCompany> GetUpperLevelCompanyByCompanyTypeId(Int16 companyTypeId)
        {
            return repository.GetUpperLevelCompanyByCompanyTypeId(companyTypeId);
        }

        public async Task<List<CmnCompany>> GetTopNLevelCompany(int level)
        {
            return await repository.GetTopNLevelCompany(level);
        }

        public async Task<List<CmnCompany>> GetLoaneeByCompanyType(Int16 lnModelId, Int16 companyTypeId)
        {
            return await repository.GetLoaneeByCompanyType(lnModelId, companyTypeId);
        }

        public List<CmnCompanyViewModel> GetClientByCompanyId(int companyId)
        {
            try
            {
                DataTable dt = repository.GetByCompanyId(companyId);
                List<CmnCompanyViewModel> list = null;
                if (dt != null && dt.Rows.Count > 0)
                {
                    list = new List<CmnCompanyViewModel>();
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(((CmnCompanyViewModel)Helper.FillTo(row, typeof(CmnCompanyViewModel))));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //New 23.09.2024
        public List<CmnCompanyViewModel> GetSlsCustomerByCompanyId(int companyId)
        {
            try
            {
                DataTable dt = repository.GetSlsCustomerByCompanyId(companyId);
                List<CmnCompanyViewModel> list = null;
                if (dt != null && dt.Rows.Count > 0)
                {
                    list = new List<CmnCompanyViewModel>();
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(((CmnCompanyViewModel)Helper.FillTo(row, typeof(CmnCompanyViewModel))));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public async Task<List<CmnCompanyType>> GetAllCompanyType()
        //{
        //    return await repository.GetAllCompanyType();
        //}
        public List<CmnCompanyType> GetSucceedingChildCompanyType(int? companyId)
        {
            return repository.GetSucceedingChildCompanyType(companyId);
        }

        public async Task<List<CmnCompanyType>> GetSecondThirdLevelCompanyType()
        {
            return await repository.GetSecondThirdLevelCompanyType();
        }

        public async Task<List<CmnCompanyType>> GetChildCompanyType(int? companyId, int? userLevel)
        {
           return await repository.GetChildCompanyType(companyId, userLevel);
        }
        public async Task<List<CmnCompanyType>> GetAllCompanyType()
        {
            return await repository.GetAllCompanyType();
        }

        public async Task<List<CmnCompanyTypeViewModel>> GetCompanyTypeByLoanModelId(Int16 loanModelId)
        {
            List<CmnCompanyTypeViewModel> list = await repository.GetCompanyTypeByLoanModelId(loanModelId);
            return list;
        }

        public async Task<CmnCompanyType> GetCompanyTypeById(Int16 id)
        {
           return await repository.GetCompanyTypeById(id);
        }
        public CmnCompanyType GetCompanyTypeByCompanyId(int? companyId)
        {
            return repository.GetCompanyTypeByCompanyId(companyId);
        }

        public CmnCompany GetParentCompanyByCompanyId(int? companyId)
        {
            return repository.GetParentCompanyByCompanyId(companyId);
        }

        public List<CmnCompanyViewModel> GetChildCompanyByParentCompanyId(int? companyId)
        {
            
            return repository.GetChildCompanyByParentCompanyId(companyId);
        }
        // New : 04.11.2024 
        
        public List<CmnCompanyViewModel> GetChildCompanyUserByParentCompanyId(Int16? cmnCompanyTypeId, int? companyId, int? userLevel)
        {
            return repository.GetChildCompanyUserByParentCompanyId(cmnCompanyTypeId, companyId, userLevel);
        }
        public CmnCompany GetByEmailID(string emailID)
        {
            return repository.GetMany(w => w.Email.ToUpper() == emailID.ToUpper()).FirstOrDefault();
        }

        public CmnCompany GetByName(string name)
        {
            return repository.GetMany(w => w.Name.ToUpper() == name.ToUpper()).FirstOrDefault();
        }

        
        public CmnCompany GetSolutionProvider()
        {
            return repository.GetSolutionProvider();
        }

        public CmnCompany GetMainServiceOperator()
        {
            return repository.GetMainServiceOperator();
        }
        public async Task<List<CmnCompany>> GetMainServiceOperators()
        {
            return await repository.GetMainServiceOperators();
        }

        public List<CmnCompany> GetSecondLevelCompanies()
        {
            return repository.GetSecondLevelCompanies();
        }

        public async Task<List<CmnCompany>> GetThirdLevelCompanies()
        {
            return await repository.GetThirdLevelCompanies();
        }

        public RevenueSubscription SetLoanCompany(RevenueSubscription objRevSubscription)
        {
            //New: 19052025
            var objDistributor = repository.GetSolutionProvider();
            var objMSO = repository.GetMainServiceOperator();
            if (objDistributor != null)
            {
                objRevSubscription.DistributorCompanyId = objDistributor.Id;
            }
            if (objMSO != null)
            {
                objRevSubscription.MSOCompanyId = objMSO.Id;
            }
            //End
            return objRevSubscription;
        }

        public async Task<RevenueSubscription> SetLoanCompany(RevenueSubscription objRevSubscription, string baseUrl = "")
        {

            var adminUrl = baseUrl + "adminapi/Common/GetSolutionProvider";
            var objDistributor = await Request<CmnCompany, CmnCompany>.GetObject(adminUrl);

            var objMSO = repository.GetMainServiceOperator();
            if (objDistributor != null)
            {
                objRevSubscription.DistributorCompanyId = objDistributor.Id;
            }
            if (objMSO != null)
            {
                objRevSubscription.MSOCompanyId = objMSO.Id;
            }
            //End
            return objRevSubscription;
        }

    }
}