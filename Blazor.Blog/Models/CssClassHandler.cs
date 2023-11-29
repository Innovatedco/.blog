namespace Blazor.Blog.Models
{
    public class CssClassHandler
    {
        public string ClassString { get; set; } = default!;
        public List<string> CssClasses = new ();

        /// <summary>
        /// Initializes the class with an empty List
        /// </summary>
        public CssClassHandler()
        {
            RenderClasses();
        }

        /// <summary>
        /// Initializes the class with a string of class names
        /// </summary>
        /// <param name="classString"></param>
        public CssClassHandler(string classString)
        {
            CssClasses = classString.Split(' ').Where(x => x != string.Empty).ToList<string>();
            RenderClasses();
        }

        /// <summary>
        /// Removes a class from the list 
        /// </summary>
        /// <param name="cssClass"></param>
        public string RemoveClass(string cssClass)
        {
            if (CssClasses.Contains(cssClass))
            {
                CssClasses.Remove(cssClass);
                RenderClasses();
            }
            return ClassString;
        }

        /// <summary>
        /// Adds a class from the list
        /// </summary>
        /// <param name="cssClass"></param>
        public string AddClass(string cssClass)
        {
            if (!CssClasses.Contains(cssClass))
            {
                CssClasses.Add(cssClass);
                RenderClasses();
            }
            return ClassString;
        }

        /// <summary>
        /// Builds a new class string based on the current list
        /// </summary>
        private void RenderClasses()
        {
            ClassString = string.Empty;
            foreach (var css in CssClasses) ClassString += css + " ";
            ClassString = ClassString.Trim();
        }
    }
}
