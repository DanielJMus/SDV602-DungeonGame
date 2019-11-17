using SQLite4Unity3d;

public class Inventory
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public string ItemName { get; set; }
    public int PlayerID { get; set; }

    public override string ToString ()
    {
        return string.Format("[ID: {0}, ItemName: {1}, PlayerID: {2}", ID, ItemName, PlayerID);
    }
}
