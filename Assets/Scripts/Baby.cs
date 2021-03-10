using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby : MonoBehaviour
{
    private const float MAX_SPEED = 9999999999999;

    Controller controllerScript;

    private GameObject bestDoor;

    float directionx;
    float directiony;
    Vector2 direction;

    Rigidbody2D rib;

    private bool foundWaypoint;
        
    bool escaped = false;
    public bool ShouldLog = false;

    AudioSource squeak;

    // Start is called before the first frame update
    void Start()
    {
        directionx = Random.Range(-5f, 5f);
        directiony = Random.Range(-5f, 5f);
        direction = new Vector2(directionx, directiony);

        rib = GetComponent<Rigidbody2D>();
        rib.velocity = direction;

        squeak = gameObject.GetComponent<AudioSource>();

        controllerScript = GameObject.Find("Controller").GetComponent<Controller>();

        Invoke("FindSuitableDoor", 18f);
    }

    private void OnEnable()
    {
        directionx = Random.Range(-5f, 5f);
        directiony = Random.Range(-5f, 5f);
        direction = new Vector2(directionx, directiony);

        rib = GetComponent<Rigidbody2D>();
        rib.velocity = direction;

        controllerScript = GameObject.Find("Controller").GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        ManageSpeed();
    }

    private void ManageSpeed()
    {
        float curSpeed = rib.velocity.magnitude;

        if (foundWaypoint == false)
        {
            if (curSpeed > MAX_SPEED)
            {
                float scaleFactor = (MAX_SPEED / curSpeed);

                Vector2 newV = rib.velocity;
                newV.Scale(new Vector2(scaleFactor, scaleFactor));
                rib.velocity = newV;

                if (ShouldLog)
                {
                    Debug.Log(gameObject.name + " speed is " + curSpeed + " will scale down by " +
                        scaleFactor + ". New speed is " + rib.velocity.magnitude);
                }

            }
            else if (curSpeed < direction.magnitude)
            {
                // Don't let the velocity in either axis drop below the starting value
                //Debug.Log(gameObject.name + " has slowed down");
                // Speed up
                rib.AddForce(rib.velocity / 5);
            }
        }
        else if (foundWaypoint == true)
        {
            GoToDoor();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (squeak.isPlaying == false)
        {
            squeak.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);
        Destroy(gameObject);

        controllerScript.BabyEscaped();
    }

    private void FindSuitableDoor()
    {
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("waypoint");
        
        //foreach(waypoint in waypoints)
        //{
        //}
    }

    private void GoToDoor()
    {

    }
}
