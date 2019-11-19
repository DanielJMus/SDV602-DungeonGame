using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTile : MonoBehaviour
{
    [SerializeField] private string keyName;

    public string KeyName {
        get { return keyName; }
    }

    // When the player enters the collision box, send the data to the player.
    void OnTriggerEnter (Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            Player lcPlayer = col.gameObject.GetComponent<Player>();
            lcPlayer.SetDoorTile(this);
        }
    }
}
