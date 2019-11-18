using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupRemoteDatabase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        JSONDropService jsDrop = new JSONDropService { Token = "d341e18b-b0b5-4d33-a33d-9239ea617e5a" };

        // jsDrop.Drop<Account, JsnReceiver>(fi);

        // jsDrop.Create<Account, JsnReceiver>(new Account
        // {               
        //     Username = "******************************",
        //     Password = "******************************",
        //     Level = 1,
        // }, fi);

        // jsDrop.Create<Inventory, JsnReceiver>(new Inventory
        // {               
        //     ItemName = "******************************",
        //     PlayerID = 254,
        // }, jsnReceiverDel);

        // jsDrop.Store<Account, JsnReceiver> (new List<Account>
        // {
        //     new Account{Username = "Admin", Password = "Admin", Level = 0},
        //     new Account{Username = "DanielJMus", Password = "Password1", Level = 0},
        //     new Account{Username = "Testo", Password = "Testo", Level = 0}
        //  }, fi);
    }

    // Update is called once per frame
    void fi(JsnReceiver r)
    {
        
    }
}
