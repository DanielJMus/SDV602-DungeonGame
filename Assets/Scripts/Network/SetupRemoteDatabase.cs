using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupRemoteDatabase : MonoBehaviour
{

    // Various database queries to set things up.
    void Start()
    {
        GameManager.instance.JSON.Drop<Account, JsnReceiver>(Received);

        // GameManager.instance.JSON.Create<Account, JsnReceiver>(new Account
        // {               
        //     Username = "******************************",
        //     Password = "******************************",
        //     Level = 0,
        // }, Received);

        // GameManager.instance.JSON.Create<Inventory, JsnReceiver>(new Inventory
        // {               
        //     ItemName = "******************************",
        //     PlayerID = 254,
        // }, Received);

        // GameManager.instance.JSON.Store<Account, JsnReceiver> (new List<Account>
        // {
        //     new Account{Username = "Admin", Password = "Admin", Level = 0},
        //     new Account{Username = "DanielJMus", Password = "Password1", Level = 0}
        //  }, Received);

        // GameManager.instance.JSON.All<Account, JsnReceiver>(ReceivedList, Received);
    }

    void Received(JsnReceiver r) { }
    void ReceivedList(List<Account> r) { }
}
