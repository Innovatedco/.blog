using Blazor.Blog.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using Blazor.Blog.Models;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Radzen;
using Radzen.Blazor;
using Blazor.Blog.Pages;
using AngleSharp.Dom;
using Blazor.Blog.Globals;
using static Blazor.Blog.Tests.HelperClasses.ActHelpers;
using Blazor.Blog.Shared;

namespace Blazor.Blog.Tests.HelperClasses
{
    public class GlobalHelpers<T>
    {
        public static Mock<IDbContextFactory<DataContext>> BuildMockDbFactory(IEnumerable<T> list)
        {
            var mockDbFactory = new Mock<IDbContextFactory<DataContext>>();
            var dbName = Guid.NewGuid().ToString() + "BlogPostsInMemory";
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .EnableSensitiveDataLogging()
                .Options;
            var context = new DataContext(options);
            foreach (var listItem in list)
            {
                if (listItem != null) context.Add(listItem);
            }
            context.Add(new SiteSettings
            {
                SiteId = Guid.NewGuid(),
                SiteName = "SiteName",
                SiteTagLine = "SiteNameTagLine",
                SiteDevUrl = "SiteDevUrl",
                SiteProdUrl = "SiteProdUrl",
                SiteLogo = "SiteLogo",
                SiteLogoSmall = "SiteLogoSmall"
            });
            context.SaveChanges();
            mockDbFactory.Setup(f => f.CreateDbContext()).Returns(context);
            return mockDbFactory;
        }
    }

    public class ArrangeResult
    {
        public AuthorServiceEF AuthorServiceEF { get; set; } = default!;
        public BlogCategoryServiceEF BlogCategoryServiceEF { get; set; } = default!;
        public BlogPostServiceEF BlogPostServiceEF { get; set; } = default!;
        public SiteSettingsServiceEf SiteSettingsServiceEF { get; set; } = default!;
        public RandomPostDto RandomPostDto { get; set; } = default!;
        public Mock<IDbContextFactory<DataContext>> MockDbFactory { get; set; } = default!;
    }

    public class RandomPostDto
    {
        public IEnumerable<BlogPost> List { get; set; } = default!;
        public int PublishableCount { get; set; } = default!;
        public int UnpublishedCount { get; set; } = default!;
        public int DeletedCount { get; set; } = default!;
        public int Total
        {
            get
            {
                return List.Count();
            }
        }
        public bool Tallies
        {
            get
            {
                return (Total == (PublishableCount + UnpublishedCount + DeletedCount));
            }
        }
    }

    public static class ArrangeHelpers
    {
        public class ArrangeConfig
        {
            /// <summary>
            /// injects the Authorization service into the fixture, flagged authorized or not authorized
            /// for testing behaviours of components based on AuthorizationStatus
            /// </summary>
            public AuthorizationStatus? UseAuthorization { get; set; }
            /// <summary>
            /// overrides UseDataBlogPost, UseDataBlogCategory, UseAuthor and injects all data services
            /// into the fixture
            /// </summary>
            public bool UseData { get; set; } = default!;
            /// <summary>
            /// injects the blog post service into the fixture
            /// </summary>
            public bool UseDataBlogPost { get; set; } = default!;
            /// <summary>
            /// injects the blog category service into the fixture
            /// </summary>
            public bool UseDataBlogCategory { get; set; } = default!;
            /// <summary>
            /// injects the author service into the fixture
            /// </summary>
            public bool UseDataAuthor { get; set; } = default!;
            public bool UseConfiguration { get; set; } = default!;
            public WebHostEnvironment? UseWebHostEnvironment { get; set; }
            public bool UseNavigation { get; set; } = default!;
            public string? UseNavigationUri { get; set; } = default!;
            public enum AuthorizationStatus { Authorized, NotAuthorized }
            public enum WebHostEnvironment { Prod, Dev }
            public bool UseRadzen { get; set; } = default!;
        }

