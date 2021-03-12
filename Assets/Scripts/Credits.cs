using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject manager;
    private Manager managerScript;


    private void OnEnable()
    {
        gameObject.transform.position = new Vector2(0, -16);
        managerScript = manager.GetComponent<Manager>();
        Invoke("CreditsExit", 15);
    }

    private void OnDisable()
    {
        CancelInvoke("CreditsExit");
    }

    private void FixedUpdate()
    {
        transform.Translate(0, .05f, 0);
    }

    public void CreditsExit()
    {
        managerScript.GoToMainMenu();
    }
}
