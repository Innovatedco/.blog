using Blazor.Blog.Models;

namespace Blazor.Blog.Tests.Models
{
    public class CssClassHandlerTests : TestContext
    {
        [Fact(DisplayName = "Should initialize with an empty string")]
        public void ShouldInitializeWithAnEmptyString()
        {
            var handler = new CssClassHandler();
            Assert.Empty(handler.CssClasses);
            Assert.Empty(handler.ClassString);
        }

        [Fact(DisplayName = "Should initialize with a class string")]
        public void ShouldInitializeWithAClassString()
        {
            var handler = new CssClassHandler(" xy xm abc q");
            Assert.NotEmpty(handler.CssClasses);
            Assert.NotEmpty(handler.ClassString);
            Assert.Equal(4, handler.CssClasses.Count);
            Assert.Equal("xy xm abc q", handler.ClassString);
        }

        [Fact(DisplayName = "Should add a class")]
        public void ShouldAddAClass()
        {
            var handler = new CssClassHandler(" xy xm abc q");
            handler.AddClass("oops");
            Assert.NotEmpty(handler.CssClasses);
            Assert.NotEmpty(handler.ClassString);
            Assert.Equal(5, handler.CssClasses.Count);
            Assert.Equal("xy xm abc q oops", handler.ClassString);
        }

        [Fact(DisplayName = "Should remove a class")]
        public void ShouldRemoveAClass()
        {
            var handler = new CssClassHandler(" xy xm abc q");
            handler.RemoveClass("q");
            Assert.NotEmpty(handler.CssClasses);
            Assert.NotEmpty(handler.ClassString);
            Assert.Equal(3, handler.CssClasses.Count);
            Assert.Equal("xy xm abc", handler.ClassString);
        }

        [Fact(DisplayName = "Should not add a class if already exists")]
        public void ShouldNotAddAClassIfAlreadyExists()
        {
            var handler = new CssClassHandler(" xy xm abc q");
            handler.AddClass("xm");
            Assert.NotEmpty(handler.CssClasses);
            Assert.NotEmpty(handler.ClassString);
            Assert.Equal(4, handler.CssClasses.Count);
            Assert.Equal("xy xm abc q", handler.ClassString);
        }

        [Fact(DisplayName = "Should not delete a class if not exists")]
        public void ShouldNotDeleteAClassIfnotExists()
        {
            var handler = new CssClassHandler(" xy xm abc q");
            handler.RemoveClass("oops");
            Assert.NotEmpty(handler.CssClasses);
            Assert.NotEmpty(handler.ClassString);
            Assert.Equal(4, handler.CssClasses.Count);
            Assert.Equal("xy xm abc q", handler.ClassString);
        }
    }
}
