using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

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
        string query = "username = '" + usernameField.text + "' AND " + "password = '" + passwordField.text + "'";
        GameManager.instance.JSON.Select<Account, JsnReceiver>(query, LoginSuccess, LoginFail);
    }

    public void Register ()
    {
        GameManager.instance.JSON.All<Account, JsnReceiver>(CheckUsers, null);

    }

    void CheckUsers (List<Account> users) {
        bool accountExists = false;
        foreach(Account user in users) {
            if(user.Username == usernameField.text) {
                print("Account already exists");
                accountExists = true;
            }
        }
        if(!accountExists) {
            GameManager.instance.JSON.Store<Account, JsnReceiver> (new List<Account>
            {
                new Account{Username = usernameField.text, Password = passwordField.text, Level = 1}
            }, RegisterSuccess);
        }
    }

    // Submit a message on the end screen.
    public void SubmitMessage () {
        outputText.text = "Message submitted";
        GameManager.instance.JSON.Store<Account, JsnReceiver> (new List<Account>
        {
            new Account{Username = GameManager.instance.Username, 
                        Password = GameManager.instance.Password, 
                        Level = 1 }
        }, SubmitSuccess);
    }

    private void SubmitSuccess (JsnReceiver pReceived)
    {
        SceneManager.LoadScene(0);
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
        GameManager.instance.ID = pReceivedList[0].ID;
        GameManager.instance.Username = pReceivedList[0].Username;
        GameManager.instance.Password = pReceivedList[0].Password;
        GameManager.instance.Level = pReceivedList[0].Level;
        SceneManager.LoadScene(GameManager.instance.Level + 1);
    }
}
