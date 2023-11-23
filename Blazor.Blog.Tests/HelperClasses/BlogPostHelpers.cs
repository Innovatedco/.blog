using AutoFixture;
using Blazor.Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.Blog.Tests.HelperClasses
{
    public class BlogPostHelpers
    {
        private static readonly Author _author = new()
        {
            AuthorId = 1,
            AuthorName = "Test Name",
            AuthorIcon = "TestIcon"
        };

        /// <summary>
        /// Builds a base blog post fixture with circular reference fix
        /// </summary>
        /// <returns>Fixture</returns>
        public static Fixture BuildBaseBlogPostFixture()
        {
            var blogPostFixtureBase = new Fixture();
            blogPostFixtureBase.Behaviors.OfType<ThrowingRecursionBehavior>()
            .ToList()
            .ForEach(b => blogPostFixtureBase.Behaviors.Remove(b));
            blogPostFixtureBase.Behaviors.Add(new OmitOnRecursionBehavior());
            return blogPostFixtureBase;
        }

        /// <summary>
        /// Builds a blog post from the base fixture with published and archived attributes,
        /// Which enable rendering in the component
        /// </summary>
        /// <returns>BlogPost</returns>
        public static BlogPost BuildPublishableBlogPostFixture()
        {
            var baseFixture = BuildBaseBlogPostFixture();
            var post = baseFixture.Build<BlogPost>()
                .With(x => x.Published, true)
                .With(x => x.Archived, false)
                .With(x => x.AuthorId, 1)
                .With(x => x.Author, _author)
                .Create();
            return post;
        }

        /// <summary>
        /// Builds a blog post list from the base fixture with published and archived attributes,
        /// Which enable rendering in the component
        /// </summary>
        /// <param name="count">int</param>
        /// <returns>IEnumerable<BlogPost></returns>
        public static IEnumerable<BlogPost> BuildPublishableBlogPostsFixture(int count)
        {
            var baseFixture = BuildBaseBlogPostFixture();
            baseFixture.Customizations.Add(
            new RandomDateTimeSequenceGenerator(
                minDate: DateTime.Now.AddDays(-15),
                maxDate: DateTime.Now
            ));
            var posts = baseFixture.Build<BlogPost>()
                .With(x => x.Published, true)
                .With(x => x.Archived, false)
                .With(x => x.AuthorId, 1)
                .With(x => x.Author, _author)
                .With(x => x.Created, DateTime.Now)
                .CreateMany(count).ToList();
            return posts;
        }

        /// <summary>
        /// Builds a blog post list from the base fixture with published and archived attributes,
        /// Count defaults to 5
        /// </summary>
        /// <returns>IEnumerable<BlogPost></returns>
        public static IEnumerable<BlogPost> BuildPublishableBlogPostsFixture()
        {
            return BuildPublishableBlogPostsFixture(5);
        }

        /// <summary>
        /// Builds a blog post list from the base fixture with published attributes set to unpublished,
        /// </summary>
        /// <param name="count">int</param>
        /// <returns>IEnumerable<BlogPost></returns>
        public static IEnumerable<BlogPost> BuildUnPublishedBlogPostsFixture(int count)
        {
            var baseFixture = BuildBaseBlogPostFixture();
            var posts = baseFixture.Build<BlogPost>()
                .With(x => x.Published, false)
                .With(x => x.Archived, false)
                .With(x => x.AuthorId, 1)
                .With(x => x.Author, _author)
                .CreateMany(count).ToList();
            return posts;
        }

        /// <summary>
        /// Builds a blog post list from the base fixture with published attributes set to unpublished,
        /// The count defaults to 5
        /// </summary>
        /// <returns>IEnumerable<BlogPost></returns>
        public static IEnumerable<BlogPost> BuildUnPublishedBlogPostsFixture()
        {

            return BuildUnPublishedBlogPostsFixture(5);
        }

        /// <summary>
        /// Builds a blog post list from the base fixture with deleted (archived) attributes set to unpublished,
        /// Which enable rendering in the component
        /// </summary>
        /// <param name="count">int</param>
        /// <returns>IEnumerable<BlogPost></returns>
        public static IEnumerable<BlogPost> BuildDeletedBlogPostsFixture(int count)
        {
            var baseFixture = BuildBaseBlogPostFixture();
            var posts = baseFixture.Build<BlogPost>()
                .With(x => x.Published, true)
                .With(x => x.Archived, true)
                .With(x => x.AuthorId, 1)
                .With(x => x.Author, _author)
                .CreateMany(count).ToList();
            return posts;
        }

        /// <summary>
        /// Builds a blog post list from the base fixture with deleted (archived) attributes set to unpublished
        /// The count defaults to 5
        /// </summary>
        /// <returns>IEnumerable<BlogPost></returns>
        public static IEnumerable<BlogPost> BuildDeletedBlogPostsFixture()
        {
            return BuildDeletedBlogPostsFixture(5);
        }

        /// <summary>
        /// Builds a random list of BlogPosts containing publishable, unpushed and deleted posts
        /// Max count is 42 = 15 + 14 + 13
        /// </summary>
        /// <returns>IEnumerable<BlogPost></returns>
        public static RandomPostDto BuildRandomBlogPostsFixture()
        {
            var random = new Random();
            var dto = new RandomPostDto();
            var publishableCount = random.Next(1, 15);
            dto.PublishableCount = publishableCount;
            var publishable = BuildPublishableBlogPostsFixture(publishableCount);
            var unpubishedCount = random.Next(1, 15);
            dto.UnpublishedCount = unpubishedCount;
            var unpublished = BuildUnPublishedBlogPostsFixture(unpubishedCount);
            var deletedCount = random.Next(1, 15);
            dto.DeletedCount = deletedCount;
            var deleted = BuildDeletedBlogPostsFixture(deletedCount);
            var result = publishable.Concat(unpublished);
            result = result.Concat(deleted);
            dto.List = result;
            return dto;
        }
    }
}
