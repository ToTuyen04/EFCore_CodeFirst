using System.ComponentModel.DataAnnotations;

namespace EFCore_CodeFirst.Dto
{
    public class UrlQueryParameters
    {
        [Range(1, int.MaxValue, ErrorMessage = "Page number must be greater than 0")]
        public int PageNumber { get; set; } = 1;

        [Range(1, 100, ErrorMessage = "Page size must be between 1 and 100")]
        public int PageSize { get; set; } = 10;

        public string? SearchTerm { get; set; }

        public string? SortBy { get; set; }

        public bool SortDescending { get; set; } = false;
    }
}
