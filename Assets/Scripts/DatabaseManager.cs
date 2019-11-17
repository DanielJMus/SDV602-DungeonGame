using SQLite4Unity3d;
using UnityEngine;
using System.Linq;
using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class DatabaseManager
{
    private SQLiteConnection _connection;
    public SQLiteConnection Connection
    {
        get
        { 
            return _connection;
        }
    }

    public DatabaseManager  (string DatabaseName)
    {
        string dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
        string filePath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if(!File.Exists(filePath))
        {
            Debug.LogWarning("Database does not exist");

            #if UNITY_ANDROID
                var loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);
                while (!loadDB.isDone) { }
                File.WriteAllBytes(filePath, loadDB.bytes);
            #else
                var loadDB = Application.dataPath + "/StreamingAssets/" + DatabaseName;
                File.Copy(loadDB, filePath);
            #endif
        }

        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
    }

    public void CreateDB (System.Type[] pTableTypes)
    {
        var createList = pTableTypes.Where<System.Type>( x => 
        {
            _connection.CreateTable(x);
            return true;
        }).ToList();
    }

    // Either inserts, or updates depending on existance in the DB
    public void AddOrUpdate<T> (T Record)
    {
        _connection.InsertOrReplace(Record);
    }

    // Add a single item to the database
    public void Add<T> (T Record)
    {
        try
        {
            _connection.Insert(Record);
        }
        catch (Exception e)
        {
            Debug.LogError(e.InnerException.Message);
        }
    }

    // Add a list of items to the database
    public void AddAll<T> (T[] RecordList)
    {
        try
        {
            _connection.InsertAll(RecordList);
        }
        catch (Exception e)
        {
            Debug.LogError(e.InnerException.Message);
        }
    } 

    // Create some default accounts in the database
    public void CreateDB ()
    {
        _connection.DropTable<Account>();
        _connection.CreateTable<Account>();
        _connection.DropTable<Inventory>();
        _connection.CreateTable<Inventory>();
        _connection.DropTable<Chat>();
        _connection.CreateTable<Chat>();

        _connection.InsertAll (new[] {
            new Account {
                ID = 1,
                Username = "DanielJMus",
                Password = "Password1"
            },
            new Account {
                ID = 2,
                Username = "Bob",
                Password = "bob"
            }
        });
    }

    // Return all accounts from the database as an enumerable list.
    public IEnumerable<Account> GetAccounts ()
    {
        return _connection.Table<Account>();
    }

    // Returns an account (or null) from the database
    public Account GetAccount (string username)
    {
        return _connection.Table<Account>().Where(x => x.Username.ToLower() == username.ToLower()).FirstOrDefault();
    } 

    // Adds a new account to the database
    public Account CreateAccount (string username)
    {
        Account newAccount = new Account{
            Username = username
        };
        _connection.Insert(newAccount);
        return newAccount;
    }
    
    // Returns the user account if username and password match, otherwise returns null
    public Account Login (string username, string password)
    {
        return _connection.Table<Account>().Where(x => (x.Username == username) && 
                                                       (x.Password == password)).FirstOrDefault();
    }

    // Give an item to the specified player.
    public Inventory AddInventoryItem (string itemname, int playerid)
    {
        Inventory newItem = new Inventory {
            ItemName = itemname,
            PlayerID = playerid
        };
        _connection.Insert(newItem);
        return newItem;
    }

    // Returns the inventory item of the specified player.
    public Inventory GetInventoryItem (string itemname, int playerid)
    {
        return _connection.Table<Inventory>().Where(x => (x.ItemName == itemname) && 
                                                       (x.PlayerID == playerid)).FirstOrDefault();
    }

    // Remove an item from the inventory of the specified player.
    public void RemoveInventoryItem (string itemname, int playerid)
    {
        Inventory item = new Inventory {
            ItemName = itemname,
            PlayerID = playerid
        };
        _connection.Delete(item);
    }
}
