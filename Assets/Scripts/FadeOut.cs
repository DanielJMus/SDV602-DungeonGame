using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    private Image image;

    void Start () {
        image = GetComponent<Image>();
    }

    // Fade out the black overlay over a few seconds (Fade-in effect)
    void Update()
    {
        image.color = Color.Lerp(image.color, Color.clear, Time.deltaTime * 1);
    }
}
