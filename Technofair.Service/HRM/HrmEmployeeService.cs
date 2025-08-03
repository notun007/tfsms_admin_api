using Technofair.Lib.Model;
using System;
using System.Collections.Generic;
using Technofair.Model.HRM;
using Technofair.Model.ViewModel.HRM;
using System.Data;
using Technofair.Lib.Utilities;
using System.Collections.ObjectModel;
using System.Linq;
using Technofair.Model.ViewModel.HRM.Reports;
using TFSMS.Admin.Data.Repository.HRM;
using TFSMS.Admin.Data.Infrastructure;
using Technofair.Data.Infrastructure.TFAdmin;


namespace TFSMS.Admin.Service.HRM
{
    public interface IHrmEmployeeService
    {
        Operation Save(HrmEmployee obj);
        Task<Operation> AddWithNoCommit(HrmEmployee obj);
        Operation Update(HrmEmployee obj);
        Operation UpdateWithNoCommit(HrmEmployee obj);
        Operation Delete(HrmEmployee obj);
        HrmEmployee GetById(int id);
        HrmEmployee GetByEmployeeId(string employeeId);
        IList<int> GetAllEmployeeIds(int companyId, int officeId);
        List<HrmEmployeeViewModel> GetEmployeeByAnyKey(Int16? cmnCompanyTypeId, int? companyId, Int16 userLevel);
        List<HrmEmployeeViewModel> GetEmployeeByCompanyId(int? companyId, Int16 userLevel);
        HrmEmployeeViewModel GetEmployee(String employeeID);
        HrmEmployeeViewModel GetByCardNo(String cardNo);
        HrmEmployeeViewModel GetEmployeeById(int employeeId);
        //List<HrmEmployee> GetByCompanyId(int companyId);
        List<HrmEmployee> GetByOfficeId(int officeId);
        List<HrmEmployeeViewModel> GetEmployeeByParameterWise(SearchViewModel obj);
        List<HrmEmployeeProfile> GetEmployeeProfileByEmployeeId(string employeeId);
        List<HrmEmployeeSummary> GetEmployeeSummary(SearchViewModel obj);
        List<HrmEmployee> GetAll();
        List<HrmEmployeeViewModel> GetEqualOrToManagement(int employeeId);
        List<HrmEmployee> GetEmployeeByLineManagerId(int employeeId);
        List<HrmOverview> GetHROverviewForDashboard();
        List<HrmEmployeeViewModel> GetEmployeeByCompanyId(int companyId);
        HrmEmployee GetByEmailID(string emailID);
    }
    public class HrmEmployeeService : IHrmEmployeeService
    {
        private IHrmEmployeeRepository repository;
        private IAdminUnitOfWork _UnitOfWork;
        public HrmEmployeeService(IHrmEmployeeRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }
        public List<HrmEmployee> GetAll()
        {
            try
            {
                return repository.GetAll().OrderBy(o => o.EmployeeId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        public Operation Save(HrmEmployee obj)
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

        public async Task<Operation> AddWithNoCommit(HrmEmployee obj)
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

        public Operation Update(HrmEmployee obj)
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

        public Operation UpdateWithNoCommit(HrmEmployee obj)
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
        public Operation Delete(HrmEmployee obj)
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
        public HrmEmployee GetById(int id)
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
        public HrmEmployee GetByEmployeeId(string EmployeeId)
        {
            try
            {
                return repository.GetMany(w => w.EmployeeId == EmployeeId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<HrmEmployee> GetByOfficeId(int officeId)
        {
            return repository.GetMany(e => e.HrmOfficeId == officeId).OrderBy(o=>o.EmployeeId).ToList();
        }
        //public List<HrmEmployee> GetByCompanyId(int companyId)
        //{
        //    try
        //    {
        //        return repository.GetMany(e => e.CmnCompanyId == companyId).OrderBy(o => o.EmployeeId).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public List<HrmEmployee> GetEmployeeByLineManagerId(int employeeId)
        {
            try
            {
                return repository.GetMany(e => e.HrmEmployeeId == employeeId).OrderBy(o => o.EmployeeId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HrmEmployee GetByEmailID(string emailID)
        {
            try
            {
                return repository.GetMany(e => e.OfficialEmail == emailID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public HrmEmployeeViewModel GetEmployee(string employeeID)
        {
            return repository.GetEmployee(employeeID);
        }
        
        public List<HrmEmployeeViewModel> GetEmployeeByAnyKey(Int16? cmnCompanyTypeId, int? companyId, Int16 userLevel)
        {
            try
            {
                List<HrmEmployeeViewModel> list = null;
                DataTable dt = repository.GetEmployeeByAnyKey(cmnCompanyTypeId, companyId, userLevel);
                if (dt != null && dt.Rows.Count > 0)
                {
                    list = new List<HrmEmployeeViewModel>();
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add((HrmEmployeeViewModel)Helper.FillTo(row, typeof(HrmEmployeeViewModel)));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<HrmEmployeeViewModel> GetEmployeeByCompanyId(int? companyId, Int16 userLevel)
        {
            try
            {
                List<HrmEmployeeViewModel> list = null;
                DataTable dt = repository.GetEmployeeByCompanyId(companyId, userLevel);
                if (dt != null && dt.Rows.Count > 0)
                {
                    list = new List<HrmEmployeeViewModel>();
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add((HrmEmployeeViewModel)Helper.FillTo(row, typeof(HrmEmployeeViewModel)));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public HrmEmployeeViewModel GetByCardNo(String cardNo)
        {
            return repository.GetByCardNo(cardNo);
        }
        public HrmEmployeeViewModel GetEmployeeById(int employeeId)
        {
            return repository.GetEmployeeById(employeeId);
        }

        public List<HrmEmployeeViewModel> GetEmployeeByCompanyId(int companyId)
        {
            try
            {
                List<HrmEmployeeViewModel> list = null;
                DataTable dt = repository.GetEmployeeByCompanyId(companyId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    list = new List<HrmEmployeeViewModel>();
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add((HrmEmployeeViewModel)Helper.FillTo(row, typeof(HrmEmployeeViewModel)));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<HrmEmployeeViewModel> GetEmployeeByParameterWise(SearchViewModel obj)
        {
            try
            {
                List<HrmEmployeeViewModel> list = null;
                DataTable dt = repository.GetEmployeeByParameterWise(obj);
                if (dt != null && dt.Rows.Count > 0)
                {
                    list = new List<HrmEmployeeViewModel>();
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add((HrmEmployeeViewModel)Helper.FillTo(row, typeof(HrmEmployeeViewModel)));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IList<int> GetAllEmployeeIds(int companyId, int officeId)
        {
            try
            {
                IList<int> Ids = new List<int>();
                IEnumerable<HrmEmployee> list = repository.GetMany(e => e.CmnCompanyId == companyId && e.HrmOfficeId == officeId);
                if (list != null)
                {
                    Ids = (from HrmEmployee obj in list select obj.Id).ToList();
                }
                return Ids;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HrmEmployeeProfile> GetEmployeeProfileByEmployeeId(string employeeId)
        {
            try
            {
                List<HrmEmployeeProfile> employees = null;
                DataTable dt = repository.GetEmployeeProfileByEmployeeId(employeeId);
                if (dt != null)
                {
                    employees = new List<HrmEmployeeProfile>();
                    foreach (DataRow row in dt.Rows)
                    {
                        employees.Add((HrmEmployeeProfile)Helper.FillTo(row, typeof(HrmEmployeeProfile)));
                    }
                }
                return employees;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HrmEmployeeSummary> GetEmployeeSummary(SearchViewModel obj)
        {
            try
            {
                List<HrmEmployeeSummary> list = null;
                DataTable dt = repository.GetHrmEmployeeSummary(obj);
                if (dt != null && dt.Rows.Count > 0)
                {
                    list = new List<HrmEmployeeSummary>();
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add((HrmEmployeeSummary)Helper.FillTo(row, typeof(HrmEmployeeSummary)));
                    } 
                    list.OrderBy(o => o.Priority).ThenBy(o => o.EmployeeId);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HrmEmployeeViewModel> GetEqualOrToManagement(int employeeId)
        {
            try
            {
                return repository.GetEqualOrToManagement(employeeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HrmOverview> GetHROverviewForDashboard()
        {
            try
            {
                List<HrmOverview> list = null;
                DataTable dt = repository.GetHROverviewForDashboard();
                if (dt != null && dt.Rows.Count > 0)
                {
                    list = new List<HrmOverview>();
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add((HrmOverview)Helper.FillTo(row, typeof(HrmOverview)));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
