using Npgsql;
using System.Globalization;

class UserDAO : DAO<User>
{

    NpgsqlCommand cmd;

    public UserDAO()
    {
        cmd = new DataBaseConnector().CreateDbStatement();

        cmd.CommandText = "CREATE TABLE IF NOT EXISTS Users(" +
        "Id VARCHAR, " +
        "Login VARCHAR, " +
        "Password VARCHAR, " +
        "Name VARCHAR, " +
        "Surname VARCHAR, " +
        "Gender VARCHAR, " +
        "Role VARCHAR, " +
        "RegDate TIMESTAMP, " +
        "Posts VARCHAR[] " +
        ")";
        cmd.ExecuteNonQuery();
    }

    public void Create(User user)
    {
        string posts = DBService.TakeListToSqlArray<String>(user.Posts);
        cmd.CommandText = $"INSERT INTO Users VALUES(\'{user.Id}', '{user.Login}', '{user.Password}'," +
        $" '{user.Name}', '{user.Surname}', '{user.Gender}', '{user.Role}', '{user.CreatedDate}', '{posts}')";
        cmd.ExecuteNonQuery();
        cmd.CommandText = String.Empty;
        Console.WriteLine("Created!!!");
    }

    public List<User> List()
    {
        cmd.CommandText = "SELECT * FROM users;";
        NpgsqlDataReader rdr = cmd.ExecuteReader();
        List<User> list = new();
        while (rdr.Read())
        {
            User user = new User();
            user.SetId(rdr[0].ToString());
            user.Login = rdr[1].ToString();
            user.Password = rdr[2].ToString();
            user.Name = rdr[3].ToString();
            user.Surname = rdr[4].ToString();
            user.Gender = DBService.WhichGender(rdr[5].ToString());
            user.Role = DBService.WhichRole(rdr[6].ToString());
            string[] date = rdr[7].ToString().Split()[0].Split('.');
            string[] time = rdr[7].ToString().Split()[1].Split(':');
            user.SetDateTime(new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), Convert.ToInt32(date[0]),
                Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), Convert.ToInt32(time[2])));
            List<string> posts = new();
            for (int i = 0; i < ((string[])rdr[8]).Length; i++)
                posts.Add(((string[])rdr[8])[i]);
            user.Posts = posts;
            list.Add(user);
        }
        rdr.Close();
        return list;
    }

    public User Get(string id)
    {
        cmd.CommandText = "SELECT * FROM users;";
        // Console.WriteLine($"|input_id:{id}|");
        NpgsqlDataReader rdr = cmd.ExecuteReader();
        User user = new User();
        while (rdr.Read())
        {
            if (rdr[0].ToString() == id)
            {
                user.SetId(rdr[0].ToString());
                user.Login = rdr[1].ToString();
                user.Password = rdr[2].ToString();
                user.Name = rdr[3].ToString();
                user.Surname = rdr[4].ToString();
                user.Gender = DBService.WhichGender(rdr[5].ToString());
                user.Role = DBService.WhichRole(rdr[6].ToString());
                string[] date = rdr[7].ToString().Split()[0].Split('.');
                string[] time = rdr[7].ToString().Split()[1].Split(':');
                user.SetDateTime(new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), Convert.ToInt32(date[0]),
                    Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), Convert.ToInt32(time[2])));
                List<string> posts = new();
                for (int i = 0; i < ((string[])rdr[8]).Length; i++)
                    posts.Add(((string[])rdr[8])[i]);
                user.Posts = posts;
                rdr.Close();

                return user;
            }
            // Console.WriteLine($"|true: {rdr[0]}|");
            // Console.WriteLine($"|id:{rdr[0]}|");
        }
        rdr.Close();

        return null;
    }

    public bool Update(User t, string id)
    {
        Console.WriteLine(id);

        cmd.CommandText = "SELECT * FROM users;";

        NpgsqlDataReader rdr = cmd.ExecuteReader();
        string command = String.Empty;
        while (rdr.Read())
        {
            Console.WriteLine(rdr[0].ToString());
            if (rdr[0].ToString() == id)
            {
                command = "UPDATE Users SET";
                if (t.Login is not null)
                    command += $" Login=\'{t.Login}\',";
                if (t.Password is not null)
                    command += $" Password=\'{t.Password}\',";
                if (t.Name is not null)
                    command += $" Name=\'{t.Name}\',";
                if (t.Surname is not null)
                    command += $" Surname=\'{t.Surname}\',";
                if (t.Gender is not null)
                    command += $" Gender=\'{t.Gender}\',";
                if (t.Role is not null)
                    command += $" Role=\'{t.Role}\',";
                if (t.Posts is not null)
                    command += $" Posts=\'{DBService.TakeListToSqlArray<String>(t.Posts)}\',";
                command = command.Remove(command.Length - 1);
                command += $" WHERE id=\'{id}\';";
                // Console.WriteLine(cmd.CommandText);
                Console.WriteLine("Changed!!!");
                break;


            }
            // Console.WriteLine($"|true: {rdr[0]}|");
            // Console.WriteLine($"|id:{rdr[0]}|");
        }
        rdr.Close();

        if (command != String.Empty)
        {
            Console.WriteLine(command);
            cmd.CommandText = command;
            cmd.ExecuteNonQuery();

            return true;
        }
        return false;
    }

    public bool Delete(string id)
    {
        cmd.CommandText = "SELECT * FROM users;";

        NpgsqlDataReader rdr = cmd.ExecuteReader();
        string command = String.Empty;
        while (rdr.Read())
        {
            if (rdr[0].ToString() == id)
            {
                command = $"DELETE FROM Users WHERE id=\'{id}\'";
                break;
            }
        }
        rdr.Close();

        if (command != String.Empty)
        {
            cmd.CommandText = command;
            cmd.ExecuteNonQuery();
            Console.WriteLine(cmd.CommandText);
            return true;
        }
        return false;
    }
}