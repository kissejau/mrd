using Npgsql;

public class DataBaseConnector
{

    public NpgsqlConnection InstanceDb()
    {
        string connectionString = "Host=localhost;Username=postgres;Password=1243;Database=mrd";

        NpgsqlConnection con = new NpgsqlConnection(connectionString);
        con.Open();

        return con;
    }

    public NpgsqlCommand CreateDbStatement()
    {
        var cmd = new NpgsqlCommand();
        cmd.Connection = InstanceDb();

        return cmd;
    }

}