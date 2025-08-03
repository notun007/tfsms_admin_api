using Technofair.Data.Infrastructure;
using Technofair.Data.Repository.HRM;
using Technofair.Data.Repository.Security;
using Technofair.Lib.Model;
using TFSMS.Admin.Model.HRM;
using TFSMS.Admin.Model.Security;
using TFSMS.Admin.Model.ViewModel.Common;
using TFSMS.Admin.Model.ViewModel.Security;
using Technofair.Service.HRM;
using Technofair.Service.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TFSMS.Admin.Model.Common;
using Technofair.SMS;
using Microsoft.Extensions.Options;
using TFSMS.Admin.Models;
using Technofair.Lib.Utilities;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Technofair.Utiity.Security;
using Technofair.Service.TFAdmin;
using Technofair.Data.Repository.TFAdmin;
using Technofair.Data.Infrastructure.TFAdmin;
using TFSMS.Admin.Controllers.Security;
using Technofair.Service.Common;
using Technofair.Data.Repository.Common;
using TFSMS.Admin.Model.TFAdmin;
using Technofair.Utiity.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting.Internal;
using Technofair.Utiity.Log;
using Technofair.Service.TFLoan.Device;
using Technofair.Data.Repository.TFLoan.Device;


[Route("Security/[Controller]")]
public class UserController : ControllerBase
{
    private readonly string TFAdminBaseUrl = string.Empty;

    private ISecUserService service;
    private ISecUserRoleService serviceUserRole;
    private ISecCompanyUserService serviceComUser;
    private IHrmEmployeeService serviceEmployee;
    //private ISecUserVisitorService serviceUserVisitor;
    private ITFAClientServerInfoService serviceClientServerInfo;
    private ICmnAppSettingService serviceAppSetting;
    private ITFACompanyCustomerService serviceCompanyCustomerService;
    private IWebHostEnvironment hostingEnvironment;
    IConfiguration configuration;

    private readonly ITFLogger _logger;

    //private readonly JwtConfig

