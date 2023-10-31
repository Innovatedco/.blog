using Microsoft.AspNetCore.Mvc;

namespace Blazor.Blog.Data
{
    public class ImageUploadService : IImageUploadService
    {
        private readonly IWebHostEnvironment _environment;
        public ImageUploadService(IWebHostEnvironment environment) 
        {
            _environment = environment;
        }

        /// <summary>
        /// returns the file extension for the posted image
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public string GetExtension(IFormFile file)
        {
            return Path.GetExtension(file.FileName);
        }

        /// <summary>
        /// Generates and returns a guid based file name for the image
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public string GetFileName(string extension)
        {
            return $"{Guid.NewGuid()}{extension}";
        }

        /// <summary>
        /// Gets the url for the uploaded image
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetUrl(Controller controller, string fileName)
        {
            string url = controller.Url.Content($"~/upload/image/{fileName}");
            return url;
        }

        /// <summary>
        /// Gets the url for the uploaded avatar
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetAvatarUrl(Controller controller, string fileName)
        {
            string url = controller.Url.Content($"~/upload/image/author/small-{fileName}");
            return url;
        }

        /// <summary>
        /// Saves the file to upload\image with the guid based name and extension
        /// </summary>
        /// <param name="file"></param>
        /// <param name="environment"></param>
        /// <param name="fileName"></param>
        public void SaveFile(IFormFile file, string fileName)
        {
            using (var stream = new FileStream(Path.Combine(_environment.WebRootPath + "\\upload\\image", fileName), FileMode.Create))
            {
                // Save the file
                file.CopyTo(stream);
            }
        }

        /// <summary>
        /// Saves a cropped thumbnail version of the file 250px X 250px to upload\image\thumbnail with the small- prefix
        /// </summary>
        /// <param name="file"></param>
        /// <param name="environment"></param>
        /// <param name="fileName"></param>
        public void SaveThumbnail(IFormFile file, string fileName)
        {
            using var image = Image.Load(file.OpenReadStream());
            {
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(250, 250),
                    Mode = ResizeMode.Crop
                }));
                image.Save(Path.Combine(_environment.WebRootPath + "\\upload\\image\\thumb", "small-" + fileName));
            }
        }

        /// <summary>
        /// Saves a cropped thumbnail version of the file 250px X 250px to upload\image\thumbnail with the small- prefix
        /// </summary>
        /// <param name="file"></param>
        /// <param name="environment"></param>
        /// <param name="fileName"></param>
        public void SaveAvatar(IFormFile file, string fileName)
        {
            using var image = Image.Load(file.OpenReadStream());
            {
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(32, 32),
                    Mode = ResizeMode.Crop
                }));
                image.Save(Path.Combine(_environment.WebRootPath + "\\upload\\image\\author", "small-" + fileName));
            }
        }
    }
}
