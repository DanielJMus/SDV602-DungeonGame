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

        output.text += '\n' + aCmd.Parse(_input);

        input.text = string.Empty;
        input.ActivateInputField();
    }
}
