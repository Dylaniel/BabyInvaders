using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public GameObject[] BabyPrefabs;

    private int numBabies;

    private float spawnRate;

    private bool babiesDone = false;
    public bool BabiesDone
    {
        get { return babiesDone; }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("basket starting");
    }

    private void OnDisable()
    {
        CancelInvoke("spawnBaby");
    }

    private void OnEnable()
    {
        //we will be enabled when the game starts and after we unpause
        //when the game starts the spawnrate will be zero because apply
        //difficulty hasn't been called yet
        if (spawnRate != 0)
        {
            InvokeRepeating("spawnBaby", 1f, spawnRate);
        }
    }
    
    public void ApplyDifficulty(int numBabies, float spawnRate)
    {
        this.numBabies = numBabies;
        this.spawnRate = spawnRate;

        InvokeRepeating("spawnBaby", 1f, spawnRate);


    }

    private void spawnBaby()
    {
        if (numBabies > 0)
        {
            int babySelected = Random.Range(0, BabyPrefabs.Length);

            //Debug.Log("the baby is: " + babySelected);
            // Debug.Log("There are " + numBabies + " babies left");

            GameObject newBaby = Instantiate(BabyPrefabs[babySelected],
                transform.position, transform.rotation);

            newBaby.transform.parent = gameObject.transform;

            numBabies--;
        }
        if (numBabies == 0)
        {
            CancelInvoke("spawnBaby");

            babiesDone = true;
        }
    }
}
