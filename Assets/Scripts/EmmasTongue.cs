using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmmasTongue : MonoBehaviour
{
    private float speed = .10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D otherObject)
    {
        if (otherObject.gameObject.CompareTag("baby"))
        {
            Destroy(otherObject.gameObject);

            //TODO: add code to increment score

        }

        if (!otherObject.gameObject.CompareTag("emma"))
        {
            Destroy(gameObject);
        }

        Debug.Log("hit");
    }
}
