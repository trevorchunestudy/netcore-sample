using Sample.Core.Domain;
using Sample.Core.Domain.Automotive;
using System.Linq;

namespace Sample.Data.Extensions
{
    public static class PageSortExtensions
    {
        #region Searching

        public static IQueryable<Vehicle> VehicleContains(this IQueryable<Vehicle> entity, PagingSort pagingSort)
        {
            return entity.Where(x => string.IsNullOrEmpty(pagingSort.Contains) ||
                                     x.Title.Contains(pagingSort.Contains));
        }

        public static IQueryable<Owner> OwnerContains(this IQueryable<Owner> entity, PagingSort pagingSort)
        {
            return entity.Where(x => string.IsNullOrEmpty(pagingSort.Contains) ||
                                     x.Name.Contains(pagingSort.Contains));
        }

        #endregion

        #region Sorting

        public static IQueryable<Vehicle> SortVehicleBy(this IQueryable<Vehicle> entity, PagingSort pagingSort)
        {
            var isDescending = pagingSort.IsDescending();
            switch (pagingSort.Order)
            {
                case "title":
                    return isDescending
                        ? entity.OrderByDescending(x => x.Title)
                        : entity.OrderBy(x => x.Title);
                default:
                    return entity.OrderBy(x => x.Id);
            }
        }

        public static IQueryable<Owner> SortOwnerBy(this IQueryable<Owner> entity, PagingSort pagingSort)
        {
            var isDescending = pagingSort.IsDescending();
            switch (pagingSort.Order)
            {
                case "name":
                    return isDescending
                        ? entity.OrderByDescending(x => x.Name)
                        : entity.OrderBy(x => x.Name);
                default:
                    return entity.OrderBy(x => x.Id);
            }
        }

        #endregion

        private static bool IsDescending(this PagingSort pagingSort)
        {
            if (string.IsNullOrEmpty(pagingSort.Order))
                return false;

            var split = pagingSort.Order.Split('-');
            pagingSort.Order = split.Length > 1 ? split[1] : split[0];
            return split.Length > 1;
        }
    }
}
