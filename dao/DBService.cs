class DBService
{

    public static string TakeListToSqlArray<T>(List<T> list)
    {
        string sqlContext = "{";
        for (int i = 0; i < list.Count; i++)
        {
            sqlContext += $"\"{list[i]}\"";
            if (list.Count > i + 1)
                sqlContext += ", ";
        }
        return (sqlContext + "}");
    }

    public static Gender WhichGender(string gender)
    {
        switch (gender)
        {
            case "Male":
                return Gender.Male;
                break;
            case "Female":
                return Gender.Female;
                break;
            default:
                return Gender.None;
                break;
        }
    }

    public static Role WhichRole(string role)
    {
        switch (role)
        {
            case "Admin":
                return Role.Admin;
                break;
            case "Owner":
                return Role.Owner;
                break;
            default:
                return Role.User;
                break;
        }
    }
}