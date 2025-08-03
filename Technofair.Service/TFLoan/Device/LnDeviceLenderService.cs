using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Data.Infrastructure;
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Data.Repository.TFLoan.Device;

//using Technofair.Data.Repository.Loan.Device;
using Technofair.Lib.Model;
using TFSMS.Admin.Model.TFLoan.Device;
using TFSMS.Admin.Model.ViewModel.TFLoan;



namespace TFSMS.Admin.Service.TFLoan.Device
{
    public interface ILnDeviceLenderService
    {
        Task<Operation> Save(LnDeviceLender obj);
        Operation Update(LnDeviceLender obj);
        Task<Operation> AddWithNoCommitAsync(LnDeviceLender obj);
        List<LnDeviceLender> GetAll();
        LnDeviceLender GetById(Int16 id);
        Task<List<LnDeviceLender>> GetLenderByCompanyTypeId(Int16 companyTypeId);
        //Task<List<LnDeviceLender>> GetLenderByLenderTypeId(Int16 lenderTypeId);
        //Task<LnDeviceLender> GetLenderByLenderTypeId(Int16 lnDeviceLender, Int16 lenderTypeId);
        List<LnDeviceLenderViewModel> GetDeviceParentLender();
        List<LnDeviceLenderViewModel> GetLender();
    }
    public class LnDeviceLenderService : ILnDeviceLenderService
    {
        private ILnDeviceLenderRepository repository;
        private IAdminUnitOfWork _UnitOfWork;
        public LnDeviceLenderService(ILnDeviceLenderRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        public async Task<Operation> Save(LnDeviceLender obj)
        {
            Operation objOperation = new Operation { Success = false };

            try
            {
                objOperation.OperationId = await repository.AddEntityAsync(obj);
                objOperation.Success = await _UnitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }

        public Operation Update(LnDeviceLender obj)
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

        public async Task<Operation> AddWithNoCommitAsync(LnDeviceLender obj)
        {
            Operation objOperation = new Operation { Success = false };
            try
            {
                objOperation.OperationId = await repository.AddEntityAsync(obj);
                objOperation.Success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }

        public List<LnDeviceLender> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public LnDeviceLender GetById(Int16 id)
        {
            try
            {
                return repository.GetById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<LnDeviceLender>> GetLenderByCompanyTypeId(Int16 companyTypeId)
        {
            return await repository.GetLenderByCompanyTypeId(companyTypeId);
        }
        //public async Task<List<LnDeviceLender>> GetLenderByLenderTypeId(Int16 lenderTypeId)
        //{
        //    return await repository.GetLenderByLenderTypeId(lenderTypeId);
        //}
        //public async Task<LnDeviceLender> GetLenderByLenderTypeId(Int16 lnDeviceLender, Int16 lenderTypeId)
        //{
        //    return await repository.GetLenderByLenderTypeId(lenderTypeId, lnDeviceLender);
        //}
        public List<LnDeviceLenderViewModel> GetDeviceParentLender()
        {
            return repository.GetDeviceParentLender().ToList();
        }
        public List<LnDeviceLenderViewModel> GetLender()
        {
            return repository.GetLender().ToList();
        }
    }
}
