using Technofair.Lib.Model;
using Technofair.Lib.Utilities;
using TFSMS.Admin.Model.Security;
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
    
    public interface ISecDashboardPermissionService
    { 
        Operation Save(List<SecDashboardPermission> SecDashboardPermissionList,int userId);

        DataTable GetDashboardPermissionByRoleId(int roleId, int moduleId);

        IList<PermittedDashboard> GetPermittedDashBoard(int companyId, int roleId, int moduleId);

        int Delete(int roleId, int moduleId);

    }
    public class SecDashboardPermissionService : ISecDashboardPermissionService
    {
        private ISecDashboardPermissionRepository _SecDashboardPermissionRepository;
        private IAdminUnitOfWork _UnitOfWork;


        public SecDashboardPermissionService(ISecDashboardPermissionRepository SecDashboardPermissionRepository, IAdminUnitOfWork unitOfWork)
        {
            this._SecDashboardPermissionRepository = SecDashboardPermissionRepository;
            this._UnitOfWork = unitOfWork;
        }
        public DataTable GetDashboardPermissionByRoleId(int roleId, int moduleId)
        {
            try
            {
                return _SecDashboardPermissionRepository.GetDashboardPermissionByRoleId(roleId, moduleId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Delete(int roleId,int moduleId)
        {
            int ret = 0;
            try
            {
                return _SecDashboardPermissionRepository.Delete(roleId, moduleId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Operation Save(List<SecDashboardPermission> SecDashboardPermissionList,int userId)
        {
            Operation objOperation = new Operation { Success = true };
            //this.Delete(SecDashboardPermissionList[0].SecRoleId);
            foreach (SecDashboardPermission obj in SecDashboardPermissionList)
            {
                obj.CreatedBy = userId;
                obj.CreatedDate = DateTime.Now.Date;
                long Id = _SecDashboardPermissionRepository.AddEntity(obj);
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
            return objOperation;
        }


        public IList<PermittedDashboard> GetPermittedDashBoard(int companyId, int roleId, int moduleId)
        {

            return _SecDashboardPermissionRepository.GetPermittedDashBoard(companyId,roleId,moduleId);


        }





    }
}
