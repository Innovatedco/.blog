using Microsoft.AspNetCore.Mvc;
namespace Blazor.Blog.Data
{
    public interface IImageUploadService
    {
        public string GetExtension(IFormFile file);
        public string GetFileName(string extension);
        public void SaveFile(IFormFile file, string fileName);
        public void SaveThumbnail(IFormFile file, string fileName);
        public void SaveAvatar(IFormFile file, string fileName);
        public string GetUrl(Controller controller, string fileName);
        public string GetAvatarUrl(Controller controller, string fileName);
    }
}
