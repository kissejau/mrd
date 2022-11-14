public class PostApi : IApi
{
    private List<Post> posts = new();

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
        return Results.Ok(posts);
    }

    private IResult GetById(string id)
    {
        var u = posts.Where(u => u.Id.ToString() == id).ToArray();
        if (u.Length != 1)
            return Results.NotFound();
        return Results.Ok(u);

    }
    private IResult Post([FromBody] Post post)
    {
        if (post == null || post.Title == null || post.UserId == null)
            return Results.BadRequest(post);
        posts.Add(post);
        // db.Create(post);
        return Results.Created($"/post/{post.Id}", post);
    }

    private IResult PutById([FromBody] Post post, string id)
    {
        var u = posts.Where(u => u.Id.ToString() == id).ToArray();
        if (u.Length != 1)
            return Results.BadRequest();


        var temp = u[0];

        if (!(post.Title == null)) temp.Title = post.Title;
        if (!(post.Context == null)) temp.Context = post.Context;

        return Results.Ok(temp);
    }

    private IResult DeleteById(string id)
    {
        var u = posts.Where(u => u.Id.ToString() == id).ToArray();
        if (u.Length != 1)
            return Results.BadRequest();
        return Results.Ok(posts.Remove(u[0]));
    }

}