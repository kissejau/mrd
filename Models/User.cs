public record User
{
    private string _id { get; init; } = Guid.NewGuid().ToString();
    public string Id => _id; // GENIUS

    [Required]
    public string? Login { get; set; }

    [Required]
    public string? Password { get; set; }

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