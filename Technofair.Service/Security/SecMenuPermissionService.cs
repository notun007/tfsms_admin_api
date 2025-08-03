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
using TFSMS.Admin.Model.ViewModel.Common;
using TFSMS.Admin.Data.Repository.Security;
using TFSMS.Admin.Data.Infrastructure;
using Technofair.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Service.Security
{

    public interface ISecMenuPermissionService
    {
        Operation Save(List<SecMenuPermission> list);
        Task<Operation> Save(SecMenuPermission obj);
        Operation Update(SecMenuPermission obj);
        Operation Delete(SecMenuPermission obj);
        int DeleteSecRolePermission(int? roleId, int? userId, int moduleId);
        List<SecMenuPermissionViewModel> GetMenuPermissionByUserOrRoleId(int? roleId, int? userId, int moduleId);
        List<SecMenuPermission> GetByModuleAndRoleId(int moduleId, int? roleId, int? userId);
        SecMenuPermissionViewModel GetSecMenuButtonPermission(string menuName, int userId, int moduleId);

        //Task<List<SecMenuViewModel>> GetRolesBaseMenu();
        Task<List<Modules>> ModuleMenuByRole(int roleId);
        List<Modules> ModuleMenuByRole(int roleId, string withEventPerm); //New
        Task<bool> HasPermision(string linkurl, int roleId);
        SecMenuPermission GetByRoleAndMenuId(int? roleId, int? userId, int menuId);
    }
    public class SecMenuPermissionService : ISecMenuPermissionService
    {
        private ISecMenuPermissionRepository repository;
        private IAdminUnitOfWork _UnitOfWork;
        public SecMenuPermissionService(ISecMenuPermissionRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        public Operation Save(List<SecMenuPermission> list)
        {
            Operation objOperation = new Operation { Success = false };
            foreach (SecMenuPermission obj in list)
            {
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
            }
            return objOperation;
        }

        public async Task<Operation> Save(SecMenuPermission obj)
        {
            Operation objOperation = new Operation { Success = false };
            try
            {
                objOperation.OperationId = await repository.AddEntityAsync(obj);
                objOperation.Success = await _UnitOfWork.CommitAsync();

                //objOperation.OperationId = repository.AddEntity(obj);
                //_UnitOfWork.Commit();
                //await _UnitOfWork.CommitWithTransaction();
                //objOperation.Success = true;

                //await repository.Save(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }

        public Operation Update(SecMenuPermission obj)
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
        public Operation Delete(SecMenuPermission obj)
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
        public int DeleteSecRolePermission(int? roleId, int? userId, int moduleId)
        {
            try
            {
                return repository.DeleteSecRolePermission(roleId, userId, moduleId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SecMenuPermissionViewModel> GetMenuPermissionByUserOrRoleId(int ?roleId, int? userId, int moduleId)
        {
            List<SecMenuPermissionViewModel> list = null;
            try
            {
                DataTable dt = repository.GetMenuPermissionByUserOrRoleId(roleId, userId, moduleId);
                if (dt != null)
                {
                    list = new List<SecMenuPermissionViewModel>();
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add((SecMenuPermissionViewModel)Helper.FillTo(row, typeof(SecMenuPermissionViewModel)));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SecMenuPermission> GetByModuleAndRoleId(int moduleId, int? roleId, int? userId)
        {
            try
            {
                return repository.GetByModuleAndRoleId(moduleId, roleId, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SecMenuPermissionViewModel GetSecMenuButtonPermission(string menuName, int userId, int moduleId)
        {
            SecMenuPermissionViewModel obj = null;
            try
            {
                DataTable dt = repository.GetSecMenuButtonPermission(menuName, userId, moduleId);
                if (dt != null)
                {
                    obj = new SecMenuPermissionViewModel();
                    foreach (DataRow row in dt.Rows)
                    {
                        obj = (SecMenuPermissionViewModel)Helper.FillTo(row, typeof(SecMenuPermissionViewModel));
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SecMenuPermission GetByRoleAndMenuId(int? roleId, int? userId, int menuId)
        {
            return repository.GetMany(w => (w.SecRoleId == roleId || roleId == null) && (w.SecUserId == userId || userId == null) && w.SecMenuId == menuId).FirstOrDefault();
        }

        //public Task<List<SecMenuViewModel>> GetRolesBaseMenu()
        //{
        //    return repository.GetRolesBaseMenu();
        //}
        public Task<List<Modules>> ModuleMenuByRole(int roleId)
        {
            return repository.ModuleMenuByRole(roleId);
        }

        public List<Modules> ModuleMenuByRole(int roleId, string withEventPerm) //New
        {
            return repository.ModuleMenuByRole(roleId, "No");
        }
        public Task<bool> HasPermision(string linkurl, int roleId)
        {
            return repository.HasPermision(linkurl, roleId);
        }


    }
}
