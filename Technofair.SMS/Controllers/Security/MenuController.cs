using Technofair.Data.Infrastructure;
using Technofair.Data.Repository.Security;
using Technofair.Lib.Model;
using TFSMS.Admin.Model.Security;
using TFSMS.Admin.Model.ViewModel.Security;
using Technofair.Service.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TFSMS.Admin.Model.ViewModel.Subscription;
using System.Net.Http.Headers;
using System.ComponentModel.DataAnnotations;
using TFSMS.Admin.Model.ViewModel.Common;
using Microsoft.AspNetCore.Authorization;
using Technofair.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Controllers.Security
{
    [Route("Security/[Controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {

        private ISecMenuService service;
        private readonly ISecModuleService moduleSvc;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public MenuController(IWebHostEnvironment _hostingEnvironment)
        {
            this._hostingEnvironment = _hostingEnvironment;
            var dbfactory = new AdminDatabaseFactory();
            service = new SecMenuService(new SecMenuRepository(dbfactory), new AdminUnitOfWork(dbfactory));
            moduleSvc = new SecModuleService(new SecModuleRepository(dbfactory), new AdminUnitOfWork(dbfactory));
        }


        [Authorize(Policy = "Authenticated")]
        [HttpPost("AddOrUpdateMenu")]
        public async Task<SecMenu> AddMenu([FromBody] MenuViewModel data)
        {
            SecMenu obj = new SecMenu();
            obj.Id = data.Id;
            obj.TitleBn = data.TitleBn;
            obj.Title = data.Title;
            obj.Link = data.Link;
            obj.ParentMenuId = data.ParentMenuId;
            obj.SecModuleId = data.SecModuleId;
            obj.ParentSerialNo = data.ParentSerialNo;
            obj.ChildSerialNo = data.ChildSerialNo;
            obj.LevelNo = data.LevelNo;
            obj.IsParent = data.IsParent;
            obj.Icon = data.Icon;
            obj.IsActive = data.IsActive;
            obj.IsModule = obj.IsModule;
            obj.CreatedBy = obj.CreatedBy;
            obj.CreatedDate = DateTime.Now;
            if (obj.Id == 0)
            {
                service.Save(obj);
                return null;
            }else
            {
                return await service.Update(obj);
            }
           
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("Update")]
        public async Task<SecMenu> Update([FromBody] SecMenu obj)
        {
            return await service.Update(obj);
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetMenus")]
        public async Task<List<SecMenu>> GetMenus()
        {
            return await service.GetMenus();
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetMenuById")]
        public SecMenu GetMenuById(int id)
        {
            return service.GetById(id);
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("Modules")]
        public async Task<List<SecModule>> Modules()
        {
            return moduleSvc.GetAll();
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetModuleById")]
        public async Task<SecModule> GetModule(int id)
        {
            return  moduleSvc.GetById(id);
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("AddModule")]
        public Operation AddModule([FromBody] SecModule ob)
        {
            return moduleSvc.Save(ob);
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("UpdateModule")]
        public Operation UpdateModule([FromBody] SecModule ob)
        {
            return moduleSvc.Update(ob);
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile()//IFormFile file
        {
            try
            {
                  if (!Request.Form.Files.Any())
                    return BadRequest("No files found in the request");

                if (Request.Form.Files.Count > 1)
                    return BadRequest("Cannot upload more than one file at a time");

                if (Request.Form.Files[0].Length <= 0)
                    return BadRequest("Invalid file length, seems to be empty");
                string webRootPath = _hostingEnvironment.WebRootPath;
                string uploadsDir = Path.Combine(webRootPath, "puchaseOrderExcel");

                // wwwroot/uploads/
                if (!Directory.Exists(uploadsDir))
                    Directory.CreateDirectory(uploadsDir);

                IFormFile file = Request.Form.Files[0];
                string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                string fullPath = Path.Combine(uploadsDir, fileName);

                var buffer = 1024 * 1024;
                using var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, buffer, useAsync: false);
                await file.CopyToAsync(stream);
                await stream.FlushAsync();

                string location = $"images/{fileName}";

                var result = new
                {
                    message = "Upload successful",
                    url = location
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Upload failed: " + ex.Message);
            }
        }
    }
}