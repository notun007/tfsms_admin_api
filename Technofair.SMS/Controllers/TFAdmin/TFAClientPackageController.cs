using Microsoft.AspNetCore.Mvc;
using Technofair.Lib.Model;
using TFSMS.Admin.Model.TFAdmin;
using TFSMS.Admin.Model.ViewModel.TFAdmin;
using Microsoft.AspNetCore.Authorization;
using TFSMS.Admin.Service.TFAdmin;
using TFSMS.Admin.Data.Repository.TFAdmin;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Controllers.TFAdmin
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class TFAClientPackageController : ControllerBase
    {
        private ITFAClientPackageService service;
        private ITFAClientPackageLogService serviceLog;
        private ITFAClientPackageRepository repository;
        public TFAClientPackageController()
        {
            var dbfactory = new AdminDatabaseFactory();
            repository = new TFAClientPackageRepository(dbfactory);
            service = new TFAClientPackageService(new TFAClientPackageRepository(dbfactory), new AdminUnitOfWork(dbfactory));
            serviceLog = new TFAClientPackageLogService(new TFAClientPackageLogRepository(dbfactory), new AdminUnitOfWork(dbfactory));
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetActivePackageByClientId")]
        public async Task<List<TFAClientPackage>> GetActivePackageByClientId(int clientId)
        {
            List<TFAClientPackage> list = service.GetActivePackageByClientId(clientId);
            return list;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetAllClientPackage")]
        public List<TFAClientPackageViewModel> GetAllClientPackage()
        {
            List<TFAClientPackageViewModel> list = service.GetAllClientPackage();
            return list;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("Save")]
        public async Task<Operation> Save([FromBody] TFAClientPackageViewModel obj)
        {


            Operation objOperation = new Operation { Success = false };
            TFAClientPackage objExist = new TFAClientPackage();
            TFAClientPackage objTFAClientPackage = new TFAClientPackage();

            using (var dbContextTransaction = repository.BeginTransaction())
            {
                try
                {
                    objExist = service.GetById(obj.Id);

                    if (objExist == null)
                    {
                        var objDupClientPackage = service.GetClientPackageByCustomerId(obj.TFACompanyCustomerId);
                        if (objDupClientPackage != null)
                        {
                            objOperation.Success = false;
                            objOperation.Message = "You have a package .You canot create again .";
                            return objOperation;
                        }

                        objTFAClientPackage.Id = obj.Id;
                        objTFAClientPackage.TFACompanyCustomerId = obj.TFACompanyCustomerId;
                        objTFAClientPackage.TFACompanyPackageTypeId = obj.TFACompanyPackageTypeId;
                        objTFAClientPackage.TFACompanyPackageId = obj.TFACompanyPackageId;
                        objTFAClientPackage.Date = DateTime.Now;
                        objTFAClientPackage.Discount = obj.Discount;
                        objTFAClientPackage.IsActive = obj.IsActive;
                        //objTFAClientPackage.IsFixed = obj.IsFixed;
                        objTFAClientPackage.Amount = obj.Amount;
                        objTFAClientPackage.Rate = obj.Rate;
                        objTFAClientPackage.CreatedBy = obj.CreatedBy;
                        objTFAClientPackage.CreatedDate = DateTime.Now;
                        objOperation = await service.AddWithNoCommit(objTFAClientPackage);
                        objOperation.Success = true;
                        objOperation.Message = "Package save successfully";

                    }


                    if (objExist != null)
                    {
                        var objDuplicate = service.GetCompanyPackageExceptItSelf(obj);

                        if (objDuplicate.Count() > 0)
                        {
                            objOperation.Success = false;
                            objOperation.Message = "Client package already exist in other record";
                            return objOperation;
                        }

                        objExist.Id = obj.Id;
                        objExist.TFACompanyCustomerId = obj.TFACompanyCustomerId;
                        objExist.TFACompanyPackageTypeId = obj.TFACompanyPackageTypeId;
                        objExist.TFACompanyPackageId = obj.TFACompanyPackageId;
                        // objExist.Date = obj.Date;
                        objExist.Discount = obj.Discount;
                        objExist.IsActive = obj.IsActive;
                        //objExist.IsFixed = obj.IsFixed;
                        objExist.Amount = obj.Amount;
                        objExist.Rate = obj.Rate;
                        objExist.ModifiedBy = obj.CreatedBy;
                        objExist.ModifiedDate = DateTime.Now;
                        objOperation = service.UpdateWithNoCommit(objExist);
                        objOperation.Success = true;
                        objOperation.Message = "Package Update successfully";
                       
                    }

                    //Log
                    if (objOperation.Success)
                    {
                        TFAClientPackageLog objTFAClientPackageLog = new TFAClientPackageLog();
                        objTFAClientPackageLog.Id = 0;
                        objTFAClientPackageLog.TFACompanyCustomerId = obj.TFACompanyCustomerId;
                        objTFAClientPackageLog.TFACompanyPackageTypeId = obj.TFACompanyPackageTypeId;
                        objTFAClientPackageLog.TFACompanyPackageId = obj.TFACompanyPackageId;
                        objTFAClientPackageLog.Date = DateTime.Now;
                        objTFAClientPackageLog.Discount = obj.Discount;
                        objTFAClientPackageLog.IsActive = obj.IsActive;
                        objTFAClientPackageLog.Amount = obj.Amount;
                        objTFAClientPackageLog.Rate = obj.Rate;
                        objTFAClientPackageLog.CreatedBy = obj.CreatedBy;
                        objTFAClientPackageLog.CreatedDate = DateTime.Now;
                        objOperation = await serviceLog.AddWithNoCommit(objTFAClientPackageLog);
                    }
                    //End


                    repository.SaveChanges();
                    repository.Commit(dbContextTransaction);
                    objOperation.Success = true;
                    objOperation.Message = "Saved Successfully.";
                }
                catch (Exception ex)
                {
                    repository.Rollback(dbContextTransaction);
                    objOperation.Success = false;
                    objOperation.Message = "Operation Rolled Back By SMS";
                }

            }

            return objOperation;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetAll")]
        public async Task<List<TFAClientPackage>> GetAll()
        {
            List<TFAClientPackage> list = service.GetAll();
            return list;
        }
    }

}
