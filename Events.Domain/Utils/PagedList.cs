using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Domain.Utils
{
    public sealed class PagedList<T> where T : class
    {
        public int Page { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalItens { get; private set; }
        public IEnumerable<T> Data { get; private set; }

        /// <summary>
        /// Create a PagedList intance. Prefer to use PagedListBuilder.Create() to create an instance.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="page"></param>
        /// <param name="totalPerPage"></param>
        /// 
        public PagedList()
        {

        }
        public PagedList(IQueryable<T> data, int page, int totalPerPage)
        {
            TotalItens = data.Count();
            Data = data.Skip(totalPerPage * (page - 1)).Take(totalPerPage).ToList();
            Page = page;
            TotalPages = (int)Math.Ceiling((decimal)TotalItens / (decimal)totalPerPage);
        } 

        public PagedList(IEnumerable<T> data, int totalItens, int page, int totalPages) {
            Data = data;
            TotalItens = totalItens;
            Page = page;
            TotalPages = totalPages;
        }
    }

    /// <summary>
    /// Builder for PagedList instances
    /// </summary>
    public static class PagedListBuilder
    {
        /// <summary>
        /// Returns a PagedList instance.
        /// </summary>
        /// <typeparam name="T">Data class</typeparam>
        /// <param name="data">Data list</param>
        /// <param name="page">Requested page</param>
        /// <param name="totalPerPage">Total of itens per page</param>
        /// <returns></returns>
        public static PagedList<T> Create<T>(IQueryable<T> data, int page = 1, int totalPerPage = 10) where T : class
        {
            if (page == 0) throw new ArgumentException("Page cannot be 0");
            if (totalPerPage == 0) throw new ArgumentException("TotalPerPage cannot be 0");
            return new PagedList<T>(data, page, totalPerPage);
        }

        public static PagedList<T> Create<T>(IEnumerable<T> data, int totalItens, int page, int totalPerPage) where T : class
        {
            if (page == 0) throw new ArgumentException("Page cannot be 0");
            if (totalPerPage == 0) throw new ArgumentException("TotalPerPage cannot be 0");
            return new PagedList<T>(data, totalItens, page, totalPerPage);
        }


    }
}
