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

namespace TFSMS.Admin.Service.TFLoan.Device
{
    public interface IAnFPaymentChannelService
    {
        Operation Save(AnFPaymentChannel obj);
        Operation Delete(AnFPaymentChannel obj);
        AnFPaymentChannel GetById(Int16 Id);
        Operation Update(AnFPaymentChannel obj);
        List<AnFPaymentChannel> GetAll();
    }
    public class AnFPaymentChannelService : IAnFPaymentChannelService
    {
        private IAnFPaymentChannelRepository repository;
        private IAdminUnitOfWork _UnitOfWork;
        public AnFPaymentChannelService(IAnFPaymentChannelRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }
        public List<AnFPaymentChannel> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public Operation Save(AnFPaymentChannel obj)
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

        public Operation Update(AnFPaymentChannel obj)
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

        public Operation Delete(AnFPaymentChannel obj)
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

        public AnFPaymentChannel GetById(Int16 Id)
        {
            AnFPaymentChannel obj = repository.GetById(Id);
            return obj;
        }

    }
}
