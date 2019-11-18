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
    public string Password;
    public int Level;

    public bool HasGoldKey;

    public DatabaseManager database;
    public CommandLine cmdLine;
    public InventoryManager inventory;

    public int PreviousChatMessageCount;
    public int LevelChatMessageCount;
    public bool chatHasUpdatedOnce;

    public JSONDropService JSON = null;

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

        JSON = new JSONDropService { Token = "d341e18b-b0b5-4d33-a33d-9239ea617e5a" };
        string query = "Level = '" + Level + "'";
        JSON.Select<Chat, JsnReceiver>(query, CheckChatMessageCount, ChatMessageFail);

        if (instance == null)
        { 
            instance = this;
        }

        // Find or create necessary objects
        player = Object.FindObjectOfType<Player>();
        inventory = Object.FindObjectOfType<InventoryManager>();
        // database = new DatabaseManager("GameData.db");
        cmdLine = Object.FindObjectOfType<CommandLine>();
    }

    private float chatUpdateInterval = 1.0f, lastChatUpdate = 0.0f;

    void Update ()
    {
        // Don't check chat messages for main menu or end screen
        if (Level < 0) return;

        // Check the number of chat messages for the current level in the database every second (chatUpdateInterval) 
        if(Time.time > lastChatUpdate + chatUpdateInterval)
        {
            lastChatUpdate = Time.time;
            string query = "Level = '" + Level + "'";
            JSON.Select<Chat, JsnReceiver>(query, CheckChatMessageCount, ChatMessageFail);
        }
    }

    // If the number of chat messages in the current level has changed, append all of the new messages to the chat log
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
    void ChatMessageFail(JsnReceiver pReceived) { }

    // Append all new messages to the chat log
    void UpdateChatMessages (List<Chat> chatMessageList) {
        for(int i = PreviousChatMessageCount; i < chatMessageList.Count; i++) {
            Chat message = chatMessageList[i];
            if(message.PlayerName == Username) continue;
            string content = string.Format("[{0}]: {1}", message.PlayerName, message.Content);
            cmdLine.Send(content);
        }
        PreviousChatMessageCount = chatMessageList.Count;
    }

    public void FinishLevel () {
        JSON.Store<Account, JsnReceiver> (new List<Account>
        {
            new Account{ID = ID, Username = Username, Password = Password, Level = Level + 1},
        }, LoadNextLevelSuccess);
        
    }

    private void LoadNextLevelSuccess (JsnReceiver pReceived)
    {
        Level++;
        SceneManager.LoadScene(Level + 1);
    }
}
