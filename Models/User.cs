public record User
{
    public Guid? Id { get; init; }

    [Required]
    public string? Login { get; set; } = string.Empty;

    [Required]
    public string? Password { get; set; } = string.Empty;

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public Gender? Gender { get; set; }

    public Role? Role { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Gender
{
    Brogy,
    Dorry
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Role
{
    Owner,
    Admin,
    User
}