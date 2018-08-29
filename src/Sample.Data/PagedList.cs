using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Data
{
    /// <summary>
    /// Paged list
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    public class PagedList<T> : IPagedList<T>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="limit">Page size or number of records per page</param>
        /// <param name="page">Page number to return</param>
        public PagedList(IQueryable<T> source, int limit, int page)
        {
            Initialization = InitializeAsync(source, limit, page);
        }

        public PagedList(IEnumerable<T> source, int limit, int page)
        {
            var enumerable = source as IList<T> ?? source.ToList();
            TotalCount = enumerable.Count();
            Data = enumerable;

            SetPaging(limit, page);
            PageData(Data);
        }

        [JsonIgnore]
        public Task Initialization { get; private set; }
        public IEnumerable<T> Data { get; private set; }

        public int Limit { get; private set; }
        public int Page { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }

        public bool HasPreviousPage
        {
            get { return (Page > 1); }
        }
        public bool HasNextPage
        {
            get { return (Page < TotalPages); }
        }

        private async Task InitializeAsync(IQueryable<T> source, int limit, int page)
        {
            var totalCount = await source.CountAsync().ConfigureAwait(false);
            TotalCount = totalCount;

            SetPaging(limit, page);
            await LoadDataAsync(source).ConfigureAwait(false);
        }

        private async Task LoadDataAsync(IQueryable<T> source)
        {
            var data = await source
                .Skip((Page - 1) * Limit)
                .Take(Limit)
                .ToListAsync()
                .ConfigureAwait(false);

            Data = data;
        }

        private void PageData(IEnumerable<T> source)
        {
            var data = source
                .Skip((Page - 1) * Limit)
                .Take(Limit);

            Data = data;
        }

        private void SetPaging(int limit, int page)
        {
            Limit = limit > 0 ? limit : 10;
            Page = page > 0 ? page : 1;
            TotalPages = (int)Math.Ceiling((double)TotalCount / Limit);
        }
    }
}
