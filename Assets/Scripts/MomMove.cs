using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomMove : MonoBehaviour
{
    //GOAL: Move the mother back and forth at a random speed each time
    //      Raycast towards the pile, detect when it hits

    public GameObject Mom;
    public float momSpeed;
    public float speedMax;
    public float speedMin;
    public Vector3 momPos;

    public float xMin;
    public float xMax;
    
    // Start is called before the first frame update
    void Start()
    {
        momPos = Mom.transform.position;
        momSpeed = Random.Range(speedMin, speedMax);
    }

    // Update is called once per frame
    void Update()
    {
        Mom.transform.Translate(momSpeed, 0f, 0f);
        momPos = Mom.transform.position;

        if (momPos.x <= xMin)
        {
            momPos.x = xMin;
            momSpeed = Random.Range(speedMin, speedMax);
            //momSpeed = momSpeed * -1f;
            Mom.transform.Translate(momSpeed, 0f, 0f);
        }

        if (momPos.x >= xMax)
        {
            momPos.x = xMax;
            momSpeed = Random.Range(speedMin, speedMax);
            momSpeed = momSpeed * -1f;
            Mom.transform.Translate(momSpeed, 0f, 0f);
        }
    }
}
