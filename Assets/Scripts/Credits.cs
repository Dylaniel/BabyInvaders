using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject manager;
    private Manager managerScript;


    private void OnEnable()
    {
        gameObject.transform.position = new Vector2(1.485165f, -7.562866f);
        managerScript = manager.GetComponent<Manager>();
        Invoke("CreditsExit", 25);
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
