using System.Globalization;
using System.Text;

namespace TesteAplicacao.DTO
{
    /**
     * Request padrão quando tem paginação
     */
    public class PaginacaoRequestDTO
    {
        private string? _filtroGenerico;
        public string? filtro_generico
        {
            get => _filtroGenerico;
            set
            {
                _filtroGenerico = NormalizaFiltro(value);
            }
        }
        public int? page { get; set; } = 1;
        public int? perPage { get; set; } = 0;
        public string sortDirection { get; set; } = "asc";
        public string? sortBy { get; set; }
        public bool ativo { get; set; } = true;

        private static string NormalizaFiltro(string? filtro)
        {
            if (string.IsNullOrWhiteSpace(filtro)) return filtro ?? string.Empty;

            var tamanho = filtro.Length;

            var stringBuilder = new StringBuilder(tamanho + 2);
            stringBuilder.Append('%');

            for (int i = 0; i < tamanho; i++)
            {
                var caractere = filtro[i];

                switch (caractere)
                {
                    case ' ': stringBuilder.Append('%'); break;
                    case '%': stringBuilder.Append("/%"); break;

                    default:
                        if (CharUnicodeInfo.GetUnicodeCategory(caractere) == UnicodeCategory.NonSpacingMark) continue;
                        stringBuilder.Append(caractere);
                        break;
                }
            }

            stringBuilder.Append('%');

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }

    /**
     * Retorno padrão para paginação
     */
    public class PaginacaoResponseDTO
    {
        public int totalRecords { get; set; }
        public int totalPages { get; set; }
        public int currentPage { get; set; }
    }
}
