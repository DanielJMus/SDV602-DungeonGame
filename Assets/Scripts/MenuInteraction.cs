﻿using UnityEngine;
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
    [SerializeField] private GameObject registerText;
    [SerializeField] private GameObject loadingText;
    [SerializeField] private GameObject existsText;

    private DatabaseManager db;

    void Start ()
    {
        db = new DatabaseManager("GameData.db");
    }

    public void Login ()
    {
        loadingText.SetActive(true);
        string query = "username = '" + usernameField.text + "' AND " + "password = '" + passwordField.text + "'";
        GameManager.instance.JSON.Select<Account, JsnReceiver>(query, LoginSuccess, LoginFail);
    }

    public void Register ()
    {
        loadingText.SetActive(true);
        GameManager.instance.JSON.All<Account, JsnReceiver>(CheckUsers, null);
    }

    // When a user registers, check if a user already exists with that username, if not then create the account.
    void CheckUsers (List<Account> users) {
        bool accountExists = false;
        foreach(Account user in users) {
            if(user.Username == usernameField.text) {
                loadingText.SetActive(false);
                existsText.SetActive(true);
                accountExists = true;
            }
        }
        if(!accountExists) {
            GameManager.instance.JSON.Store<Account, JsnReceiver> (new List<Account>
            {
                new Account{Username = usernameField.text, Password = passwordField.text, Level = 0}
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
        loadingText.SetActive(false);
        registerText.SetActive(true);
    }

    public void LoginFail(JsnReceiver pReceived)
    {
        loadingText.SetActive(false);
        incorrectText.SetActive(true);
    }

    // Retrieve player information into GameManager for reference and load them into the last level they were in.
    public void LoginSuccess(List<Account> pReceivedList)
    {
        GameManager.instance.ID = pReceivedList[0].ID;
        GameManager.instance.Username = pReceivedList[0].Username;
        GameManager.instance.Password = pReceivedList[0].Password;
        GameManager.instance.Level = pReceivedList[0].Level;
        SceneManager.LoadScene(GameManager.instance.Level + 1);
    }
}
