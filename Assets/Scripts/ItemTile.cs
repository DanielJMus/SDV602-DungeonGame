﻿using System.Collections;
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

    // When the player enters the collision box, send the data to the player.
    void OnTriggerExit (Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            Player lcPlayer = col.gameObject.GetComponent<Player>();
            lcPlayer.IsOnItemTile = false;
        }
    }

    private bool hasCheckedInventoryState = false;

    void Update () {
        // When the game loads the player's inventory, check if the player already has this item, if they do then destroy it
        if(GameManager.instance.inventory.HasLoaded && !hasCheckedInventoryState) {
            hasCheckedInventoryState = true;
            if(GameManager.instance.inventory.HasItem(Name)) {
                Destroy(this.gameObject);
            }
        }
    }
}
