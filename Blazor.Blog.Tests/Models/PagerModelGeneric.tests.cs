using Blazor.Blog.Models;
using Blazor.Blog.Tests.HelperClasses;
using System;
using System.Collections.Generic;

namespace Blazor.Blog.Tests.Models
{
    public class PagerModelGenericTests : TestContext
    {
        [Fact(DisplayName = "Should initialize set properties when inititialized")]
        public void ShouldSetPropertyDefaultsWhenInitialized()
        {
            // Arrange/Act
            var pagerModel = ArrangeHelpers.ArrangePageModelGeneric();
            var totalPages = (int)Math.Ceiling(Decimal.Divide(10, 5));
            // Assert
            Assert.True(pagerModel.PageSize == 5);
            Assert.True(pagerModel.PrevPage == 1);
            Assert.True(pagerModel.CurrentPage == 1);
            Assert.True(pagerModel.ItemCount == 10);
            Assert.True(pagerModel.TotalPages == totalPages);
            Assert.True(pagerModel.LastPage == totalPages);
        }

        [Fact(DisplayName = "Should throw an error if an out of range page is called")]
        public void ShouldThrowAnErrorIfAnOutOfRangePageIsCalled()
        {
            // Arrange/Act
            var pagerModel = ArrangeHelpers.ArrangePageModelGeneric();

            // Assert
            Assert.Throws<Exception>(() => pagerModel.SetPage(-1));
        }
    }
}
