using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortManager : MonoBehaviour
{

    //GOAL: Define an array of game objects where the next object is randomly
    //      selected out of the different laundry types
    //      Display the current object and then place it in a "pile" when the correct
    //      button is pressed. The piles should be at a location where the object's position can
    //      vary a little but fall within bounds set by the script

    //public GameObject[] Laundry;
    public int currentLaundryNum;
    public int laundryCounter;
    public int laundryMax;
    public Vector3 pilePos;

    public GameObject currentLaundry;

    public GameObject blueLaundry;
    public GameObject whiteLaundry;

    bool spawnedPile;

    

    // Start is called before the first frame update
    void Start()
    {
        //currentLaundry = Laundry[currentLaundryNum];
        currentLaundryNum = Random.Range(1, 2);
        spawnedPile = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (laundryCounter <= 49)
        {
            spawnLaundry();
        }
        
        //currentLaundryNum = Random.Range(1, 2);

        pilePos.x = Random.Range(-.2f, .2f);
        pilePos.y = Random.Range(-4.5f, -4.1f);

        //winstate: if you run out of laundry you win
        if (laundryCounter == laundryMax)
        {
            currentLaundryNum = 50;
            spawnedPile = true;
            Debug.Log("pile is spawned");
        }
    }

    void spawnLaundry()
    {
        if (currentLaundryNum == 1)
        {
            Instantiate(blueLaundry, pilePos, Quaternion.identity);
            laundryCounter++;
            currentLaundryNum = Random.Range(1, 3);
            Debug.Log("spawned blue");
            pilePos.z = pilePos.z - .1f;
        }
        else if (currentLaundryNum == 2)
        {
            Instantiate(whiteLaundry, pilePos, Quaternion.identity);
            laundryCounter++;
            currentLaundryNum = Random.Range(1, 3);
            Debug.Log("spawned white");
            pilePos.z = pilePos.z - .1f;
        }
    }
}
