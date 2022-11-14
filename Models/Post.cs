public record Post : Entity
{

    [Required]
    public string? Title { get; set; }

    public string? Context { get; set; }

    public DateTime CreateDate { get; set; } = DateTime.Now;

    [Required]
    public string? UserId { get; set; }

    public List<string> Replies { get; set; } = new List<string>();

}