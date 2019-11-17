using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public Player player;
    public int ID = -1;
    public string Username;
    public int Level;

    public bool HasGoldKey;

    public DatabaseManager database;
    public CommandLine cmdLine;

    public int PreviousChatMessageCount;
    public int LevelChatMessageCount;
    public bool chatHasUpdatedOnce;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    // As soon as the script wakes up, set the script up as a singleton.
    void OnLevelFinishedLoading (Scene scene, LoadSceneMode mode) {
        DontDestroyOnLoad(this.gameObject);

        JSONDropService jsDrop = new JSONDropService { Token = "d341e18b-b0b5-4d33-a33d-9239ea617e5a" };
        string query = "Level = '" + Level + "'";
        jsDrop.Select<Chat, JsnReceiver>(query, CheckChatMessageCount, ChatMessageFail);

        if (instance == null)
        { 
            instance = this;
        }

        player = Object.FindObjectOfType<Player>();
        database = new DatabaseManager("GameData.db");
        cmdLine = Object.FindObjectOfType<CommandLine>();
    }

    private float chatUpdateInterval = 1.0f, lastChatUpdate = 0.0f;

    void Update ()
    {
        if (Level < 0) return;
        if(Time.time > lastChatUpdate + chatUpdateInterval)
        {
            lastChatUpdate = Time.time;
            JSONDropService jsDrop = new JSONDropService { Token = "d341e18b-b0b5-4d33-a33d-9239ea617e5a" };
            string query = "Level = '" + Level + "'";
            jsDrop.Select<Chat, JsnReceiver>(query, CheckChatMessageCount, ChatMessageFail);
        }
    }

    void CheckChatMessageCount (List<Chat> chatMessageList)
    {
        if(chatMessageList.Count != LevelChatMessageCount && chatHasUpdatedOnce) {
            UpdateChatMessages(chatMessageList);
        }
        if(!chatHasUpdatedOnce) {
            PreviousChatMessageCount = chatMessageList.Count;
            chatHasUpdatedOnce = true;
        }
        
        LevelChatMessageCount = chatMessageList.Count;
    }

    void ChatMessageFail(JsnReceiver pReceived)
    {
        print("No chat messages found in current level");
    }

    void UpdateChatMessages (List<Chat> chatMessageList) {
        print("Detected new chat messages");
        for(int i = PreviousChatMessageCount; i < chatMessageList.Count; i++) {
            Chat message = chatMessageList[i];
            if(message.PlayerName == Username) continue;
            string content = string.Format("[{0}]: {1}", message.PlayerName, message.Content);
            cmdLine.Send(content);
        }
        PreviousChatMessageCount = chatMessageList.Count;
        // Go through list and append newest messages to chat
    }
}
