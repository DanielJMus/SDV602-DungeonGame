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
        JSONDropService jsDrop = new JSONDropService { Token = "d341e18b-b0b5-4d33-a33d-9239ea617e5a" };
        jsDrop.All<Inventory, JsnReceiver>(LoadInventory, LoadInventoryFail);
    }

    private void LoadInventory (List<Inventory> inventoryItems) {
        inventory.Clear();
        foreach(Inventory item in inventoryItems) {
            inventory.Add(item.ItemName);
        }
        HasLoaded = true;
    }

    public void AddInventoryItem (string _itemname)
    {
        itemContext = _itemname;
        JSONDropService jsDrop = new JSONDropService { Token = "d341e18b-b0b5-4d33-a33d-9239ea617e5a" };
        jsDrop.Store<Inventory, JsnReceiver> (new List<Inventory>
        {
            new Inventory{ItemName = _itemname, PlayerID = GameManager.instance.ID}
        }, AddInventorySuccess);
    }

    public void RemoveInventoryItem (string _itemname)
    {
        itemContext = _itemname;
        JSONDropService jsDrop = new JSONDropService { Token = "d341e18b-b0b5-4d33-a33d-9239ea617e5a" };
        string query = "PlayerID = '" + GameManager.instance.ID + "' AND " + "ItemName = '" + _itemname + "'";
        jsDrop.Delete<Inventory, JsnReceiver>(query, RemoveInventorySuccess);
    }

    public bool HasItem (string _itemname) {
        return inventory.Contains(_itemname);
    }
}