        public static void Arrange(TestContext ctx, ArrangeConfig arrangeConfig, ArrangeResult? arrangeResult = null)
        {
            ctx.JSInterop.Mode = JSRuntimeMode.Loose;
            if (arrangeConfig.UseAuthorization != null && arrangeConfig.UseAuthorization == ArrangeConfig.AuthorizationStatus.Authorized) ArrangeAuthorized(ctx);
            if (arrangeConfig.UseAuthorization != null && arrangeConfig.UseAuthorization == ArrangeConfig.AuthorizationStatus.NotAuthorized) ArrangeNotAuthorized(ctx);
            if (arrangeConfig.UseNavigation)
            {
                if(!string.IsNullOrEmpty(arrangeConfig.UseNavigationUri)) 
                    ctx.Services.AddSingleton<NavigationManager>(new MockNavigationManager(arrangeConfig.UseNavigationUri));
                else ctx.Services.AddSingleton<NavigationManager>(new MockNavigationManager());
            } 
            if (arrangeConfig.UseData)
            {
                arrangeResult ??= GetArrangeResult();
                ctx.Services.AddSingleton<IBlogPostService>(arrangeResult.BlogPostServiceEF);
                ctx.Services.AddSingleton<IBlogCategoryService>(arrangeResult.BlogCategoryServiceEF);
                ctx.Services.AddSingleton<IAuthorService>(arrangeResult.AuthorServiceEF);
                ctx.Services.AddSingleton<ISiteSettingsService>(arrangeResult.SiteSettingsServiceEF);
                var siteSettings = new GlobalSiteSettings(arrangeResult.SiteSettingsServiceEF);
                ctx.Services.AddSingleton<GlobalSiteSettings>(siteSettings);
            }
            if (!arrangeConfig.UseData && arrangeConfig.UseDataBlogPost)
            {
                arrangeResult ??= GetArrangeResult();
                ctx.Services.AddSingleton<IBlogPostService>(arrangeResult.BlogPostServiceEF);
            }
            if (!arrangeConfig.UseData && arrangeConfig.UseDataBlogCategory)
            {
                arrangeResult ??= GetArrangeResult();
                ctx.Services.AddSingleton<IBlogCategoryService>(arrangeResult.BlogCategoryServiceEF);
            }
            if (!arrangeConfig.UseData && arrangeConfig.UseDataAuthor)
            {
                arrangeResult ??= GetArrangeResult();
                ctx.Services.AddSingleton<IAuthorService>(arrangeResult.AuthorServiceEF);
            }
            if (arrangeConfig.UseWebHostEnvironment != null
                && arrangeConfig.UseWebHostEnvironment == ArrangeConfig.WebHostEnvironment.Prod)
                ctx.Services.AddSingleton<IWebHostEnvironment>(IWebHostEnvironmentHelper.MockIWebHostEnvironmentProd().Object);
            if (arrangeConfig.UseWebHostEnvironment != null
                && arrangeConfig.UseWebHostEnvironment == ArrangeConfig.WebHostEnvironment.Dev)
                ctx.Services.AddSingleton<IWebHostEnvironment>(IWebHostEnvironmentHelper.MockIWebHostEnvironmentDev().Object);
            if (arrangeConfig.UseConfiguration || arrangeConfig.UseData || arrangeConfig.UseNavigation) ctx.Services.AddSingleton<IConfiguration>(IConfigurationHelper.Configuration());
            if (arrangeConfig.UseRadzen) ArrangeRadzen(ctx);
        }

        public static ArrangeResult ArrangeWithResult(TestContext? ctx = null, ArrangeConfig? arrangeConfig = null)
        {
            var arrangeResult = GetArrangeResult();
            if(arrangeConfig != null && ctx != null) Arrange(ctx, arrangeConfig, arrangeResult);
            return arrangeResult;
        }

        public static PagerModelGeneric<int> ArrangePageModelGeneric(int listLength, int pageSize = 0)
        {
            var list = new List<int>();
            for (int i = 1; i <= listLength; i++) list.Add(i);
            PagerModelGeneric<int> pagerModel;
            if (pageSize != 0)
                pagerModel = new PagerModelGeneric<int>(list, pageSize);
            else
                pagerModel = new PagerModelGeneric<int>(list);
            return pagerModel;
        }

        public static PagerModelGeneric<int> ArrangePageModelGeneric()
        {
            return ArrangePageModelGeneric(10);
        }

