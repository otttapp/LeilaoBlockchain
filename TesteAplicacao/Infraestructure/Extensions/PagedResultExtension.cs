using Microsoft.EntityFrameworkCore;
using TesteAplicacao.DTO;

namespace TesteAplicacao.Infraestructure.Extensions
{
    public static class PagedResultExtension
    {
        public static async Task<PagedResult<T>> PaginateAsync<T>(this IQueryable<T> data, PaginacaoRequestDTO queryPaginate)
        {
            int page = queryPaginate.page switch
            {
                <= 0 or null => 1,
                _ => queryPaginate.page.Value
            };

            int limitPage = queryPaginate.perPage switch
            {
                <= 0 or null => 0,
                _ => queryPaginate.perPage.Value
            };

            var result = await data.ToListAsync();

            int totalItens = result.Count;
            int totalPages = limitPage == 0 || totalItens <= limitPage ? 1 : (int)Math.Ceiling((decimal)totalItens / limitPage);
            page = page > totalPages ? totalPages : page;

            if (!String.IsNullOrWhiteSpace(queryPaginate.sortBy))
            {
                var propertyInfo = typeof(T)
                    .GetProperties()
                    .FirstOrDefault(pr => pr.Name.Equals(queryPaginate.sortBy));

                if (propertyInfo != null)
                {
                    if (!String.IsNullOrWhiteSpace(queryPaginate.sortDirection) && queryPaginate.sortDirection.Equals("desc", StringComparison.CurrentCultureIgnoreCase))
                    {
                        result = [.. result
                            .OrderByDescending(p => propertyInfo
                                .GetValue(p, null))];
                    }
                    else
                    {
                        result = [.. result
                            .OrderBy(p => propertyInfo
                                .GetValue(p, null))];
                    }
                }
            }

            result = totalPages > 1 ? [.. result.Skip((page - 1) * limitPage).Take(limitPage)] : result;

            return new()
            {
                Result = [.. result],
                MetaData = new()
                {
                    currentPage = page,
                    totalPages = totalPages,
                    totalRecords = totalItens
                }
            };
        }

        public static PagedResult<T> PaginateList<T>(this List<T> data, int page, int results)
        {
            if (page <= 0)
            {
                page = 1;
            }

            results = results switch
            {
                <= 0 => 10,
                > 100 => 100,
                _ => results
            };

            var totalResults = data.Count;
            var totalPages = totalResults <= results ? 1 : (int)Math.Ceiling((double)totalResults / results);
            var result = data.Skip((page - 1) * results).Take(results).ToList();

            return PagedResult<T>.Create(result, page, results, totalPages, totalResults);
        }
    }
}