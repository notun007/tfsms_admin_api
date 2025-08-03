
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Lib.Model;

using Technofair.Model.TFAdmin;
using Technofair.Service.TFLoan.Device;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using TFSMS.Admin.Data.Repository.Accounts;
using TFSMS.Admin.Data.Repository.TFAdmin;



namespace TFSMS.Admin.Service.TFAdmin
{
    public interface ITFAPaymentRequestProcessService
    {
        Task<Operation> Save(TFAPaymentRequestProcess obj);
        Task<Operation> AddWithNoCommit(TFAPaymentRequestProcess obj);
        Operation Delete(TFAPaymentRequestProcess obj);
        Task<Operation> Update(TFAPaymentRequestProcess obj);
        Operation UpdateWithNoCommit(TFAPaymentRequestProcess obj);

        TFAPaymentRequestProcess GetById(Int64 Id);
        string GetProcessIDByPaymentMethodId(Int16 paymentMethodId, string partnerCode);
        string GetProcessID(int deviceNumberId);
        TFAPaymentRequestProcess GetByProcessID(string processID);
    }
    public class TFAPaymentRequestProcessService : ITFAPaymentRequestProcessService
    {
        private ITFAPaymentRequestProcessRepository repository;
        private IAdminUnitOfWork _UnitOfWork;
        private IAnFPaymentMethodService servicePayment;
        public TFAPaymentRequestProcessService(ITFAPaymentRequestProcessRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
            AdminDatabaseFactory dbfactory = new AdminDatabaseFactory();

            servicePayment = new AnFPaymentMethodService(new AnFPaymentMethodRepository(dbfactory), new AdminUnitOfWork(dbfactory));
        }

        public string GetProcessIDByPaymentMethodId(Int16 paymentMethodId, string partnerCode)
        {
            string processID = "";
            try
            {
                Technofair.Model.Accounts.AnFPaymentMethod objMethod = servicePayment.GetById(paymentMethodId);
                if (objMethod != null)
                {
                    TFAPaymentRequestProcess? obj = repository.GetMany(p => p.AnFPaymentMethodId == paymentMethodId).OrderByDescending(o => o.Id).FirstOrDefault();
                    Int64 serial = 1;
                    processID = partnerCode.Trim().ToUpper() + DateTime.Now.ToString("yyMMdd");
                    if (obj != null)
                    {
                        if (obj.ProcessID != null && obj.ProcessID != "")
                        {
                            string sub = obj.ProcessID.Substring(processID.Length);
                            serial = Convert.ToInt64(sub) + 1;
                        }
                    }
                    processID = processID + serial;
                }
                return processID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TFAPaymentRequestProcess GetById(Int64 Id)
        {
            return repository.GetById(Id);
        }

        public TFAPaymentRequestProcess GetByProcessID(string processID)
        {
            return repository.GetMany(w => w.ProcessID == processID.Trim()).FirstOrDefault();
        }

        public async Task<Operation> Save(TFAPaymentRequestProcess obj)
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
        public async Task<Operation> AddWithNoCommit(TFAPaymentRequestProcess obj)
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
        public async Task<Operation> Update(TFAPaymentRequestProcess obj)
        {
            Operation objOperation = new Operation { Success = false, OperationId = obj.Id };
            try
            {
                repository.UpdateAsync(obj);
                objOperation.Success = await _UnitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }

        public Operation UpdateWithNoCommit(TFAPaymentRequestProcess obj)
        {
            Operation objOperation = new Operation { Success = false, OperationId = obj.Id };
            try
            {
                repository.UpdateEntity(obj);
                objOperation.Success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }
        public Operation Delete(TFAPaymentRequestProcess obj)
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

        public string GetProcessID(int deviceNumberId)
        {
            try
            {
                return repository.GetProcessID(deviceNumberId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}
