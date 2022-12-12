class UserService
{

    public bool isLoginExist(List<User> users, string login)
    {
        foreach (var u in users)
        {
            if (u.Login == login)
                return true;
        }
        return false;
    }

}