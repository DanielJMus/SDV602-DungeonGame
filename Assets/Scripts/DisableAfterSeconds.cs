using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterSeconds : MonoBehaviour
{
    [SerializeField] private int seconds = 3;

    void OnEnable()
    {
        StartCoroutine(Disable());
    }

    // After X seconds, set the object as inactive.
    IEnumerator Disable () {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
    }


}
