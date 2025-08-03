
using TFSMS.Admin.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Reflection;
using TFSMS.Admin.Model.ViewModel.Common;
using Technofair.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;


namespace TFSMS.Admin.Data.Repository.TFLoan.Device
{

    #region Interface

    public interface ICmnCompanyRepository : IRepository<CmnCompany>
    {
        Task<int> AddEntityAsync(CmnCompany obj);
        int UpdateEntity(CmnCompany obj);
        DataTable GetByCompanyId(int companyId);

        //New 23.09.2024
        DataTable GetSlsCustomerByCompanyId(int companyId);
        //New: 05.02.2024
        List<CmnCompanyViewModel> GetClientForQuickLoginByCompanyId(int cmnCompanyId, int userId); //New
        DataTable GetCompanyBySelfOrParentCompanyId(int companyId);
        DataTable GetSubscriptionStatistics();
        Task<List<CmnCompany>> GetSelfAndChildCompanyByCompanyId(int companyId);
        DataTable GetCompanyByCompanyId(int companyId);
        Task<List<CmnCompany>> GetCompanyByCompanyTypeId(Int16 companyTypeId);
        Task<List<CmnCompany>> GetCompanyByCompanyTypeShortName(string shortName);
        List<CmnCompany> GetUpperLevelCompanyByCompanyTypeId(Int16 companyTypeId);
        Task<List<CmnCompany>> GetTopNLevelCompany(int level);
        Task<List<CmnCompany>> GetLoaneeByCompanyType(Int16 lnModelId, Int16 companyTypeId);
        string GetLastCode(Int16 companyTypeId);
        //Task<List<CmnCompanyType>> GetAllCompanyType();
        List<CmnCompanyType> GetSucceedingChildCompanyType(int? companyId);
        Task<List<CmnCompanyType>> GetSecondThirdLevelCompanyType();
        Task<CmnCompanyType> GetCompanyTypeById(Int16 id);
        Task<List<CmnCompanyType>> GetChildCompanyType(int? companyId, int? userLevel);
        Task<List<CmnCompanyType>> GetAllCompanyType();
        Task<List<CmnCompanyTypeViewModel>> GetCompanyTypeByLoanModelId(Int16 loanModelId);
        CmnCompanyType GetCompanyTypeByCompanyId(int? companyId);
        CmnCompany GetParentCompanyByCompanyId(int? companyId);
        List<CmnCompanyViewModel> GetChildCompanyByParentCompanyId(int? companyId);
        List<CmnCompanyViewModel> GetChildCompanyUserByParentCompanyId(Int16? cmnCompanyTypeId, int? companyId, int? userLevel);
        CmnCompany PrepareObject(CmnCompanyViewModel objView);
        //Added By Asad On 28.11.2023
        Task<CmnCompany> GetSolutionProvider();
        CmnCompany GetMainServiceOperator();
        Task<List<CmnCompany>> GetMainServiceOperators();
        List<CmnCompany> GetSecondLevelCompanies();
        Task<List<CmnCompany>> GetThirdLevelCompanies();


        List<CmnCompanyViewModel> cmnCompanyViewList();
    }

    #endregion

    public class CmnCompanyRepository : AdminBaseRepository<CmnCompany>, ICmnCompanyRepository
    {
        public CmnCompanyRepository(IAdminDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }


