class ReplyService
{

    UserDAO userDB;
    PostDAO postDB;

    public ReplyService()
    {
        userDB = new UserDAO();
        postDB = new PostDAO();
    }

    public bool isUserExist(string id)
    {
        List<User> users = userDB.List();
        foreach (var u in users)
        {
            if (u.Id == id)
                return true;
        }
        return false;
    }
    public bool isPostExist(string id)
    {
        List<Post> posts = postDB.List();
        foreach (var p in posts)
        {
            if (p.Id == id)
                return true;
        }
        return false;
    }

    public void InsertIdIntoUser(string id, string uId)
    {
        User user = userDB.Get(uId);
        user.Posts.Add(id);
        userDB.Update(user, uId);
    }
    public void InsertIdIntoPost(string id, string pId)
    {
        Post post = postDB.Get(pId);
        post.Replies.Add(id);
        postDB.Update(post, pId);
    }
}