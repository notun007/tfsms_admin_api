using Technofair.Lib.Model;
using Technofair.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Model.ViewModel.Security;
using Technofair.Model.Common;
using TFSMS.Admin.Data.Repository.Security;
using TFSMS.Admin.Data.Infrastructure;
using Technofair.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Service.Security
{
    public interface ISecRoleService
    {
        Operation Save(SecRole obj);
        Operation Update(SecRole obj);
        Operation Delete(SecRole obj);
        SecRole GetById(int id);
        List<SecRole> GetAll();
        Task<List<SecRoleViewModel>> GetAllSecRole();
        Task<List<SecRole>> GetSecRoleByCompanyId(Int16 cmnCompanyId);
        Task<List<SecRole>> GetSecRoleByCompanyTypeId(Int16? cmnCompanyTypeId);
    }

    public class SecRoleService : ISecRoleService
    {
        private ISecRoleRepository repository;
        private IAdminUnitOfWork _UnitOfWork;
        public SecRoleService(ISecRoleRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }


        public Operation Save(SecRole obj)
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
        public Operation Update(SecRole obj)
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
        public Operation Delete(SecRole obj)
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

        public SecRole GetById(int id)
        {
            return repository.GetById(id);
        }
        public List<SecRole> GetAll()
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
        public async Task<List<SecRoleViewModel>> GetAllSecRole()
        {
            return await repository.GetAllSecRole();
        }

        public async Task<List<SecRole>> GetSecRoleByCompanyId(Int16 cmnCompanyId)
        {
            return await repository.GetSecRoleByCompanyId(cmnCompanyId);
        }

        public async Task<List<SecRole>> GetSecRoleByCompanyTypeId(Int16? cmnCompanyTypeId)
        {
            return await repository.GetSecRoleByCompanyTypeId(cmnCompanyTypeId);
        }
    }
}