        public async Task<int> AddEntityAsync(CmnCompany obj)
        {
            int Id = 1;
            if (obj.Id == 0)
            {
                CmnCompany? last = DataContext.CmnCompanies.OrderByDescending(x => x.Id).FirstOrDefault();
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

        public int UpdateEntity(CmnCompany obj)
        {
            DataContext.CmnCompanies.Update(obj);
            return obj.Id;
        }

        List<CmnCompany> childs = new List<CmnCompany>();
        public DataTable GetByCompanyId(int companyId)
        {
            string @Ids = "";
            childs = new List<CmnCompany>();
            CmnCompany objParent = DataContext.CmnCompanies.Where(c => c.Id == companyId).FirstOrDefault();
            CmnCompanyType? objType = DataContext.CmnCompanyTypes.Where(w => w.Id == objParent.CmnCompanyTypeId).FirstOrDefault();
            List<CmnCompany> list = new List<CmnCompany>();
            if (objType != null)
            {
                if (objType.ShortName == "SP")
                {
                    list = DataContext.CmnCompanies.OrderBy(o => o.Id).ToList();
                }
                else if (objType.ShortName == "MSO")
                {
                    list = DataContext.CmnCompanies.OrderBy(o => o.Id).ToList();
                }
                else if (objType.ShortName == "LSO")
                {
                    list = DataContext.CmnCompanies.Where(c => c.CmnCompanyId == companyId).Distinct().OrderBy(o => o.Id).ToList();
                }
            }

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
                    if (@Ids == "")
                    {
                        @Ids = childs[i].Id.ToString();
                    }
                    else
                    {
                        @Ids += "," + childs[i].Id;
                    }
                }

            }

            DataTable dt = new DataTable();
            SqlParameter[] paramsToStore = new SqlParameter[1];
            paramsToStore[0] = new SqlParameter("@Ids", @Ids);
            try
            {
                dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.Company.GetCmnCompanies, paramsToStore).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //New 23.09.2024

        public DataTable GetSlsCustomerByCompanyId(int companyId)
        {
            string @Ids = "";
            childs = new List<CmnCompany>();
            CmnCompany objParent = DataContext.CmnCompanies.Where(c => c.Id == companyId).FirstOrDefault();
            CmnCompanyType? objType = DataContext.CmnCompanyTypes.Where(w => w.Id == objParent.CmnCompanyTypeId).FirstOrDefault();
            List<CmnCompany> list = new List<CmnCompany>();
            if (objType != null)
            {
                if (objType.ShortName == "SP")
                {
                    list = DataContext.CmnCompanies.OrderBy(o => o.Id).ToList();
                }
                else if (objType.ShortName == "MSO")
                {
                    list = DataContext.CmnCompanies.OrderBy(o => o.Id).ToList();
                }
                else if (objType.ShortName == "LSO")
                {
                    list = DataContext.CmnCompanies.Where(c => c.CmnCompanyId == companyId).Distinct().OrderBy(o => o.Id).ToList();
                }
            }

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
                    if (@Ids == "")
                    {
                        @Ids = childs[i].Id.ToString();
                    }
                    else
                    {
                        @Ids += "," + childs[i].Id;
                    }
                }

            }

