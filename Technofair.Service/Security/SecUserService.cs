using Technofair.Lib.Model;
using Technofair.Lib.Utilities;
using TFSMS.Admin.Model.Security;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TFSMS.Admin.Model.ViewModel.Security;
using TFSMS.Admin.Model.Common;
using System.ComponentModel.Design;


using Technofair.Utiity.Security;
using System.Net.Sockets;
using TFSMS.Admin.Data.Repository.Security;
using TFSMS.Admin.Data.Infrastructure;
using Technofair.Data.Infrastructure.TFAdmin;
//using static Technofair.Lib.Utilities.SPList;

namespace TFSMS.Admin.Service.Security
{
    public interface ISecUserService
    {

        Operation Save(SecUser obj);
        Task<Operation> AddWithNoCommit(SecUser obj);
        Operation Delete(SecUser obj);
        SecUser GetById(int Id);
        Operation Update(SecUser obj);
        Operation UpdateWithNoCommit(SecUser obj);
        //List<SecUser> GetUserByLevel(int level, int companyId);
        //List<SecUser> GetByCompanyId(int companyId);
        SecUser GetByLoginID(string loginID);
        List<SecUser> GetAll();
        SecUserViewModel GetUserInfoById(int Id);
        List<SecUserViewModel> GetUserInfoByCompanyId(int companyId);
        List<SecUserViewModel> GetUserByAnyKey(Int16? cmnCompanyTypeId, int? companyId, Int16 userLevel);
        List<SecUserViewModel> GetUserByCompanyId(int companyId, Int16 userLevel);
        Task<LoginResponse> Authenticate(AuthenticateUser obj, string secret);
        //Task<SecUser> GetUserBySubscriberId(int subscriberId);
        List<SecUser> GetUserByCompanyId(int companyId);
        List<SecUser> GetSecUsersByCompanyAndUserType(int cmnCompanyId, int secUserTypeId);
        List<SecUserViewModel> GetSecUsersByCompanyAndUserLevel(int cmnCompanyId, int userLevel);
        Task<SecUser> GetSecUsersByEmployeeId(int employeeId);
    }

    public class SecUserService : ISecUserService
    {
        private ISecUserRepository repository;
        private IAdminUnitOfWork _UnitOfWork;


        public SecUserService(ISecUserRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        //public List<SecUser> GetByCompanyId(int companyId)
        //{
        //    try
        //    {
        //        return repository.GetMany(u => u.CmnCompanyId == companyId).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<SecUser> GetUserByCompanyId(int companyId)
        {
            try
            {
                DataTable dt = repository.GetUserByCompanyId(companyId);
                List<SecUser> list = new List<SecUser>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(((SecUser)Helper.FillTo(row, typeof(SecUser))));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public async Task<SecUser> GetSecUsersByEmployeeId(int employeeId)
        {
            return await repository.GetSecUsersByEmployeeId(employeeId);
        }
        public List<SecUser> GetSecUsersByCompanyAndUserType(int cmnCompanyId, int secUserTypeId)
        {
            try
            {
                DataTable dt = repository.GetSecUsersByCompanyAndUserType(cmnCompanyId, secUserTypeId);
                List<SecUser> list = new List<SecUser>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(((SecUser)Helper.FillTo(row, typeof(SecUser))));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SecUserViewModel> GetSecUsersByCompanyAndUserLevel(int cmnCompanyId, int userLevel)
        {
            try
            {
                DataTable dt = repository.GetSecUsersByCompanyAndUserLevel(cmnCompanyId, userLevel);
                List<SecUserViewModel> list = new List<SecUserViewModel>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(((SecUserViewModel)Helper.FillTo(row, typeof(SecUserViewModel))));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SecUser GetByLoginID(string loginID)
        {
            try
            {
                return repository.GetMany(u => u.LoginID.ToUpper() == loginID.ToUpper()).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public List<SecUser> GetUserByLevel(int level, int companyId)
        //{
        //    List<SecUser> list = new List<SecUser>();
        //    try
        //    {
        //        DataTable dt = repository.GetUserByLevel(level, companyId);
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                list.Add((SecUser)Helper.FillTo(row, typeof(SecUser)));
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return list;
        //}

        public List<SecUser> GetAll()
        {
            //return repository.GetSecUsers();
            return repository.GetAll().ToList();

        }

        public SecUser GetById(int Id)
        {
            SecUser objSecUser = repository.GetById(Id);
            return objSecUser;

        }

        public Operation Save(SecUser obj)
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

        public async Task<Operation> AddWithNoCommit(SecUser obj)
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

        public Operation Update(SecUser obj)
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

        public Operation UpdateWithNoCommit(SecUser obj)
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
        public Operation Delete(SecUser obj)
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
        public SecUserViewModel GetUserInfoById(int Id)
        {
            try
            {
                return repository.GetUserInfoById(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SecUserViewModel> GetUserInfoByCompanyId(int companyId)
        {
            try
            {
                DataTable dt = repository.GetUserInfoByCompanyId(companyId);
                List<SecUserViewModel> list = new List<SecUserViewModel>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(((SecUserViewModel)Helper.FillTo(row, typeof(SecUserViewModel))));
                    }
                }

                foreach(var item in list)
                {
                    //New
                    item.Password = AES.GetPlainText(item.Password);
                    //Old
                    //item.Password = AES.Decrypt(item.Password);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SecUserViewModel> GetUserByAnyKey(Int16? cmnCompanyTypeId, int? companyId, Int16 userLevel)
        {
            try
            {
                DataTable dt = repository.GetUserByAnyKey(cmnCompanyTypeId, companyId, userLevel);
                List<SecUserViewModel> list = new List<SecUserViewModel>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(((SecUserViewModel)Helper.FillTo(row, typeof(SecUserViewModel))));
                    }
                }

                foreach (var item in list)
                {
                    //New
                    item.Password = AES.GetPlainText(item.Password);
                    //Old
                    //item.Password = AES.Decrypt(item.Password);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SecUserViewModel> GetUserByCompanyId(int companyId, Int16 userLevel)
        {
            try
            {
                DataTable dt = repository.GetUserByCompanyId(companyId, userLevel);
                List<SecUserViewModel> list = new List<SecUserViewModel>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(((SecUserViewModel)Helper.FillTo(row, typeof(SecUserViewModel))));
                    }
                }

                foreach (var item in list)
                {
                    //New
                    item.Password = AES.GetPlainText(item.Password);
                    //Old
                    //item.Password = AES.Decrypt(item.Password);
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<LoginResponse> Authenticate(AuthenticateUser obj, string secret)
        {
            try
            {
                return repository.Authenticate(obj, secret);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public async Task<SecUser> GetUserBySubscriberId(int subscriberId)
        //{
        //    return await repository.GetUserBySubscriberId(subscriberId);
        //}

    }
}