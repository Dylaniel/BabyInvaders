using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emma : MonoBehaviour
{
    public GameObject emmasTonguePrefab;

    const float SPEED = .1f;

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        float x = (Input.GetAxis("Horizontal") * SPEED);
        float y = (Input.GetAxis("Vertical") * SPEED);
        transform.Translate(x, y, 0);

        float inverseRotation = transform.rotation.z * -1 * 25;
        transform.Rotate(0, 0, inverseRotation);


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
