using System.ComponentModel.DataAnnotations;

public class CreateMovieDto
{
    [Required]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public string Genre { get; set; }

    [Required]
    public DateTime ReleaseDate { get; set; }

    public IFormFile? Image { get; set; }
}
