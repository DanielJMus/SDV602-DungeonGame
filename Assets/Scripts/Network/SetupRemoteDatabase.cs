using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupRemoteDatabase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        // GameManager.instance.JSON.Drop<Account, JsnReceiver>(fi);

        // GameManager.instance.JSON.Create<Account, JsnReceiver>(new Account
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

        // GameManager.instance.JSON.Store<Account, JsnReceiver> (new List<Account>
        // {
        //     new Account{Username = "Admin", Password = "Admin", Level = 0},
        //     new Account{Username = "DanielJMus", Password = "Password1", Level = 0},
        //     new Account{Username = "Testo", Password = "Testo", Level = 0}
        //  }, fi);

        // GameManager.instance.JSON.All<Account, JsnReceiver>(fli, fi);
    }

    // Update is called once per frame
    void fi(JsnReceiver r)
    {
        
    }

    void fli(List<Account> r)
    {
        
    }
}
