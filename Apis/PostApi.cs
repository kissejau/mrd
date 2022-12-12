public class PostApi : IApi
{
    private List<Post> posts = new();
    private PostDAO db;

    private PostService service;

    public PostApi()
    {
        db = new PostDAO();
        service = new PostService();
    }

    public void Register(WebApplication app)
    {
        app.MapGet("/post/{id}", GetById)
        .Produces<List<Post>>(StatusCodes.Status202Accepted)
        .WithName("GetPosts")
        .WithTags("Getters");

        app.MapGet("/post", Get)
        .Accepts<Post>("application/json")
        .Produces<Post>(StatusCodes.Status202Accepted)
        .WithName("GetPostById")
        .WithTags("Getters");

        app.MapPost("/post", Post)
        .Accepts<Post>("application/json")
        .WithName("UpdatePost")
        .WithTags("Updaters");

        app.MapPut("/post/{id}", PutById)
        .Accepts<Post>("application/json")
        .Produces<Post>(StatusCodes.Status201Created)
        .WithName("CreatePost")
        .WithTags("Creators");

        app.MapDelete("/post/{id}", DeleteById)
        .WithName("DeletePost")
        .WithTags("Deleters");
    }

    private IResult Get()
    {
        Console.WriteLine("GET_POSTS()");
        return Results.Ok(db.List());
    }

    private IResult GetById(string id)
    {
        Console.WriteLine("GET_POST_BY_ID()");
        Post p = db.Get(id);
        if (p != null)
            return Results.Ok(p);
        return Results.NotFound();

    }
    private IResult Post([FromBody] Post post)
    {
        Console.WriteLine("CREATE_POST()");
        if (post == null || post.Title == null || post.UserId == null)
            return Results.BadRequest(post);
        if (!service.isUserExist(post.UserId))
            return Results.BadRequest(post);
        // posts.Add(post);
        db.Create(post);
        service.InsertIdIntoUser(post.Id, post.UserId);
        return Results.Created($"/post/{post.Id}", post);
    }

    private IResult PutById([FromBody] Post post, string id)
    {
        Console.WriteLine("CHANGE_POST()");

        bool fl = db.Update(post, id);


        if (fl)
            return Results.Ok(db.Get(id));
        return Results.BadRequest();
    }

    private IResult DeleteById(string id)
    {
        Console.WriteLine("DELETE_POST()");

        bool fl = db.Delete(id);
        service.DeleteInserts(id);

        if (fl)
            return Results.Ok(id);
        return Results.BadRequest();
    }

}