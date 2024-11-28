using System.ComponentModel.DataAnnotations;

namespace Tan.Application.Dtos;

public record SampleRequestDto
{
    [Required(ErrorMessage = "the name parameter required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "the 'name' parameter must be greater than 2 characters and less than 100 characters. ")]
    [Display(Name = "Name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "the description parameter required")]
    [MaxLength(100, ErrorMessage = "the 'description' parameter must not exceed 100 characters.")]
    [Display(Name = "Description")]
    public string Description { get; set; }
}