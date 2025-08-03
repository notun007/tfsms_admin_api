
using Technofair.Lib.Utilities;
using Technofair.Model.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Model.ViewModel.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Technofair.Model.HRM;
using Technofair.Model.Security;
using Technofair.Model.ViewModel.Security;
using Technofair.Model.Accounts;

using Technofair.Utiity.Security;
using Newtonsoft.Json.Linq;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Technofair.Utiity.Configuration;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;


namespace TFSMS.Admin.Data.Repository.Security
{
    #region Interface
    public interface ISecUserRepository : IRepository<SecUser>
    {
        int AddEntity(SecUser obj);
        Task<int> AddEntityAsync(SecUser obj);
        int UpdateEntity(SecUser obj);
        SecUserViewModel GetUserInfoById(int Id);
        DataTable GetUserInfoByCompanyId(int companyId);
        DataTable GetUserByAnyKey(Int16? cmnCompanyTypeId, int? companyId, Int16 userLevel);
        DataTable GetUserByCompanyId(int companyId, Int16 userLevel);
        Task<LoginResponse> Authenticate(AuthenticateUser obj, string secret);
       // Task<SecUser> GetUserBySubscriberId(int subscriberId);
        DataTable GetUserByCompanyId(int companyId);
        Task<SecUser> GetSecUsersByEmployeeId(int employeeId);
        DataTable GetSecUsersByCompanyAndUserType(int cmnCompanyId, int secUserTypeId);
        DataTable GetSecUsersByCompanyAndUserLevel(int cmnCompanyId, int userLevel);
    }

    #endregion


    public class SecUserRepository : AdminBaseRepository<SecUser>, ISecUserRepository
    {
        private readonly IConfigurationService configService;
        string TFAdminBaseUrl = string.Empty;
        public SecUserRepository(IAdminDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            configService = new ConfigurationService();
            TFAdminBaseUrl = configService.GetSetting("TFAdmin:BaseUrl");
        }

        public int AddEntity(SecUser obj)
        {
            int Id = 1;
            SecUser? last = DataContext.SecUsers.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;
        }

