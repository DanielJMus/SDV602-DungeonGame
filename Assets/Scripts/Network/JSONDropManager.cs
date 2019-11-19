using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONDropManager : MonoBehaviour
{
    public void jsnReceiverDel(JsnReceiver pReceived)
    {
        // Debug.Log(pReceived.JsnMsg + " ..." + pReceived.Msg);
    }

    public void jsnListReceiverDel(List<Account> pReceivedList)
    {
        // Debug.Log("Received items " + pReceivedList.Count);
        // foreach (Account lcReceived in pReceivedList)
        // {
        //     Debug.Log("Received {" + lcReceived.Username + "," + lcReceived.Password + "," + lcReceived.Level.ToString()+"}");
        // }
    }

    // Add a new account to the database.
    public void Register (string username, string password)
    {
        GameManager.instance.JSON.Store<Account, JsnReceiver> (new List<Account>
        {
            new Account{Username = username, Password = password, Level = 0}
        }, jsnReceiverDel);
    }

    // Check if the user credentials are correct.
    public void CheckLogin <T>(string username, string password)
    {
        string query = "username = '" + username + "' AND " + "password = '" + password + "'";
        GameManager.instance.JSON.Select<Account, JsnReceiver>(query, jsnListReceiverDel, jsnReceiverDel);
    }
}
