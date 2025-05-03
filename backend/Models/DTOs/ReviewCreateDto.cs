using System.ComponentModel.DataAnnotations;

public class ReviewCreateDto
{
    [Required]
    public int MovieId { get; set; }

    [Required]
    [Range(1, 5)]
    public int Rating { get; set; }

    [MaxLength(500)]
    public string? Comment { get; set; } // تعليق اختياري
}
