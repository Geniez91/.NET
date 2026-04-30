using System.ComponentModel.DataAnnotations;

public record ArticleInputDto(
    [Required] [StringLength(50,MinimumLength = 2)] string Name,
    [Required] [StringLength(200,MinimumLength = 2)] string Description, 
    [Required] [Range(0,9999)] decimal Price, 
    [Required] int UserId);