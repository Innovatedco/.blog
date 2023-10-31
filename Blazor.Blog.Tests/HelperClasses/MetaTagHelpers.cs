using System.Collections.Generic;

namespace Blazor.Blog.Tests.HelperClasses
{
    public class MetaTagHelpers
    {
        public static readonly string Name = "name";
        public static readonly string Property = "property";
        public static readonly string Description = "description";
        public static readonly List<MetaTag> BlogCategoryMetaTags = new List<MetaTag> { 
            new MetaTag(Name, "description"),
            new MetaTag(Property, "og:locale"),
            new MetaTag(Property, "og:type"),
            new MetaTag(Property, "og:title"),
            new MetaTag(Property, "og:description"),
            new MetaTag(Property, "og:url"),
            new MetaTag(Property, "og:site_name"),
            new MetaTag(Name, "twitter:card"),
            new MetaTag(Name, "twitter:title"),
            new MetaTag(Name, "twitter:description")
        };
        public static readonly List<MetaTag> BlogPostMetaTags = new List<MetaTag> {
            new MetaTag(Name, "description"),
            new MetaTag(Property, "og:locale"),
            new MetaTag(Property, "og:type"),
            new MetaTag(Property, "og:title"),
            new MetaTag(Property, "og:description"),
            new MetaTag(Property, "og:url"),
            new MetaTag(Property, "og:site_name"),
            new MetaTag(Property, "og:image"),
            new MetaTag(Name, "twitter:card"),
            new MetaTag(Name, "twitter:title"),
            new MetaTag(Name, "twitter:description"),
            new MetaTag(Name, "twitter:image")
        };
    }

    public class MetaTag
    {
        public string MetaProperty { get; set; }
        public string MetaValue { get; set; }
        public MetaTag(string metaProperty, string metaValue) 
        { 
            MetaProperty = metaProperty;
            MetaValue = metaValue;
        }
    }
}
