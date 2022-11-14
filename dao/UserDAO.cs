using Npgsql;

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
    // , {user.Login}, {user.Password}, {user.Name}, {user.Surname}, {user.Gender}, {user.Role}, {user.RegDate}, {user.Posts}
    public void Create(User user)
    {
        cmd.CommandText = $"INSERT INTO Users VALUES(\'{user.Id}', '{user.Login}', '{user.Password}', '{user.Name}', '{user.Surname}', '{user.Gender}', '{user.Role}', '{user.RegDate}')";
        cmd.ExecuteNonQuery();
    }

}