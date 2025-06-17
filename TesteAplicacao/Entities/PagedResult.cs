using System.Text.Json;
using System.Text.Json.Serialization;
using TesteAplicacao.DTO;

namespace TesteAplicacao.DTO
{
    public class PagedResult<T>
    {
        [JsonPropertyName("result")]
        public IList<T> Result { get; set; }

        [JsonPropertyName("metadata")]
        public PaginacaoResponseDTO MetaData { get; set; }

        public static PagedResult<T> Create(IList<T> results, int page, int resultsPage, int totalPages, int totalResults)
        {
            return new PagedResult<T>()
            {
                Result = results,
                MetaData = new PaginacaoResponseDTO()
                {
                    currentPage = page,
                    totalPages = totalPages,
                    totalRecords = totalResults
                }
            };
        }

        public static PagedResult<T> Empty => new PagedResult<T>
        {
            Result = [],
            MetaData = new PaginacaoResponseDTO()
        };
    }
}