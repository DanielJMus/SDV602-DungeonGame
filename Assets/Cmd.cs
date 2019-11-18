using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cmd : MonoBehaviour
{
    // Set up a virtual void that is overridden by each command to perform different actions.
    public virtual void Execute(CmdMap _cmd) { }
}

// Command to move the player forward or backwards
public class CmdGo : Cmd
{
    private string destination;

    public CmdGo (string _destination)
    {
        destination = _destination;
    }

    public override void Execute (CmdMap _cmd)
    {
        switch(destination)
        {
            case "forward":
                GameManager.instance.player.Move("forward");
                break;
            case "back":
                GameManager.instance.player.Move("back");
                break;
        }
    }
}

// Command to rotate the camera by 90 degree increments
public class CmdTurn : Cmd
{
    private string destination;

    public CmdTurn (string _destination)
    {
        destination = _destination;
    }

    public override void Execute (CmdMap _cmd)
    {
        switch(destination)
        {
            case "left":
                GameManager.instance.player.Move("left");
                break;
            case "right":
                GameManager.instance.player.Move("right");
                break;
        }
    }
}

// Command to pick up an item in the current tile.
public class CmdGet : Cmd
{
    private string item;

    public CmdGet (string _item)
    {
        item = _item;
    }

    public override void Execute (CmdMap _cmd)
    {
        switch(item)
        {
            case "key":
                if(GameManager.instance.player.IsOnItemTile)
                {
                    if(GameManager.instance.player.GetItemTile.ItemName == ("Gold Key"))
                    {
                        GameManager.instance.inventory.AddInventoryItem("Gold Key");
                        GameManager.instance.player.IsOnItemTile = false;
                        Destroy(GameManager.instance.player.GetItemTile.gameObject);
                    }
                }
                break;
        }
    }
}

// Command to use an item from the inventory
public class CmdUse : Cmd
{
    private string item;

    public CmdUse (string _item)
    {
        item = _item;
    }

    public override void Execute (CmdMap _cmd)
    {
        switch(item)
        {
            case "key":
                if(GameManager.instance.player.IsOnDoorTile)
                {
                    if(GameManager.instance.player.GetDoorTile.KeyName == ("Gold Key"))
                    {
                        if(GameManager.instance.inventory.HasItem("Gold Key")) {
                            GameManager.instance.inventory.RemoveInventoryItem("Gold Key");
                            GameManager.instance.FinishLevel();
                            // SceneManager.LoadScene(2);
                        }
                    }
                }
                break;
        }
    }
}

// Command to say something in chat
public class CmdSay : Cmd
{
    private string message;

    public CmdSay (string _message)
    {
        message = _message;
    }

    public override void Execute (CmdMap _cmd)
    {
        if(string.IsNullOrEmpty(message) ) {
            Debug.LogError("Tried sending message but it was empty.");
        }

        JSONDropService jsDrop = new JSONDropService { Token = "d341e18b-b0b5-4d33-a33d-9239ea617e5a" };
        jsDrop.Store<Chat, JsnReceiver> (new List<Chat>
        {
            new Chat{PlayerName = GameManager.instance.Username, 
                     Level = GameManager.instance.Level, 
                     Content = message}
        }, MessageSuccess);
    }

    public void MessageSuccess (JsnReceiver pReceived) {
        Debug.Log("Message sent successfully.");
    }
}