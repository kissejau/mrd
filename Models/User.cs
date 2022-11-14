public record User : Entity
{
    [Required]
    public string? Login { get; set; }

    [Required]
    public string? Password { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public Gender? Gender { get; set; }

    public Role? Role { get; set; }

    public DateTime RegDate { get; } = DateTime.Now;

    public List<string> Posts { get; set; } = new List<string>();

}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Gender
{
    Male,
    Female,
    None
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Role
{
    Owner,
    Admin,
    User
}