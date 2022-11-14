public record Reply : Entity
{

    public string? Context { get; set; }

    public DateTime CreateDate { get; set; } = DateTime.Now;

    [Required]
    public string? UserId { get; set; }

    [Required]
    public string? PostId { get; set; }

}