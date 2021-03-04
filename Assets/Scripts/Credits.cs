using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{

    private void OnEnable()
    {
        Invoke("CreditsExit", 15);
    }

    private void FixedUpdate()
    {
        transform.Translate(0, .05f, 0);
    }

    public void CreditsExit()
    {
        Application.Quit();
    }
}
