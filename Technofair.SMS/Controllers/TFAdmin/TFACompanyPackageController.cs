using Microsoft.AspNetCore.Mvc;
using TFSMS.Admin.Model.TFAdmin;
using Technofair.Lib.Model;

using TFSMS.Admin.Model.ViewModel.TFAdmin;
using Microsoft.AspNetCore.Authorization;
using TFSMS.Admin.Service.TFAdmin;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using TFSMS.Admin.Data.Repository.TFAdmin;

namespace TFSMS.Admin.Controllers.TFAdmin
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class TFACompanyPackageController : ControllerBase
    {
        private ITFACompanyPackageService service;
        public TFACompanyPackageController()
        {
            var dbfactory = new AdminDatabaseFactory();
            service = new TFACompanyPackageService(new TFACompanyPackageRepository(dbfactory), new AdminUnitOfWork(dbfactory));
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetAll")]
        public async Task<List<TFACompanyPackage>> GetAll()
        {
            List<TFACompanyPackage> list = service.GetAll();
            return list;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetCurrentPackage")]
        public async Task<List<TFACompanyPackageViewModel>> GetCurrentPackage()
        {
            List<TFACompanyPackageViewModel> list = service.GetCurrentPackage();
            return list;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetAllCompanyPackage")]
        public List<TFACompanyPackageViewModel> GetAllCompanyPackage()
        {
            List<TFACompanyPackageViewModel> list = service.GetAllCompanyPackage();
            return list;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetCompanyPackageByPackageType")]
        public List<TFACompanyPackageViewModel> GetCompanyPackageByPackageType(int anFCompanyPackageTypeId)
        {
            List<TFACompanyPackageViewModel> list = service.GetCompanyPackageByPackageType(anFCompanyPackageTypeId);
            return list;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("Save")]
        public async Task<Operation> Save([FromBody] TFACompanyPackageViewModel obj)
        {
            Operation objOperation = new Operation();
            TFACompanyPackage?  objExist = new TFACompanyPackage();
            TFACompanyPackage objTFACompanyPackage = new TFACompanyPackage();
             if(obj.MinSubscriber >= obj.MaxSubscriber)
            {
                objOperation.Success = false;
                objOperation.Message = "Minimum subscriber can not getter then maximum subscriber ! Please check your data . ";
                return objOperation;
            }
            objExist = service.GetById(obj.Id);
            
            if (objExist == null)//&& ButtonPermission.Add
            {

                var objCompanyPackageDuplicate = service.GetTFACompanyPackageByPackageTypeIdMinMaxSub(obj.TFACompanyPackageTypeId, obj.MinSubscriber, obj.MaxSubscriber);
                if (objCompanyPackageDuplicate != null)
                {
                    objOperation.Success = false;
                    objOperation.Message = "This package is exist ! Please check your data . ";
                    return objOperation;
                }
                objTFACompanyPackage.Id = obj.Id;
                objTFACompanyPackage.TFACompanyPackageTypeId = obj.TFACompanyPackageTypeId;
                objTFACompanyPackage.MinSubscriber = obj.MinSubscriber;
                objTFACompanyPackage.MaxSubscriber = obj.MaxSubscriber;
                objTFACompanyPackage.Rate = obj.Rate;
                objTFACompanyPackage.Price = obj.Price;
                objTFACompanyPackage.Remarks = obj.Remarks;
                objTFACompanyPackage.IsActive = obj.IsActive;
                objTFACompanyPackage.CreatedBy = obj.CreatedBy;
                objTFACompanyPackage.CreatedDate = DateTime.Now;
                objOperation = service.Save(objTFACompanyPackage);
                objOperation.Message = "Company Package Created Successfully";
            }

            if(objExist != null) 
            {
                var objDuplicate = service.GetCompanyPackageExceptItSelf(obj);

                if (objDuplicate.Count() > 0)
                {
                    objOperation.Success = false;
                    objOperation.Message = "Company package already exist in other record";
                    return objOperation;
                }

                objExist.TFACompanyPackageTypeId = obj.TFACompanyPackageTypeId;
                objExist.MinSubscriber = obj.MinSubscriber;
                objExist.MaxSubscriber = obj.MaxSubscriber;
                objExist.Rate = obj.Rate;
                objExist.Price = obj.Price;
                objExist.Remarks = obj.Remarks;
                objExist.IsActive = obj.IsActive;
                objExist.ModifiedBy = obj.ModifiedBy;
                objExist.ModifiedDate = DateTime.Now;
                objOperation = service.Update(objExist);
                objOperation.Message = "Company Package Updated Successfully";
            }
            return objOperation;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost(("Delete/{id:int}"))]
        public async Task<Operation> Delete(int Id)
        {
            Operation objOperation = new Operation() { Success = false };
            if (Id > 0)//&& ButtonPermission.Delete
            {
                TFACompanyPackage obj = service.GetById(Id);
                if (obj != null)
                {
                    objOperation = service.Delete(obj);
                }
            }
            return objOperation;
        }
    }
}
