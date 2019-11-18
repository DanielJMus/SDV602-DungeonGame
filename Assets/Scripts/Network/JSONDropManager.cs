using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONDropManager : MonoBehaviour
{
    public void jsnReceiverDel(JsnReceiver pReceived)
    {
        Debug.Log(pReceived.JsnMsg + " ..." + pReceived.Msg);
    }

    public void jsnListReceiverDel(List<Account> pReceivedList)
    {
        Debug.Log("Received items " + pReceivedList.Count);
        foreach (Account lcReceived in pReceivedList)
        {
            Debug.Log("Received {" + lcReceived.Username + "," + lcReceived.Password + "," + lcReceived.Level.ToString()+"}");
        }
    }

    public void Register (string username, string password)
    {
        JSONDropService jsDrop = new JSONDropService { Token = "d341e18b-b0b5-4d33-a33d-9239ea617e5a" };
        jsDrop.Store<Account, JsnReceiver> (new List<Account>
        {
            new Account{Username = username, Password = password, Level = 0}
        }, jsnReceiverDel);
        print("Account registered successfully");
    }

    // public void jsnLoginDel(List<Account> pReceivedList)
    // {
    //     if(pReceivedList[0].Password == password) {
    //         print("YEEt");
    //     }
    // }

    public void CheckLogin <T>(string username, string password)
    {
        JSONDropService jsDrop = new JSONDropService { Token = "d341e18b-b0b5-4d33-a33d-9239ea617e5a" };
        string query = "username = '" + username + "' AND " + "password = '" + password + "'";
        jsDrop.Select<Account, JsnReceiver>(query, jsnListReceiverDel, jsnReceiverDel);
    }

    void Start () {
        // StartCoroutine(StartRoutine());
    }

    // Start is called before the first frame update
    // IEnumerator StartRoutine()
    // {
        // #region Test jsn drop
        // JSONDropService jsDrop = new JSONDropService { Token = "d341e18b-b0b5-4d33-a33d-9239ea617e5a" };
        
        // jsDrop.Drop<Chat, JsnReceiver>(jsnReceiverDel);

        // Create table player
        // jsDrop.Create<Chat, JsnReceiver>(new Chat
        // {               
        //     PlayerName = "******************************",
        //     Level = 0,
        //     Content = "************************************************************************************************************************"
        // }, jsnReceiverDel);

        // jsDrop.Create<Inventory, JsnReceiver>(new Inventory
        // {               
        //     ItemName = "******************************",
        //     PlayerID = 254,
        // }, jsnReceiverDel);

        // yield return new WaitForSeconds(1);

        // // Store player records

        // jsDrop.Store<Account, JsnReceiver> (new List<Account>
        // {
        //     new Account{Username = "Admin", Password = "Admin", Level = 0},
        //     new Account{Username = "DanielJMus", Password = "Password1", Level = 0}
        // }, jsnReceiverDel);

        // yield return new WaitForSeconds(1);

        // jsDrop.All<Account, JsnReceiver>(jsnListReceiverDel, jsnReceiverDel);

        // CheckLogin("DanielJMus", "Password1");

        // jsDrop.Store<Account, JsnReceiver> (new List<Account>
        // {
        //     new Account{Username = "DanielJMus", Password = "Password1", Level = 0}
        // }, jsnReceiverDel);

        //jsDrop.Drop<Account, JsnReceiver>(jsnReceiverDel);

        // jsDrop.Store<Account, JsnReceiver> (new List<Account>
        // {
        //     new Account{Username = "Admin", Password = "Admin", Level = 0},
        //     new Account{Username = "DanielJMus", Password = "Password1", Level = 0},
        //     new Account{Username = "Testo", Password = "Testo", Level = 0}
        //  }, jsnReceiverDel);

        // yield return new WaitForSeconds(1);

        // // CheckLogin("DanielJMus", "Admin");
        // print("ALL");//
        // // Retreive all player records
        
        // // jsDrop.Select<Account, JsnReceiver>("Level > 0", jsnListReceiverDel, jsnReceiverDel);
        
        // // jsDrop.Delete<Account, JsnReceiver>("Username = 'Test'", jsnReceiverDel);
        
        // #endregion

    // }
}
