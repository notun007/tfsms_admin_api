using Technofair.Data.Infrastructure;
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Data.Repository.Bank;
using Technofair.Lib.Model;

using Technofair.Model.Bank;
using Technofair.Model.ViewModel.Bank;
using TFSMS.Admin.Model.Common;


namespace Technofair.Service.Bank
{
    public interface IBnkBranchService
    {
        List<BnkBranch> Get();
        Operation Save(BnkBranch obj);
        Operation Delete(BnkBranch obj);
        BnkBranch GetById(int Id);
        List<BnkBranch> GetAll();
        Operation Update(BnkBranch obj);
        List<BnkBranchViewModel> GetBranchByBankId(int bankId);
    }

    public class BnkBranchService : IBnkBranchService
    {
        private IBnkBranchRepository repository;
        private IAdminUnitOfWork _UnitOfWork;


        public BnkBranchService(IBnkBranchRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        public List<BnkBranch> Get()
        {
            return repository.GetAll().ToList();
        }

        public BnkBranch GetById(int Id)
        {
            BnkBranch obj = repository.GetById(Id);
            return obj;
        }

        public List<BnkBranch> GetAll()
        {
            return repository.GetAll().ToList();
        }
        public Operation Update(BnkBranch obj)
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

        public Operation Delete(BnkBranch obj)
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

        public Operation Save(BnkBranch obj)
        {
            Operation objOperation = new Operation { Success = true };

            long Id = repository.AddEntity(obj);
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


        public List<BnkBranchViewModel> GetBranchByBankId(int bankId)
        {
            List<BnkBranchViewModel> list = new List<BnkBranchViewModel>();
            try
            {
                list = repository.GetBranchByBankId(bankId);

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


}






