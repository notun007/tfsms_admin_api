using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Lib.Utilities;
using TFSMS.Admin.Model.Security;
using TFSMS.Admin.Model.ViewModel.Security;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Repository.Security;
using Technofair.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Service.Security
{
    public interface ISecUserTypeService
    {
        Task<List<SecUserType>> GetAllSecUserType();
    }
    public class SecUserTypeService : ISecUserTypeService
    {
        private ISecUserTypeRepository repository;
        private IAdminUnitOfWork _UnitOfWork;


        public SecUserTypeService(ISecUserTypeRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        public async Task<List<SecUserType>> GetAllSecUserType()
        {
            List<SecUserType> list = await repository.GetAllSecUserType();
            return list;
        }

    }

}
