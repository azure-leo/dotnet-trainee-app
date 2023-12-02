using System.ComponentModel.DataAnnotations;

namespace TraineeAPI.DTO;

public class ArticleDTO
{
    [Required]
    [MinLength(2), MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required] [MaxLength(1000)] 
    public string Content { get; set; } = string.Empty;
}