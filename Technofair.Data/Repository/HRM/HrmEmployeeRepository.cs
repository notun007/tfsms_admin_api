using Technofair.Lib.Utilities;
using Technofair.Model.HRM;
using Technofair.Model.ViewModel.HRM;
using Technofair.Model.ViewModel.HRM.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Technofair.Model.Common;
using Technofair.Model.Accounts;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;


namespace TFSMS.Admin.Data.Repository.HRM
{
    #region Interface
    public interface IHrmEmployeeRepository : IRepository<HrmEmployee>
    {
        int AddEntity(HrmEmployee obj);
        Task<int> AddEntityAsync(HrmEmployee obj);
        int UpdateEntity(HrmEmployee obj);
        
        DataTable GetEmployeeByAnyKey(Int16? cmnCompanyTypeId, int? companyId, Int16 userLevel);
        DataTable GetEmployeeByCompanyId(int? companyId, Int16 userLevel);
        HrmEmployeeViewModel GetEmployee(String employeeID);
        HrmEmployeeViewModel GetByCardNo(String cardNo);
        HrmEmployeeViewModel GetEmployeeById(int employeeId);
        DataTable GetEmployeeByParameterWise(SearchViewModel obj);
        DataTable GetEmployeeProfileByEmployeeId(string employeeId);
        DataTable GetHrmEmployeeSummary(SearchViewModel obj);
        List<HrmEmployeeViewModel> GetEqualOrToManagement(int employeeId);
        DataTable GetHROverviewForDashboard();
        DataTable GetEmployeeByCompanyId(int companyId);
    }
    #endregion
    public class HrmEmployeeRepository : AdminBaseRepository<HrmEmployee>, IHrmEmployeeRepository
    {
        public HrmEmployeeRepository(IAdminDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }


        public int AddEntity(HrmEmployee obj)
        {
            int Id = 1;
            HrmEmployee last = DataContext.HrmEmployees.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;
        }

        public async Task<int> AddEntityAsync(HrmEmployee obj)
        {
            int Id = 1;
            if (obj.Id == 0)
            {
                HrmEmployee? last = DataContext.HrmEmployees.OrderByDescending(x => x.Id).FirstOrDefault();
                if (last != null)
                {
                    Id = last.Id + 1;
                }
                obj.Id = Id;
            }
            else
            {
                Id = obj.Id;
            }
            await base.AddAsync(obj);
            return Id;
        }

        public int UpdateEntity(HrmEmployee obj)
        {
            DataContext.HrmEmployees.Update(obj);
            return obj.Id;
        }

        List<CmnCompany> childs = new List<CmnCompany>();
        public DataTable GetEmployeeByCompanyId(int companyId)
        {
            DataTable dt = null;
            string clients = "";
            childs = new List<CmnCompany>();
            CmnCompany? objParent = DataContext.CmnCompanies.Where(c => c.Id == companyId).FirstOrDefault();
            if (objParent != null)
            {
                //New
                CmnCompanyType? objType = DataContext.CmnCompanyTypes.Where(w => w.Id == objParent.CmnCompanyTypeId).FirstOrDefault();
                List<CmnCompany> list = new List<CmnCompany>();
                if (objType != null)
                {
                    if (objType.ShortName == "MSO")
                    {
                        list = DataContext.CmnCompanies.OrderBy(o => o.Id).ToList();
                    }
                    else if (objType.ShortName == "LSO")
                    {
                        list = DataContext.CmnCompanies.Where(c => c.CmnCompanyId == companyId).Distinct().OrderBy(o => o.Id).ToList();
                    }
                }
                
                //Old
                //List<CmnCompany> list = DataContext.CmnCompanies.Where(c => c.CmnCompanyId == companyId).Distinct().OrderBy(o => o.Id).ToList();

                if (list != null && list.Count > 0)
                {
                    list.Add(objParent);
                }
                else
                {
                    list = new List<CmnCompany>();
                    list.Add(objParent);
                }

                FindChild(companyId, list);
                childs.Add(objParent);
                if (childs != null && childs.Count > 0)
                {
                    childs = childs.Distinct().OrderBy(o => o.Id).ToList();
                    for (int i = 0; i < childs.Count; i++)
                    {
                        if (clients == "")
                        {
                            clients = childs[i].Id.ToString();
                        }
                        else
                        {
                            clients += "," + childs[i].Id;
                        }
                    }
                }

                dt = new DataTable();
                SqlParameter[] paramsToStore = new SqlParameter[1];
                paramsToStore[0] = new SqlParameter("@clientIds", clients);
                try
                {
                    dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.HrmEmployee.GetHrmEmployeeByCompanyId, paramsToStore).Tables[0];
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return dt;
        }

        public void FindChild(int parentId, List<CmnCompany> list)
        {
            CmnCompany com = new CmnCompany();
            for (int i = 0; i < list.Count; i++)
            {
                com = list[i];
                if (com.CmnCompanyId == parentId)
                {
                    childs.Add(com);
                    int innerParent = com.Id;
                    FindChild(innerParent, list);
                }
            }
        }


        //New
        
        public DataTable GetEmployeeByAnyKey(Int16? cmnCompanyTypeId, int? companyId, Int16 userLevel)
        {


            DataTable dt = new DataTable();


            SqlParameter[] paramsToStore = new SqlParameter[3];

            paramsToStore[0] = new SqlParameter("@cmnCompanyTypeId", cmnCompanyTypeId);
            paramsToStore[1] = new SqlParameter("@cmnCompanyId", companyId);
            paramsToStore[2] = new SqlParameter("@userLevel", userLevel);
            try
            {
                dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.HrmEmployee.GetEmployeeByAnyKey, paramsToStore).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }
        public DataTable GetEmployeeByCompanyId(int? companyId, Int16 userLevel)
        {
           
           
            DataTable dt = new DataTable();

                                                                          
                SqlParameter[] paramsToStore = new SqlParameter[2];
            
                paramsToStore[0] = new SqlParameter("@cmnCompanyId", companyId);
                paramsToStore[1] = new SqlParameter("@userLevel", userLevel);
                try
                {
                    dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.HrmEmployee.GetEmployeeByCompanyId, paramsToStore).Tables[0];
                }
                catch (Exception ex)
                {
                    throw ex;
                }
           
            return dt;
        }


