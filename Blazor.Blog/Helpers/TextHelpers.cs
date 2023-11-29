using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace Blazor.Blog.Helpers
{
    public class TextHelpers
    {
        /// <summary>
        /// Removes html tags from an html string
        /// </summary>
        /// <param name="html"></param>
        /// <returns>string text</returns>
        public static string RemoveHTMLTags(string html)
        {
            return Regex.Replace(html, "<.*?>", string.Empty);
        }

        /// <summary>
        /// Formats a stub from an html string
        /// The entire string is returned if the length is less than the stipulated length
        /// Or a substring with elipses
        /// </summary>
        /// <param name="html"></param>
        /// <param name="length"></param>
        /// <returns>string stub</returns>
        public static string FormatStub(string html, int length)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var nodes = doc.DocumentNode.SelectNodes("//h3");
            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    node.Remove();
                }
            }
            html = doc.DocumentNode.OuterHtml;
            var replaced = RemoveHTMLTags(html);
            if (replaced.Length < length)
            {
                return replaced;
            }
            else return replaced.Substring(0, length) + "...";
        }

        #region Highlighter.js language constants
        private static readonly string _lang = "lang-";
        private static readonly string _ext = ".min.js";
        private static readonly string _highlight = "highlight.min.js";
        private static readonly string _cs = "csharp";
        private static readonly string _css = "css";
        private static readonly string _http = "http";
        private static readonly string _js = "javascript";
        private static readonly string _json = "json";
        private static readonly string _shell = "shell";
        private static readonly string _sql = "sql";
        private static readonly string _ts = "typescript";
        private static readonly string _xml = "xml";
        private static readonly string _yaml = "yaml";

        private static readonly Dictionary<string, string> HandledCodeSnippetLangauges = new()
        {
            { _lang + _cs, _cs + _ext},
            { _lang + _http, _http + _ext },
            { _lang + _css, _css + _ext },
            { _lang + _js, _js + _ext },
            { _lang + _json, _json + _ext },
            { _lang + _shell, _shell + _ext },
            { _lang + _sql, _sql + _ext },
            { _lang + _ts, _ts + _ext },
            { _lang + _xml, _xml + _ext },
            { _lang + _yaml, _yaml + _ext }
        };
        #endregion

        /// <summary>
        /// Parses the provided markup, finds any code snippets <code></code> and returns a list of handled css-classes
        /// for use with Highlighter.js any unhandled classes are removed
        /// </summary>
        /// <param name="markUp"></param>
        /// <returns></returns>
        public static List<string> GetCodeLanguagesToHighight(string markUp)
        {
            var langList = new List<string>();
            var doc = new HtmlDocument();
            doc.LoadHtml(markUp);
            var codeNodes = doc.DocumentNode.SelectNodes("//code");
            if (codeNodes != null && codeNodes.Count > 0)
            {
                foreach (var codeNode in codeNodes)
                {
                    string[] cssClasses = codeNode.Attributes["class"] != null
                                      ? codeNode.Attributes["class"].Value.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                                      : Array.Empty<string>();
                    foreach (var cssClass in cssClasses)
                    {
                        if (cssClass.Contains("lang-"))
                        {
                            if(HandledCodeSnippetLangauges.ContainsKey(cssClass)) langList.Add(cssClass);
                        }
                    }
                }
            }
            return langList;
        }

        /// <summary>
        /// Builds a manifest of required js files required to highlight code snippets for the particular languages
        /// This list may be empty if no handled languages are found
        /// </summary>
        /// <param name="markUp"></param>
        /// <returns>Lists<string></returns>
        public static List<string> BuildManifestForHighlighter(string markUp)
        {
            var jsManifest = new List<string>();
            var languages = GetCodeLanguagesToHighight(markUp);
            if (languages.Count > 0) jsManifest.Add(_highlight);
            foreach ( var language in languages.Distinct() )
            {
                var handledLangScript = HandledCodeSnippetLangauges[language];
                jsManifest.Add(handledLangScript);
            }
            return jsManifest;
        }
    }
}
