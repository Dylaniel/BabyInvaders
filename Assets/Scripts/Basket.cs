using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public GameObject[] BabyPrefabs;

    public int numBabies;

    private bool babiesDone = false;
    public bool BabiesDone
    {
        get { return babiesDone; }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("basket starting");

        InvokeRepeating("spawnBaby", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void spawnBaby()
    {
        if (numBabies > 0)
        {
            int babySelected = Random.Range(0, BabyPrefabs.Length);

            //Debug.Log("the baby is: " + babySelected);

            Instantiate(BabyPrefabs[babySelected], transform.position, transform.rotation);

            numBabies--;
        }
        if (numBabies == 0)
        {
            CancelInvoke("spawnBaby");

            babiesDone = true;
        }
    }
}
