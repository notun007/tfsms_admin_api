using Technofair.Data.Repository.HRM;
using Technofair.Data.Infrastructure;
using Technofair.Lib.Model;
using Technofair.Model.HRM;
using Technofair.Service.HRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Technofair.Model.Common;
using Technofair.Data.Infrastructure.TFAdmin;


namespace Technofair.SMS.Controllers.HRM
{
    [Route("HRM/[controller]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        //
        // GET: /HRM/Designation/
        private IHrmDesignationService service;
        public DesignationController()
        {
            var dbfactory = new AdminDatabaseFactory();
            service = new HrmDesignationService(new HrmDesignationRepository(dbfactory), new AdminUnitOfWork(dbfactory));
        }


        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetAll")]
        public async Task<List<HrmDesignation>> GetAll()
        {
            List<HrmDesignation> list = service.GetAll();
            return list;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("Save")]

        //new
        public async Task<Operation> Save([FromBody] HrmDesignation obj)
        {
            Operation objOperation = new Operation();
            HrmDesignation objHrmDesignation = new HrmDesignation();

            try
            {

                objHrmDesignation.Id = obj.Id;
                objHrmDesignation.Name = obj.Name;
                objHrmDesignation.ShortName = obj.ShortName;
                objHrmDesignation.HrmDesignationId = obj.HrmDesignationId;
                objHrmDesignation.Priority = obj.Priority;                
                objHrmDesignation.IsActive = obj.IsActive;

                var objExit = service.GetById(obj.Id);

                if (objExit == null)
                {
                    var objDesignationDuplicate = service.GetDesignationByNameAndShortName(obj.Name, obj.ShortName);
                    if (objDesignationDuplicate != null)
                    {
                        objOperation.Success = false;
                        objOperation.Message = "This Designation Already exist.";
                        return objOperation;
                    }

                    objHrmDesignation.CreatedBy = obj.CreatedBy;
                    objHrmDesignation.CreatedDate = DateTime.Now;
                    objOperation = await service.Save(objHrmDesignation);
                    objOperation.Message = "Designation Created Successfully.";
                }
                if (objExit != null)
                {
                    var objDesignationDuplicate = service.GetDesignationByNameAndShortName(obj.Name, obj.ShortName);
                    if (objDesignationDuplicate != null && objDesignationDuplicate.Id != obj.Id)
                    {
                        objOperation.Success = false;
                        objOperation.Message = "This Designation Already exist.";
                        return objOperation;
                    }
                    objExit.Name = obj.Name;
                    objExit.ShortName = obj.ShortName;
                    objExit.HrmDesignationId = obj.HrmDesignationId;
                    objExit.Priority = obj.Priority;
                    objExit.IsActive = obj.IsActive;
                    objExit.ModifiedBy = obj.ModifiedBy;
                    objExit.ModifiedDate = DateTime.Now;
                    objOperation = service.Update(objExit);
                    objOperation.Message = "Designation Updated Successfully.";
                }
            }
            catch (Exception exp)
            {
                objOperation.Success = false;
                objOperation.Message = "Unable to save Designation";
            }
            return objOperation;
        }
        //old
        //public async Task<Operation> Save([FromBody] HrmDesignation obj)
        //{
        //    Operation objOperation = new Operation();
        //    if (obj.Id == 0)
        //    {
        //        var objDesignationDuplicate = service.GetDesignationByNameAndShortName(obj.Name, obj.ShortName);
        //        if (objDesignationDuplicate != null)
        //        {
        //            objOperation.Success = false;
        //            objOperation.Message = "This Designation Already exist.";
        //            return objOperation;
        //        }
        //        //obj.CmnCompanyId = companyId;
        //        //obj.CreatedBy = userId;
        //        obj.CreatedDate = DateTime.Now;
        //        objOperation = service.Save(obj);
        //        objOperation.Message = "successfully Created Designation.";
        //    }
        //    else if (obj.Id > 0)
        //    {
        //        //var objDesignationDuplicate = service.GetDesignationByNameAndShortName(obj.Name, obj.ShortName);
        //        //if (objDesignationDuplicate != null && objDesignationDuplicate.Id != obj.Id)
        //        //{
        //        //    objOperation.Success = false;
        //        //    objOperation.Message = "This Designation Already exist.";
        //        //    return objOperation;
        //        //}
        //        //obj.ModifiedBy = userId;
        //        obj.ModifiedDate = DateTime.Now;
        //        objOperation = service.Update(obj);
        //        objOperation.Message = "successfully Update Designation.";
        //    }
        //    return objOperation;
        //}

        [Authorize(Policy = "Authenticated")]
        [HttpPost(("Delete/{id:int}"))]
        public async Task<Operation> Delete(int id)
        {
            Operation objOperation = new Operation { Success = false };
            if (id > 0)
            {
                HrmDesignation obj = service.GetById(id);
                if (obj != null)
                {
                    objOperation = service.Delete(obj);
                }
            }
            return objOperation;
        }
    }
}
