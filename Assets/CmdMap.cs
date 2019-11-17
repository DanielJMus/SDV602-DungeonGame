using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdMap 
{

    private Dictionary<string, Cmd> commands;
    
    // List of every possible command in the game, including aliases for multiple 'versions' of the same command.
    public CmdMap () {
        commands = new Dictionary<string, Cmd>();
        CmdGo goFwd = new CmdGo("forward");
        CmdGo goBck = new CmdGo("back");
        commands.Add("go forward", goFwd);
        commands.Add("go north", goFwd);
        commands.Add("go forwards", goFwd);
        commands.Add("go n", goFwd);

        commands.Add("go back", goBck);
        commands.Add("go south", goBck);
        commands.Add("go backwards", goBck);
        commands.Add("go s", goBck);

        commands.Add("turn left", new CmdTurn("left"));
        commands.Add("turn l", new CmdTurn("left"));
        commands.Add("turn right", new CmdTurn("right"));
        commands.Add("turn r", new CmdTurn("right"));

        commands.Add("get key", new CmdGet("key"));
        commands.Add("use key", new CmdUse("key"));
    }

    // Run a command from the command list by matching the input string.
    public bool Run (string _command) 
    {
        bool foundCmd = false;
        Cmd aCmd;
        if(commands.ContainsKey(_command))
        {
            Debug.Log("OK");
            aCmd = commands[_command];
            aCmd.Execute(this);
            foundCmd = true;
        } else if (!commands.ContainsKey(_command)) {
            if(_command.StartsWith("say")) {
                aCmd = new CmdSay(_command.Split(new string[] {"say"}, System.StringSplitOptions.None)[1]);
                aCmd.Execute(this);
            }
            foundCmd = false;
        }
        return foundCmd;
    }
    
}
