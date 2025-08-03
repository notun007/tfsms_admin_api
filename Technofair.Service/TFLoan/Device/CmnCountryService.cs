using Technofair.Data.Infrastructure;
using Technofair.Data.Repository.TFLoan.Device;

//using Technofair.Data.Repository.Common;
using Technofair.Lib.Model;
using Technofair.Model.Common;
using TFSMS.Admin.Data.Repository.Common;

namespace TFSMS.Admin.Service.TFLoan.Device
{

    #region Interface
    public interface ICmnCountryService
    {
        Task<CmnCountry> Add(CmnCountry obj);
        Task<CmnCountry> Update(CmnCountry obj);
        Task<CmnCountry> GetCountryById(int id);
        Task<List<CmnCountry>> GetCountryList();
        Task<bool> DelCountryById(int id);
    }
    #endregion


    #region Member
    public class CmnCountryService : ICmnCountryService
    {
        private ICmnCountryRepository repository;
        private IAdminUnitOfWork _UnitOfWork;

        public CmnCountryService(ICmnCountryRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        public Task<CmnCountry> Add(CmnCountry obj)
        {

            return repository.Add(obj);
        }

        public Task<CmnCountry> Update(CmnCountry obj)
        {

            return repository.Update(obj);
        }
        public Task<bool> DelCountryById(int id)
        {           
            return DelCountryById(id);
        }


        public Task<List<CmnCountry>> GetCountryList()
        {
            try
            {
                return repository.GetCountryList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public Task<CmnCountry> GetCountryById(int id)
        {
            return repository.GetCountryById(id);
        }
        
    }

    #endregion
}
