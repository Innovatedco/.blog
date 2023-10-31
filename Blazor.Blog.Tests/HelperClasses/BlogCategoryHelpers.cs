using AutoFixture;
using Blazor.Blog.Data;
using Blazor.Blog.Migrations;
using Blazor.Blog.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.Blog.Tests.HelperClasses
{
    public class BlogCategoryHelpers
    {
        /// <summary>
        /// Builds a base blog category fixture with circular reference fix
        /// </summary>
        /// <returns>Fixture</returns>
        public static Fixture BuildBaseBlogCategoryFixture()
        {
            var blogCategoryFixtureBase = new Fixture();
            blogCategoryFixtureBase.Behaviors.OfType<ThrowingRecursionBehavior>()
            .ToList()
            .ForEach(b => blogCategoryFixtureBase.Behaviors.Remove(b));
            blogCategoryFixtureBase.Behaviors.Add(new OmitOnRecursionBehavior());
            return blogCategoryFixtureBase;
        }

        /// <summary>
        /// Builds a blog category from the base fixture
        /// </summary>
        /// <returns>BlogCategory</returns>
        public static BlogCategory BuildBlogCategoryFixture()
        {
            var baseFixture = BuildBaseBlogCategoryFixture();
            var category = baseFixture.Build<BlogCategory>()
                .Create();
            return category;
        }

        /// <summary>
        /// Builds a blog category list from the base fixture
        /// </summary>
        /// <param name="count">int</param>
        /// <returns>IEnumerable<BlogCategory></returns>
        public static IEnumerable<BlogCategory> BuildBlogCategoriesFixture(int count)
        {
            var baseFixture = BuildBaseBlogCategoryFixture();
            var categories = baseFixture.Build<BlogCategory>()
                .CreateMany(count).ToList();
            return categories;
        }

        /// <summary>
        /// Builds a blog category list from the base fixture
        /// Count defaults to 5
        /// </summary>
        /// <returns>IEnumerable<BlogCategory></returns>
        public static IEnumerable<BlogCategory> BuildBlogCategoriesFixture()
        {
            return BuildBlogCategoriesFixture(5);
        }
    }
}
