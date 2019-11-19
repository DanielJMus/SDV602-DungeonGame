using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFlasher : MonoBehaviour
{

    private Text text;
    private float timer;

    void Start()
    {
        text = GetComponent<Text>();
    }

    // Flash the text between white and red every 0.1 seconds (For alerts)
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 0.1f)
        {
            timer = 0;
            if(text.color == Color.red) text.color = Color.white;
            else if(text.color == Color.white) text.color = Color.red;
        }
    }
}
