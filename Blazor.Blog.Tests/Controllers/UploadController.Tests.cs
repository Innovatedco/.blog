using Moq;
using Blazor.Blog.Data;
using System;
using Blazor.Blog.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.Blog.Tests.Controllers
{
    public class UploadControllerTests : TestContext
    {
        [Fact(DisplayName = "Should upload an image")]
        public void ShouldUploadAnImage()
        {
            // Arrange
            var mockImageUploadService = new Mock<IImageUploadService>();
            var mockImage = new Mock<Microsoft.AspNetCore.Http.IFormFile>();
            mockImage.Setup(image => image.FileName).Returns("image.jpg");
            var extension = ".jpg";
            var fileName = Guid.NewGuid().ToString() + extension;
            var fileNameWithPath = "upload/image/" + fileName;

            var controller = new UploadController(mockImageUploadService.Object);
            mockImageUploadService.Setup(service => service.GetUrl(controller, fileName))
                .Returns(fileNameWithPath);
            mockImageUploadService.Setup(service => service.GetExtension(mockImage.Object)).Returns(".jpg");
            mockImageUploadService.Setup(service => service.GetFileName(".jpg")).Returns(fileName);

            // Act
            var result = controller.Image(mockImage.Object);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var okResultObject = okResult!.Value;
        }

        [Fact(DisplayName = "Should return 500 if image upload fails")]
        public void ShouldReturn500IfImageUploadFails()
        {
            // Arrange
            var mockImageUploadService = new Mock<IImageUploadService>();
            var mockImage = new Mock<Microsoft.AspNetCore.Http.IFormFile>();
            mockImage.Setup(image => image.FileName).Returns("image.jpg");
            var extension = ".jpg";
            var fileName = Guid.NewGuid().ToString() + extension;
            var fileNameWithPath = "upload/image/" + fileName;

            var controller = new UploadController(mockImageUploadService.Object);
            mockImageUploadService.Setup(service => service.GetUrl(controller, fileName))
                .Returns(fileNameWithPath);
            mockImageUploadService.Setup(service => service.GetExtension(mockImage.Object)).Returns(".jpg");
            mockImageUploadService.Setup(service => service.GetFileName(".jpg")).Returns(fileName);
            mockImageUploadService.Setup(service => service.SaveFile(mockImage.Object, fileName)).Throws(new Exception("failed to upload image"));

            // Act
            var result = controller.Image(mockImage.Object);
            var objectResult = result as ObjectResult;

            // Assert
            Assert.Equal(500, objectResult!.StatusCode);
        }

        [Fact(DisplayName = "Should upload an avatar")]
        public void ShouldUploadAnAvatar()
        {
            // Arrange
            var mockImageUploadService = new Mock<IImageUploadService>();
            var mockImage = new Mock<Microsoft.AspNetCore.Http.IFormFile>();
            mockImage.Setup(image => image.FileName).Returns("image.jpg");
            var extension = ".jpg";
            var fileName = Guid.NewGuid().ToString() + extension;
            var fileNameWithPath = "upload/image/avatar/" + fileName;

            var controller = new UploadController(mockImageUploadService.Object);
            mockImageUploadService.Setup(service => service.GetAvatarUrl(controller, fileName))
                .Returns(fileNameWithPath);
            mockImageUploadService.Setup(service => service.GetExtension(mockImage.Object)).Returns(".jpg");
            mockImageUploadService.Setup(service => service.GetFileName(".jpg")).Returns(fileName);

            // Act
            var result = controller.Avatar(mockImage.Object);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var okResultObject = okResult!.Value;
        }

        [Fact(DisplayName = "Should return 500 if avatar upload fails")]
        public void ShouldReturn500IfAvatarUploadFails()
        {
            // Arrange
            var mockImageUploadService = new Mock<IImageUploadService>();
            var mockImage = new Mock<Microsoft.AspNetCore.Http.IFormFile>();
            mockImage.Setup(image => image.FileName).Returns("image.jpg");
            var extension = ".jpg";
            var fileName = Guid.NewGuid().ToString() + extension;
            var fileNameWithPath = "upload/image/avatar/" + fileName;

            var controller = new UploadController(mockImageUploadService.Object);
            mockImageUploadService.Setup(service => service.GetAvatarUrl(controller, fileName))
                .Returns(fileNameWithPath);
            mockImageUploadService.Setup(service => service.GetExtension(mockImage.Object)).Returns(".jpg");
            mockImageUploadService.Setup(service => service.GetFileName(".jpg")).Returns(fileName);
            mockImageUploadService.Setup(service => service.SaveAvatar(mockImage.Object, fileName)).Throws(new Exception("failed to upload image"));

            // Act
            var result = controller.Avatar(mockImage.Object);
            var objectResult = result as ObjectResult;

            // Assert
            Assert.Equal(500, objectResult!.StatusCode);
        }

        [Fact(DisplayName = "Should upload a logo")]
        public void ShouldUploadALogo()
        {
            // Arrange
            var mockImageUploadService = new Mock<IImageUploadService>();
            var mockImage = new Mock<Microsoft.AspNetCore.Http.IFormFile>();
            mockImage.Setup(image => image.FileName).Returns("image.jpg");
            var extension = ".jpg";
            var fileName = Guid.NewGuid().ToString() + extension;
            var fileNameWithPath = "upload/image/logo/" + fileName;

            var controller = new UploadController(mockImageUploadService.Object);
            mockImageUploadService.Setup(service => service.GetLogoUrl(controller, fileName))
                .Returns(fileNameWithPath);
            mockImageUploadService.Setup(service => service.GetExtension(mockImage.Object)).Returns(".jpg");
            mockImageUploadService.Setup(service => service.GetFileName(".jpg")).Returns(fileName);

            // Act
            var result = controller.Logo(mockImage.Object);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var okResultObject = okResult!.Value;
        }

        [Fact(DisplayName = "Should return 500 if a logo upload fails")]
        public void ShouldReturn500IfALogoUploadFails()
        {
            // Arrange
            var mockImageUploadService = new Mock<IImageUploadService>();
            var mockImage = new Mock<Microsoft.AspNetCore.Http.IFormFile>();
            mockImage.Setup(image => image.FileName).Returns("image.jpg");
            var extension = ".jpg";
            var fileName = Guid.NewGuid().ToString() + extension;
            var fileNameWithPath = "upload/image/logo/" + fileName;

            var controller = new UploadController(mockImageUploadService.Object);
            mockImageUploadService.Setup(service => service.GetLogoUrl(controller, fileName))
                .Returns(fileNameWithPath);
            mockImageUploadService.Setup(service => service.GetExtension(mockImage.Object)).Returns(".jpg");
            mockImageUploadService.Setup(service => service.GetFileName(".jpg")).Returns(fileName);
            mockImageUploadService.Setup(service => service.SaveLogo(mockImage.Object, fileName)).Throws(new Exception("failed to upload image"));

            // Act
            var result = controller.Logo(mockImage.Object);
            var objectResult = result as ObjectResult;

            // Assert
            Assert.Equal(500, objectResult!.StatusCode);
        }

        [Fact(DisplayName = "Should upload a logo")]
        public void ShouldUploadALogoSmall()
        {
            // Arrange
            var mockImageUploadService = new Mock<IImageUploadService>();
            var mockImage = new Mock<Microsoft.AspNetCore.Http.IFormFile>();
            mockImage.Setup(image => image.FileName).Returns("image.jpg");
            var extension = ".jpg";
            var fileName = Guid.NewGuid().ToString() + extension;
            var fileNameWithPath = "upload/image/logosmall/" + fileName;

            var controller = new UploadController(mockImageUploadService.Object);
            mockImageUploadService.Setup(service => service.GetLogoSmallUrl(controller, fileName))
                .Returns(fileNameWithPath);
            mockImageUploadService.Setup(service => service.GetExtension(mockImage.Object)).Returns(".jpg");
            mockImageUploadService.Setup(service => service.GetFileName(".jpg")).Returns(fileName);

            // Act
            var result = controller.LogoSmall(mockImage.Object);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var okResultObject = okResult!.Value;
        }

        [Fact(DisplayName = "Should return 500 if a logo small upload fails")]
        public void ShouldReturn500IfALogoSmallUploadFails()
        {
            // Arrange
            var mockImageUploadService = new Mock<IImageUploadService>();
            var mockImage = new Mock<Microsoft.AspNetCore.Http.IFormFile>();
            mockImage.Setup(image => image.FileName).Returns("image.jpg");
            var extension = ".jpg";
            var fileName = Guid.NewGuid().ToString() + extension;
            var fileNameWithPath = "upload/image/logosmall/" + fileName;

            var controller = new UploadController(mockImageUploadService.Object);
            mockImageUploadService.Setup(service => service.GetLogoUrl(controller, fileName))
                .Returns(fileNameWithPath);
            mockImageUploadService.Setup(service => service.GetExtension(mockImage.Object)).Returns(".jpg");
            mockImageUploadService.Setup(service => service.GetFileName(".jpg")).Returns(fileName);
            mockImageUploadService.Setup(service => service.SaveLogoSmall(mockImage.Object, fileName)).Throws(new Exception("failed to upload image"));

            // Act
            var result = controller.LogoSmall(mockImage.Object);
            var objectResult = result as ObjectResult;

            // Assert
            Assert.Equal(500, objectResult!.StatusCode);
        }
    }
}
