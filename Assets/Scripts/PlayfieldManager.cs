using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayfieldManager : MonoBehaviour
{
    public float gravitySpeed;
    public float moveSpeed;
    public int maxX;

    public int spawnY;
    public int deathY;

    public GameObject[] events;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //an event can be any object that the user will interact with (asteroids, coins, power-ups)
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Event");

        //user controls
        if(Input.GetKeyDown(KeyCode.LeftArrow))
            foreach (GameObject o in obj)
                o.transform.position = new Vector3(o.transform.position.x + moveSpeed, o.transform.position.y, o.transform.position.z);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            foreach (GameObject o in obj)
                o.transform.position = new Vector3(o.transform.position.x - moveSpeed, o.transform.position.y, o.transform.position.z);

        //check to see if an event object has gone out of bounds
        foreach(GameObject o in obj)
        {
            //if the event position is too far to the left, the move it to the right side
            if (o.transform.position.x < 0 - maxX)
                o.transform.position = new Vector3(o.transform.position.x + (maxX * 2), o.transform.position.y, o.transform.position.z);
            //if the event position is too far to the right, the move it to the left side
            if (o.transform.position.x > 0 + maxX)
                o.transform.position = new Vector3(o.transform.position.x - (maxX * 2), o.transform.position.y, o.transform.position.z);
        }

        //Move everything down at the variable rate of GravitySpeed and check to see if it still in vertical bounds
        foreach (GameObject o in obj)
        {
            o.transform.position = new Vector3(o.transform.position.x, o.transform.position.y - gravitySpeed, o.transform.position.z);
            if (o.transform.position.y < deathY)
                Destroy(o);
        }
  
    }
}
