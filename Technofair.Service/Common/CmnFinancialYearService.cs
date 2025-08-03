using TFSMS.Admin.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Data.Repository.Accounts;
using System.Data;
using System.Data.SqlClient;
using Technofair.Lib.Model;
using Technofair.Lib.Utilities;
using TFSMS.Admin.Model.ViewModel.Common;
using TFSMS.Admin.Data.Repository.Common;
using TFSMS.Admin.Data.Infrastructure;
using Technofair.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Service.Common
{
    public interface ICmnFinancialYearService
    {               
        Operation Save(CmnFinancialYear obj);
        Operation Update(CmnFinancialYear obj);
        Operation Delete(CmnFinancialYear obj);
        CmnFinancialYear GetById(int id);
        int GetCurrentFinancialYearId();
        List<CmnFinancialYear> GetAll();
    }
    public class CmnFinancialYearService : ICmnFinancialYearService
    {
        private ICmnFinancialYearRepository repository;
        private IAdminUnitOfWork _UnitOfWork;
        public CmnFinancialYearService(ICmnFinancialYearRepository cmnFinYearRepository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = cmnFinYearRepository;
            this._UnitOfWork = unitOfWork;
        }

        public CmnFinancialYear GetById(int id)
        {
            return repository.GetById(id);
        }
        public List<CmnFinancialYear> GetAll()
        {
            return repository.GetAll().ToList();
        }
        public Operation Save(CmnFinancialYear obj)
        {
            Operation objOperation = new Operation { Success = false };            

            try
            {
                objOperation.OperationId = repository.AddEntity(obj);
                _UnitOfWork.Commit();
                objOperation.Success = true; ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }
        public Operation Update(CmnFinancialYear obj)
        {
            Operation objOperation = new Operation { Success = false, OperationId = obj.Id };
            try
            {
                repository.Update(obj);
                _UnitOfWork.Commit();
                objOperation.Success = true; ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }
        public Operation Delete(CmnFinancialYear obj)
        {
            Operation objOperation = new Operation { Success = false, OperationId = obj.Id };
            try
            {
                repository.Delete(obj);
                _UnitOfWork.Commit();
                objOperation.Success = true; ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }        

        public int GetCurrentFinancialYearId()
        {
            int yearId = 0;
            CmnFinancialYear obj= repository.GetAll().OrderByDescending(X => X.OpeningDate).FirstOrDefault();
            if(obj != null)
            {
                yearId = obj.Id;
            }
            return yearId;
        }

   
    }
}