        public async Task<int> AddEntityAsync(SecUser obj)
        {
            int Id = 1;
            if (obj.Id == 0)
            {
                SecUser? last = DataContext.SecUsers.OrderByDescending(x => x.Id).FirstOrDefault();
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

        public int UpdateEntity(SecUser obj)
        {
            DataContext.SecUsers.Update(obj);
            return obj.Id;
        }

        //public List<SecUser> GetUserByCompanyId(int companyId)
        //{
        //    List<SecUser> list = new List<SecUser>();
        //    CmnCompany objCom = DataContext.CmnCompanies.Where(w => w.Id == companyId).FirstOrDefault();
        //    if (objCom != null && objCom.CmnCompanyId == null)// for MSO or mother company
        //    {
        //        list = DataContext.SecUsers.ToList();
        //    }
        //    else
        //    {
        //        list = DataContext.SecUsers.Where(w => w.CmnCompanyId == companyId).ToList();
        //    }
        //    return list;
        //}

        List<CmnCompany> childs = new List<CmnCompany>();
        public DataTable GetUserByCompanyId(int companyId)
        {
            string clients = "";
            childs = new List<CmnCompany>();
            CmnCompany objParent = DataContext.CmnCompanies.Where(c => c.Id == companyId).FirstOrDefault();
            List<CmnCompany> list = DataContext.CmnCompanies.Where(c => c.CmnCompanyId == companyId).Distinct().OrderBy(o => o.Id).ToList();
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

            DataTable dt = new DataTable();
            SqlParameter[] paramsToStore = new SqlParameter[1];
            paramsToStore[0] = new SqlParameter("@clientIds", clients);
            try
            {
                dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.SecUser.GetSecUsersByCompanyId, paramsToStore).Tables[0];
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
        public async Task<SecUser> GetSecUsersByEmployeeId(int employeeId)
        {
            return await DataContext.SecUsers.Where(x => x.HrmEmployeeId == employeeId).SingleOrDefaultAsync();
        }
        public DataTable GetSecUsersByCompanyAndUserType(int cmnCompanyId, int secUserTypeId)
        {
         
           
            DataTable dt = new DataTable();
            SqlParameter[] paramsToStore = new SqlParameter[2];
            paramsToStore[0] = new SqlParameter("@cmnCompanyId", cmnCompanyId);
            paramsToStore[1] = new SqlParameter("@secUserTypeId", secUserTypeId);
            try
            {
                dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.SecUser.GetSecUsersByCompanyAndUserType, paramsToStore).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //New
        public DataTable GetSecUsersByCompanyAndUserLevel(int cmnCompanyId, int userLevel)
        {

            DataTable dt = new DataTable();
            SqlParameter[] paramsToStore = new SqlParameter[2];
            paramsToStore[0] = new SqlParameter("@cmnCompanyId", cmnCompanyId);
            paramsToStore[1] = new SqlParameter("@userLevel", userLevel);
            try
            {
                dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.SecUser.GetSecUsersByCompanyAndUserLevel, paramsToStore).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        //public DataTable GetUserByLevel(int level, int companyId)
        //{
        //    try
        //    {
        //        SqlParameter[] parameters = new SqlParameter[2];
        //        parameters[0] = new SqlParameter("@Level", level);
        //        parameters[1] = new SqlParameter("@CompanyId", companyId);
        //        DataTable dt = Helper.GetFromStoredProcedure(DataContext.Database.GetDbConnection(), SPList.SecUser.GetSecUsersByLevel, parameters, true);
        //        return dt;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public DataTable GetSecUsersByCompanyId(int companyId)
        //{
        //    try
        //    {

        //        SqlParameter[] parameters = new SqlParameter[1];
        //        parameters[0] = new SqlParameter("@CmnCompanyId", companyId);
        //        DataTable dt = Helper.GetFromStoredProcedure(DataContext.Database.GetDbConnection(), SPList.SecUser.GetSecUsersByCompanyId, parameters, true);
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public SecUserViewModel GetUserInfoById(int Id)
        {
            SecUserViewModel? obj = (from u in DataContext.SecUsers
                                     join c in DataContext.CmnCompanies on u.CmnCompanyId equals c.Id
                                     join ct in DataContext.CmnCompanyTypes on c.CmnCompanyTypeId equals ct.Id
                                     join e in DataContext.HrmEmployees on u.HrmEmployeeId equals e.Id into temp
                                     from e in temp.DefaultIfEmpty()
                                     where u.Id == Id
                                     select new SecUserViewModel()
                                     {
                                         Id = u.Id,
                                         HrmEmployeeId = u.HrmEmployeeId,
                                         LoginID = u.LoginID,
                                         //Password = u.Password,
                                         IsActive = u.IsActive,
                                         LevelNo = u.LevelNo,
                                         //ParentUserId = u.ParentUserId,
                                         CmnCompanyId = u.CmnCompanyId,
                                         CreatedBy = u.CreatedBy,
                                         CreatedDate = u.CreatedDate,
                                         ModifiedBy = u.ModifiedBy,
                                         ModifiedDate = u.ModifiedDate,
                                         CompanyName = c.Name,
                                         CompanyType = ct.ShortName,
                                         Prefix = c.Prefix,
                                         EmployeeID = e.EmployeeId,
                                         EmployeeName = e.Name,
                                         PhotoUrl = e.PhotoUrl
                                     }).FirstOrDefault();
            return obj;
        }

        //public List<SecUserViewModel> GetUserInfoByCompanyId(int companyId)
        //{
        //    CmnCompany objCom = DataContext.CmnCompanies.Where(w => w.Id == companyId).FirstOrDefault();
        //    if (objCom != null && objCom.CmnCompanyId == null)// for MSO or mother company
        //    {
        //        companyId = 0;// for all
        //    }
        //    List<SecUserViewModel> list = (from u in DataContext.SecUsers
        //                                   join c in DataContext.CmnCompanies on u.CmnCompanyId equals c.Id
        //                                   join e in DataContext.HrmEmployees on u.HrmEmployeeId equals e.Id into temp
        //                                   from tm in temp.DefaultIfEmpty()
        //                                   where (u.CmnCompanyId == companyId || companyId == 0)
        //                                   select new SecUserViewModel()
        //                                   {
        //                                       Id = u.Id,
        //                                       HrmEmployeeId = u.HrmEmployeeId,
        //                                       LoginID = u.LoginID,
        //                                       Password = u.Password,
        //                                       IsActive = u.IsActive,
        //                                       LevelNo = u.LevelNo,
        //                                       //ParentUserId = u.ParentUserId,
        //                                       CmnCompanyId = u.CmnCompanyId,
        //                                       CreatedBy = u.CreatedBy,
        //                                       CreatedDate = u.CreatedDate,
        //                                       ModifiedBy = u.ModifiedBy,
        //                                       ModifiedDate = u.ModifiedDate,
        //                                       CompanyName = c.Name,
        //                                       EmployeeID = tm.EmployeeId,
        //                                       EmployeeName = tm.Name,
        //                                   }).ToList();
        //    return list;
        //}

        public DataTable GetUserInfoByCompanyId(int companyId)
        {
            string clients = "";
            childs = new List<CmnCompany>();
            CmnCompany objParent = DataContext.CmnCompanies.Where(c => c.Id == companyId).FirstOrDefault();
            List<CmnCompany> list = DataContext.CmnCompanies.Where(c => c.CmnCompanyId == companyId).Distinct().OrderBy(o => o.Id).ToList();
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

            DataTable dt = new DataTable();
            SqlParameter[] paramsToStore = new SqlParameter[1];
            paramsToStore[0] = new SqlParameter("@clientIds", clients);
            try
            {
                dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.SecUser.GetSecUserInfoByCompanyId, paramsToStore).Tables[0];
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //New---
        public DataTable GetUserByAnyKey(Int16? cmnCompanyTypeId, int? companyId, Int16 userLevel)
        {

            //CmnCompany? objParent = DataContext.CmnCompanies.Where(c => c.Id == companyId).FirstOrDefault();

            DataTable dt = new DataTable();


            SqlParameter[] paramsToStore = new SqlParameter[3];
            paramsToStore[0] = new SqlParameter("@cmnCompanyTypeId", cmnCompanyTypeId);
            paramsToStore[1] = new SqlParameter("@cmnCompanyId", companyId);
            paramsToStore[2] = new SqlParameter("@userLevel", userLevel);
            try
            {
                dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.SecUser.GetSecUserByAnyKey, paramsToStore).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }
        public DataTable GetUserByCompanyId(int companyId, Int16 userLevel)
        {

            //CmnCompany? objParent = DataContext.CmnCompanies.Where(c => c.Id == companyId).FirstOrDefault();

            DataTable dt = new DataTable();


            SqlParameter[] paramsToStore = new SqlParameter[2];
            paramsToStore[0] = new SqlParameter("@cmnCompanyId", companyId);
            paramsToStore[1] = new SqlParameter("@userLevel", userLevel);
            try
            {
                dt = Helper.ExecuteDataset(DataContext.Database.GetDbConnection().ConnectionString, CommandType.StoredProcedure, SPList.SecUser.GetSecUserByCompanyId, paramsToStore).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }
        public async Task<LoginResponse> Authenticate(AuthenticateUser obj, string secret)
        {
            LoginResponse objLoginResponse = new LoginResponse();

            try
            {
                
                SecUser user = await DataContext.SecUsers.Where(w => w.LoginID == obj.Username && w.IsActive).FirstOrDefaultAsync();

               
                    if (user == null)
                    {
                    objLoginResponse = new LoginResponse();
                    objLoginResponse.IsAuhentic = false;
                    objLoginResponse.Message = "Invalid User";
                    return objLoginResponse;
                    }

                CmnCompany? objCom = DataContext.CmnCompanies.Where(w => w.Id == user.CmnCompanyId).FirstOrDefault();

                var objCmnAppSetting = await DataContext.CmnAppSettings.SingleOrDefaultAsync();


                if (objCom == null || objCmnAppSetting == null)
                {
                    objLoginResponse = new LoginResponse();
                    objLoginResponse.IsAuhentic = false;
                    objLoginResponse.Message = "Some thing went wrong, please try again later";
                    return objLoginResponse;
                }

                if(objCom.IsActive == false)
                {
                    objLoginResponse = new LoginResponse();
                    objLoginResponse.IsAuhentic = false;
                    objLoginResponse.Message = "Some thing went wrong, please contact with operator";
                    return objLoginResponse;
                }

                var userType = await DataContext.SecUserTypes.Where(w => w.Id == user.SecUserTypeId).SingleOrDefaultAsync();

                if (userType == null)
                {
                    objLoginResponse = new LoginResponse();
                    objLoginResponse.IsAuhentic = false;
                    objLoginResponse.Message = "Invalid User Type";
                    return objLoginResponse;
                }

                string password = "";
                if (user != null)
                {
                    password = AES.GetPlainText(user.Password);
                }

                if (!(user.LoginID == obj.Username && password == obj.UserPassword))
                {
                    objLoginResponse = new LoginResponse();
                    objLoginResponse.IsAuhentic = false;
                    objLoginResponse.Message = "Incorrect Password";
                    return objLoginResponse;
                }

                              
                HrmEmployee? objEmp = DataContext.HrmEmployees.Where(w => w.Id == (int)user.HrmEmployeeId).FirstOrDefault();

                    if (objEmp == null)
                    {
                        objLoginResponse = new LoginResponse();
                        objLoginResponse.IsAuhentic = false;
                        objLoginResponse.Message = "You don't have employment profile";
                        return objLoginResponse;
                    }

                    SecUserRole? objRole = DataContext.SecUserRoles.Where(w => w.SecUserId == user.Id).FirstOrDefault();


                        if (objRole == null)
                        {
                            objLoginResponse = new LoginResponse();
                            objLoginResponse.IsAuhentic = false;
                            objLoginResponse.Message = "You are not Authorized";
                            return objLoginResponse;
                        }
                        
                    
                        //Old: 31.12.2024
                    //    CmnCompany? objCom = DataContext.CmnCompanies.Where(w => w.Id == user.CmnCompanyId).FirstOrDefault();

                    //        var objCmnAppSetting = await DataContext.CmnAppSettings.SingleOrDefaultAsync();


                    //        if (objCom == null || objCmnAppSetting == null)
                    //        {
                    //        objLoginResponse = new LoginResponse();
                    //        objLoginResponse.IsAuhentic = false;
                    //objLoginResponse.Message = "Some thing went wrong, please try again later";
                    //        return objLoginResponse;
                    //        }

                    
                            CmnCompanyType? objComType = DataContext.CmnCompanyTypes.Where(w => w.Id == objCom.CmnCompanyTypeId).FirstOrDefault();

                var objCompanyUser = (from sc in DataContext.SecUsers
                                      join c in DataContext.CmnCompanies
                                      on sc.CmnCompanyId equals c.Id
                                      where sc.IsPowerUser == true && sc.LoginID == obj.Username
                                      select sc).FirstOrDefault();

                string isCompanyUser = objCompanyUser != null ? "Yes" : "No";

                objLoginResponse = new LoginResponse();

                    objLoginResponse.UserName = user.LoginID;
                    objLoginResponse.UserId = user.Id;
                    objLoginResponse.UserLevel = userType.UserLevel;

                    objLoginResponse.PhotoUrl = objEmp?.PhotoUrl;
                    objLoginResponse.RoleId = objRole.SecRoleId;
                    objLoginResponse.CompanyId = user.CmnCompanyId;

                    objLoginResponse.CompanyTypeShortName = objComType?.ShortName;
                    objLoginResponse.DistrictId = objCom.CmnDistrictId;
                    objLoginResponse.UpazilaId = objCom.CmnUpazillaId;
                    objLoginResponse.UnionId = objCom.CmnUnionId;

                    objLoginResponse.CmnAppSetting = objCmnAppSetting;
                    objLoginResponse.IsAuhentic = true;
                    objLoginResponse.IsCompanyUser = isCompanyUser;
                    objLoginResponse.IsGlobalBalance = objCmnAppSetting.IsGlobalBalance;

                objLoginResponse.LnLoanModelId = objCmnAppSetting.LnLoanModelId;

                objLoginResponse.Message = "Logged In Successfully";
                    return objLoginResponse;

            }
            catch (Exception ex)
            {
                objLoginResponse = new LoginResponse();
                objLoginResponse.IsAuhentic = false;
                objLoginResponse.Message = "Some thing went wrong, please try again later";
                return objLoginResponse;
            }
        }

        //public async Task<SecUser> GetUserBySubscriberId(int subscriberId)
        //{
        //    var objUser = await (from u in DataContext.SecUsers
        //                 join s in DataContext.ScpSubscribers
        //                 on u.LoginID equals s.CustomerNumber
        //                 where s.Id == subscriberId
        //                 select u).FirstOrDefaultAsync();
        //    return objUser;
        //}

        private string GenerateJwtToken(string mobileNo, string Secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("MobileNo", mobileNo.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
                

    }
}
