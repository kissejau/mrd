public record Reply : Entity
{

    public string? Context { get; set; }

    [Required]
    public string? UserId { get; set; }

    [Required]
    public string? PostId { get; set; }

}