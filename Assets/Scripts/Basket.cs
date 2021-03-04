﻿using System.Collections;
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

    private void OnDisable()
    {
        CancelInvoke("spawnBaby");
    }

    private void OnEnable()
    {
        InvokeRepeating("spawnBaby", 1f, 1f);
    }
    

    private void spawnBaby()
    {
        if (numBabies > 0)
        {
            int babySelected = Random.Range(0, BabyPrefabs.Length);

            //Debug.Log("the baby is: " + babySelected);

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
