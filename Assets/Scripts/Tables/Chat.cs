using SQLite4Unity3d;

public class Chat
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public string PlayerName { get; set; }
    public int Level { get; set; }
    public string Content { get; set; }

    public override string ToString ()
    {
        return string.Format("[ID: {0}, PlayerName: {1}, Level: {2}, Content: {3}", ID, PlayerName, Level, Content);
    }
}

