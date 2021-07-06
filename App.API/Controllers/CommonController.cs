using App.API.DTOs;
using App.API.Helper;
using App.Common.Services.Logger;
using App.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace App.API.Controllers
{
    public class CommonController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        protected readonly Ilogger _logger;
        public CommonController(IWebHostEnvironment hostingEnvironment, Ilogger logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }
        [HttpPost]
        [Route("api/[controller]/[action]/{target}")]
        public async Task<ResponseModel<List<UploadFilePathDTO>>> UploadFile(IFormCollection Images, string target)
        {

            try
            {
                List<UploadFilePathDTO> paths = new List<UploadFilePathDTO>();
                string finalName = "";
                string PhysicalfilePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads/" + target + "/");
                if (!Directory.Exists(PhysicalfilePath))
                {
                    Directory.CreateDirectory(PhysicalfilePath);
                }
                foreach (var formFile in Images.Files)
                {
                    finalName = Guid.NewGuid().ToString() + "." + formFile.FileName.Substring(formFile.FileName.LastIndexOf(".") + 1);

                    if (formFile.Length > 0)
                    {
                        paths.Add(new UploadFilePathDTO { URL = string.Concat("/Uploads/" + target + "/", finalName) });
                        using (var stream = new FileStream(PhysicalfilePath + finalName, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                    }
                }
                var responseModel = HelperClass<List<UploadFilePathDTO>>.CreateResponseModel(paths, false, "");
                return responseModel;
            }
            catch (Exception ex)
            {
                _logger.Error("Error occured CommonController\\UploadFile" + " with EX: " + ex.Message);
                return HelperClass<List<UploadFilePathDTO>>.CreateResponseModel(null, true, ex.Message);
            }
        }
    }
}
