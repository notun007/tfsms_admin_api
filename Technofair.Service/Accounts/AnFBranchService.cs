using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Data.Repository.Accounts;
using Technofair.Lib.Model;
using Technofair.Model.Accounts;

namespace Technofair.Service.Accounts
{
    public interface IAnFBranchService
    {
        Operation Save(AnFBranch obj);
        Operation Update(AnFBranch obj);
        Operation Delete(AnFBranch obj);
        AnFBranch GetById(int Id);
        List<AnFBranch> GetAll();
    }
    public class AnFBranchService: IAnFBranchService
    {
        private IAnFBranchRepository repository;
        private IAdminUnitOfWork _UnitOfWork;


        public AnFBranchService(IAnFBranchRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        public List<AnFBranch> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public AnFBranch GetById(int Id)
        {
            AnFBranch obj = repository.GetById(Id);
            return obj;
        }

        public Operation Save(AnFBranch obj)
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

        public Operation Update(AnFBranch obj)
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

        public Operation Delete(AnFBranch obj)
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
