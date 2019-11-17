using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class MenuInteraction : MonoBehaviour
{
    [SerializeField] private InputField usernameField;
    [SerializeField] private InputField passwordField;
    [SerializeField] private Text outputText;
    [SerializeField] private GameObject incorrectText;

    private DatabaseManager db;
    private JSONDropManager jsn;

    void Start ()
    {
        db = new DatabaseManager("GameData.db");
    }

    public void Login ()
    {
        JSONDropService jsDrop = new JSONDropService { Token = "d341e18b-b0b5-4d33-a33d-9239ea617e5a" };
        string query = "username = '" + usernameField.text + "' AND " + "password = '" + passwordField.text + "'";
        jsDrop.Select<Account, JsnReceiver>(query, LoginSuccess, LoginFail);
    }

    public void Register ()
    {
        JSONDropService jsDrop = new JSONDropService { Token = "d341e18b-b0b5-4d33-a33d-9239ea617e5a" };
        jsDrop.Store<Account, JsnReceiver> (new List<Account>
        {
            new Account{Username = usernameField.text, Password = passwordField.text, Level = 1}
        }, RegisterSuccess);
    }

    // Submit a message on the end screen.
    public void SubmitMessage () {
        outputText.text = "Message submitted";
    }

    public void RegisterSuccess(JsnReceiver pReceived)
    {
        print("Registered! You can now log in.");
    }

    public void LoginFail(JsnReceiver pReceived)
    {
        incorrectText.SetActive(true);
        print("Username or password incorrect.");
    }

    public void LoginSuccess(List<Account> pReceivedList)
    {
        print("Logged in!");
        GameManager.instance.ID = pReceivedList[0].ID;
        GameManager.instance.Username = pReceivedList[0].Username;
        GameManager.instance.Level = pReceivedList[0].Level;
        SceneManager.LoadScene(GameManager.instance.Level + 1);
    }
}
