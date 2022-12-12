class UserService
{

    PostDAO postDB;
    ReplyDAO replyDB;

    public UserService()
    {
        postDB = new PostDAO();
        replyDB = new ReplyDAO();
    }

    public bool isLoginExist(List<User> users, string login)
    {
        foreach (var u in users)
        {
            if (u.Login == login)
                return true;
        }
        return false;
    }

    public void DeleteInserts(string id)
    {
        List<Post> posts = postDB.List();
        List<Reply> replies = replyDB.List();

        foreach (var p in posts)
            if (p.UserId == id)
                postDB.Delete(p.Id);
        foreach (var r in replies)
            if (r.UserId == id)
                replyDB.Delete(r.Id);
    }

}