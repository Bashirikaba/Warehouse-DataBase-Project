using System.Linq.Dynamic.Core;
using System.Reflection;
using Business.Dto.Search.Params;
using Business.Enums;

namespace ApplicationData.Shared.Helpers;

public static class QueryFilterHelper
{
    public static IQueryable<T> ApplyStringFilters<T>(this IQueryable<T> query, IList<StringParam> filters)
    {
        foreach (var filter in filters)
        {
            if (IsValidField<T>(filter.Field))
            {
                string predicate = filter.Operation == SearchOperations.Equal
                    ? $"{filter.Field} == @0"
                    : $"{filter.Field}.Contains(@0)";
                query = query.Where(predicate, filter.Value);
            }
        }
        return query;
    }

    public static IQueryable<T> ApplyNumberFilters<T>(this IQueryable<T> query, IEnumerable<NumberParam> filters)
    {
        foreach (var filter in filters)
        {
            if (IsValidField<T>(filter.Field))
            {
                string op = filter.Operation switch
                {
                    SearchOperations.MoreThan => ">",
                    SearchOperations.LessThan => "<",
                    _ => "=="
                };
                string predicate = $"{filter.Field} {op} @0";
                query = query.Where(predicate, filter.Value);
            }
        }
        return query;
    }

    public static IQueryable<T> ApplyDateFilters<T>(this IQueryable<T> query, IEnumerable<DateParam> filters)
    {
        foreach (var filter in filters)
        {
            if (IsValidField<T>(filter.Field))
            {
                string op = filter.Operation switch
                {
                    SearchOperations.MoreThan => ">",
                    SearchOperations.LessThan => "<",
                    _ => "=="
                };
                string predicate = $"{filter.Field} {op} @0";
                query = query.Where(predicate, filter.Value);
            }
        }
        return query;
    }

    private static bool IsValidField<T>(string fieldName)
    {
        if (fieldName == "Id") return false;

        return typeof(T).GetProperty(fieldName, BindingFlags.Public | BindingFlags.Instance) != null;
    }
}
