using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blazor.Blog.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorIcon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.AuthorId);
                });

            migrationBuilder.CreateTable(
                name: "BlogCategory",
                columns: table => new
                {
                    BlogCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CategoryNameNormalized = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCategory", x => x.BlogCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "SiteSetings",
                columns: table => new
                {
                    SiteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SiteName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiteTagLine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiteDevUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiteProdUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiteLogo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiteLogoSmall = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteSetings", x => x.SiteId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogPost",
                columns: table => new
                {
                    BlogPostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NormalizedTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Post = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BlogCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Archived = table.Column<bool>(type: "bit", nullable: false),
                    Published = table.Column<bool>(type: "bit", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPost", x => x.BlogPostId);
                    table.ForeignKey(
                        name: "FK_BlogPost_Author_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Author",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogPost_BlogCategory_BlogCategoryId",
                        column: x => x.BlogCategoryId,
                        principalTable: "BlogCategory",
                        principalColumn: "BlogCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Author",
                columns: new[] { "AuthorId", "AuthorIcon", "AuthorName" },
                values: new object[] { 1, "/upload/image/author/small-11786136-2406-409a-a4ae-f9102cef92f0.jpg", "Admin" });

            migrationBuilder.InsertData(
                table: "BlogCategory",
                columns: new[] { "BlogCategoryId", "CategoryName", "CategoryNameNormalized", "Icon" },
                values: new object[] { new Guid("ea072a8b-48f9-4529-b00e-8ce536d5f576"), "News", "news", "fa-newspaper" });

            migrationBuilder.InsertData(
                table: "SiteSetings",
                columns: new[] { "SiteId", "SiteDevUrl", "SiteLogo", "SiteLogoSmall", "SiteName", "SiteProdUrl", "SiteTagLine" },
                values: new object[] { new Guid("38d31336-c55a-402a-a276-ccc2540d491b"), "https://localhost:7079", "upload/image/logo/logo.png", "upload/image/logosmall/logosmall.png", ".blog", "https://blazor.blog", "Talking about life, the universe and everything" });

            migrationBuilder.InsertData(
                table: "BlogPost",
                columns: new[] { "BlogPostId", "Archived", "AuthorId", "BlogCategoryId", "Created", "NormalizedTitle", "Post", "Published", "ThumbnailUrl", "Title", "Updated" },
                values: new object[,]
                {
                    { new Guid("28478684-7ea6-472d-b5f0-fe1ed24b4543"), false, 1, new Guid("ea072a8b-48f9-4529-b00e-8ce536d5f576"), new DateTime(2023, 11, 20, 23, 58, 38, 173, DateTimeKind.Local).AddTicks(2787), "add-code-snippets-to-posts", "<img src=\"/upload/image/f9472e3f-659d-44ba-b887-2df3d20fc472.jpg\" class=\"w-100 mb-3\"/><p>You can also add code snippets to posts using the highlighter.js plugin, the code should be enclosed in a &ltpre&gt;&ltcode&gt;Code goes here&lt/pre&gt;&lt/code&gt; block. </p><p>We have already added some language specific formatting for C#, Html &amp; Json. Just add the css class lang-csharp to the code tag. </p><pre><code class=\"lang-csharp\">public void HelloWorld() => Console.Writeline(\"Hello .blog\");</code></pre>", true, "/upload/image/thumb/small-f9472e3f-659d-44ba-b887-2df3d20fc472.jpg", "Add code snippets to posts", new DateTime(2023, 11, 20, 23, 58, 38, 173, DateTimeKind.Local).AddTicks(2787) },
                    { new Guid("3b5d0654-a5ea-4773-989c-984fe75ec33d"), false, 1, new Guid("ea072a8b-48f9-4529-b00e-8ce536d5f576"), new DateTime(2023, 11, 22, 23, 58, 38, 173, DateTimeKind.Local).AddTicks(2787), "getting-started", "<img src=\"/upload/image/09d237bc-6557-43e7-99b8-6835dc75fc9b.jpg\" class=\"w-100 mb-3\"/><p>We added some posts to get you started, a category called news to store them in and an author called Admin. Don't worry, you can archive, unpublish or modify them at any time and create your own posts, categories and authors.</p><p>First things first, if you haven't already done so, you will need to un-comment line 16 in the program.cs file and rebuild.</p><p>This will enable access to the login creation page, so go there now and create a login for yourself: <a href=\"/account/create\">/account/create</a>, use an email address and the password of your choice.</p><p>Now you have an account, you MUST now comment out or delete (recommended) line 16 in program.cs (and rebuild).</p><p>And that's it! You can now log in and edit this post/category/author or create your own. Happy Blogging</p>", true, "/upload/image/thumb/small-09d237bc-6557-43e7-99b8-6835dc75fc9b.jpg", "Getting started", new DateTime(2023, 11, 22, 23, 58, 38, 173, DateTimeKind.Local).AddTicks(2787) },
                    { new Guid("b40902d5-1328-429f-ba28-395f526b3f8a"), false, 1, new Guid("ea072a8b-48f9-4529-b00e-8ce536d5f576"), new DateTime(2023, 11, 21, 23, 58, 38, 173, DateTimeKind.Local).AddTicks(2787), "adding-category-icons", "<img src=\"/upload/image/fc635c36-a3af-4da7-977b-f4cc86b450e3.png\" class=\"w-100 mb-3\"><p>You can add or change icons for categories easily on the Category Create or Category edit page.</p><p>We've already added the required font awesome CSS and Javascript so just search for the icon you would like to use on the <a href=\"https://fontawesome.com/search?o=r&m=free\">font awesome site.</a></p><p>You should only choose from the free icons and it's as simple as adding the css class to the icon text box</p>\r\n<p>Note: the fa-solid class is automatically added, so you only need add the icon class e.g. fa-newspaper, however, for fa-brand icons both classes should be added e.g. fa-brand fa-microsoft. </p><p>You can also check the icon code by clicking the check button, if the icon code is correct, then the icon will appear in the icon box.</p>", true, "/upload/image/thumb/small-fc635c36-a3af-4da7-977b-f4cc86b450e3.png", "Adding category icons", new DateTime(2023, 11, 21, 23, 58, 38, 173, DateTimeKind.Local).AddTicks(2787) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPost_AuthorId",
                table: "BlogPost",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPost_BlogCategoryId",
                table: "BlogPost",
                column: "BlogCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BlogPost");

            migrationBuilder.DropTable(
                name: "SiteSetings");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "BlogCategory");
        }
    }
}
