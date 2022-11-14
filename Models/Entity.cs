public record Entity
{

    private string _id { get; init; } = Guid.NewGuid().ToString();
    public string Id => _id; // GENIUS

}