        public static ArrangeResult GetArrangeResult()
        {
            var randomPostDto = BlogPostHelpers.BuildRandomBlogPostsFixture();
            var mockDbFactory = GlobalHelpers<BlogPost>.BuildMockDbFactory(randomPostDto.List);
            var blogPostService = new BlogPostServiceEF(mockDbFactory.Object);
            var blogCategoryService = new BlogCategoryServiceEF(mockDbFactory.Object);
            var authorService = new AuthorServiceEF(mockDbFactory.Object);
            var globalSiteSettingsService = new SiteSettingsServiceEf(mockDbFactory.Object);
            var arrangeResult = new ArrangeResult
            {
                BlogCategoryServiceEF = blogCategoryService,
                BlogPostServiceEF = blogPostService,
                AuthorServiceEF = authorService,
                SiteSettingsServiceEF = globalSiteSettingsService,
                RandomPostDto = randomPostDto,
                MockDbFactory = mockDbFactory
            };
            return arrangeResult;
        }

        private static void ArrangeRadzen(TestContext ctx)
        {
            ctx.Services.AddScoped<DialogService>();
            ctx.Services.AddScoped<NotificationService>();
            ctx.JSInterop.SetupModule("_content/Radzen.Blazor/Radzen.Blazor.js"); 
        }

        private static void ArrangeNotAuthorized(TestContext ctx)
        {
            ctx.Services.AddSingleton<NavigationManager>(new MockNavigationManager());
            var authContext = ctx.AddTestAuthorization();
            authContext.SetNotAuthorized();
            //authContext.SetAuthorized("TEST USER", AuthorizationState.Unauthorized);
            //authContext.SetPolicies("Admin");
        }

        private static void ArrangeAuthorized(TestContext ctx)
        {
            ctx.Services.AddSingleton<NavigationManager>(new MockNavigationManager());
            var authContext = ctx.AddTestAuthorization();
            authContext.SetAuthorized("TEST USER");
            authContext.SetPolicies("Admin");
        }
    }

    public static class ActHelpers
    {
        // Post Handles
        public class CreateEditPostHandles
        {
            public IElement? TitleTag { get; set; }
            public IRenderedComponent<RadzenHtmlEditor>? HtmlEditor { get; set; }
            public IRenderedComponent<RadzenTextBox>? TitleInput { get; set; }
            public IRenderedComponent<RadzenDropDown<Guid>>? CategoryDropdown { get; set; }
            public IRenderedComponent<RadzenDropDown<int>>? AuthorDropdown { get; set; }
            public IRenderedComponent<RadzenCheckBox<bool>>? PublishedCheckbox { get; set; }
            public IRenderedComponent<RadzenCheckBox<bool>>? DeletedCheckbox { get; set; }
            public IElement? SaveButton { get; set; }
            public IElement? DeleteButton { get; set; }
            public IElement? CancelButton { get; set; }
        }
        public class CreateEditPostHandlesInstances
        {
            public IElement? TitleTag { get; set; }
            public RadzenHtmlEditor? HtmlEditor { get; set; }
            public RadzenTextBox? TitleInput { get; set; }
            public RadzenDropDown<Guid>? CategoryDropdown { get; set; }
            public RadzenDropDown<int>? AuthorDropdown { get; set; }
            public RadzenCheckBox<bool>? PublishedCheckbox { get; set; }
            public RadzenCheckBox<bool>? DeletedCheckbox { get; set; }
            public IElement? SaveButton { get; set; }
            public IElement? DeleteButton { get; set; }
            public IElement? CancelButton { get; set; }
        }
        public static CreateEditPostHandles GetCreateEditPostHandles(IRenderedComponent<CreateEditPost> cut)
        {
            var createEditPostHandles = new CreateEditPostHandles
            {
                TitleTag = cut.Find("h3.title"),
                HtmlEditor = cut.FindComponent<RadzenHtmlEditor>(),
                TitleInput = cut.FindComponent<RadzenTextBox>(),
                CategoryDropdown = cut.FindComponent<RadzenDropDown<Guid>>(),
                AuthorDropdown = cut.FindComponent<RadzenDropDown<int>>(),
                SaveButton = cut.Find("button[name='save']"),
            };
            var checkBoxes = cut.FindComponents<RadzenCheckBox<bool>>();
            createEditPostHandles.PublishedCheckbox = checkBoxes.Where(x => x.Instance.Name == "Published").First();
            createEditPostHandles.DeletedCheckbox = checkBoxes.Where(x => x.Instance.Name == "Deleted").First();
            try
            {
                createEditPostHandles.DeleteButton = cut.Find("button[name='delete']");
            }
            catch 
            {
                createEditPostHandles.DeleteButton = null;
            }
            try
            {
                createEditPostHandles.CancelButton = cut.Find("button[name='cancel']");
            }
            catch
            {
                createEditPostHandles.CancelButton = null;
            }
            return createEditPostHandles;
        }
        public static CreateEditPostHandlesInstances GetCreateEditPostHandlesInstances(IRenderedComponent<CreateEditPost> cut)
        {
            var createEditPostHandles = new CreateEditPostHandlesInstances
            {
                TitleTag = cut.Find("h3.title"),
                HtmlEditor = cut.FindComponent<RadzenHtmlEditor>().Instance,
                TitleInput = cut.FindComponent<RadzenTextBox>().Instance,
                CategoryDropdown = cut.FindComponent<RadzenDropDown<Guid>>().Instance,
                AuthorDropdown = cut.FindComponent<RadzenDropDown<int>>().Instance,
                SaveButton = cut.Find("button[name='save']")
            };
            var checkBoxes = cut.FindComponents<RadzenCheckBox<bool>>();
            createEditPostHandles.PublishedCheckbox = checkBoxes.Where(x => x.Instance.Name == "Published").First().Instance;
            createEditPostHandles.DeletedCheckbox = checkBoxes.Where(x => x.Instance.Name == "Deleted").First().Instance;
            try
            {
                createEditPostHandles.DeleteButton = cut.Find("button[name='delete']");
            }
            catch
            {
                createEditPostHandles.DeleteButton = null;
            }
            try
            {
                createEditPostHandles.CancelButton = cut.Find("button[name='cancel']");
            }
            catch
            {
                createEditPostHandles.CancelButton = null;
            }

            return createEditPostHandles;
        }

