using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTile : MonoBehaviour
{
    [SerializeField] private string Name;

    public string ItemName {
        get { return Name; }
    }

    // When the player enters the collision box, send the data to the player.
    void OnTriggerEnter (Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            Player lcPlayer = col.gameObject.GetComponent<Player>();
            lcPlayer.SetItemTile(this);
        }
    }
}
