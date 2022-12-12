class PostService
{
    private UserDAO userDB;
    private ReplyDAO replyDB;
    public PostService()
    {
        userDB = new UserDAO();
        replyDB = new ReplyDAO();
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

    public void InsertIdIntoUser(string id, string uId)
    {
        User user = userDB.Get(uId);
        user.Posts.Add(id);
        userDB.Update(user, uId);
    }

    public void DeleteInserts(string id)
    {
        List<Reply> replies = replyDB.List();
        foreach (var r in replies)
            if (r.PostId == id)
                replyDB.Delete(r.Id);
    }
}