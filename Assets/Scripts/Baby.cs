using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby : MonoBehaviour
{
    float directionx;
    float directiony;

    // Start is called before the first frame update
    void Start()
    {
        directionx = Random.Range(-5f, 5f);
        directiony = Random.Range(-5f, 5f);
        Vector2 direction = new Vector2 (directionx, directiony);

        GetComponent<Rigidbody2D>().velocity = direction;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(directionx, directiony, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
