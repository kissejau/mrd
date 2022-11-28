using Npgsql;
using System.Globalization;

class ReplyDAO : DAO<Reply>
{

    NpgsqlCommand cmd;

    public ReplyDAO()
    {
        cmd = new DataBaseConnector().CreateDbStatement();

        cmd.CommandText = "CREATE TABLE IF NOT EXISTS Replies(" +
        "Id VARCHAR, " +
        "Context VARCHAR, " +
        "CreatedDate TIMESTAMP, " +
        "UserId VARCHAR, " +
        "PostId VARCHAR" +
        ")";
        cmd.ExecuteNonQuery();
    }

    public void Create(Reply reply)
    {
        cmd.CommandText = $"INSERT INTO Replies VALUES(\'{reply.Id}', '{reply.Context}', '{reply.CreatedDate}'," +
        $" '{reply.UserId}', '{reply.PostId}')";
        cmd.ExecuteNonQuery();
        cmd.CommandText = String.Empty;
        Console.WriteLine("Created!!!");
    }

    public List<Reply> List()
    {
        cmd.CommandText = "SELECT * FROM Replies;";
        NpgsqlDataReader rdr = cmd.ExecuteReader();
        List<Reply> list = new();
        while (rdr.Read())
        {
            Reply reply = new Reply();
            reply.SetId(rdr[0].ToString());
            reply.Context = rdr[1].ToString();
            string[] date = rdr[2].ToString().Split()[0].Split('.');
            string[] time = rdr[2].ToString().Split()[1].Split(':');
            reply.SetDateTime(new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), Convert.ToInt32(date[0]),
                Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), Convert.ToInt32(time[2])));
            reply.UserId = rdr[3].ToString();
            reply.PostId = rdr[4].ToString();
            list.Add(reply);
        }
        rdr.Close();
        return list;
    }

    public Reply Get(string id)
    {
        cmd.CommandText = "SELECT * FROM Replies;";
        // Console.WriteLine($"|input_id:{id}|");
        NpgsqlDataReader rdr = cmd.ExecuteReader();
        Reply reply = new Reply();
        while (rdr.Read())
        {
            if (rdr[0].ToString() == id)
            {
                reply.SetId(rdr[0].ToString());
                reply.Context = rdr[1].ToString();
                string[] date = rdr[2].ToString().Split()[0].Split('.');
                string[] time = rdr[2].ToString().Split()[1].Split(':');
                reply.SetDateTime(new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), Convert.ToInt32(date[0]),
                    Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), Convert.ToInt32(time[2])));
                reply.UserId = rdr[3].ToString();
                reply.PostId = rdr[4].ToString();
                rdr.Close();

                return reply;
            }
            // Console.WriteLine($"|true: {rdr[0]}|");
            // Console.WriteLine($"|id:{rdr[0]}|");
        }
        rdr.Close();

        return null;
    }

    public bool Update(Reply t, string id)
    {
        Console.WriteLine(id);

        cmd.CommandText = "SELECT * FROM Replies;";

        NpgsqlDataReader rdr = cmd.ExecuteReader();
        string command = String.Empty;
        while (rdr.Read())
        {
            Console.WriteLine(rdr[0].ToString());
            if (rdr[0].ToString() == id)
            {
                command = "UPDATE Replies SET";
                if (t.Context is not null)
                    command += $" Context=\'{t.Context}\',";
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
        cmd.CommandText = "SELECT * FROM Replies;";

        NpgsqlDataReader rdr = cmd.ExecuteReader();
        string command = String.Empty;
        while (rdr.Read())
        {
            if (rdr[0].ToString() == id)
            {
                command = $"DELETE FROM Replies WHERE id=\'{id}\'";
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