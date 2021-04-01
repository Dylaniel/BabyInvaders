using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emma : MonoBehaviour
{
    private Rigidbody2D rib;
    private Controller controllerScript;
    public GameObject emmasTonguePrefab;
    private float lastFiredTime;

    private AudioSource audioSource;
    public AudioClip whimper;
    public AudioClip lick;

    const float SPEED = 250f;

    // Start is called before the first frame update
    void Start()
    {
        rib = GetComponent<Rigidbody2D>();

        audioSource = gameObject.GetComponent<AudioSource>();

        controllerScript = GameObject.Find("Controller").GetComponent<Controller>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal") * SPEED;
        float v = Input.GetAxis("Vertical") * SPEED;

        // Find out how we're rotated and apply the inverse se Emma returns to "normal"
        //float inverseRotation = transform.rotation.z * -1 * 25;
        //transform.Rotate(0, 0, inverseRotation);
        //rib.MoveRotation(0);

        if (controllerScript.gameOver == false)
        {
            rib.AddForce(new Vector2(h, v));
        }   
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            Debug.Log("Detected key code: " + e.keyCode);
            if (Event.current.Equals(Event.KeyboardEvent(KeyCode.Space.ToString())) && allowedFire())
            {
                Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                //adjust the position of the tongue so it matches the sprite
                pos.x -= .23f;
                pos.y -= .32f;
                Instantiate(emmasTonguePrefab, pos, transform.rotation);
                lastFiredTime = Time.time;
                
                if (audioSource.isPlaying == false)
                {
                    audioSource.clip = lick;
                    audioSource.Play();
                }
            }

        }
    }

    private bool allowedFire()
    {
        if(Time.time >= lastFiredTime + .65f)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (controllerScript.gameOver==false && collision.gameObject.CompareTag("baby"))
        {
            controllerScript.EmmaHit();
            
            audioSource.clip = whimper;
            audioSource.Play();
        }
    }
}
