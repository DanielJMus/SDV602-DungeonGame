using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public Player player;

    public bool HasGoldKey;

    public DatabaseManager database;

    // As soon as the script wakes up, set the script up as a singleton.
    void Awake () {
        //DontDestroyOnLoad(this.gameObject);
        if (instance == null) { 
            instance = this;
            player = Object.FindObjectOfType<Player>();
            database = new DatabaseManager("GameData.db");
            database.CreateDB();
        } else {
            Destroy(gameObject);
        }
    }
}
