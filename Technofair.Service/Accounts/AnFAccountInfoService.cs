using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Data.Repository.Accounts;
using Technofair.Data.Repository.Bank;
using Technofair.Lib.Model;
using Technofair.Model.Accounts;
using Technofair.Model.Bank;

namespace Technofair.Service.Accounts
{
    public interface IAnFAccountInfoService
    {
        Operation Save(AnFAccountInfo obj);
        Operation Update(AnFAccountInfo obj);
        Operation Delete(AnFAccountInfo obj);
        AnFAccountInfo GetById(int Id);
        List<AnFAccountInfo> GetAll();
    }
    public class AnFAccountInfoService: IAnFAccountInfoService
    {
        private IAnFAccountInfoRepository repository;
        private IAdminUnitOfWork _UnitOfWork;


        public AnFAccountInfoService(IAnFAccountInfoRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }
        public List<AnFAccountInfo> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public AnFAccountInfo GetById(int Id)
        {
            AnFAccountInfo obj = repository.GetById(Id);
            return obj;
        }

        public Operation Save(AnFAccountInfo obj)
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

        public Operation Update(AnFAccountInfo obj)
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

        public Operation Delete(AnFAccountInfo obj)
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
    }
}
