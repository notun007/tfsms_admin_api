using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Data.Infrastructure;
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Data.Repository.TFLoan.Device;
using Technofair.Lib.Model;
using Technofair.Lib.Utilities;
using TFSMS.Admin.Model.TFLoan.Device;
using TFSMS.Admin.Model.ViewModel.Common;
using TFSMS.Admin.Model.ViewModel.TFLoan;
using TFSMS.Admin.Data.Repository.TFLoan.Device;



namespace TFSMS.Admin.Service.TFLoan.Device
{
    public interface ILnDeviceLoanDisbursementService
    {
        Task<Operation> Save(LnDeviceLoanDisbursement obj);
        Operation Update(LnDeviceLoanDisbursement obj);
        Operation AddWithNoCommit(LnDeviceLoanDisbursement obj);
        Task<Operation> AddWithNoCommitAsync(LnDeviceLoanDisbursement obj);
        List<LnDeviceLoanDisbursement> GetAll();
        LnDeviceLoanDisbursement GetById(Int64 id);
        Task<List<LnDeviceLoanDisbursementViewModel>> GetDeviceLoanDisbursement();
        string NextLoanNo();

    }
    public class LnDeviceLoanDisbursementService : ILnDeviceLoanDisbursementService
    {
        private ILnDeviceLoanDisbursementRepository repository;
        private IAdminUnitOfWork _UnitOfWork;
        public LnDeviceLoanDisbursementService(ILnDeviceLoanDisbursementRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        public async Task<Operation> Save(LnDeviceLoanDisbursement obj)
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

        public Operation Update(LnDeviceLoanDisbursement obj)
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

        public Operation AddWithNoCommit(LnDeviceLoanDisbursement obj)
        {
            Operation objOperation = new Operation { Success = false };
            try
            {
                objOperation.OperationId = repository.AddEntity(obj);
                objOperation.Success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }
        public async Task<Operation> AddWithNoCommitAsync(LnDeviceLoanDisbursement obj)
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

        public List<LnDeviceLoanDisbursement> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public LnDeviceLoanDisbursement GetById(Int64 id)
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
        public Task<List<LnDeviceLoanDisbursementViewModel>> GetDeviceLoanDisbursement()
        {
            return repository.GetDeviceLoanDisbursement();
        }

        public string NextLoanNo()
        {
            string nextSl = string.Empty;

            try
            {
                DataTable dt = repository.NextLoanNo();
                NextSLViewModel objNextSl = null;
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        objNextSl = (NextSLViewModel)Helper.FillTo(row, typeof(NextSLViewModel));
                    }
                }

                nextSl = objNextSl == null ? "" : objNextSl.NextSL;

            }
            catch (Exception ex)
            {
            }
            return nextSl;
        }
    }
}