        // Category handles
        public class CreateEditCategoryHandles
        {
            public IElement? TitleTag { get; set; }
            public IRenderedComponent<RadzenTextBox>? CategoryNameInput { get; set; }
            public IRenderedComponent<RadzenTextBox>? CategoryIconInput { get; set; }
            public IElement? SaveButton { get; set; }
            public IElement? DeleteButton { get; set; }

            public IElement? DeleteFinalButton { get; set; }
            public IElement? CancelButton { get; set; }
        }
        public class CreateEditCategoryHandlesInstances
        {
            public IElement? TitleTag { get; set; }
            public RadzenTextBox? CategoryNameInput { get; set; }
            public RadzenTextBox? CategoryIconInput { get; set; }
            public IElement? SaveButton { get; set; }
            public IElement? DeleteButton { get; set; }
            public IElement? DeleteFinalButton { get; set; }
            public IElement? CancelButton { get; set; }
        }
        public static CreateEditCategoryHandles GetCreateEditCategoryHandles(IRenderedComponent<CreateEditCategory> cut)
        {
            var createEditCategoryHandles = new CreateEditCategoryHandles
            {
                TitleTag = cut.Find("h3.title"),
                SaveButton = cut.Find("button[name='save']")
            };
            var textBoxes = cut.FindComponents<RadzenTextBox>();
            createEditCategoryHandles.CategoryNameInput = textBoxes.Where(x => x.Instance.Name == "category-name").First();
            createEditCategoryHandles.CategoryIconInput = textBoxes.Where(x => x.Instance.Name == "category-icon").First();
            try
            {
                createEditCategoryHandles.DeleteButton = cut.Find("button[name='delete']");
            }
            catch
            {
                createEditCategoryHandles.DeleteButton = null;
            }
            try
            {
                createEditCategoryHandles.DeleteFinalButton = cut.Find("button[name='delete']");
            }
            catch
            {
                createEditCategoryHandles.DeleteFinalButton = null;
            }
            try
            {
                createEditCategoryHandles.CancelButton = cut.Find("button[name='cancel']");
            }
            catch
            {
                createEditCategoryHandles.CancelButton = null;
            }
            return createEditCategoryHandles;
        }
        public static CreateEditCategoryHandlesInstances GetCreateEditCategoryHandlesInstances(IRenderedComponent<CreateEditCategory> cut)
        {
            var createEditCategoryHandlesInstances = new CreateEditCategoryHandlesInstances
            {
                TitleTag = cut.Find("h3.title"),
                SaveButton = cut.Find("button[name='save']")
            };
            var textBoxes = cut.FindComponents<RadzenTextBox>();
            createEditCategoryHandlesInstances.CategoryNameInput = textBoxes.Where(x => x.Instance.Name == "category-name").First().Instance;
            createEditCategoryHandlesInstances.CategoryIconInput = textBoxes.Where(x => x.Instance.Name == "category-icon").First().Instance;
            try
            {
                createEditCategoryHandlesInstances.DeleteButton = cut.Find("button[name='delete']");
            }
            catch
            {
                createEditCategoryHandlesInstances.DeleteButton = null;
            }
            try
            {
                createEditCategoryHandlesInstances.DeleteFinalButton = cut.Find("button[name='delete-final']");
            }
            catch
            {
                createEditCategoryHandlesInstances.DeleteFinalButton = null;
            }
            try
            {
                createEditCategoryHandlesInstances.CancelButton = cut.Find("button[name='cancel']");
            }
            catch
            {
                createEditCategoryHandlesInstances.CancelButton = null;
            }
            return createEditCategoryHandlesInstances;
        }

