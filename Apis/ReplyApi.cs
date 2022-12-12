public class ReplyApi : IApi
{

    private List<Reply> replies = new();
    private ReplyDAO db;
    private ReplyService service;

    public ReplyApi()
    {
        db = new ReplyDAO();
        service = new ReplyService();
    }

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
        Console.WriteLine("GET_REPLIES()");
        return Results.Ok(db.List());
    }

    private IResult GetById(string id)
    {
        Console.WriteLine("GET_REPLY_BY_ID()");
        Reply r = db.Get(id);
        if (r == null)
            return Results.NotFound();
        return Results.Ok(r);

    }
    private IResult Post([FromBody] Reply reply)
    {
        Console.WriteLine("CREATE_REPLY()");
        if (reply == null || reply.UserId == null || reply.PostId == null)
            return Results.BadRequest(reply);
        if (!service.isPostExist(reply.PostId) || !service.isUserExist(reply.UserId))
            return Results.BadRequest(reply);
        service.InsertIdIntoPost(reply.Id, reply.PostId);
        service.InsertIdIntoUser(reply.Id, reply.UserId);
        db.Create(reply);
        return Results.Created($"/reply/{reply.Id}", reply);
    }

    private IResult PutById([FromBody] Reply reply, string id)
    {
        Console.WriteLine("CHANGE_REPLY()");
        bool fl = db.Update(reply, id);

        if (fl)
            return Results.Ok(db.Get(id));
        return Results.BadRequest();
    }

    private IResult DeleteById(string id)
    {
        Console.WriteLine("DELETE_Reply()");

        bool fl = db.Delete(id);
        if (fl)
            return Results.Ok(id);
        return Results.BadRequest();
    }

}