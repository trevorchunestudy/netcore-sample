using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.Data
{
    public interface IPagedList<out T>
    {
        Task Initialization { get; }
        IEnumerable<T> Data { get; }
        int Limit { get; }
        int Page { get; }
        int TotalCount { get; }
        int TotalPages { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
    }
}
