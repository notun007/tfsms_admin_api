using Technofair.Lib.Model;
using System;
using System.Linq;
using System.Collections.Generic;
using TFSMS.Admin.Model.HRM;
using TFSMS.Admin.Model.Common;
using TFSMS.Admin.Data.Repository.HRM;
using TFSMS.Admin.Data.Infrastructure;
using Technofair.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Service.HRM
{

    #region Interface
    public interface IHrmDesignationService
    {
        //Operation Save(HrmDesignation obj);
        Task<Operation> Save(HrmDesignation obj);
        Operation Update(HrmDesignation obj);
        Operation Delete(HrmDesignation obj);
        //List<HrmDesignation> GetByCompanyId(int companyId);
        List<HrmDesignation> GetAll();
        HrmDesignation GetById(int Id);
        HrmDesignation? GetDesignationByNameAndShortName(string name, string shortName);


    }
    #endregion


    #region Member
    public class HrmDesignationService : IHrmDesignationService
    {
        private IHrmDesignationRepository repository;
        private IAdminUnitOfWork _UnitOfWork;
        public HrmDesignationService(IHrmDesignationRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        //public Operation Save(HrmDesignation obj)
        //{
        //    Operation objOperation = new Operation { Success = false };
        //    try
        //    {
        //        objOperation.OperationId = repository.AddEntity(obj);
        //        if (obj.Id != obj.HrmDesignationId)
        //        {
        //            _UnitOfWork.Commit();
        //            objOperation.Success = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return objOperation;
        //}

        public async Task<Operation> Save(HrmDesignation obj)
        {
            Operation objOperation = new Operation { Success = false };
            try
            {
                objOperation.OperationId = repository.AddEntity(obj);
                _UnitOfWork.Commit();
                objOperation.Success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }

        public Operation Update(HrmDesignation obj)
        {
            Operation objOperation = new Operation { Success = false, OperationId = obj.Id };
            try
            {
                if (obj.Id != obj.HrmDesignationId)
                {
                    repository.Update(obj);
                    _UnitOfWork.Commit();
                    objOperation.Success = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }
        public Operation Delete(HrmDesignation obj)
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

        //public List<HrmDesignation> GetByCompanyId(int companyId)
        //{
        //    return repository.GetMany(w => w.CmnCompanyId == companyId).ToList();
        //}


        public List<HrmDesignation> GetAll()
        {
            return repository.GetAll().ToList();
        }
        public HrmDesignation GetById(int Id)
        {
            HrmDesignation obj = repository.GetById(Id);
            return obj;
        }

        public HrmDesignation? GetDesignationByNameAndShortName(string name, string shortName)

        {
            return repository.GetDesignationByNameAndShortName(name, shortName);
        }

    }

    #endregion
}