        public HrmEmployeeViewModel GetEmployee(String employeeID)
        {
            HrmEmployeeViewModel? obj = (from em in DataContext.HrmEmployees
                                        join Des in DataContext.HrmDesignations on em.HrmDesignationId equals Des.Id
                                        //join Div in DataContext.HrmDivisions on em.HrmDivisionId equals Div.Id into tDiv
                                        //from tempDiv in tDiv.DefaultIfEmpty()
                                        //join Dep in DataContext.HrmDepartments on em.HrmDepartmentId equals Dep.Id into tDep
                                        //from tempDep in tDep.DefaultIfEmpty()
                                        //join Sec in DataContext.HrmSections on em.HrmSectionId equals Sec.Id into tSec
                                        //from tempSec in tSec.DefaultIfEmpty()
                                        //join Grd in DataContext.HrmGrades on em.HrmGradeId equals Grd.Id into tGrd
                                        //from tempGrd in tGrd.DefaultIfEmpty()
                                        where (em.EmployeeId == employeeID)
                                        select new HrmEmployeeViewModel()
                                        {
                                            Id = em.Id,
                                            CmnCompanyId = em.CmnCompanyId,
                                            EmployeeId = em.EmployeeId,
                                            Name = em.Name,
                                            DesignationName = Des.Name,
                                            HrmDesignationId = em.HrmDesignationId,
                                            //DivisionName = tempDiv.Name,
                                            //DepartmentName = tempDep.Name,
                                            //SectionName = tempSec.Name,
                                            //GradeName = tempGrd.Name,
                                            HrmGradeId = em.HrmGradeId,
                                            Basic = em.Basic,
                                            Gross = em.Gross,
                                            JoiningDate = em.JoiningDate,
                                            LineManager = em.LineManager,
                                            AttendanceCardNo = em.AttendanceCardNo

                                        }).FirstOrDefault();
            return obj;
        }
        public HrmEmployeeViewModel GetByCardNo(String cardNo)
        {
            HrmEmployeeViewModel obj = (from em in DataContext.HrmEmployees
                                        join Des in DataContext.HrmDesignations on em.HrmDesignationId equals Des.Id
                                        //join Div in DataContext.HrmDivisions on em.HrmDivisionId equals Div.Id into tDiv
                                        //from tempDiv in tDiv.DefaultIfEmpty()
                                        //join Dep in DataContext.HrmDepartments on em.HrmDepartmentId equals Dep.Id into tDep
                                        //from tempDep in tDep.DefaultIfEmpty()
                                        //join Sec in DataContext.HrmSections on em.HrmSectionId equals Sec.Id into tSec
                                        //from tempSec in tSec.DefaultIfEmpty()
                                        //join Grd in DataContext.HrmGrades on em.HrmGradeId equals Grd.Id into tGrd
                                        //from tempGrd in tGrd.DefaultIfEmpty()
                                        where (em.AttendanceCardNo == cardNo)
                                        select new HrmEmployeeViewModel()
                                        {
                                            Id = em.Id,
                                            CmnCompanyId = em.CmnCompanyId,
                                            EmployeeId = em.EmployeeId,
                                            Name = em.Name,
                                            HrmDesignationId = em.HrmDesignationId,
                                            DesignationName = Des.Name,
                                            //DivisionName = tempDiv.Name,
                                            //DepartmentName = tempDep.Name,
                                            //SectionName = tempSec.Name,
                                            //GradeName = tempGrd.Name,
                                            Basic = em.Basic,
                                            Gross = em.Gross,
                                            JoiningDate = em.JoiningDate,
                                            LineManager = em.LineManager,
                                            AttendanceCardNo = em.AttendanceCardNo
                                        }).FirstOrDefault();
            return obj;
        }

