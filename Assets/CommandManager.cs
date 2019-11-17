using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager
{
    // Default constructor
    public CommandManager ()
    {

    }

    // Parse the command by splitting it up into arguments and determining which command is being run.
    public string Parse (string _command)
    {
        string _result = "No.";

        _command = _command.ToLower();
        string[] _arguments = _command.Split(' ');
        if(_arguments.Length >= 2)
        {
            string _cmd = _arguments[0] + " " + _arguments[1];
            CmdMap _map = new CmdMap();
            if(_map.Run(_cmd)) {
                _result = "Did a thing successfully.";
            }
        }
        return _result;
    }
}