        // Author handles
        public class CreateEditAuthorHandles
        {
            public IElement? TitleTag { get; set; }
            public IRenderedComponent<RadzenTextBox>? AuthorNameInput { get; set; }
            public IRenderedComponent<RadzenUpload>? AuthorIcon { get; set; }
            public IElement? SaveButton { get; set; }
            public IElement? CancelButton { get; set; }
        }
        public class CreateEditAuthorHandlesInstances
        {
            public IElement? TitleTag { get; set; }
            public RadzenTextBox? AuthorNameInput { get; set; }
            public RadzenUpload? AuthorIcon { get; set; }
            public IElement? SaveButton { get; set; }
            public IElement? CancelButton { get; set; }
        }
        public static CreateEditAuthorHandles GetCreateEditAuthorHandles(IRenderedComponent<CreateEditAuthor> cut)
        {
            var createEditAuthorHandles = new CreateEditAuthorHandles
            {
                TitleTag = cut.Find("h3.title"),
                SaveButton = cut.Find("button[name='save']")
            };
            var textBoxes = cut.FindComponents<RadzenTextBox>();
            createEditAuthorHandles.AuthorNameInput = textBoxes.Where(x => x.Instance.Name == "author-name").First();
            var upload = cut.FindComponent<RadzenUpload>();
            createEditAuthorHandles.AuthorIcon = upload;
            try
            {
                createEditAuthorHandles.CancelButton = cut.Find("button[name='cancel']");
            }
            catch
            {
                createEditAuthorHandles.CancelButton = null;
            }
            return createEditAuthorHandles;
        }
        public static CreateEditAuthorHandlesInstances GetCreateEditAuthorHandlesInstances(IRenderedComponent<CreateEditAuthor> cut)
        {
            var createEditAuthorHandlesInstances = new CreateEditAuthorHandlesInstances
            {
                TitleTag = cut.Find("h3.title"),
                SaveButton = cut.Find("button[name='save']")
            };
            var textBoxes = cut.FindComponents<RadzenTextBox>();
            createEditAuthorHandlesInstances.AuthorNameInput = textBoxes.Where(x => x.Instance.Name == "author-name").First().Instance;
            var upload = cut.FindComponent<RadzenUpload>();
            createEditAuthorHandlesInstances.AuthorIcon = upload.Instance;
            try
            {
                createEditAuthorHandlesInstances.CancelButton = cut.Find("button[name='cancel']");
            }
            catch
            {
                createEditAuthorHandlesInstances.CancelButton = null;
            }
            return createEditAuthorHandlesInstances;
        }

