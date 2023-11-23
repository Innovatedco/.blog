<h1 align="center">

<img src="logosmall.png"/>
<br/>
.blog
</h1>

### **.blog** is a new, open source, simple single blog platform, built with Blazor Server. 

.blog is designed to be simple, flexible and extensible.

Built against [.NET 8.0 LTS Blazor](https://learn.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-8.0).

## Features

- East to configure, quick to get up and running.
- Social-media-friendly out of the box, with sharing functionality and appropriate dynamic meta tag creation.
- Automatic featuring and display of recent posts.
- GDPR built in.
- Easily extensible.
- SEO-friendly, meta tags and automatic sitemap creation.
- Mobile-friendly responsive design.

## License
  
- .blog is licensed under the [MIT License](https://github.com/sedgey/BlazorBlog/blob/master/LICENSE.txt)

## Getting started

1. Ok, lets get this show on the road! First, clone the repository and make sure to open the solution file in Visual Studio.\
🟩 __NOTE__ 🟩\
If you clone a project in Visual Studio with a solution file in it, Visual Studio will not automatically open the solution file, so you must open the file yourself.
```
git clone https://github.com/sedgey/BlazorBlog.git
```
2. Create an empty sql server/sql express database in a location of your choosing and note the connection string.
3. Add an appsettings.json file to the BlazorBlog project with the following content,\
🟥 __IMPORTANT__ 🟥\
__Replace all entries surrounded with #### with your own values.__
```JSON with comments
{
  "DetailedErrors": true, // turns on CircuitOptions.DetailedErrors
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information",
      "Microsoft.AspNetCore.SignalR": "Debug" // turns on SignalR debugging
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "default": "####Your Database Connection String####" // e.g. Data Source=YourComputer\\SQLEXPRESS;Integrated Security=True;Database=BlazorBlog;Trust Server Certificate=true;
  },
  "CustomSettings": {
    "ProdBlogUrl": "####The Production Url Of Your Blog####",  // e.g. https://blog.yoursite.com (no trailing / required)
    "DevBlogUrl": "####The Devlopment Url For Your Blog####", // e.g. https://localhost:58945
    "SiteName": "####Blazor Blog#####", //  e.g. will appear anywhere the blog name is used on the site, for example on the privacy page or in the meta tags in the header and page titles etc
    "TagLine": "####Talking about life, the universe and everything####" // will appear in the meta tags in the header
  }
}
```
4. Next we are going to scaffold the database so open the package manager console and cd into the blog project folder:
```
cd BlazorBlog
```
 Then run the following command to generate an entity framework migration: 
 ```PowerShell
 dotnet ef migrations add InitialCreate
 ```
 And finally update your database.
 ```PowerShell
 dotnet ef database update
 ```
 5. Additionally you can brand the blog by replacing the following files located in the BlazorBlog wwwroot/images folder:
    - logo.png (960px X 150px)
    - logosmall.png (630px X 100px)
 6. Uncomment the following line in program.cs (line 16).
 ```C#
 options.Conventions.AllowAnonymousToAreaPage("Account", "/Create");
 ```
 \
 <img src="programcs.png"/>
 
 
 7. Build & run the project.

 # Set Up
 If everything went well then the project is up and running, so you need to create a user login:
 1. Navigate to /account/create and create a user login (the route you allowed by uncommenting the line in the program.cs file)\
 🟥 __VERY IMPORTANT__ 🟥\
It is __ESSENTIAL__ that you comment out or delete (recommended) this line in your program.cs as soon as possible to prevent the creation of any new user logins.\
 🟩 __NOTE__ 🟩\
You can have multiple logins and multiple authors, but since it is a single blog, 
any user is able to create/edit/archive(delete)/draft any post, create/edit/delete categories and create/edit authors. Authors are NOT linked to logins.

 <img src="createlogin.png"/>
 
 2. Now you can edit the existing Post/Category/Author or create and post your own categories/authors/blog posts etc. Enjoy!
 