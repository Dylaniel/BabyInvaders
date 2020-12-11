using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emma : MonoBehaviour
{
    private Rigidbody2D rib;

    public GameObject emmasTonguePrefab;

    const float SPEED = 100f;

    // Start is called before the first frame update
    void Start()
    {
        rib = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal") * SPEED;
        float v = Input.GetAxis("Vertical") * SPEED;

        rib.AddForce(new Vector2(h, v));

        // Find out how we're rotated and apply the inverse se Emma returns to "normal"
        //float inverseRotation = transform.rotation.z * -1 * 25;
        //transform.Rotate(0, 0, inverseRotation);
        //rib.MoveRotation(0);


        if (Input.GetKeyDown("space"))
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            //adjust the position of the tongue so it matches the sprite
            pos.x -= .23f;
            pos.y -= .32f;
            Instantiate(emmasTonguePrefab, pos, transform.rotation);
        }
    }
}