    private readonly AppSettings _appSettings;
    public UserController(IConfiguration _configuration, IWebHostEnvironment _hostingEnvironment, IOptions<AppSettings> appSettings, ITFLogger logger)
    {
        this.configuration = _configuration;
        TFAdminBaseUrl = configuration.GetValue<string>("TFAdmin:BaseUrl");

        var adminDbfactory = new AdminDatabaseFactory();
        var dbfactory = new AdminDatabaseFactory();
        service = new SecUserService(new SecUserRepository(dbfactory), new AdminUnitOfWork(dbfactory));

        serviceUserRole = new SecUserRoleService(new SecUserRoleRepository(dbfactory), new AdminUnitOfWork(dbfactory));

        serviceComUser = new SecCompanyUserService(new SecCompanyUserRepository(dbfactory), new AdminUnitOfWork(dbfactory));
        serviceEmployee = new HrmEmployeeService(new HrmEmployeeRepository(dbfactory), new AdminUnitOfWork(dbfactory));
       
        serviceClientServerInfo = new TFAClientServerInfoService(new TFAClientServerInfoRepository(adminDbfactory), new AdminUnitOfWork(adminDbfactory));

        serviceAppSetting = new CmnAppSettingService(new CmnAppSettingRepository(dbfactory), new AdminUnitOfWork(dbfactory));
        serviceCompanyCustomerService = new TFACompanyCustomerService(new TFACompanyCustomerRepository(adminDbfactory), new AdminUnitOfWork(adminDbfactory));
       
        hostingEnvironment = _hostingEnvironment;
        if (string.IsNullOrWhiteSpace(hostingEnvironment.WebRootPath))
        {
            hostingEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        }
        _appSettings = appSettings.Value;
        this._logger = logger;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthenticateUser obj)
    {
        //LoginResponse response = new LoginResponse();
        var appSettings = await serviceAppSetting.GetCmnAppSetting();
        var response = await service.Authenticate(obj, _appSettings.Secret);//
        // Validate user credentials (e.g., check against a database)
        if (obj.Username == "woahiduzzaman17@gmail.com" && obj.UserPassword == "123")
        {

            //New
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, obj.Username),
                    new Claim("UserId", response.UserName), // Custom 

                    new Claim("UserName", response.UserName), 
                    //new Claim("UserId", Convert.ToString(response.UserId)),
                    new Claim("UserLevel", Convert.ToString(response.UserLevel)),
                    new Claim("PhotoUrl", response.PhotoUrl),
                    new Claim("RoleId", Convert.ToString(response.RoleId)),
                    new Claim("CompanyId", Convert.ToString(response.CompanyId)),
                    new Claim("CompanyTypeShortName", Convert.ToString(response.CompanyTypeShortName)),
                    new Claim("DistrictId", Convert.ToString(response.DistrictId)),
                    new Claim("UpazilaId", Convert.ToString(response.UpazilaId)),
                    new Claim("UnionId", Convert.ToString(response.UnionId)),

                    //AppSetting
                    new Claim("ApplicationId", Convert.ToString(response.CmnAppSetting.ApplicationId)),
                    new Claim("AllowAutoSubscriberNumber", response.CmnAppSetting.AllowAutoSubscriberNumber == true ? "YES" : "NO"),
                    new Claim("SubscriberNumberLength", Convert.ToString(response.CmnAppSetting.SubscriberNumberLength)),

                    new Claim("AllowPurchase", response.CmnAppSetting.AllowPurchase == true ? "YES" : "NO"),
                    new Claim("AllowSale", response.CmnAppSetting.AllowSale == true ? "YES" : "NO"),
                    new Claim("AllowRenewableArrear", response.CmnAppSetting.AllowRenewableArrear == true ? "YES" : "NO"),
                    new Claim("AllowMigration", response.CmnAppSetting.AllowMigration == true ? "YES" : "NO"),
                    new Claim("IsProduction", response.CmnAppSetting.IsProduction == true ? "YES" : "NO"),
                    new Claim("AppKey", response.CmnAppSetting.AppKey),
                    //AppSettings

                    //Result
                    new Claim("IsAuhentic", Convert.ToString(response.IsAuhentic)),
                    new Claim("Message", response.Message)

                }),

                Expires = DateTime.Now.AddDays(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("YourSecretKeyHereYourSecretKeyHere")), SecurityAlgorithms.HmacSha256),
                Issuer = "http://localhost:5269/",
                Audience = "http://localhost:5269/"
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(new { Token = tokenString });
                        
        }
        return Unauthorized();
    }

  
    [AllowAnonymous]
    [HttpPost("Authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticateUser obj)
    {
        #region Application Verification
        string token = string.Empty;

        try
        {
            var text = "Data Source=103.183.116.14\\MSSQLSERVER02;Initial Catalog=tfsmsdb_dev;Persist Security Info=True;User ID=sa;Password=smSdBTf@25;MultipleActiveResultSets=True";

            //var connectionString = "Server=Server=103.118.78.242;Database=tfsmsdb_dev;Persist Security Info=True;User ID=sa;Password=tfsunsms;MultipleActiveResultSets=True;TrustServerCertificate=True;";
            //var connectionString = "Server=103.183.116.14\\MSSQLSERVER02;Database=tfadmindb_live;Persist Security Info=True;User ID=sa;Password=smSdBTf@25;MultipleActiveResultSets=True;TrustServerCertificate=True;";
            var connectionString = "Server=103.183.116.14\\MSSQLSERVER02;Database=tfsmsdb_madbor_v3;Persist Security Info=True;User ID=sa;Password=smSdBTf@25;MultipleActiveResultSets=True;TrustServerCertificate=True;";

            //var connectionString = "Server=103.118.76.238;Database=tfsmsdb_sun;Persist Security Info=True;User ID=sa;Password=tfsunsms;MultipleActiveResultSets=True;TrustServerCertificate=True;";


            var encrptText = AES.GetEncryptedText(connectionString);
            var plainText = AES.GetPlainText(encrptText);

            _logger.LogError("Logged In User: " + obj.Username);           
            obj.Username = obj.Username.Trim();
            obj.UserPassword = obj.UserPassword.Trim();
                        
            //var ip = HttpContext.Connection.LocalIpAddress?.ToString();
            //_logger.LogError("Remote IP Address: " + ip);

            var appSettings = await serviceAppSetting.GetCmnAppSetting();

            if (appSettings.IsProduction)
            {
                if (appSettings.ApplicationId == 2) //1: TFAdmin; 2: SMS
                {
                    var adminUrl = TFAdminBaseUrl + "api/TFAClientPaymentDetail/VerifyClientPackageByAppKey?appKey=" + appSettings.AppKey;

                    var objPaymentDetail = await Request<Operation, Operation>.GetObject(adminUrl);

                    if (objPaymentDetail == null)
                    {                      
                        return Ok(new
                        {
                            IsAuhentic = false,
                            Message = "Something went wrong, Failed verying client information, please try again later",
                            Token = string.Empty
                        });
                    }

                    if (objPaymentDetail.Success == false)
                    {
                        return Ok(new
                        {
                            IsAuhentic = false,
                            Message = objPaymentDetail.Message,
                            Token = string.Empty
                        });
                    }

                    if (!objPaymentDetail.Success)
                    {                        
                        return Ok(new
                        {
                            IsAuhentic = false,
                            Message = objPaymentDetail.Message,
                            Token = string.Empty
                        });
                    }

                   var clientServerInfo = serviceClientServerInfo.ReadServerInfo();
                    
                   adminUrl = TFAdminBaseUrl + "api/TFACompanyCustomer/GetCompanyCustomerByAppKey?appKey=" + appSettings.AppKey;

                    var objCompanyCustomer = await Request<TFACompanyCustomer, TFACompanyCustomer>.GetObject(adminUrl);
                                        
                    bool serverIPExists = Array.Exists(clientServerInfo.ServerIPList, element => element == objCompanyCustomer.ServerIP);
                    bool motherBoardExists = Array.Exists(clientServerInfo.MotherBoardList, element => element == objCompanyCustomer.MotherBoardId);
                    bool networkAdapterExists = Array.Exists(clientServerInfo.NetworkAdapterList, element => element == objCompanyCustomer.NetworkAdapterId);

                    if (!(serverIPExists && motherBoardExists && networkAdapterExists))
                    {
                     return Ok(new
                      {
                       IsAuhentic = false,
                       Message = "Could not find Server Information",
                       Token = string.Empty
                      });

                    }
                }
            }
           
        }
        catch(Exception ex)
        {
            ////Log: Start
            try
            {
                _logger.LogError(ex.Message);
            }
            catch
            {
            }
            ////Log: End

            return Ok(new 
            { 
                   IsAuhentic = false, 
                   Message = "Could not find Server Information", 
                   Token = string.Empty
            });
        }



        #endregion
        //Old: Login Process

        //try
        //{

            var response = await service.Authenticate(obj, _appSettings.Secret);

            if (response != null)
            {
                if (response.IsAuhentic)
                {
                    token = GenerateJwtToken(response);
                    //return Ok(new { Token = token });

                    return Ok(new
                    {
                        IsAuhentic = response.IsAuhentic,
                        Message = response.Message,
                        Token = token
                    });
                }
                else
                {
                    return Ok(new
                    {
                        IsAuhentic = response.IsAuhentic,
                        Message = response.Message,
                        Token = string.Empty
                    });
                }
            }
            else
            {
                return Ok(new
                {
                    IsAuhentic = false,
                    Message = "Unable to Authenticate",
                    Token = string.Empty
                });
            }
        //}
        //catch(Exception exp)
        //{
        //    return Ok(new
        //    {
        //        IsAuhentic = false,
        //        Message = exp.Message,
        //        Token = string.Empty
        //    });
        //}
    }
        
    private string GenerateJwtToken(LoginResponse response)
    {
        string tokenString = string.Empty;
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, response.UserName),
                    new Claim("UserId", Convert.ToString(response.UserId)),
                    new Claim("UserName", response.UserName),
                    new Claim("UserLevel", Convert.ToString(response.UserLevel)),
                    new Claim("PhotoUrl", response.PhotoUrl == null ? "" : response.PhotoUrl),
                    new Claim("RoleId", Convert.ToString(response.RoleId)),
                    new Claim("CompanyId", Convert.ToString(response.CompanyId)),
                    new Claim("CompanyTypeShortName", Convert.ToString(response.CompanyTypeShortName)),
                    new Claim("DistrictId", response.DistrictId == null ? "0" : Convert.ToString(response.DistrictId)),
                    new Claim("UpazilaId", response.UpazilaId == null ? "0" : Convert.ToString(response.UpazilaId)),
                    new Claim("UnionId", response.UnionId == null ? "0" : Convert.ToString(response.UnionId)),

                    //AppSetting
                    new Claim("ApplicationId", Convert.ToString(response.CmnAppSetting.ApplicationId)),
                    new Claim("AllowAutoSubscriberNumber", response.CmnAppSetting.AllowAutoSubscriberNumber == true ? "YES" : "NO"),
                    new Claim("SubscriberNumberLength", Convert.ToString(response.CmnAppSetting.SubscriberNumberLength)),

                    new Claim("AllowPurchase", response.CmnAppSetting.AllowPurchase == true ? "YES" : "NO"),
                    new Claim("AllowSale", response.CmnAppSetting.AllowSale == true ? "YES" : "NO"),
                    new Claim("AllowRenewableArrear", response.CmnAppSetting.AllowRenewableArrear == true ? "YES" : "NO"),
                    new Claim("AllowMigration", response.CmnAppSetting.AllowMigration == true ? "YES" : "NO"),
                    new Claim("IsProduction", response.CmnAppSetting.IsProduction == true ? "YES" : "NO"),
                    new Claim("AppKey", response.CmnAppSetting.AppKey),
                    //AppSettings

                    new Claim("LnLoanModelId", response.LnLoanModelId == null ? "0" : Convert.ToString(response.LnLoanModelId)),

                    //Result
                    new Claim("IsCompanyUser", response.IsCompanyUser),
                    new Claim("IsAuhentic", Convert.ToString(response.IsAuhentic)),
                    new Claim("Message", response.Message)

                }),

                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("YourSecretKeyHereYourSecretKeyHere")), SecurityAlgorithms.HmacSha256),
                Issuer = "http://localhost:5269/",
                Audience = "http://localhost:5269/"
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            tokenString = tokenHandler.WriteToken(token);
        }
        catch (Exception exp)
        {
        }
        return tokenString;
    }



    [Authorize(Policy = "Authenticated")]
    [HttpGet("VerifyClientByAppKey")]
    public async Task<Operation> VerifyClientByAppKey(string appKey)
    {
        Operation objOperation = new Operation();

        var appSettings = await serviceAppSetting.GetCmnAppSetting();

        if (appSettings.IsProduction)
        {
            var adminUrl = TFAdminBaseUrl + "api/TFAClientPaymentDetail/VerifyClientPackageByAppKey?appKey=" + appKey;

            objOperation = await Request<Operation, Operation>.GetObject(adminUrl);
        }
        else
        {
            objOperation.Success = true;
            objOperation.Message = "Development Environment";
        }
        return objOperation;
        
    }


    [Authorize(Policy = "Authenticated")]
    [HttpPost("User/{id:int}")]
    public async Task<SecUser> GetById(int id)
    {
        return  service.GetById(id);
    }

    [Authorize(Policy = "Authenticated")]
    [HttpPost("GetAll")]
    public async Task<List<SecUser>> GetAll()
    {
        return service.GetAll();
    }

    //[HttpPost("GetUserByCompanyId")]
    ////[HttpPost("GetUserByCompanyId/{companyId:int}")]
    //public async Task<List<SecUser>> GetUserByCompanyId(int companyId)
    //{
    //    return service.GetUserByCompanyId(companyId);
    //}

    [Authorize(Policy = "Authenticated")]
    [HttpPost("GetSecUsersByCompanyAndUserLevel")]
    public async Task<List<SecUserViewModel>> GetSecUsersByCompanyAndUserLevel(int cmnCompanyId, int userLevel)
    {
        return service.GetSecUsersByCompanyAndUserLevel(cmnCompanyId, userLevel);
    }

    //New
    //[HttpPost("GetSecUsersByEmployeeId")]
    //public async Task<SecUser> GetSecUsersByEmployeeId(int employeeId)
    //{
    //    return await service.GetSecUsersByEmployeeId(employeeId);
    //}

    [Authorize(Policy = "Authenticated")]
    [HttpPost("GetUserByLoginId")]
    //New
    public async Task<SecUser> GetUserByLoginId(string loginId)
    //New
    //Old
    //public async Task<List<CompanyUserViewModel>> GetUserByLoginId(SecUserViewModel obj)
    {
        //New
        SecUser user = service.GetByLoginID(loginId);

        //Old and Commented On 08.09.2024
        //List<CompanyUserViewModel> list = new List<CompanyUserViewModel>();

        //SecUser user = service.GetByLoginID(loginId);

        //if (user != null)
        //{
        //    if (user.IsActive == true)
        //    {
        //        if (user.Password.ToUpper() == obj.Password.ToUpper())
        //        {
        //            list = serviceComUser.GetCompanyUserByUserId(user.Id);
        //        }
        //    }
        //}
        return user;
    }

    [Authorize(Policy = "Authenticated")]
    [HttpPost("GetSecUserByLoginId")]
    public async Task<SecUserViewModel> GetSecUserByLoginId(string loginId)
    {
        SecUserViewModel objUserVm = new SecUserViewModel();

        SecUser user = service.GetByLoginID(loginId);

        if (user != null)
        {
            if (user.IsActive == true)
            {
                objUserVm.LoginID = user.LoginID;
                //New
                objUserVm.Password = AES.GetPlainText(user.Password);
                //Old
                //objUserVm.Password = AES.Decrypt(user.Password);
            }
        }
        return objUserVm;
    }


    private int GetRoleIdByUserId(int usrId)
    {
        int roleId = 0;
        if (usrId > 0)
        {
            SecUserRole obj = serviceUserRole.GetUserRoleByUserId(usrId);
            if (obj != null)
            {
                roleId = obj.SecRoleId;
            }
        }
        return roleId;
    }

    [Authorize(Policy = "Authenticated")]
    [HttpPost("GetUsers")]
    public async Task<List<SecUser>> GetUsers()
    {
        var list = service.GetAll().Select(su => new SecUser
        {
            Id = su.Id,
            LoginID = su.LoginID,
            IsActive = su.IsActive,
            HrmEmployeeId = su.HrmEmployeeId,
            CmnCompanyId = su.CmnCompanyId
        }).ToList();
        return list;
    }

    [Authorize(Policy = "Authenticated")]
    [HttpPost("Save")]
    //New
    public async Task<Operation> Save([FromBody] SecUserViewModel objSecUser)
    {
        Operation objOperation = new Operation { Success = false };
        
        SecUser obj = new SecUser();
        obj.Id = objSecUser.Id;
        obj.SecUserTypeId = objSecUser.SecUserTypeId;
        //New: 10.20.2024
        obj.IsPowerUser = false;
        obj.HrmEmployeeId = objSecUser.HrmEmployeeId;
        obj.CmnCompanyId = Convert.ToInt32(objSecUser.CmnCompanyId);
        obj.LoginID = objSecUser.LoginID.Trim();
        obj.Password = AES.GetEncryptedText(objSecUser.Password.Trim());
        obj.OriginalPassword = obj.Password;
        obj.IsActive = objSecUser.IsActive == true ? true : false;
        obj.LevelNo = objSecUser.LevelNo;
        obj.CreatedBy = Convert.ToInt32(objSecUser.CreatedBy);
        obj.CreatedDate = DateTime.Now;
        obj.ModifiedBy = objSecUser.ModifiedBy;
        obj.ModifiedDate = objSecUser.ModifiedDate;
        
        try
        {
            if (obj.Id == 0)//&& ButtonPermission.Add
            {
                SecUser objExist = service.GetByLoginID(obj.LoginID.Trim());
                if (objExist == null)
                {
                    var user = await service.GetSecUsersByEmployeeId(Convert.ToInt32(obj.HrmEmployeeId));

                    if (user != null)
                    {
                        objOperation.Success = false;
                        objOperation.Message = "User Already exist for this employee";
                        return objOperation;
                    }

                    string orgPass = obj.Password;
                    obj.OriginalPassword = orgPass;
                    objOperation = service.Save(obj);
                }
                else
                {
                    objOperation.Success = false;
                    objOperation.Message = "User Already Exist";
                }
            }
            else if (obj.Id > 0)
            {
                string orgPass = obj.Password;
                obj.OriginalPassword = orgPass;
                objOperation = service.Update(obj);
            }
            if (objOperation.Success)
            {
                SecCompanyUser objExist = serviceComUser.GetByUserId(obj.Id);
                if (objExist == null)
                {
                    objExist = new SecCompanyUser();
                    objExist.CmnCompanyId = (int)obj.CmnCompanyId;
                    objExist.SecUserId = obj.Id;
                    objExist.IsActive = true;
                    serviceComUser.Save(objExist);
                }

                objOperation.Success = true;
                objOperation.Message = "User Saved Successfully";
            }
        }
        catch(Exception exp)
        {
            objOperation.Success = false;
            objOperation.Message = "Unable to Save User";
        }
        return objOperation; 
    }

    [Authorize(Policy = "Authenticated")]
    [HttpPost("Delete/{id:int}")]
    public async Task<Operation> Delete(int id)
    {
        Operation objOperation = new Operation() { Success = false };
        if (id > 0)//&& ButtonPermission.Delete
        {
            SecUser obj = service.GetById(id);
            if (obj != null)
            {
                objOperation = service.Delete(obj);
            }
        }
        return objOperation;
    }

    [Authorize(Policy = "Authenticated")]
    [HttpPost("GetUserInfoByCompanyId")]
    public async Task<List<SecUserViewModel>> GetUserInfoByCompanyId(int companyId)
    {
        List<SecUserViewModel> list = service.GetUserInfoByCompanyId(companyId);
        return list;
    }

    [Authorize(Policy = "Authenticated")]
    [HttpPost("GetUserByAnyKey")]
    public List<SecUserViewModel> GetUserByAnyKey(Int16? cmnCompanyTypeId, int? companyId, Int16 userLevel)
    {
        List<SecUserViewModel> list = service.GetUserByAnyKey(cmnCompanyTypeId, companyId, userLevel);
        return list;
    }

    [Authorize(Policy = "Authenticated")]
    [HttpPost("GetUserByCompanyId")]
    public async Task<List<SecUserViewModel>> GetUserByCompanyId(int companyId, Int16 userLevel)
    {
        List<SecUserViewModel> list = service.GetUserByCompanyId(companyId, userLevel);
        return list;
    }

    [Authorize(Policy = "Authenticated")]
    [HttpPost("GetUserInfoById")]
    public async Task<SecUserViewModel> GetUserInfoById(int userId)
    {
        SecUserViewModel obj = service.GetUserInfoById(userId);
        return obj;
    }

    [Authorize(Policy = "Authenticated")]
    [HttpPost("ChangeUser")]
    public async Task<Operation> ChangeUser([FromBody] SecUserViewModel objSecUser, int userId)
    {
        Operation objOperation = new Operation { Success = false };

        SecUser obj = new SecUser();
        obj.Id = objSecUser.Id;
        obj.SecUserTypeId = objSecUser.SecUserTypeId;
        obj.HrmEmployeeId = objSecUser.HrmEmployeeId;
        obj.CmnCompanyId = Convert.ToInt32(objSecUser.CmnCompanyId);
        obj.LoginID = objSecUser.LoginID;
        //New
        obj.Password = AES.GetEncryptedText(objSecUser.Password);
        //Old
        //obj.Password = AES.Encrypt(objSecUser.Password);
        obj.OriginalPassword = obj.Password;
        obj.IsActive = objSecUser.IsActive == true ? true : false;
        obj.LevelNo = objSecUser.LevelNo;
        obj.CreatedBy = Convert.ToInt32(objSecUser.CreatedBy);
        obj.CreatedDate = DateTime.Now;
        obj.ModifiedBy = objSecUser.ModifiedBy;
        obj.ModifiedDate = objSecUser.ModifiedDate;
        //End

        if (obj.Id > 0  && obj.HrmEmployeeId != null)//&& ButtonPermission.Edit
        {
            SecUser objExist = service.GetById(obj.Id);
            if (objExist != null)
            {
                objExist.Password = obj.Password;
                objExist.OriginalPassword = obj.Password;
                objExist.ModifiedBy = userId;
                objExist.ModifiedDate = DateTime.Now;
                objOperation = service.Update(objExist);
                //}
                //else
                //{
                //    objOperation.OperationId = -1;
                //}
            }
        }
        return objOperation;
    }        

    
}
