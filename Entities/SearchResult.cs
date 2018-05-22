using System.Collections.Generic;

namespace Beehouse.FrameworkStandard.Entities
{
    public class SearchResult<T> where T:class
    {
        public ICollection<T> Items { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
        public int Count { get; set; }

        public SearchResult()
        {

        }
    }
}