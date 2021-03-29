using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmmasTongue : MonoBehaviour
{
    Controller controllerScript;

    private float speed = .2f;
    // Start is called before the first frame update
    void Start()
    {
        controllerScript = GameObject.Find("Controller").GetComponent<Controller>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(speed, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D otherObject)
    {        

        if (!otherObject.gameObject.CompareTag("emma"))
        {
            Destroy(gameObject);
        }

        //Debug.Log("hit");
    }
}
