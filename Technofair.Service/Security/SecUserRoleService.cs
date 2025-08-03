using Technofair.Lib.Model;
using Technofair.Lib.Utilities;
using Technofair.Model.Security;
using Technofair.Model.ViewModel.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFSMS.Admin.Data.Repository.Security;
using TFSMS.Admin.Data.Infrastructure;
using Technofair.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Service.Security
{
    public interface ISecUserRoleService
    {
        List<SecUserRoleViewModel> GetSecUserRoles(int userId);
        Task<Operation> Save(SecUserRole objSecUserRole, int userId);
        Task<Operation> Save(SecUserRole obj);
        Operation Update(SecUserRole obj);
        Operation Delete(SecUserRole obj);
        Operation DeleteByUserId(int userId);
        SecUserRole GetById(int Id);
        List<SecUserRole> GetByRoleId(int rlId);
        SecUserRole GetByRoleAndUserId(int userId, int roleId);
        SecUserRole GetUserRoleByUserId(int usrId);
       // List<SecUserRoleViewModel> GetUserRolesByCompanyAndRoleId(int companyId, int roleId);
        List<UserRoleViewModel> GetRoleLessUserByCompanyId(int companyId);
        List<SecUserRoleViewModel> GetRoleOrientedUserByCompanyRoleId(int companyId, int? roleId);
        
        List<SecUserRole> GetByUserId(int userId);
    }
    public class SecUserRoleService : ISecUserRoleService
    {
        private ISecUserRoleRepository repository;
        private IAdminUnitOfWork _UnitOfWork;


        public SecUserRoleService(ISecUserRoleRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        //public DataTable GetSecUserRoles(int userId)
        //{
        //    try
        //    {

        //        SqlParameter[] parameters = new SqlParameter[1];
        //        parameters[0] = new SqlParameter("@SecUserId", userId);
        //        DataTable dt = repository.GetFromStoredProcedure(SPList.SecUserRole.GetSecUserRoleByUserId, parameters);

        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public SecUserRole GetById(int Id)
        {
            return repository.GetById(Id);
        }

        public List<SecUserRole> GetByRoleId(int rlId)
        {
            return repository.GetMany(w => w.SecRoleId == rlId).ToList();
        }
        public SecUserRole GetByRoleAndUserId(int userId, int roleId)
        {
            return repository.GetMany(w => w.SecUserId == userId && w.SecRoleId == roleId).FirstOrDefault();
        }

        public SecUserRole GetUserRoleByUserId(int userId)
        {
            return repository.GetMany(w => w.SecUserId == userId).OrderByDescending(o => o.Id).FirstOrDefault();
        }

        
        //public List<SecUserRoleViewModel> GetUserRolesByCompanyAndRoleId(int companyId, int roleId)
        //{
        //    try
        //    {
        //        DataTable dt = repository.GetUserRolesByCompanyAndRoleId(companyId, roleId);
        //        List<SecUserRoleViewModel> list = new List<SecUserRoleViewModel>();
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                list.Add(((SecUserRoleViewModel)Helper.FillTo(row, typeof(SecUserRoleViewModel))));
        //            }
        //        }
        //        return list;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<UserRoleViewModel> GetRoleLessUserByCompanyId(int companyId)
        {
            try
            {
                DataTable dt = repository.GetRoleLessUserByCompanyId(companyId);
                List<UserRoleViewModel> list = new List<UserRoleViewModel>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(((UserRoleViewModel)Helper.FillTo(row, typeof(UserRoleViewModel))));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SecUserRoleViewModel> GetRoleOrientedUserByCompanyRoleId(int companyId, int? roleId)
        {
            try
            {
                DataTable dt = repository.GetRoleOrientedUserByCompanyRoleId(companyId, roleId);
                List<SecUserRoleViewModel> list = new List<SecUserRoleViewModel>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(((SecUserRoleViewModel)Helper.FillTo(row, typeof(SecUserRoleViewModel))));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Operation> Save(SecUserRole obj)
        {
            Operation objOperation = new Operation { Success = false };
            try
            {
                objOperation.OperationId = await repository.AddEntityAsync(obj);
                objOperation.Success = await _UnitOfWork.CommitAsync();
                objOperation.Success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }
        public Operation Update(SecUserRole obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
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

        public List<SecUserRoleViewModel> GetSecUserRoles(int userId)
        {
            List<SecUserRoleViewModel> list = null;
            try
            {
                DataTable dt = repository.GetSecUserRoles(userId);
                if (dt != null)
                {
                    list = new List<SecUserRoleViewModel>();
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add((SecUserRoleViewModel)Helper.FillTo(row, typeof(SecUserRoleViewModel)));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SecUserRole> GetByUserId(int userId)
        {
            try
            {
                List<SecUserRole> list = repository.GetMany(w => w.SecUserId == userId).ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Operation DeleteByUserId(int userId)
        {
            Operation objOperation = new Operation { Success = false };
            try
            {
                List<SecUserRole> list = repository.GetMany(w => w.SecUserId == userId).ToList();
                if (list != null && list.Count > 0)
                {
                    foreach (SecUserRole obj in list)
                    {
                        objOperation = Delete(obj);
                    }
                }
                return objOperation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Operation Delete(SecUserRole obj)
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

        public async Task<Operation> Save(SecUserRole obj, int userId)
        {
            Operation objOperation = new Operation { Success = false };
            this.DeleteByUserId(userId);
            if (obj.SecRoleId != 0)
            {
                try
                {
                    objOperation.OperationId = await repository.AddEntityAsync(obj);
                    objOperation.Success = await _UnitOfWork.CommitAsync();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return objOperation;
        }
    }
}
