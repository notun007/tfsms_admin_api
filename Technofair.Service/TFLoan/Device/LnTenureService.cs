using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Data.Infrastructure;
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Data.Repository.TFLoan.Device;
using TFSMS.Admin.Model.TFLoan.Device;
using TFSMS.Admin.Data.Repository.TFLoan.Device;



namespace TFSMS.Admin.Service.TFLoan.Device
{
    public interface ILnTenureService
    {
        List<LnTenure> GetAll();
    }
    public class LnTenureService : ILnTenureService
    {
        private ILnTenureRepository repository;
        private IAdminUnitOfWork _UnitOfWork;
        public LnTenureService(ILnTenureRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        public List<LnTenure> GetAll()
        {
            return repository.GetAll().ToList();
        }
    }
}
