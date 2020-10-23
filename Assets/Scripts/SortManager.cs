using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortManager : MonoBehaviour
{

    //GOAL: Define a function that spawns game objects where the next object is randomly
    //      selected out of the different laundry types until the pile hits a max size.
    //      Display the current object and then place it in a "pile" when the correct
    //      button is pressed. The piles should be at a location where the object's position can
    //      vary a little but fall within bounds set by the script

    //public GameObject[] Laundry;
    [Header("Laundry Spawning")]
    public int currentLaundryNum;
    public int laundryCounter;
    public int laundryMax;
    public Vector3 pilePos;
    public GameObject currentLaundry;
    public GameObject blueLaundry;
    public GameObject whiteLaundry;
    bool spawnedPile;

    public float bluePileX;
    public float bluePileXMax;
    public float bluePileXMin;

    public float bluePileY;
    public float bluePileYMax;
    public float bluePileYMin;

    public float whitePileX;
    public float whitePileXMax;
    public float whitePileXMin;

    public float whitePileY;
    public float whitePileYMax;
    public float whitePileYMin;

    [Header("General")]
    bool gamePlaying;

    [Header("Laundry Raycasting & Selection")]
    public Ray2D laundrySelRay;
    public RaycastHit2D hit;
    public Transform hitTransform;
    public Vector2 rayPos;

    public bool isClickable;
    public bool isDragging;


    

    // Start is called before the first frame update
    void Start()
    {
        //currentLaundry = Laundry[currentLaundryNum];
        currentLaundryNum = Random.Range(1, 2);
        spawnedPile = false;
        gamePlaying = false;
    }

    // Update is called once per frame
    void Update()
    {

        //spawn the laundry
        if (laundryCounter < laundryMax)
        {
            spawnLaundry();
        }
        

        //each object in the pile will be offset within a range set below
        pilePos.x = Random.Range(-.2f, .2f);
        pilePos.y = Random.Range(-4.5f, -4.1f);

        //winstate: if you run out of laundry you win
        if (laundryCounter == laundryMax)
        {
            currentLaundryNum = 50;
            spawnedPile = true;
            Debug.Log("pile is spawned");
        }

        if (spawnedPile)
        {
            gamePlaying = true;
        }

        //detect laundry highlighted by mouse
        rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);
        if (hit)
        {
            hitTransform = hit.transform;
            if (hitTransform.CompareTag("White"))
            {
                Debug.Log("White laundry");
            }
            if (hitTransform.CompareTag("Blue"))
            {
                Debug.Log("Blue laundry");
            }

        }
        //drag and drop said laundry onto the correct pile, otherwise reset it
        if (hitTransform.CompareTag("White") || hitTransform.CompareTag("Blue"))
        {
            isClickable = true;
        }
        else isClickable = false;

        if (isClickable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
        //if (hitTransform.CompareTag("White"))
        if (isDragging)
        {
            hitTransform.position = new Vector3(rayPos.x, rayPos.y, hitTransform.position.z);
        }
        
        if (hitTransform.position.x >= bluePileXMin && hitTransform.position.x <= bluePileXMax &&
            hitTransform.position.y >= bluePileYMin && hitTransform.position.y <= bluePileYMax &&
            hitTransform.CompareTag("Blue") && isDragging && Input.GetMouseButtonUp(0))
        {
            hitTransform.position = new Vector3(bluePileX, bluePileY, hitTransform.position.z);
            hitTransform.gameObject.GetComponent<BoxCollider2D>().enabled = !hitTransform.gameObject.GetComponent<BoxCollider2D>().enabled;
            isDragging = false;
        }

        if (hitTransform.position.x >= whitePileXMin && hitTransform.position.x <= whitePileXMax &&
            hitTransform.position.y >= whitePileYMin && hitTransform.position.y <= whitePileYMax &&
            hitTransform.CompareTag("White") && isDragging && Input.GetMouseButtonUp(0))
        {
            hitTransform.position = new Vector3(whitePileX, whitePileY, hitTransform.position.z);
            hitTransform.gameObject.GetComponent<BoxCollider2D>().enabled = !hitTransform.gameObject.GetComponent<BoxCollider2D>().enabled;
            isDragging = false;
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
