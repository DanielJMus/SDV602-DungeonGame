using SQLite4Unity3d;

public class Account
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public int Level { get; set; }

    public override string ToString ()
    {
        return string.Format("[ID: {0}, Username: {1}, Password: {2}, Level: {3}", ID, Username, Password, Level);
    }
}
