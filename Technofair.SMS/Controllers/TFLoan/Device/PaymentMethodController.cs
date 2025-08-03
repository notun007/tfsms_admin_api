using Technofair.Lib.Model;
using TFSMS.Admin.Model.Accounts;
using TFSMS.Admin.Model.ViewModel.Accounts;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Technofair.Utiity.Security;
using TFSMS.Admin.Model.ViewModel.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TFSMS.Admin.Service.Accounts;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using TFSMS.Admin.Data.Repository.Accounts;

namespace TFSMS.Admin.Controllers.TFLoan.Device
{
    [Route("apiadmin/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private IAnFPaymentMethodService service;
        public PaymentMethodController()
        {
            //New:28072025
            var dbfactory = new AdminDatabaseFactory();
            service = new AnFPaymentMethodService(new AnFPaymentMethodRepository(dbfactory), new AdminUnitOfWork(dbfactory));

            //var dbfactory = new DatabaseFactory();
            //service = new AnFPaymentMethodService(new AnFPaymentMethodRepository(dbfactory), new UnitOfWork(dbfactory));
        }


        #region Payment Method

        [HttpPost("GetAll")]
        public async Task<List<AnFPaymentMethod>> GetAll()
        {
            List<AnFPaymentMethod> list = service.GetAll();
            return list;
        }
        [HttpPost("GetById")]
        public async Task<AnFPaymentMethod> GetByIdAsync(Int16 id)
        {
            AnFPaymentMethod obj = await service.GetByIdAsync(id);
            return obj;
        }

        [HttpPost("GetList")]
        public async Task<List<AnFPaymentMethodViewModel>> GetList()
        {
            List<AnFPaymentMethodViewModel> list = await service.GetList();
            return list;
        }

        [HttpPost("GetActivePaymentMethod")]
        public async Task<List<AnFPaymentMethod>> GetActivePaymentMethod()
        {
            List<AnFPaymentMethod> list = service.GetAll().Where(w => w.IsActive).ToList();
            return list;
        }

        [HttpPost("Save")]
        public async Task<object> Save([FromBody] AnFPaymentMethodViewModel obj)
        {
            Operation objOperation = new Operation { Success = false };
            AnFPaymentMethod objPaymentMethod = new AnFPaymentMethod();

            //obj.Password = AES.GetEncryptedText(objSecUser.Password.Trim());
            //var plainText = AES.GetPlainText(encrptText);

            var objExit = service.GetById(obj.Id);

            if (objExit == null)
            {
                var objPaymentMethodDuplicate = service.GetPaymentMethodByName(obj.Name);
                if (objPaymentMethodDuplicate != null)
                {
                    objOperation.Success = false;
                    objOperation.Message = "This Payment Method Already exist.";
                    return objOperation;
                }

                objPaymentMethod.Id = obj.Id;
                objPaymentMethod.Name = obj.Name;
                objPaymentMethod.UserId = obj.UserId;
                objPaymentMethod.Password = obj.Password;
                objPaymentMethod.AnFFinancialServiceProviderId = obj.AnFFinancialServiceProviderId;
                objPaymentMethod.AnFPaymentChannelId = obj.AnFPaymentChannelId;
                objPaymentMethod.UserId = obj.UserId;
                objPaymentMethod.Password = AES.GetEncryptedText(obj.Password.Trim());
                objPaymentMethod.IsActive = obj.IsActive;                
                objPaymentMethod.CreatedBy = obj.CreatedBy;
                objPaymentMethod.CreatedDate = DateTime.Now;
                objOperation = service.Save(objPaymentMethod);
                objOperation.Message = "Payment Method Created Successfully";
            }
            else if (objExit != null)
            {
                var objPaymentMethodDuplicate = service.GetPaymentMethodByName(obj.Name);
                if (objPaymentMethodDuplicate != null && objPaymentMethodDuplicate.Id != obj.Id)
                {
                    objOperation.Success = false;
                    objOperation.Message = "This Payment Method Already exist.";
                    return objOperation;
                }
                //objExit.Id = obj.Id;
                objExit.Name = obj.Name;
                objExit.AnFFinancialServiceProviderId = obj.AnFFinancialServiceProviderId;
                objExit.AnFPaymentChannelId = obj.AnFPaymentChannelId;
                objExit.IsActive = obj.IsActive;
                objExit.ModifiedBy = obj.ModifiedBy;
                objExit.ModifiedDate = DateTime.Now;
                objOperation = service.Update(objExit);
                objOperation.Message = "Payment Method Update Successfully";
            }
            return objOperation;
        }


        [HttpPost("Delete")]
        public async Task<object> Delete(Int16 Id)
        {
            Operation objOperation = new Operation { Success = false };
            if (Id > 0)
            {
                AnFPaymentMethod obj = service.GetById(Id);
                if (obj != null)
                {
                    objOperation = service.Delete(obj);
                }
            }
            return objOperation;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginViewModel obj)
        {

            Operation objOperation = new Operation();
            string token = string.Empty;

            objOperation = await service.Authenticate(obj);

            if (objOperation != null)
            {
                if (objOperation.Success)
                {
                    token = GenerateJwtToken(obj);

                    return Ok(new
                    {
                        IsAuhentic = objOperation.Success,
                        Message = objOperation.Message,
                        Token = token
                    });
                }
                else
                {
                    return Ok(new
                    {
                        IsAuhentic = objOperation.Success,
                        Message = objOperation.Message,
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
        }

        private string GenerateJwtToken(LoginViewModel objLogin)
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
              new Claim(ClaimTypes.Name, objLogin.UserId),
              new Claim("UserId", Convert.ToString(objLogin.UserId)),
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
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

        #endregion
    }
}