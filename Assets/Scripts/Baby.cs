using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby : MonoBehaviour
{
    private const float MAX_SPEED = 9999999999999;

    Controller controllerScript;
    float origpeed;
    Rigidbody2D rib;
    private float fov = 360f;
    private GameObject bestWaypoint;
    bool escaped = false;
    public bool ShouldLog = false;

    AudioSource squeak;
    private Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        float max = 5;
        float directionx = Random.Range(-max, max);
        float directiony = Random.Range(-max, max);
        Vector2 direction = new Vector2(directionx, directiony);

        rib = GetComponent<Rigidbody2D>();
        rib.velocity = direction;
        origpeed = direction.magnitude;

        squeak = gameObject.GetComponent<AudioSource>();

        controllerScript = GameObject.Find("Controller").GetComponent<Controller>();

        Invoke("FindSuitableDoor", 1f);
    }

    public void OnEnable()
    {
        if (rib != null)
        {
            rib.velocity = velocity;
        }
    }

    public void OnDisable()
    {
        velocity = rib.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        ManageSpeed();
        ManageDirection();
    }

    private void ManageSpeed()
    {
        float curSpeed = rib.velocity.magnitude;


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
        else if (curSpeed < origpeed)
        {
            // Don't let the velocity in either axis drop below the starting value
            //Debug.Log(gameObject.name + " has slowed down");
            // Speed up
            rib.AddForce(rib.velocity / 5);
        }
    }

    private void ManageDirection()
    {/*
        if (bestWaypoint != null)
        {
            Debug.DrawLine(gameObject.transform.position,
               bestWaypoint.transform.position, Color.red, 0f, false);

            Vector2 exitDirection = bestWaypoint.transform.position - gameObject.transform.position;

            rib.velocity += exitDirection.normalized;
        }*/
        //Debug.DrawLine(Vector2.zero, rib.velocity, Color.blue);

        //Debug.DrawLine(transform.position, rib.velocity, Color.cyan);

        /*   Vector2 point = transform.TransformPoint(rib.velocity.x, rib.velocity.y, 0);
           float rotation = -transform.rotation.eulerAngles.z;
           point = rotate(point, rotation);
           Debug.Log("Rotation: " + transform.rotation.eulerAngles.z);*/

        Vector2 end = new Vector2(transform.position.x + rib.velocity.x, 
            transform.position.y + rib.velocity.y);
        Debug.DrawLine(transform.position, end, Color.blue);

        /*Debug.DrawLine(transform.position,
            transform.TransformDirection(rib.velocity.x, rib.velocity.y, 0),
            Color.green);*/

    }

    public static Vector2 rotate(Vector2 v, float delta)
    {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
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
        float bestWaypointAngle = 181;
    
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("waypoint");

        List<GameObject> elligibleWaypoints = EliminateDoors(waypoints);
        foreach (GameObject waypoint in elligibleWaypoints)
        {
            Vector2 exitDirection = waypoint.transform.position - gameObject.transform.position;
            float exitAngle = Vector2.Angle(rib.velocity, exitDirection);

            if (exitAngle < bestWaypointAngle)
            {
                bestWaypoint = waypoint;
                bestWaypointAngle = exitAngle;

                Debug.Log("Selected waypoint: " + bestWaypoint.name);
            }
        }

        
    }

    private List<GameObject> EliminateDoors(GameObject[] waypoints)
    {
        List<GameObject> elligibleWaypoints = new List<GameObject>(); 

        foreach(GameObject waypoint in waypoints)
        {
            Vector2 exitDirection = waypoint.transform.position - gameObject.transform.position;
            float exitAngle = Vector2.Angle(rib.velocity, exitDirection);
            Debug.Log("the angle to " + waypoint.name + " is " + exitAngle);

            if (exitAngle <= fov/2 )
            {
                elligibleWaypoints.Add(waypoint);
            }
            else
            {
                Debug.Log("no elligible waypoints found");
            }
        }
        return elligibleWaypoints;
    }

    private void GoToDoor()
    {

    }
}
