using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Data.Repository.Accounts;

using Technofair.Lib.Model;
using TFSMS.Admin.Model.Accounts;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Repository.Accounts;


namespace TFSMS.Admin.Service.Accounts
{
    public interface IAnFFinancialServiceProviderTypeService
    {
        Operation Save(AnFFinancialServiceProviderType obj);
        Operation Delete(AnFFinancialServiceProviderType obj);
        AnFFinancialServiceProviderType GetById(Int16 Id);
        Operation Update(AnFFinancialServiceProviderType obj);
        List<AnFFinancialServiceProviderType> GetAll();
    }
    public class AnFFinancialServiceProviderTypeService: IAnFFinancialServiceProviderTypeService
    {
        private IAnFFinancialServiceProviderTypRepository repository;
        private IAdminUnitOfWork _UnitOfWork;
        public AnFFinancialServiceProviderTypeService(IAnFFinancialServiceProviderTypRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        public List<AnFFinancialServiceProviderType> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public Operation Save(AnFFinancialServiceProviderType obj)
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

        public Operation Update(AnFFinancialServiceProviderType obj)
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

        public Operation Delete(AnFFinancialServiceProviderType obj)
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

        public AnFFinancialServiceProviderType GetById(Int16 Id)
        {
            AnFFinancialServiceProviderType obj = repository.GetById(Id);
            return obj;
        }

    }
}
