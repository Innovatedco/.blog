using AutoFixture;
using Blazor.Blog.Models;
using System.Linq;

namespace Blazor.Blog.Tests.HelperClasses
{
    public class AuthorHelpers
    {
        /// <summary>
        /// Builds a base author fixture with circular reference fix
        /// </summary>
        /// <returns>Fixture</returns>
        public static Fixture BuildBaseAuthorFixture()
        {
            var authorFixtureBase = new Fixture();
            authorFixtureBase.Behaviors.OfType<ThrowingRecursionBehavior>()
            .ToList()
            .ForEach(b => authorFixtureBase.Behaviors.Remove(b));
            authorFixtureBase.Behaviors.Add(new OmitOnRecursionBehavior());
            return authorFixtureBase;
        }

        /// <summary>
        /// Builds an author from the base fixture
        /// </summary>
        /// <returns>Author</returns>
        public static Author BuildAuthorFixture()
        {
            var baseFixture = BuildBaseAuthorFixture();
            var author = baseFixture.Build<Author>()
                .Create();
            return author;
        }
    }
}
