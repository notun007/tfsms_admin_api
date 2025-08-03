using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Lib.Model;
using Technofair.Model.TFAdmin;
using Technofair.Model.ViewModel.Common;
using Technofair.Model.ViewModel.TFAdmin;
using Technofair.Service.TFAdmin;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Repository.TFAdmin;

namespace TFSMS.Admin.Service.TFAdmin
{
   
    public interface ITFAClientBillService
    { 
         Operation GenerateClientBill(int billGenPermssionId, int? companyCustomerId, int createdBy);
         List<TFAClientInvoiceViewModel> GetClientInvoice(int? TFACompanyCustomerId);
        List<TFAClientInvoiceViewModel> GetClientApprovedUnpaidBill(int? companyCustomerId);
        List<TFAClientInvoiceViewModel> GetClientApprovedBill(int? companyCustomerId);
        

         Operation ApproveClientBill(int tfaClientPaymentDetailId, int approveBy);
    }

    public class TFAClientBillService : ITFAClientBillService
    {
        private ITFAClientBillRepository repository;
        private IAdminUnitOfWork _UnitOfWork;

        public TFAClientBillService(ITFAClientBillRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        public Operation GenerateClientBill(int billGenPermssionId, int? companyCustomerId, int createdBy)
        {
            return repository.GenerateClientBill(billGenPermssionId, companyCustomerId, createdBy);
        }
       public List<TFAClientInvoiceViewModel> GetClientInvoice(int? TFACompanyCustomerId)
        {
            return repository.GetClientInvoice(TFACompanyCustomerId);
        }

        public List<TFAClientInvoiceViewModel> GetClientApprovedUnpaidBill(int? companyCustomerId)
        {
            return repository.GetClientApprovedUnpaidBill(companyCustomerId);
        }
        public List<TFAClientInvoiceViewModel> GetClientApprovedBill(int? companyCustomerId)
        {
            return repository.GetClientApprovedBill(companyCustomerId);
        }
        
      public  Operation ApproveClientBill(int tfaClientPaymentDetailId, int approveBy)
        {
            return repository.ApproveClientBill(tfaClientPaymentDetailId, approveBy);
        }
    }
}
