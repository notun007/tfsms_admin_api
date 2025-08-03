using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Technofair.Data.Infrastructure;
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Data.Repository.Common;
using Technofair.Data.Repository.TFLoan.Device;
using Technofair.Lib.Model;
using TFSMS.Admin.Model.TFLoan.Device;


using Technofair.Service.TFLoan.Device;


namespace TFSMS.Admin.Controllers.TFLoan.Device
{
    [Route("api/[controller]")]
    [ApiController]
    public class LnLoanModelController : ControllerBase
    {
        private ILnLoanModelService service;
        private IWebHostEnvironment _hostingEnvironment;      
        public LnLoanModelController()
        {
            //New:28072025
            var dbfactory = new AdminDatabaseFactory();
            service = new LnLoanModelService(new LnLoanModelRepository(dbfactory), new AdminUnitOfWork(dbfactory));

            //var dbfactory = new DatabaseFactory();
            //service = new LnLoanModelService(new LnLoanModelRepository(dbfactory), new UnitOfWork(dbfactory));           
        }

        [HttpPost("GetAll")]
        public List<LnLoanModel> GetAll()
        {
            List<LnLoanModel> list = service.GetAll();
            return list;
        }

        public async Task<List<LnLoanModel>> GetActiveLoanModel()
        {
            return await service.GetActiveLoanModel();
        }

        [HttpPost("SaveLoanModel")]
        public async Task<Operation> Save([FromBody] LnLoanModel obj)
        {
            Operation objOperation = new Operation();
            LnLoanModel objLnLoanModel = new LnLoanModel();

            var objExit = service.GetById(obj.Id);

            if (objExit == null)
            {
                objLnLoanModel.Id = obj.Id;
                objLnLoanModel.Name = obj.Name;
                objLnLoanModel.CreatedBy = obj.CreatedBy;
                objLnLoanModel.CreatedDate = DateTime.Now;
                objOperation = await service.Save(objLnLoanModel);
                objOperation.Message = "Loan Model Created Successfully.";
            }
            if (objExit != null)
            {
                objExit.Name = obj.Name;                
                objExit.ModifiedBy = obj.ModifiedBy;
                objExit.ModifiedDate = DateTime.Now;
                objOperation = service.Update(objExit);
                objOperation.Message = "Loan Model Updated Successfully.";
            }
            return objOperation;
        }
    }
}
