using System.ComponentModel.DataAnnotations;

public record UserInputDto(
    [Required] [EmailAddress] String Email, 
    [Required] [StringLength(50,MinimumLength = 2)] String UserName,
    [Required] [StringLength(100,MinimumLength = 8)] String Password);