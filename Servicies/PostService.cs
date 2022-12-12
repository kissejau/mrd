class PostService
{
    private UserDAO userDB;
    public PostService()
    {
        userDB = new UserDAO();
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
}