        // Site Settings handles
        public class EditSiteSettingsHandles
        {
            public IElement? TitleTag { get; set; }
            public IRenderedComponent<RadzenTextBox>? SiteNameInput { get; set; }
            public IRenderedComponent<RadzenTextArea>? SiteTaglineInput { get; set; }
            public IRenderedComponent<RadzenTextBox>? ProdUrlInput { get; set; }
            public IRenderedComponent<RadzenTextBox>? DevUrlInput { get; set; }
            public IRenderedComponent<RadzenUpload>? SiteLogo { get; set; }
            public IRenderedComponent<RadzenUpload>? SiteLogoSmall { get; set; }
            public IRenderedComponent<Logo>? Logo { get; set; }
            public IRenderedComponent<Logo>? LogoSmall { get; set; }
            public IElement? SaveButton { get; set; }
            public IElement? CancelButton { get; set; }
        }
        public class EditSiteSettingsHandlesInstances
        {
            public IElement? TitleTag { get; set; }
            public RadzenTextBox? SiteNameInput { get; set; }
            public RadzenTextArea? SiteTaglineInput { get; set; }
            public RadzenTextBox? ProdUrlInput { get; set; }
            public RadzenTextBox? DevUrlInput { get; set; }
            public RadzenUpload? SiteLogo { get; set; }
            public RadzenUpload? SiteLogoSmall { get; set; }
            public Logo? Logo { get; set; }
            public Logo? LogoSmall { get; set; }
            public IElement? SaveButton { get; set; }
            public IElement? CancelButton { get; set; }
        }
        public static EditSiteSettingsHandles GetEditSiteSettingsHandles(IRenderedComponent<EditSiteSettings> cut)
        {
            var editSiteSettingsHandles = new EditSiteSettingsHandles
            {
                TitleTag = cut.Find("h3.title"),
                SaveButton = cut.Find("button[name='save']")
            };
            var textBoxes = cut.FindComponents<RadzenTextBox>();
            editSiteSettingsHandles.SiteNameInput = textBoxes.Where(x => x.Instance.Name == "site-name").First();
            editSiteSettingsHandles.SiteTaglineInput = cut.FindComponent<RadzenTextArea>();
            editSiteSettingsHandles.ProdUrlInput = textBoxes.Where(x => x.Instance.Name == "prod-url").First();
            editSiteSettingsHandles.DevUrlInput = textBoxes.Where(x => x.Instance.Name == "dev-url").First();
            var uploads = cut.FindComponents<RadzenUpload>();
            editSiteSettingsHandles.SiteLogo = uploads[0];
            editSiteSettingsHandles.SiteLogoSmall = uploads[1];
            var logos = cut.FindComponents<Logo>();
            editSiteSettingsHandles.Logo = logos.Where(x => x.Instance.Name == "logo").First();
            editSiteSettingsHandles.LogoSmall = logos.Where(x => x.Instance.Name == "logo-small").First();
            try
            {
                editSiteSettingsHandles.CancelButton = cut.Find("button[name='cancel']");
            }
            catch
            {
                editSiteSettingsHandles.CancelButton = null;
            }
            return editSiteSettingsHandles;
        }
        public static EditSiteSettingsHandlesInstances GetEditSiteSettingsHandlesInstances(IRenderedComponent<EditSiteSettings> cut)
        {
            var editSiteSettingsHandlesInstances = new EditSiteSettingsHandlesInstances
            {
                TitleTag = cut.Find("h3.title"),
                SaveButton = cut.Find("button[name='save']")
            };
            var textBoxes = cut.FindComponents<RadzenTextBox>();
            editSiteSettingsHandlesInstances.SiteNameInput = textBoxes.Where(x => x.Instance.Name == "site-name").First().Instance;
            editSiteSettingsHandlesInstances.ProdUrlInput = textBoxes.Where(x => x.Instance.Name == "prod-url").First().Instance;
            editSiteSettingsHandlesInstances.DevUrlInput = textBoxes.Where(x => x.Instance.Name == "dev-url").First().Instance;
            editSiteSettingsHandlesInstances.SiteTaglineInput = cut.FindComponent<RadzenTextArea>().Instance;
            var uploads = cut.FindComponents<RadzenUpload>();
            editSiteSettingsHandlesInstances.SiteLogo = uploads[0].Instance;
            editSiteSettingsHandlesInstances.SiteLogoSmall = uploads[1].Instance;
            var logos = cut.FindComponents<Logo>();
            editSiteSettingsHandlesInstances.Logo = logos.Where(x => x.Instance.Name == "logo").First().Instance;
            editSiteSettingsHandlesInstances.LogoSmall = logos.Where(x => x.Instance.Name == "logo-small").First().Instance;
            try
            {
                editSiteSettingsHandlesInstances.CancelButton = cut.Find("button[name='cancel']");
            }
            catch
            {
                editSiteSettingsHandlesInstances.CancelButton = null;
            }
            return editSiteSettingsHandlesInstances;
        }
    }
}
