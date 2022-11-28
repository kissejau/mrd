using Npgsql;
using System.Globalization;

class PostDAO : DAO<Post>
{

    NpgsqlCommand cmd;

    public PostDAO()
    {
        cmd = new DataBaseConnector().CreateDbStatement();

        cmd.CommandText = "CREATE TABLE IF NOT EXISTS Posts(" +
        "Id VARCHAR, " +
        "Title VARCHAR, " +
        "Context VARCHAR, " +
        "UserId VARCHAR, " +
        "CreateDate TIMESTAMP, " +
        "Replies VARCHAR[] " +
        ")";
        cmd.ExecuteNonQuery();
    }

    public void Create(Post post)
    {
        string replies = DBService.TakeListToSqlArray<String>(post.Replies);
        cmd.CommandText = $"INSERT INTO Posts VALUES(\'{post.Id}', '{post.Title}', '{post.Context}'," +
        $" '{post.UserId}', '{post.CreatedDate}', '{replies}')";
        cmd.ExecuteNonQuery();
        cmd.CommandText = String.Empty;
        Console.WriteLine("Created!!!");
    }

    public List<Post> List()
    {
        cmd.CommandText = "SELECT * FROM Posts;";
        NpgsqlDataReader rdr = cmd.ExecuteReader();
        List<Post> list = new();
        while (rdr.Read())
        {
            Post post = new Post();
            post.Title = rdr[1].ToString();
            post.Context = rdr[2].ToString();
            post.UserId = rdr[3].ToString();
            string[] date = rdr[4].ToString().Split()[0].Split('.');
            string[] time = rdr[4].ToString().Split()[1].Split(':');
            post.SetDateTime(new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), Convert.ToInt32(date[0]),
                Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), Convert.ToInt32(time[2])));
            List<string> replies = new();
            for (int i = 0; i < ((string[])rdr[5]).Length; i++)
                replies.Add(((string[])rdr[5])[i]);
            post.Replies = replies;
            post.SetId(rdr[0].ToString());
            list.Add(post);
        }
        rdr.Close();
        return list;
    }

    public Post Get(string id)
    {
        cmd.CommandText = "SELECT * FROM posts;";
        // Console.WriteLine($"|input_id:{id}|");
        NpgsqlDataReader rdr = cmd.ExecuteReader();
        Post post = new Post();
        while (rdr.Read())
        {
            if (rdr[0].ToString() == id)
            {
                post.SetId(rdr[0].ToString());
                post.Title = rdr[1].ToString();
                post.Context = rdr[2].ToString();
                post.UserId = rdr[3].ToString();
                string[] date = rdr[4].ToString().Split()[0].Split('.');
                string[] time = rdr[4].ToString().Split()[1].Split(':');
                post.SetDateTime(new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), Convert.ToInt32(date[0]),
                    Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), Convert.ToInt32(time[2])));
                List<string> replies = new();
                for (int i = 0; i < ((string[])rdr[5]).Length; i++)
                    replies.Add(((string[])rdr[5])[i]);
                post.Replies = replies;
                rdr.Close();

                return post;
            }
            // Console.WriteLine($"|true: {rdr[0]}|");
            // Console.WriteLine($"|id:{rdr[0]}|");
        }
        rdr.Close();

        return null;
    }

    public bool Update(Post t, string id)
    {
        Console.WriteLine(id);

        cmd.CommandText = "SELECT * FROM posts;";

        NpgsqlDataReader rdr = cmd.ExecuteReader();
        string command = String.Empty;
        while (rdr.Read())
        {
            Console.WriteLine(rdr[0].ToString());
            if (rdr[0].ToString() == id)
            {
                command = "UPDATE Posts SET";
                if (t.Title is not null)
                    command += $" Title=\'{t.Title}\',";
                if (t.Context is not null)
                    command += $" Context=\'{t.Context}\',";
                if (t.Replies is not null)
                    command += $" Replies=\'{DBService.TakeListToSqlArray<String>(t.Replies)}\',";
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
        cmd.CommandText = "SELECT * FROM Posts;";

        NpgsqlDataReader rdr = cmd.ExecuteReader();
        string command = String.Empty;
        while (rdr.Read())
        {
            if (rdr[0].ToString() == id)
            {
                command = $"DELETE FROM Posts WHERE id=\'{id}\'";
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