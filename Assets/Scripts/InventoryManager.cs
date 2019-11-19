using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<string> inventory = new List<string>();
    private string itemContext;
    public bool HasLoaded = false;

    public void AddInventorySuccess(JsnReceiver pReceived) { inventory.Add(itemContext); }
    public void RemoveInventorySuccess(JsnReceiver pReceived) { inventory.Remove(itemContext); }
    public void LoadInventoryFail(JsnReceiver pReceived) { }

    void Awake () {
        GameManager.instance.JSON.All<Inventory, JsnReceiver>(LoadInventory, LoadInventoryFail);
    }

    // Get all of the current items that the player has in their inventory
    private void LoadInventory (List<Inventory> inventoryItems) {
        inventory.Clear();
        foreach(Inventory item in inventoryItems) {
            inventory.Add(item.ItemName);
        }
        HasLoaded = true;
    }

    // Add a single item to the player's inventory
    public void AddInventoryItem (string _itemname)
    {
        itemContext = _itemname;
        GameManager.instance.JSON.Store<Inventory, JsnReceiver> (new List<Inventory>
        {
            new Inventory{ItemName = _itemname, PlayerID = GameManager.instance.ID}
        }, AddInventorySuccess);
    }

    // Remove an item from the player's inventory.
    public void RemoveInventoryItem (string _itemname)
    {
        itemContext = _itemname;
        string query = "PlayerID = '" + GameManager.instance.ID + "' AND " + "ItemName = '" + _itemname + "'";
        GameManager.instance.JSON.Delete<Inventory, JsnReceiver>(query, RemoveInventorySuccess);
    }

    // Returns true if user has the specified item.
    public bool HasItem (string _itemname) {
        return inventory.Contains(_itemname);
    }
}
