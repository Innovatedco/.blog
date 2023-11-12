using Blazor.Blog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Blog.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<BlogPost> BlogPost { get; set; }
        public DbSet<BlogCategory> BlogCategory { get; set; }
        public DbSet<Author> Author { get; set; }

        /// <summary>
        /// Seed data for new install
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var blogCategoryId = Guid.NewGuid();
            var blogPostId1 = Guid.NewGuid();
            var blogPostId2 = Guid.NewGuid();
            var blogPostId3 = Guid.NewGuid();
            var date1 = DateTime.Now;
            var date2 = date1.AddDays(-1);
            var date3 = date1.AddDays(-2);
            var post = "<img src=\"/upload/image/09d237bc-6557-43e7-99b8-6835dc75fc9b.jpg\" class=\"w-100 mb-3\"/>" +
                "<p>We added some posts to get you started, a category called news to store them in and an author called Admin. Don't worry, you can archive, unpublish or modify them at any time and create your own posts, categories and authors.</p>" +
                "<p>First things first, if you haven't already done so, you will need to un-comment line 16 in the program.cs file and rebuild.</p>" +
                "<p>This will enable access to the login creation page, so go there now and create a login for yourself: <a href=\"/account/create\">/account/create</a>, use an email address and the password of your choice.</p>" +
                "<p>Now you have an account, you MUST now comment out or delete (recommended) line 16 in program.cs (and rebuild).</p>" +
                "<p>And that's it! You can now log in and edit this post/category/author or create your own. Happy Blogging</p>";
            var post1 = "<img src=\"/upload/image/fc635c36-a3af-4da7-977b-f4cc86b450e3.png\" class=\"w-100 mb-3\">" +
                "<p>You can add or change icons for categories easily on the Category Create or Category edit page.</p>" +
                "<p>We've already added the required font awesome CSS and Javascript so just search for the icon you would like to use on the <a href=\"https://fontawesome.com/search?o=r&m=free\">font awesome site.</a></p>" +
                "<p>You should only choose from the free icons and it's as simple as adding the css class to the icon text box</p>\r\n<p>Note: the fa-solid class is automatically added, so you only need add the icon class e.g. fa-newspaper," +
                " however, for fa-brand icons both classes should be added e.g. fa-brand fa-microsoft. </p>" +
                "<p>You can also check the icon code by clicking the check button, if the icon code is correct, then the icon will appear in the icon box.</p>";
            var post2 = "<img src=\"/upload/image/f9472e3f-659d-44ba-b887-2df3d20fc472.jpg\" class=\"w-100 mb-3\"/>" +
                "<p>You can also add code snippets to posts using the highlighter.js plugin, the code should be enclosed in a &ltpre&gt;&ltcode&gt;Code goes here&lt/pre&gt;&lt/code&gt; block. </p>" +
                "<p>We have already added some language specific formatting for C#, Html &amp; Json. Just add the css class lang-csharp to the code tag. </p>" +
                "<pre><code class=\"lang-csharp\">public void HelloWorld() => Console.Writeline(\"Hello .blog\");</code></pre>";                

            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    AuthorId = 1,
                    AuthorName = "Admin",
                    AuthorIcon = "/upload/image/author/small-11786136-2406-409a-a4ae-f9102cef92f0.jpg"
                }
            );
            modelBuilder.Entity<BlogCategory>().HasData(
                new BlogCategory
                {
                    BlogCategoryId = blogCategoryId,
                    CategoryName = "News",
                    CategoryNameNormalized = "news",
                    Icon = "fa-newspaper"
                }
            );

            modelBuilder.Entity<BlogPost>().HasData(new BlogPost[]
            {
                new BlogPost
                {
                    BlogPostId = blogPostId3,
                    AuthorId = 1,
                    BlogCategoryId = blogCategoryId,
                    Title = "Add code snippets to posts",
                    Created = date3,
                    Updated = date3,
                    NormalizedTitle = "add-code-snippets-to-posts",
                    Archived = false,
                    Published = true,
                    ThumbnailUrl = "/upload/image/thumb/small-f9472e3f-659d-44ba-b887-2df3d20fc472.jpg",
                    Post = post2
                },
                new BlogPost
                {
                    BlogPostId = blogPostId2,
                    AuthorId = 1,
                    BlogCategoryId = blogCategoryId,
                    Title = "Adding category icons",
                    Created = date2,
                    Updated = date2,
                    NormalizedTitle = "adding-category-icons",
                    Archived = false,
                    Published = true,
                    ThumbnailUrl = "/upload/image/thumb/small-fc635c36-a3af-4da7-977b-f4cc86b450e3.png",
                    Post = post1
                },
                new BlogPost
                {
                    BlogPostId = blogPostId1,
                    AuthorId = 1,
                    BlogCategoryId = blogCategoryId,
                    Title = "Getting started",
                    Created = date1,
                    Updated = date1,
                    NormalizedTitle = "getting-started",
                    Archived = false,
                    Published = true,
                    ThumbnailUrl = "/upload/image/thumb/small-09d237bc-6557-43e7-99b8-6835dc75fc9b.jpg",
                    Post = post
                },
            });
        }
    }
}