        public HrmEmployeeViewModel GetEmployeeById(int employeeId)
        {
            HrmEmployeeViewModel obj = (from em in DataContext.HrmEmployees
                                        join Des in DataContext.HrmDesignations on em.HrmDesignationId equals Des.Id
                                        //join Div in DataContext.HrmDivisions on em.HrmDivisionId equals Div.Id into tDiv
                                        //from tempDiv in tDiv.DefaultIfEmpty()
                                        //join Dep in DataContext.HrmDepartments on em.HrmDepartmentId equals Dep.Id into tDep
                                        //from tempDep in tDep.DefaultIfEmpty()
                                        //join Sec in DataContext.HrmSections on em.HrmSectionId equals Sec.Id into tSec
                                        //from tempSec in tSec.DefaultIfEmpty()
                                        //join Grd in DataContext.HrmGrades on em.HrmGradeId equals Grd.Id into tGrd
                                        //from tempGrd in tGrd.DefaultIfEmpty()
                                        where (em.Id == employeeId)
                                        select new HrmEmployeeViewModel()
                                        {
                                            Id = em.Id,
                                            CmnCompanyId = em.CmnCompanyId,
                                            EmployeeId = em.EmployeeId,
                                            Name = em.Name,
                                            HrmEmployeeId = em.HrmEmployeeId,
                                            HrmDesignationId = em.HrmDesignationId,
                                            DesignationName = Des.Name,
                                            Priority = Des.Priority,
                                            //DivisionName = tempDiv.Name,
                                            //DepartmentName = tempDep.Name,
                                            //SectionName = tempSec.Name,
                                            //GradeName = tempGrd.Name,
                                            Basic = em.Basic,
                                            Gross = em.Gross,
                                            JoiningDate = em.JoiningDate,
                                            LineManager = em.LineManager,
                                            AttendanceCardNo = em.AttendanceCardNo
                                        }).FirstOrDefault();
            return obj;
        }

