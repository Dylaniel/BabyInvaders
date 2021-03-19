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
    private Animator anim;

    AudioSource squeak;
    private Vector2 velocity;
    private bool hit;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent <Animator> ();

        float max = 5;
        float directionx = Random.Range(-max, max);
        float directiony = Random.Range(-max, max);
        Vector2 direction = new Vector2(directionx, directiony);

        rib = GetComponent<Rigidbody2D>();
        rib.velocity = direction;
        origpeed = direction.magnitude;

        squeak = gameObject.GetComponent<AudioSource>();

        controllerScript = GameObject.Find("Controller").GetComponent<Controller>();

        InvokeRepeating("FindSuitableDoor", 10, 1);
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
        if (hit == false)
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
    }

    private void ManageDirection()
    {
        if (hit == false)
        {
            Debug.DrawLine(new Vector2(0, -1), new Vector2(0, 1), Color.white);
            Debug.DrawLine(new Vector2(-1, 0), new Vector2(1, 0), Color.white);

            Vector2 lVelocity = new Vector2(transform.position.x + rib.velocity.x,
                transform.position.y + rib.velocity.y);
            Debug.DrawLine(transform.position, lVelocity, Color.blue);

            if (bestWaypoint != null)
            {
                Debug.DrawLine(gameObject.transform.position,
                   bestWaypoint.transform.position, Color.red, 0f, false);

                Vector2 exitDirection = bestWaypoint.transform.position -
                    gameObject.transform.position;

                float angleToDoor = AngleBetweenVector2(rib.velocity, exitDirection);

                Debug.Log("the angle is:" + angleToDoor);

                float mag = rib.velocity.magnitude;
                if (angleToDoor < -5 || angleToDoor > 5)
                {
                    rib.AddForce(exitDirection.normalized, ForceMode2D.Impulse);
                }

                Vector2 n = rib.velocity.normalized;
                rib.velocity = n * mag;

                bestWaypoint = null;
            }
        }
    }

    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 vec1Rotated90 = new Vector2(-vec1.y, vec1.x);
        float sign = (Vector2.Dot(vec1Rotated90, vec2) < 0) ? -1.0f : 1.0f;
        return Vector2.Angle(vec1, vec2) * sign;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (squeak.isPlaying == false)
        {
            squeak.Play();
        }        

        if (collision.gameObject.tag == "tongue")
        {
            hit = true;

            gameObject.GetComponent<Collider2D>().enabled = false;

            if (anim != null)
            {
                anim.SetTrigger("dead");
                Invoke("aMethodToDestroy", 1);
            }
            else
            {
                aMethodToDestroy();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Room"))
        {
            gameObject.SetActive(false);
            Destroy(gameObject);

            controllerScript.BabyEscaped();
        }

        if (collision.gameObject.CompareTag("waypoint"))
        {
            bestWaypoint = null;            
        }
    }

    private void aMethodToDestroy()
    {                
        gameObject.SetActive(false);
        Destroy(gameObject);            
        controllerScript.BabyKilled();        
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
