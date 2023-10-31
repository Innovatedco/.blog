using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Blazor.Blog.Data;
using Blazor.Blog.Models;

namespace Blazor.Blog.Controllers
{
    public class UploadController : Controller
    {
        private readonly IImageUploadService _imageUploadService; 

        public UploadController(IImageUploadService imageUploadService)
        {
            _imageUploadService = imageUploadService;
        }

        [HttpPost("upload/image")]
        [Authorize]
        public IActionResult Image(IFormFile file)
        {
            try
            {
                var extension = _imageUploadService.GetExtension(file);
                var fileName = _imageUploadService.GetFileName(extension);
                _imageUploadService.SaveFile(file, fileName);
                _imageUploadService.SaveThumbnail(file, fileName);
                var url = _imageUploadService.GetUrl(this, fileName);
                return Ok(new { Url = url });
            }
            catch (Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("upload/avatar")]
        [Authorize]
        public IActionResult Avatar(IFormFile file)
        {
            try
            {
                var extension = _imageUploadService.GetExtension(file);
                var fileName = _imageUploadService.GetFileName(extension);
                _imageUploadService.SaveAvatar(file, fileName);
                var url = _imageUploadService.GetAvatarUrl(this, fileName);
                return Ok(new { Url = url });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}