            DataTable dt = new DataTable();
            SqlParameter[] paramsToStore = new SqlParameter[1];
            paramsToStore[0] = new SqlParameter("@Ids", @Ids);
            try
            {
                dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.Company.GetSlsCustomers, paramsToStore).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        //New: 05.02.2024
        public List<CmnCompanyViewModel> GetClientForQuickLoginByCompanyId(int cmnCompanyId, int userId) //New
        {
            

            DataTable dt = new DataTable();
            SqlParameter[] paramsToStore = new SqlParameter[2];
            paramsToStore[0] = new SqlParameter("@cmnCompanyId", cmnCompanyId);
            paramsToStore[1] = new SqlParameter("@userId", userId);

            List<CmnCompanyViewModel> objCmnCompany = new List<CmnCompanyViewModel>();

            try
            {
                dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.Company.GetClientForQuickLoginByCompanyId, paramsToStore).Tables[0];

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        objCmnCompany.Add(((CmnCompanyViewModel)Helper.FillTo(row, typeof(CmnCompanyViewModel))));
                    }
                }
                
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            return objCmnCompany;
        }
        

        //New
          public DataTable GetCompanyBySelfOrParentCompanyId(int companyId)
        {

            childs = new List<CmnCompany>();

            DataTable dt = new DataTable();
            SqlParameter[] paramsToStore = new SqlParameter[1];
            paramsToStore[0] = new SqlParameter("@cmnCompanyId", companyId);
            try
            {
                dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.Company.GetCompanyBySelfOrParentCompanyId, paramsToStore).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetSubscriptionStatistics()
        {
           
            childs = new List<CmnCompany>();
                           
            DataTable dt = new DataTable();
            SqlParameter[] paramsToStore = new SqlParameter[0];
           
            try
            {
               dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.Company.GetSubscriptionStatistics, paramsToStore).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //nnnn

        public async Task<List<CmnCompany>> GetSelfAndChildCompanyByCompanyId(int companyId)
        {

            var objCompanies = await DataContext.CmnCompanies.Where(c => c.Id == companyId || c.CmnCompanyId == companyId && c.IsActive == true).ToListAsync();
            return objCompanies;
        }
        

        public DataTable GetCompanyByCompanyId(int companyId)
        {

            childs = new List<CmnCompany>();

            DataTable dt = new DataTable();
            SqlParameter[] paramsToStore = new SqlParameter[1];
            paramsToStore[0] = new SqlParameter("@cmnCompanyId", companyId);
            try
            {
                dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.Company.GetCompanyByCompanyId, paramsToStore).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
        public List<CmnCompany> GetUpperLevelCompanyByCompanyTypeId(Int16 companyTypeId)
        {
            List<CmnCompany> companies = new List<CmnCompany>();

            DataTable dt = new DataTable();
            SqlParameter[] paramsToStore = new SqlParameter[1];
            paramsToStore[0] = new SqlParameter("@companyTypeId", companyTypeId);
          
            dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.Company.GetUpperLevelCompanyByCompanyType, paramsToStore).Tables[0];

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    companies.Add(((CmnCompany)Helper.FillTo(row, typeof(CmnCompany))));
                }
            }
                       
            return companies;
        }

        //New:28.05.2024
        public async Task<List<CmnCompany>> GetTopNLevelCompany(int level)
        {
            List<CmnCompany> companyList = new List<CmnCompany>();

            if (level == 1)
            {
                companyList = await (from c in DataContext.CmnCompanies
                                    join ct in DataContext.CmnCompanyTypes
                                    on c.CmnCompanyTypeId equals ct.Id
                                    where ct.ShortName == "MSO"
                                     select c).ToListAsync();
            }
                

            if (level == 2)
            {
                companyList = await (from c in DataContext.CmnCompanies
                                     join ct in DataContext.CmnCompanyTypes
                                     on c.CmnCompanyTypeId equals ct.Id
                                     where (ct.ShortName== "MSO" || ct.ShortName == "LSO")
                                     select c).ToListAsync();
            }

            if (level == 3)
            {
                companyList = await (from c in DataContext.CmnCompanies
                                     join ct in DataContext.CmnCompanyTypes
                                     on c.CmnCompanyTypeId equals ct.Id
                                     where (ct.ShortName == "MSO" || ct.ShortName == "LSO" || ct.ShortName == "SLSO")
                                     select c).ToListAsync();
            }

            return companyList;
        }

        public async Task<List<CmnCompany>> GetLoaneeByCompanyType(Int16 lnModelId, Int16 companyTypeId)
        {
            List<CmnCompany> companyList = new List<CmnCompany>();

            if (lnModelId == (Int16)Technofair.Utiity.Enums.Loan.LnLoanModelEnum.Standard && companyTypeId == (Int16)Technofair.Utiity.Enums.Common.CmnCompanyTypeEnum.SP)
            {
                companyList = await (from c in DataContext.CmnCompanies
                                     join ct in DataContext.CmnCompanyTypes
                                     on c.CmnCompanyTypeId equals ct.Id
                                     where (ct.ShortName == "LSO" || ct.ShortName == "SLSO")
                                     select c).ToListAsync();
            }

            //if (lnModelId == 1 && companyTypeId == 1)
            //{
            //    return
            //}


            if (lnModelId == (Int16)Technofair.Utiity.Enums.Loan.LnLoanModelEnum.DoubleFlow && companyTypeId == (Int16)Technofair.Utiity.Enums.Common.CmnCompanyTypeEnum.SP)
            {
                companyList = await (from c in DataContext.CmnCompanies
                                     join ct in DataContext.CmnCompanyTypes
                                     on c.CmnCompanyTypeId equals ct.Id
                                     where ct.ShortName == "MSO" 
                                     select c).ToListAsync();
            }

            if (lnModelId == (Int16)Technofair.Utiity.Enums.Loan.LnLoanModelEnum.DoubleFlow && companyTypeId == (Int16)Technofair.Utiity.Enums.Common.CmnCompanyTypeEnum.MSO)
            {
                companyList = await (from c in DataContext.CmnCompanies
                                     join ct in DataContext.CmnCompanyTypes
                                     on c.CmnCompanyTypeId equals ct.Id
                                     where (ct.ShortName == "LSO" || ct.ShortName == "SLSO")
                                     select c).ToListAsync();
            }

            return companyList;
        }




        public async Task<List<CmnCompany>> GetCompanyByCompanyTypeId(Int16 companyTypeId)
        {
            var result = await (from c in DataContext.CmnCompanies
                                join ct in DataContext.CmnCompanyTypes
                                on c.CmnCompanyTypeId equals ct.Id
                                where ct.Id == companyTypeId
                                select c).ToListAsync();
            return result;
        }

        public async Task<List<CmnCompany>> GetCompanyByCompanyTypeShortName(string shortName)
        {
            var result = await (from c in DataContext.CmnCompanies
                                join ct in DataContext.CmnCompanyTypes
                                on c.CmnCompanyTypeId equals ct.Id
                                where ct.ShortName == shortName
                                select c).ToListAsync();
            return result;
        }
        public List<CmnCompanyType> GetSucceedingChildCompanyType(int? companyId)
        {
            List<CmnCompanyType> list = new List<CmnCompanyType>();
            if (companyId != null && companyId > 0)
            {
                CmnCompany? objCom = DataContext.CmnCompanies.Where(w => w.Id == companyId).FirstOrDefault();
                if (objCom != null)
                {
                    CmnCompanyType? objType = DataContext.CmnCompanyTypes.Where(w => w.Id == objCom.CmnCompanyTypeId).FirstOrDefault();
                    if (objType != null)
                    {
                        if (objType.ShortName == "MSO")
                        {
                            list = DataContext.CmnCompanyTypes.Where(w => !w.ShortName.Contains("MSO")).ToList();
                        }
                        else if (objType.ShortName == "LSO")
                        {
                            list = DataContext.CmnCompanyTypes.Where(w => !w.ShortName.Contains("MSO") && w.ShortName!="LSO").ToList();
                        }
                        //else
                        //{
                        //    list = DataContext.CmnCompanyTypes.ToList();
                        //}
                    }
                    //else
                    //{
                    //    list = DataContext.CmnCompanyTypes.ToList();
                    //}
                }
                //else
                //{
                //    list = DataContext.CmnCompanyTypes.ToList();
                //}
            }
            //else
            //{
            //    list = DataContext.CmnCompanyTypes.ToList();
            //}
            return list;
        }

        public async Task<List<CmnCompanyType>> GetSecondThirdLevelCompanyType()
        {
            var companyTypes = await(from ct in DataContext.CmnCompanyTypes
                                  where ct.ShortName == "LSO" || ct.ShortName == "SLSO"
                                  select ct).ToListAsync();

            return companyTypes;
        }
                
        //GetChildCompanyType
        public async Task<List<CmnCompanyType>> GetChildCompanyType(int? companyId, int? userLevel)
        {
            List<CmnCompanyType> list = new List<CmnCompanyType>();
            if (companyId != null && companyId > 0)
            {
                CmnCompany? objCom = await DataContext.CmnCompanies.Where(w => w.Id == companyId).FirstOrDefaultAsync();
                
                if (objCom != null)
                {
                    CmnCompanyType? objType = await DataContext.CmnCompanyTypes.Where(w => w.Id == objCom.CmnCompanyTypeId).FirstOrDefaultAsync();

                    
                    if (objType != null)
                    {
                        if (objType.ShortName == "MSO" && userLevel == 1)
                        {
                            list = await DataContext.CmnCompanyTypes.Where(w => w.ShortName == "MSO").ToListAsync();
                        }

                        else if (objType.ShortName == "MSO")
                        {
                            list = await DataContext.CmnCompanyTypes.Where(w => w.ShortName =="LSO").ToListAsync();
                        }
                        else if (objType.ShortName == "LSO")
                        {
                            list = await DataContext.CmnCompanyTypes.Where(w => w.ShortName == "SLSO").ToListAsync();
                        }
                    }
                }
            }
            return list;
        }

        public async Task<CmnCompanyType> GetCompanyTypeById(Int16 id)
        {
            CmnCompanyType objCompanyType = new CmnCompanyType();
            try
            {
                objCompanyType = await DataContext.CmnCompanyTypes.Where(w => w.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
            }
            return objCompanyType;
        }


        public async Task<List<CmnCompanyType>> GetAllCompanyType()
        {
            List<CmnCompanyType> list = new List<CmnCompanyType>();

            list = await DataContext.CmnCompanyTypes.ToListAsync();
            return list;
        }

        public async Task<List<CmnCompanyTypeViewModel>> GetCompanyTypeByLoanModelId(Int16 loanModelId)
        {
            List<CmnCompanyTypeViewModel> list = new List<CmnCompanyTypeViewModel>();

            if ((Int16)Technofair.Utiity.Enums.Loan.LnLoanModelEnum.Standard == loanModelId)
            {
                list = await (from ct in DataContext.CmnCompanyTypes
                              where ct.ShortName == "SP"
                              select new CmnCompanyTypeViewModel
                              {
                                  Id = ct.Id,
                                  Name = ct.Name,
                                  ShortName = ct.ShortName,
                                  SerialNo = ct.SerialNo,
                                  IsActive = ct.IsActive,
                                  CmnCompanyTypeId = ct.Id
                              }).ToListAsync();
            }
            if ((Int16)Technofair.Utiity.Enums.Loan.LnLoanModelEnum.DoubleFlow == loanModelId)
            {
                list = await (from ct in DataContext.CmnCompanyTypes
                              where new[] { "SP", "MSO" }.Contains(ct.ShortName)
                              select new CmnCompanyTypeViewModel
                              {
                                    Id  = ct.Id,
                                    Name = ct.Name,
                                    ShortName = ct.ShortName,
                                    SerialNo = ct.SerialNo,
                                    IsActive = ct.IsActive,
                                    CmnCompanyTypeId = ct.Id
                              }).ToListAsync();

                //list  = await DataContext.CmnCompanyTypes.Where(x => new[] { "SP", "MSO" }.Contains(x.ShortName)).ToListAsync();

            }
            return list;
        }



        public CmnCompanyType GetCompanyTypeByCompanyId(int? companyId)
        {
            CmnCompanyType? objType = null;
            try
            {
                CmnCompany? objCom = DataContext.CmnCompanies.Where(w => w.Id == companyId).FirstOrDefault();
                if (objCom != null)
                {
                    objType = DataContext.CmnCompanyTypes.Where(w => w.Id == objCom.CmnCompanyTypeId).FirstOrDefault();
                }
            }
            catch(Exception ex)
            {
            }
            return objType;
        }

        //New
        public CmnCompany GetParentCompanyByCompanyId(int? companyId)
        {
            CmnCompany? objParentCompany = null;
            try
            {
                CmnCompany? objChildCompany = DataContext.CmnCompanies.Where(w => w.Id == companyId).FirstOrDefault();
                if (objChildCompany != null)
                {
                    objParentCompany = DataContext.CmnCompanies.Where(w => w.Id == objChildCompany.CmnCompanyId).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
            }
            return objParentCompany;
        }

        //New
        public List<CmnCompanyViewModel> GetChildCompanyByParentCompanyId(int? companyId)
        {
            List<CmnCompanyViewModel> objCopanies= new List<CmnCompanyViewModel>();   

            try
            {
                //New
                DataTable dt = new DataTable();
                SqlParameter[] paramsToStore = new SqlParameter[1];
                
                paramsToStore[0] = new SqlParameter("@cmnCompanyId", companyId);
                

                dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.Company.GetChildCompanyByParentCompanyId, paramsToStore).Tables[0];

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        objCopanies.Add(((CmnCompanyViewModel)Helper.FillTo(row, typeof(CmnCompanyViewModel))));
                    }
                }
                return objCopanies;


                //Old
                //objCopanies =  await (from c in DataContext.CmnCompanies
                // join ct in DataContext.CmnCompanyTypes
                // on c.CmnCompanyTypeId equals ct.Id
                // where c.CmnCompanyId ==  companyId
                // select new CmnCompanyViewModel
                // {
                //     Id = c.Id,
                //     CmnCompanyTypeId = ct.Id,
                //     Code = c.Code,
                //     Name = c.Name,
                //     ShortName = c.ShortName,
                //     Address = c.Address,
                //     ContactPerson = c.ContactPerson,
                //     ContactNo = c.ContactNo,
                //     AlternatePhone = c.AlternatePhone,
                //     Email = c.Email,
                //     CmnCompanyId = c.CmnCompanyId,
                //     CmnCountryId = c.CmnCountryId,
                //     CmnDivisionId = c.CmnDivisionId,
                //     CmnDistrictId = c.CmnDistrictId,
                //     CmnUpazillaId  = c.CmnUpazillaId,
                //     CmnUnionId = c.CmnUnionId,
                //     Zip = c.Zip,
                //     Fax = c.Fax,
                //     Web = c.Web,
                //     Logo = c.Logo,
                //     Prefix = c.Prefix,
                //     WelcomeNote = c.WelcomeNote,
                //     IsActive = c.IsActive,
                //     CompanyType = ct.Name
                // }).OrderBy(x=> x.Name).ToListAsync();



            }
            catch (Exception ex)
            {
            }
            return objCopanies;
        }
        //New : 04.11.2024
        public List<CmnCompanyViewModel> GetChildCompanyUserByParentCompanyId(Int16? cmnCompanyTypeId, int? companyId, int? userLevel)
        {
            List<CmnCompanyViewModel> objCopanies = new List<CmnCompanyViewModel>();

            try
            {
                //New
                DataTable dt = new DataTable();
                SqlParameter[] paramsToStore = new SqlParameter[3];

                paramsToStore[0] = new SqlParameter("@cmnCompanyTypeId", cmnCompanyTypeId);
                paramsToStore[1] = new SqlParameter("@cmnCompanyId", companyId);
                paramsToStore[2] = new SqlParameter("@userLevel", userLevel);

                dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.Company.GetChildCompanyUserByParentCompanyId, paramsToStore).Tables[0];

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        objCopanies.Add(((CmnCompanyViewModel)Helper.FillTo(row, typeof(CmnCompanyViewModel))));
                    }
                }
                return objCopanies;




            }
            catch (Exception ex)
            {
            }
            return objCopanies;
        }

        public string GetLastCode(Int16 companyTypeId)
        {
            CmnCompanyType? objType = DataContext.CmnCompanyTypes.Where(w => w.Id == companyTypeId).FirstOrDefault();
            string refNo = objType.ShortName + "-" + objType.SerialNo;
            CmnCompany? obj = DataContext.CmnCompanies.Where(w=>w.Code.Contains(refNo)).OrderByDescending(o => o.Id).FirstOrDefault();
            int serail = 1;            
            if (obj != null && obj.Code != null && obj.Code != "")
            {
                string sub = obj.Code.ToString().Substring(refNo.Length);
                serail = Convert.ToInt32(sub) + 1;
                if (serail.ToString().Length == 1)
                {
                    refNo += "000";
                }
                else if (serail.ToString().Length == 2)
                {
                    refNo += "00";
                }
                else if (serail.ToString().Length == 3)
                {
                    refNo += "0";
                }
                refNo = refNo + serail;
            }
            else
            {
                refNo = refNo + "000" + serail;
            }
            return refNo;
        }

        //private CmnCompany MergeObjects<CmnCompany>(CmnCompany target, CmnCompanyViewModel source)
        //{
        //    CmnCompany obj = (CmnCompany)Activator.CreateInstance(typeof(CmnCompany));
        //    Type t = typeof(CmnCompany);
        //    var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);
        //    try
        //    {
        //        foreach (var prop in properties)
        //        {
        //            var value = prop.GetValue(target, null);
        //            if (value != null)
        //                prop.SetValue(obj, value, null);
        //            else
        //            {
        //                value = prop.GetValue(source, null);
        //                if (value != null)
        //                    prop.SetValue(obj, value, null);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return obj;
        //}


        public CmnCompany PrepareObject(CmnCompanyViewModel objView)
        {
            //CmnCompany target = new CmnCompany();
            //CmnCompany obj = MergeObjects(target, objView);
            CmnCompany obj = new CmnCompany();
            obj.Name = objView.Name;
            obj.Address = objView.Address;
            obj.ContactPerson = objView.ContactPerson;
            obj.ContactNo = objView.ContactNo;
            obj.AlternatePhone = objView.AlternatePhone;
            obj.Email = objView.Email;
            obj.Zip = objView.Zip;
            obj.Fax = objView.Fax;
            obj.Web = objView.Web;
            obj.CreatedBy = objView.CreatedBy;
            obj.IsActive = true;


            if (objView.Type != null && objView.Type != "")
            {
                CmnCompanyType? objType = DataContext.CmnCompanyTypes.Where(w => (w.Name.ToUpper() == objView.Type.ToUpper()) || (w.ShortName.ToUpper() == objView.Type.ToUpper())).FirstOrDefault();
                if (objType != null)
                {
                    if (objView.Type.ToUpper() == "MSO")
                    {
                        CmnCompany? objExist = DataContext.CmnCompanies.Where(w => w.CmnCompanyTypeId == objType.Id).FirstOrDefault();
                        if (objExist != null)//check MSO exist or not because of only one MSO allow per database
                        {
                            return null;
                        }
                        else
                        {
                            obj.CmnCompanyTypeId = objType.Id;
                        }
                    }
                    else
                    {
                        obj.CmnCompanyTypeId = objType.Id;
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

            if (objView.Country != null && objView.Country != "")
            {
                CmnCountry? exist = DataContext.CmnCountries.Where(w => w.Name.ToUpper() == objView.Country.ToUpper()).FirstOrDefault();
                if (exist != null)
                {
                    obj.CmnCountryId = exist.Id;
                }
            }

            if (objView.Division != null && objView.Division != "")
            {
                CmnDivision? exist = DataContext.CmnDivisions.Where(w => w.Name.ToUpper() == objView.Division.ToUpper()).FirstOrDefault();
                if (exist != null)
                {
                    obj.CmnDivisionId = exist.Id;
                }
            }

            if (objView.District != null && objView.District != "")
            {
                CmnDistrict? exist = DataContext.CmnDistricts.Where(w => w.Name.ToUpper() == objView.District.ToUpper()).FirstOrDefault();
                if (exist != null)
                {
                    obj.CmnDistrictId = exist.Id;
                }
            }

            if (objView.Upazilla != null && objView.Upazilla != "")
            {
                CmnUpazilla? exist = DataContext.CmnUpazillas.Where(w => w.Name.ToUpper() == objView.Upazilla.ToUpper()).FirstOrDefault();
                if (exist != null)
                {
                    obj.CmnUpazillaId = exist.Id;
                }
            }

            if (objView.Union != null && objView.Union != "")
            {
                CmnUnion? exist = DataContext.CmnUnions.Where(w => w.Name.ToUpper() == objView.Union.ToUpper()).FirstOrDefault();
                if (exist != null)
                {
                    obj.CmnUnionId = exist.Id;
                }
            }
            return obj;
        }

        public async Task<CmnCompany> GetSolutionProvider()
        {
            var result = await (from ct in DataContext.CmnCompanyTypes
                         join c in DataContext.CmnCompanies
                         on ct.Id equals c.CmnCompanyTypeId
                         where ct.ShortName == "SP"
                         select c).FirstOrDefaultAsync();

            return result;
        }



        //Old: 30072025
        //public CmnCompany GetSolutionProvider()
        //{
        //    List<CmnCompany> companies = new List<CmnCompany>();

        //    DataTable dt = new DataTable();
        //    SqlParameter[] paramsToStore = new SqlParameter[0];
        //    //paramsToStore[0] = new SqlParameter("@companyTypeName", companyTypeName);

        //    dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.Company.GetSolutionProvider, paramsToStore).Tables[0];

        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            companies.Add(((CmnCompany)Helper.FillTo(row, typeof(CmnCompany))));
        //        }
        //    }

        //    CmnCompany company = new CmnCompany();
        //    company = companies.FirstOrDefault();

        //    return company;
        //}

        //Added By Asad On 28.11.2023
        public CmnCompany GetMainServiceOperator()
        {
            List<CmnCompany> companies = new List<CmnCompany>();

            DataTable dt = new DataTable();
            SqlParameter[] paramsToStore = new SqlParameter[0];
            //paramsToStore[0] = new SqlParameter("@companyTypeName", companyTypeName);

            dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.Company.GetMainServiceOperator, paramsToStore).Tables[0];

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    companies.Add(((CmnCompany)Helper.FillTo(row, typeof(CmnCompany))));
                }
            }

            CmnCompany company = new CmnCompany();
            company = companies.FirstOrDefault();

            return company;
        }

        public async Task<List<CmnCompany>> GetMainServiceOperators()
        {
           var companies = await (from c in DataContext.CmnCompanies
            join ct in DataContext.CmnCompanyTypes
            on c.CmnCompanyTypeId equals ct.Id
            where ct.ShortName == "MSO"
            select c).ToListAsync();
            return companies;
        }

        public List<CmnCompany> GetSecondLevelCompanies()
        {
            List<CmnCompany> companies = new List<CmnCompany>();

            DataTable dt = new DataTable();
            SqlParameter[] paramsToStore = new SqlParameter[0];
            
            dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.Company.GetSecondLevelCompanies, paramsToStore).Tables[0];

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    companies.Add(((CmnCompany)Helper.FillTo(row, typeof(CmnCompany))));
                }
            }
            return companies;
        }

        public async Task<List<CmnCompany>> GetThirdLevelCompanies()
        {
            var companies = await(from c in DataContext.CmnCompanies
                                  join ct in DataContext.CmnCompanyTypes
                                  on c.CmnCompanyTypeId equals ct.Id
                                  where ct.ShortName == "SLSO"
                                  select c).ToListAsync();
            return companies;
        }

        public  List<CmnCompanyViewModel> cmnCompanyViewList() {

            List<CmnCompanyViewModel> companies = new List<CmnCompanyViewModel>();

            DataTable dt = new DataTable();
            SqlParameter[] paramsToStore = new SqlParameter[0];
           

            dt =  Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.Company.GetCmnCompanyViewList, paramsToStore).Tables[0];

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    companies.Add(((CmnCompanyViewModel)Helper.FillTo(row, typeof(CmnCompanyViewModel))));
                }
            }

            //CmnCompanyViewModel company = new CmnCompanyViewModel();
            //company = companies.FirstOrDefault();

            return companies;
        }


    }
}
