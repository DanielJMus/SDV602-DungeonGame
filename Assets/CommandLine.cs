using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandLine : MonoBehaviour
{
    private InputField input;
    private InputField.SubmitEvent submitEvent;
    private InputField.OnChangeEvent changeEvent;
    [SerializeField] private Text output;

    void Start ()
    {
        input = this.GetComponent<InputField>();
        if(input != null) {
            submitEvent = new InputField.SubmitEvent();
            submitEvent.AddListener(Submit);
            input.onEndEdit = submitEvent;
        }
    }

    // Submit the text via the inputfield and parse its contents.
    private void Submit (string _input)
    {
        string current = output.text;

        CommandManager aCmd = new CommandManager();

        if(_input.ToLower().StartsWith("say ")) {
            aCmd.Parse(_input);
            Send("[" + GameManager.instance.Username + "]: " + _input.Replace("say ", string.Empty).Replace("Say ", string.Empty));
        } else {
            string result = aCmd.Parse(_input);
            if(!string.IsNullOrEmpty(result))
                output.text += '\n' + result;
        }

        input.text = string.Empty;
        #if UNITY_EDITOR || UNITY_STANDALONE_WIN
            input.ActivateInputField();
        #endif
        
    }

    public void Send (string _input) {
        output.text += '\n' + _input;
    }
}