        public DataTable GetEmployeeByParameterWise(SearchViewModel obj)
        {
            DataTable dt = null;
            try
            {
                dt = new DataTable();
                SqlParameter[] paramsToStore = new SqlParameter[13];
                paramsToStore[0] = new SqlParameter("@CmnCompanyId", obj.CmnCompanyId);
                paramsToStore[1] = new SqlParameter("@EmployeeID", obj.EmployeeID);
                paramsToStore[2] = new SqlParameter("@HrmDivisionId", obj.HrmDivisionId);
                paramsToStore[3] = new SqlParameter("@HrmDepartmentId", obj.HrmDepartmentId);
                paramsToStore[4] = new SqlParameter("@HrmSectionId", obj.HrmSectionId);
                paramsToStore[5] = new SqlParameter("@HrmOfficeId", obj.HrmOfficeId);
                paramsToStore[6] = new SqlParameter("@HrmDesignationId", obj.HrmDesignationId);
                paramsToStore[7] = new SqlParameter("@HrmGradeId", obj.HrmGradeId);
                paramsToStore[8] = new SqlParameter("@HrmEmployeeTypeId", obj.HrmEmployeeTypeId);
                paramsToStore[9] = new SqlParameter("@DateOfBirth", obj.DateOfBirth);
                paramsToStore[10] = new SqlParameter("@IsRoastaringDuty", obj.IsRoastaringDuty);
                paramsToStore[11] = new SqlParameter("@IsOTPayable", obj.IsOTPayable);
                paramsToStore[12] = new SqlParameter("@IsActive", obj.IsActive);
                dt = Helper.GetFromStoredProcedure(DataContext.Database.GetDbConnection(), SPList.HrmEmployee.GetHrmEmployeeByParameterWise, paramsToStore, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }



        public DataTable GetEmployeeProfileByEmployeeId(string employeeId)
        {
            DataTable dt = null;
            try
            {
                dt = new DataTable();
                SqlParameter[] paramsToStore = new SqlParameter[1];
                paramsToStore[0] = new SqlParameter("@employeeId", employeeId);
                dt = Helper.GetFromStoredProcedure(DataContext.Database.GetDbConnection(), SPList.HrmEmployee.RptHrmEmployeeCV, paramsToStore, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetHrmEmployeeSummary(SearchViewModel obj)
        {
            DataTable dt = null;
            try
            {
                dt = new DataTable();
                SqlParameter[] paramsToStore = new SqlParameter[13];
                paramsToStore[0] = new SqlParameter("@CmnCompanyId", obj.CmnCompanyId);
                paramsToStore[1] = new SqlParameter("@EmployeeID", obj.EmployeeID);
                paramsToStore[2] = new SqlParameter("@HrmDivisionId", obj.HrmDivisionId);
                paramsToStore[3] = new SqlParameter("@HrmDepartmentId", obj.HrmDepartmentId);
                paramsToStore[4] = new SqlParameter("@HrmSectionId", obj.HrmSectionId);
                paramsToStore[5] = new SqlParameter("@HrmOfficeId", obj.HrmOfficeId);
                paramsToStore[6] = new SqlParameter("@HrmDesignationId", obj.HrmDesignationId);
                paramsToStore[7] = new SqlParameter("@HrmGradeId", obj.HrmGradeId);
                paramsToStore[8] = new SqlParameter("@HrmEmployeeTypeId", obj.HrmEmployeeTypeId);
                paramsToStore[9] = new SqlParameter("@DateOfBirth", obj.DateOfBirth);
                paramsToStore[10] = new SqlParameter("@IsRoastaringDuty", obj.IsRoastaringDuty);
                paramsToStore[11] = new SqlParameter("@IsOTPayable", obj.IsOTPayable);
                paramsToStore[12] = new SqlParameter("@IsActive", obj.IsActive);
                dt = Helper.GetFromStoredProcedure(DataContext.Database.GetDbConnection(), SPList.HrmEmployee.RptHrmEmployeeSummary, paramsToStore, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetHROverviewForDashboard()
        {
            DataTable dt = null;
            try
            {
                dt = new DataTable();
                dt = Helper.GetFromStoredProcedure(DataContext.Database.GetDbConnection(), SPList.HrmEmployee.GetHROverviewForDashboard, null, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public List<HrmEmployeeViewModel> GetEqualOrToManagement(int employeeId)
        {
            List<HrmEmployeeViewModel> list = new List<HrmEmployeeViewModel>();
            HrmEmployeeViewModel obj = (from em in DataContext.HrmEmployees
                                        join des in DataContext.HrmDesignations on em.HrmDesignationId equals des.Id
                                        where (em.Id == employeeId)
                                        select new HrmEmployeeViewModel()
                                        {
                                            Id = em.Id,
                                            HrmOfficeId = (int)(em.HrmOfficeId == null ? 0 : em.HrmOfficeId),
                                            Priority = des.Priority
                                        }).FirstOrDefault();

            if (obj != null && obj.Priority > 0)
            {
                list = (from em in DataContext.HrmEmployees
                        join des in DataContext.HrmDesignations on em.HrmDesignationId equals des.Id
                        where (em.HrmOfficeId == obj.HrmOfficeId && des.Priority >= obj.Priority)
                        select new HrmEmployeeViewModel()
                        {
                            Id = em.Id,
                            HrmOfficeId = (int)(em.HrmOfficeId == null ? 0 : em.HrmOfficeId),
                            EmployeeId = em.EmployeeId,
                            Name = em.Name,
                            HrmEmployeeId = em.HrmEmployeeId,
                            HrmDesignationId = em.HrmDesignationId,
                            DesignationName = des.Name,
                            Priority = des.Priority,
                        }).OrderBy(o => o.Priority).ThenBy(o => o.EmployeeId).ToList();
            }
            return list;
        }

    }
}