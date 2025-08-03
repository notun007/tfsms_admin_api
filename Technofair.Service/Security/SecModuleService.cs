using Technofair.Lib.Model;
using Technofair.Lib.Utilities;
using TFSMS.Admin.Model.Security;
using TFSMS.Admin.Model.ViewModel.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using TFSMS.Admin.Data.Repository.Security;
using TFSMS.Admin.Data.Infrastructure;
using Technofair.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Service.Security
{

    public interface ISecModuleService
    {

        List<SecModule> GetModuleByCompanyId(int companyId);
        List<SecPermittedModuleViewModel> GetSecPermittedModuleByUserId(int userId, int companyId);

        Operation Save(SecModule obj);
        Operation Delete(SecModule obj);
        Operation Update(SecModule obj);
        SecModule GetById(int id);
        List<SecModule> GetAll();
        SecModule? GetModuleByName(string name, string nameBn);
    }
    public class SecModuleService : ISecModuleService
    {
        private ISecModuleRepository repository;
        private IAdminUnitOfWork _UnitOfWork;
        public SecModuleService(ISecModuleRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        public SecModule GetById(int id)
        {
            return repository.GetById(id);
        }

        public Operation Save(SecModule obj)
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

        public Operation Update(SecModule obj)
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
        public Operation Delete(SecModule obj)
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


        public List<SecModule> GetAll()
        {
            return repository.GetAll().ToList();
        }
        public List<SecPermittedModuleViewModel> GetSecPermittedModuleByUserId(int userId, int companyId)
        {
            List<SecPermittedModuleViewModel> list = new List<SecPermittedModuleViewModel>();
            DataTable dt = repository.GetSecPermittedModuleByUserId(userId, companyId);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(((SecPermittedModuleViewModel)Helper.FillTo(row, typeof(SecPermittedModuleViewModel))));
                }
            }
            return list;
        }
        public List<SecModule> GetModuleByCompanyId(int companyId)
        {
            List<SecModule> list = new List<SecModule>();
            DataTable dt = repository.GetModuleByCompanyId(companyId);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(((SecModule)Helper.FillTo(row, typeof(SecModule))));
                }
            }
            return list;
        }
        public SecModule? GetModuleByName(string name, string nameBn)
        {
            return repository.GetModuleByName(name, nameBn);
        }

    }
}
