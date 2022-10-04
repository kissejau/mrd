public class UserApi : IApi
{
    private List<User> users = new();

    public void Register(WebApplication app)
    {
        app.MapGet("/", () => "Hello");

        app.MapGet("/users", Get);

        app.MapPost("/user", Post);
    }

    private IResult Get()
    {
        return Results.Ok(users);
    }

    private IResult Post([FromBody] User user)
    {
        if (user == null)
            throw new Exception("User is not valid.");
        users.Add(user);
        return Results.Ok(user);
    }
}