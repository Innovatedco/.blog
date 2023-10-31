using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Blog.Tests.HelperClasses
{
    public class HtmlHelpers
    {
        public static readonly string MockHtmlWithImage = @"
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>Document</title>
            </head>
            <body>
                <img src=""/image/xyz.jpj"" alt="""">
            </body>
            </html>";

        public static readonly string MockHtmlWithoutImage = @"
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>Document</title>
            </head>
            <body>
                Hello
            </body>
            </html>";

        public static readonly string MockHtmlWithCode = @"
            <html lang=""en"">
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>Document</title>
            </head>
            <body>
                <pre><code class=""lang-csharp""></code></pre>
                <pre><code class=""lang-javascript""></code></pre>
                <pre><code class=""lang-typescript""></code></pre>
                <pre><code class=""lang-oops""></code></pre>
            </body>
            </html>";

        public static readonly string TitleScrambled = "~!@#$%^&*()Hello- +}|}{my<>?\": name\" is:-string (yes really)";

        public static readonly string MockHtmlBodyContentShort = @"<div class=""hello"">
                <p class=""text"">This is some text to extract.</p>
                <p class=""more"">And some more</p>
                <img src=""/hello/"" alt=""hello""/>
                <a href=""links to me"">This is a link</a>
            </div>";

        public static readonly string MockHtmlBodyContentLong = @"<div class=""hello"">
                <p class=""text"">This is some text to extract.</p>
                <p class=""more"">And some more</p>
                <img src=""/hello/"" alt=""hello""/>
                <a href=""links to me"">This is a link</a>
                <p class=""text"">And even more text to extract.</p>
                <p class=""text"">And just extra text to extract.</p>
            </div>";
    }
}
