using HtmlAgilityPack;
using Blazor.Blog.Helpers;
using Blazor.Blog.Tests.HelperClasses;

namespace Blazor.Blog.Tests.Helpers
{
    public class TextHelpersTests
    {
        [Fact(DisplayName = "Should remove html tags from the provided html")]
        public void ShouldRemoveHtmlTagsFromProvideHtml()
        {
            // Arrange/Act
            var result = TextHelpers.RemoveHTMLTags(HtmlHelpers.MockHtmlBodyContentShort);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(result);

            // assert
            Assert.Single(doc.DocumentNode.ChildNodes);
        }

        [Fact(DisplayName = "Should return a stub with html tags removed entire")]
        public void ShouldReturnAStubWithHtmlTagsRemovedEntire()
        {
            // Arrange/Act
            var result = TextHelpers.FormatStub(HtmlHelpers.MockHtmlBodyContentShort, 130);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(result);

            // assert
            Assert.Single(doc.DocumentNode.ChildNodes);
        }

        [Fact(DisplayName = "Should return a stub with html tags removed stub")]
        public void ShouldReturnAStubWithHtmlTagsRemovedStub()
        {
            // Arrange/Act
            var result = TextHelpers.FormatStub(HtmlHelpers.MockHtmlBodyContentShort, 130);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(result);

            // assert
            Assert.Single(doc.DocumentNode.ChildNodes);
            Assert.Contains("...", result);
        }

        [Fact(DisplayName = "Should return an empty list if no code snippets are found with handled languages")]
        public void ShouldReturnAnEmptListIfNoCodeSnippetsAreFoundWithHandledLanguages()
        {
            // Arrange/Act
            var result = TextHelpers.BuildManifestForHighlighter(HtmlHelpers.MockHtmlWithoutImage);

            // Assert
            Assert.Empty(result);
        }

        [Fact(DisplayName = "Should return an list of handled languages")]
        public void ShouldReturnAListOfHandledLanguages()
        {
            // Arrange/Act
            var result = TextHelpers.BuildManifestForHighlighter(HtmlHelpers.MockHtmlWithCode);

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(4, result.Count);
        }
    }
}
