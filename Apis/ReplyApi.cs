public class ReplyApi : IApi
{

    private List<Reply> replies = new();

    public void Register(WebApplication app)
    {
        app.MapGet("/reply/{id}", GetById)
        .Produces<List<Reply>>(StatusCodes.Status202Accepted)
        .WithName("GetReplies")
        .WithTags("Getters");

        app.MapGet("/reply", Get)
        .Accepts<Reply>("application/json")
        .Produces<Reply>(StatusCodes.Status202Accepted)
        .WithName("GetReplyById")
        .WithTags("Getters");

        app.MapPost("/reply", Post)
        .Accepts<Reply>("application/json")
        .WithName("UpdateReply")
        .WithTags("Updaters");

        app.MapPut("/reply/{id}", PutById)
        .Accepts<Reply>("application/json")
        .Produces<Reply>(StatusCodes.Status201Created)
        .WithName("CreateReply")
        .WithTags("Creators");

        app.MapDelete("/reply/{id}", DeleteById)
        .WithName("DeleteReply")
        .WithTags("Deleters");
    }

    private IResult Get()
    {
        return Results.Ok(replies);
    }

    private IResult GetById(string id)
    {
        var u = replies.Where(u => u.Id.ToString() == id).ToArray();
        if (u.Length != 1)
            return Results.NotFound();
        return Results.Ok(u);

    }
    private IResult Post([FromBody] Reply reply)
    {
        if (reply == null || reply.UserId == null || reply.PostId == null)
            return Results.BadRequest(reply);
        replies.Add(reply);
        // db.Create(reply);
        return Results.Created($"/reply/{reply.Id}", reply);
    }

    private IResult PutById([FromBody] Reply reply, string id)
    {
        var u = replies.Where(u => u.Id.ToString() == id).ToArray();
        if (u.Length != 1)
            return Results.BadRequest();


        var temp = u[0];

        if (!(reply.Context == null)) temp.Context = reply.Context;

        return Results.Ok(temp);
    }

    private IResult DeleteById(string id)
    {
        var u = replies.Where(u => u.Id.ToString() == id).ToArray();
        if (u.Length != 1)
            return Results.BadRequest();
        return Results.Ok(replies.Remove(u[0]));
    }

}