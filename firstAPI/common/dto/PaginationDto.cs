public record PaginationDto(
    int PageNumber=1, 
    int PageSize=10,
    string? Search=null,
    string? SortBy=null
    );