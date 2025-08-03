using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Data.Infrastructure;
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Data.Repository.TFLoan.Device;

//using Technofair.Data.Repository.Accounts;
using Technofair.Lib.Model;
using Technofair.Model.Accounts;
using TFSMS.Admin.Data.Repository.Accounts;

namespace TFSMS.Admin.Service.TFLoan.Device
{
    public interface IAnFFinancialServiceProviderService
    {
        Operation Save(AnFFinancialServiceProvider obj);
        Operation Delete(AnFFinancialServiceProvider obj);
        AnFFinancialServiceProvider GetById(Int16 Id);
        Operation Update(AnFFinancialServiceProvider obj);
        List<AnFFinancialServiceProvider> GetAll();
    }
    public class AnFFinancialServiceProviderService: IAnFFinancialServiceProviderService
    {
        private IAnFFinancialServiceProviderRepository repository;
        private IAdminUnitOfWork _UnitOfWork;
        public AnFFinancialServiceProviderService(IAnFFinancialServiceProviderRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        public List<AnFFinancialServiceProvider> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public Operation Save(AnFFinancialServiceProvider obj)
        {
            Operation objOperation = new Operation { Success = false };

            Int16 Id = repository.AddEntity(obj);
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

        public Operation Update(AnFFinancialServiceProvider obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            repository.Update(obj);

            try
            {
                _UnitOfWork.Commit();
            }
            catch (Exception)
            {
                objOperation.Success = false;

            }
            return objOperation;
        }

        public Operation Delete(AnFFinancialServiceProvider obj)
        {
            Operation objOperation = new Operation { Success = true, OperationId = obj.Id };
            repository.Delete(obj);

            try
            {
                _UnitOfWork.Commit();
            }
            catch (Exception)
            {

                objOperation.Success = false;
            }
            return objOperation;
        }

        public AnFFinancialServiceProvider GetById(Int16 Id)
        {
            AnFFinancialServiceProvider obj = repository.GetById(Id);
            return obj;
        }
    }
}
