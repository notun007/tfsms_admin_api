using Technofair.Lib.Utilities;
using Technofair.Model.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Lib.Model;
using TFSMS.Admin.Data.Repository.Security;
using TFSMS.Admin.Data.Infrastructure;
using Technofair.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Service.Security
{

    public interface ISecMenuService
    {
        DataTable GetMenusByUserIdAndModualeId(Int32 userId,Int32 moduleId);
        DataTable GetApprovalProcessLevelByUserId(int userId, int approvalProcessId);
        DataTable IsPermitted(int userId, int processId, int levelId);

        DataTable GetMenuPermissionByUserId(string menuName, int userId, int secModuleId);
        DataTable GetMenuPermissionByUserOrRoleId(int roleId, int userId, int secModuleId);
        List<SecMenu> GetByModuleId(int moduleId);

        SecMenu GetById(int Id);
        Task<List<SecMenu>> GetMenus();
        Operation Save(SecMenu obj);
       // Task<SecMenu> Save(SecMenu obj);
        Task<SecMenu> Update(SecMenu obj);
        //Task<SecModule> GetModule(int id);
        //Task<List<SecModule>> GetModules();

        //void Update(SecMenu SecMenu);

        //Operation Commit();
    }
    public class SecMenuService : ISecMenuService
    {
        private ISecMenuRepository repository;
        private ISecModuleRepository _moduleRepo;
        private IAdminUnitOfWork _UnitOfWork;
        public SecMenuService(ISecMenuRepository menuRepository, ISecModuleRepository moduleRepo,IAdminUnitOfWork unitOfWork)
        {
            this.repository = menuRepository;
            this._moduleRepo = moduleRepo;
            this._UnitOfWork = unitOfWork;       
        }

        public DataTable GetMenusByUserIdAndModualeId(int userId, int moduleId)
        {
            DataTable dt = new DataTable();
            SqlParameter[] parameters = new SqlParameter[2];


            parameters[0] = new SqlParameter("@uid",userId);
            parameters[1] = new SqlParameter("@moduleid",moduleId);
            dt = repository.GetFromStoredProcedure(SPList.SecMenu.GetSecModuleResourcesByUserIdAndModuleId, parameters);

            return dt;
        }


        public DataTable GetApprovalProcessLevelByUserId(int userId, int approvalProcessId)
        {
            DataTable dt = null;
            try
            {
                dt = new DataTable();
                SqlParameter[] paramsToStore = new SqlParameter[2];

                paramsToStore[0] = new SqlParameter("@userId", userId);
                paramsToStore[1] = new SqlParameter("@approvalProcessId", approvalProcessId);
               
                dt = repository.GetFromStoredProcedure(SPList.SecMenu.GetCmnProcessLevelByUserId, paramsToStore);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable IsPermitted(int userId, int processId, int levelId)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] paramsToStore = new SqlParameter[3];
                paramsToStore[0] = new SqlParameter("@userId", userId);
                paramsToStore[1] = new SqlParameter("@processId", processId);
                paramsToStore[2] = new SqlParameter("@levelId", levelId);
               
                dt = repository.GetFromStoredProcedure(SPList.CmnApprovalUserPermissions.GetCmnApprovalUserPermissionCountByUserAndProcessAndLevelId, paramsToStore);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetMenuPermissionByUserId(string menuName, int userId, int moduleId)
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[3];
            paramsToStore[0] = new SqlParameter("@SecUserId", userId);
            paramsToStore[1] = new SqlParameter("MenuName", menuName);
            paramsToStore[2] = new SqlParameter("@SecModuleId", moduleId);
            try
            {
                dt = repository.GetFromStoredProcedure(SPList.SecMenuPermission.GetSecMenuButtonPermission, paramsToStore);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }
        public DataTable GetMenuPermissionByUserOrRoleId(int roleId, int userId, int moduleId)
        {
            DataTable dt = new DataTable();

            SqlParameter[] paramsToStore = new SqlParameter[3];
            paramsToStore[1] = new SqlParameter("@SecRoleId", roleId);
            paramsToStore[0] = new SqlParameter("@SecUserId", userId);
            paramsToStore[2] = new SqlParameter("@SecModuleId", moduleId);
            try
            {
                dt = repository.GetFromStoredProcedure(SPList.SecMenuPermission.GetSecMenuPermissionsByRoleId, paramsToStore);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public List<SecMenu> GetByModuleId(int moduleId)
        {
            return repository.GetMany(sm => sm.SecModuleId == moduleId).ToList();
        }

        public async Task<List<SecMenu>> GetMenus()
        {
            return await repository.GetMenus();
        }

        public SecMenu GetById(int Id)
        {
            return repository.GetById(Id);
        }

     
        public Operation Save(SecMenu obj)
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
       
        //public async Task<SecMenu> Save(SecMenu obj)
        //{
        //    return await repository.Save(obj);
        //}

        public async Task<SecMenu> Update(SecMenu obj)
        {
            return await repository.Update(obj);
        }

        //public async Task<SecModule> GetModule(int id)
        //{
        //    return await moduleRepo.GetModule(id);
        //}

        //public async Task<List<SecModule>> GetModules()
        //{
        //    return await moduleRepo.GetModules();
        //}
    }
}
