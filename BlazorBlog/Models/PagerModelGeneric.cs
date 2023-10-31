namespace Blazor.Blog.Models
{
    public class PagerModelGeneric<T>
    {
        /// <summary>
        /// The total number of pages based on page size and list length
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// first page in the list default is 1
        /// </summary>
        public int FirstPage { get; set; } = 1;
        /// <summary>
        /// last page in list
        /// </summary>
        public int LastPage { get; set; }
        /// <summary>
        /// the previous page in the list in relation to the current page
        /// </summary>
        public int PrevPage { get; set; } = 0;
        /// <summary>
        /// the next page in the list in relation to the current page
        /// </summary>
        public int NextPage { get; set; }
        /// <summary>
        /// The number of items per page
        /// </summary>
        public int PageSize { get; set; } = 5;
        /// <summary>
        /// the currently selected page
        /// </summary>
        public int CurrentPage { get; set; } = 1;
        /// <summary>
        /// the number of items
        /// </summary>
        public int ItemCount { get; set; }

        public IEnumerable<T>? CurrentItemList { get; set; }
        public IEnumerable<T>? ItemList {  get; set; }

        /// <summary>
        /// Initializes anew instance of the pager model generic
        /// setting up properties and default settings
        /// </summary>
        /// <param name="itemList"></param>
        /// <param name="pageSize"></param>
        public PagerModelGeneric(IEnumerable<T> itemList, int pageSize = 5)
        {
            ItemList = itemList;
            ItemCount = itemList.Count();
            PageSize = pageSize;
            // calculates the number of pages
            TotalPages = (int)Math.Ceiling(Decimal.Divide(ItemCount, pageSize));
            LastPage = TotalPages;
            NextPage = Math.Min(CurrentPage + 1, LastPage);
            Page(1);
        }

        private void Page(int newPage)
        {
            var skip = (newPage - 1) * PageSize;
            var take = PageSize;
            CurrentItemList = GetList(skip, take);
            CurrentPage = newPage;
            PrevPage = Math.Max(CurrentPage - 1, FirstPage);
            NextPage = Math.Min(CurrentPage + 1, LastPage);
        }

        /// <summary>
        /// Retrieves a slice ot items from the list for a particular page
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        private IEnumerable<T> GetList(int skip, int take)
        {
            var result = ItemList!
                .Skip(skip)
                .Take(take)
                .ToList();
            return result;
        }

        /// <summary>
        /// Moves to the previous page provided the page is in range
        /// </summary>
        public void Prev()
        {
            // decrements the current page by 1 and verifies the new value
            // is within range
            var page  = CurrentPage - 1;
            if (page <= LastPage && page >= FirstPage)
            {
                Page(page);
            }
        }

        /// <summary>
        /// Moves to the next page provided the page is in range
        /// </summary>
        public void Next()
        {
            // increments the current page by 1 and verifies the new value
            // is within range
            var page = CurrentPage + 1;
            if (page <= LastPage && page >= FirstPage)
            {
                Page(page);
            }
        }

        /// <summary>
        /// Sets the page number manually
        /// checks if the new page is in range
        /// if not an exception is thrown
        /// </summary>
        /// <param name="page"></param>
        /// <exception cref="Exception"></exception>
        public void SetPage(int page)
        {
            if (page <= LastPage && page >= FirstPage)
            {
                Page(page);
            }
            else
            {
                throw new Exception("Page is not within range");
            }
        }
    }
}