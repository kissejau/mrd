public class UserApi : IApi
{
    // private List<User> users = new();

    private UserDAO db;
    private UserService service;
    public UserApi()
    {
        db = new UserDAO();
        service = new UserService();
    }

    public void Register(WebApplication app)
    {

        app.MapGet("/user/{id}", GetById)
        .Produces<List<User>>(StatusCodes.Status202Accepted)
        .WithName("GetUsers")
        .WithTags("Getters");

        app.MapGet("/user", Get)
        .Accepts<User>("application/json")
        .Produces<User>(StatusCodes.Status202Accepted)
        .WithName("GetUserById")
        .WithTags("Getters");

        app.MapPost("/user", Post)
        .Accepts<User>("application/json")
        .WithName("UpdateUser")
        .WithTags("Updaters");

        app.MapPut("/user/{id}", PutById)
        .Accepts<User>("application/json")
        .Produces<User>(StatusCodes.Status201Created)
        .WithName("CreateUser")
        .WithTags("Creators");

        app.MapDelete("/user/{id}", DeleteById)
        .WithName("DeleteUser")
        .WithTags("Deleters");
    }

    private IResult Get()
    {
        Console.WriteLine("GET_USERS()");
        // return Results.Ok(users);
        return Results.Ok(db.List());
    }

    private IResult GetById(string id)
    {
        Console.WriteLine("GET_USER_BY_ID()");
        User user = db.Get(id);
        // var u = users.Where(u => u.Id.ToString() == id).ToArray();
        // if (u.Length != 1)
        //     return Results.NotFound();
        if (user != null)
            return Results.Ok(db.Get(id));
        return Results.NotFound();

    }
    private IResult Post([FromBody] User user)
    {
        Console.WriteLine("ADD_USER()");
        if (user == null || user.Login == null || user.Password == null)
            return Results.BadRequest(user);
        if (service.isLoginExist(db.List(), user.Login))
            return Results.BadRequest(user);
        // users.Add(user);
        db.Create(user);
        return Results.Created($"/user/{user.Id}", user);
    }

    private IResult PutById([FromBody] User user, string id)
    {
        Console.WriteLine("CHANGE_USER()");
        // var u = users.Where(u => u.Id.ToString() == id).ToArray();
        // if (u.Length != 1)
        //     return Results.BadRequest();


        // var temp = u[0];

        // if (!(user.Login == null)) temp.Login = user.Login;
        // if (!(user.Password == null)) temp.Password = user.Password;
        // if (!(user.Name == null)) temp.Name = user.Name;
        // if (!(user.Surname == null)) temp.Surname = user.Surname;
        // if (!(user.Role == null)) temp.Role = user.Role;
        // if (!(user.Gender == null)) temp.Gender = user.Gender;

        bool fl = db.Update(user, id);
        if (fl)
            return Results.Ok(db.Get(id));
        return Results.BadRequest();
    }

    private IResult DeleteById(string id)
    {
        Console.WriteLine("DELETE_USER()");
        // var u = users.Where(u => u.Id.ToString() == id).ToArray();
        // if (u.Length != 1)
        bool fl = db.Delete(id);
        if (fl)
            return Results.Ok(id);
        return Results.BadRequest();

    }

}