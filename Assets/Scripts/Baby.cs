using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby : MonoBehaviour
{
    float directionx;
    float directiony;
    Vector2 direction;

    Rigidbody2D rib;

    // Start is called before the first frame update
    void Start()
    {
        directionx = Random.Range(-5f, 5f);
        directiony = Random.Range(-5f, 5f);
        direction = new Vector2 (directionx, directiony);

        rib = GetComponent<Rigidbody2D>();
        rib.velocity = direction;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(directionx, directiony, 0);

        // Don't let the velocity in either axis drop below the starting value
        if (rib.velocity.magnitude < direction.magnitude)
        {
            Debug.Log(gameObject.name + " has slowed down");
            // Speed up
            rib.AddForce(rib.velocity / 5);
            //rib.AddForce(new Vector2(directionx - rib.velocity.x, 0));
            //rib.AddForce(new Vector2(0, directiony - rib.velocity.y));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
