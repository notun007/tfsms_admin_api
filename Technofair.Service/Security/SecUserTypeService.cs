using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Lib.Utilities;
using Technofair.Model.Security;
using Technofair.Model.ViewModel.Security;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Repository.Security;

namespace TFSMS.Admin.Service.Security
{
    public interface ISecUserTypeService
    {
        Task<List<SecUserType>> GetAllSecUserType();
    }
    public class SecUserTypeService : ISecUserTypeService
    {
        private ISecUserTypeRepository repository;
        private IUnitOfWork _UnitOfWork;


        public SecUserTypeService(ISecUserTypeRepository _repository, IUnitOfWork unitOfWork)
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
