using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOS.Base.Response;

namespace MOS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FileController : ControllerBase
    {

        public FileController()
        {

        }

        [HttpPost("UploadFile")]
        public async Task<ApiResponse<string>> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return new ApiResponse<string>(message: "Geçersiz dosya.");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            string imageUrl = Url.Content("~/uploads/" + uniqueFileName);
            return new ApiResponse<string>(data: imageUrl);
        }

        [HttpGet("downloadByPath")]
        public IActionResult DownloadFileByPath(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return BadRequest("Geçersiz dosya yolu.");

            // Dosyanın tam yolu
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath.TrimStart('/'));

            // Dosya var mı kontrol etme
            if (!System.IO.File.Exists(fullPath))
                return NotFound("Dosya bulunamadı.");

            // Dosyanın içeriğini döndürme
            var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            return new FileStreamResult(fileStream, "application/octet-stream")
            {
                FileDownloadName = Path.GetFileName(fullPath)
            };
        }

    }
}