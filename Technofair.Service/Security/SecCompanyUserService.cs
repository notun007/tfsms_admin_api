using Technofair.Lib.Model;
using Technofair.Lib.Utilities;
using TFSMS.Admin.Model.Security;
using TFSMS.Admin.Model.ViewModel.Common;
using TFSMS.Admin.Model.ViewModel.Security;
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
    public interface ISecCompanyUserService
    {
        List<CompanyUserViewModel> GetCompanyUserByUserId(int userId);
        List<CompanyUserViewModel> GetCompanyUserByUserIdForMapping(int userId);
        Operation Save(SecCompanyUser obj);
        Operation Save(List<SecCompanyUser> companyUserList, int userId);
        DataTable Delete(int userId);
        SecCompanyUser GetByUserId(int userId);

    }

    public class SecCompanyUserService : ISecCompanyUserService
    {
        private ISecCompanyUserRepository repository;
        private IAdminUnitOfWork _UnitOfWork;


        public SecCompanyUserService(ISecCompanyUserRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }


        public SecCompanyUser GetByUserId(int userId)
        {
            try
            {
                return repository.GetMany(u => u.SecUserId == userId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public Operation Save(SecCompanyUser obj)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = repository.AddEntity(obj);
            objOperation.OperationId = Id;

            try
            {
                _UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                objOperation.Success = false;
            }
            return objOperation;
        }
        public Operation Save(List<SecCompanyUser> companyUserList, int userId)
        {
            Operation objOperation = new Operation { Success = true };
            this.Delete(userId);
            if (companyUserList != null)
            {
                foreach (SecCompanyUser objSecCompanyUser in companyUserList)
                {
                    long Id = repository.AddEntity(objSecCompanyUser);
                    objOperation.OperationId = Id;

                    try
                    {
                        _UnitOfWork.Commit();
                    }
                    catch (Exception ex)
                    {
                        objOperation.Success = false;
                    }
                }
            }
            return objOperation;
        }
        public DataTable Delete(int userId)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@SecUserId", userId);
                DataTable dt = repository.GetFromStoredProcedure(SPList.SecCompanyUser.DeleteSecCompanyUsersBySecUserId, parameters);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CompanyUserViewModel> GetCompanyUserByUserId(int userId)
        {
            List<CompanyUserViewModel> list = new List<CompanyUserViewModel>();
            DataTable dt = repository.GetCompanyUserByUserId(userId);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(((CompanyUserViewModel)Helper.FillTo(row, typeof(CompanyUserViewModel))));
                }
            }
            return list;
        }

        public List<CompanyUserViewModel> GetCompanyUserByUserIdForMapping(int userId)
        {
            List<CompanyUserViewModel> list = new List<CompanyUserViewModel>();
            DataTable dt = repository.GetCompanyUserByUserIdForMapping(userId);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(((CompanyUserViewModel)Helper.FillTo(row, typeof(CompanyUserViewModel))));
                }
            }
            return list;
        }

    }


}

