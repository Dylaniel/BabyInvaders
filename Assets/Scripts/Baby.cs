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
        directionx = Random.Range(-.03f, .03f);
        directiony = Random.Range(-.03f, .03f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(directionx, directiony, 0);
    }
}
