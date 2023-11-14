using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    //this speed should increase to reflect the games difficulty speed
    public float VerticalSpeedAndDirection;
    public float HorizontalSpeedAndDirection;

    //the first layers should be furthest from the screen
    public GameObject[] layers;

    public float offScreenResetPoints;

    public void Start()
    {
        //get the X and Y limits from the viewing size of the camera
        offScreenResetPoints = gameObject.GetComponent<Camera>().orthographicSize*2;

        //Set the y position of the layers
        for (int i = 0; i < layers.Length/4; i++)
        {
            layers[0 + (i * 4)].transform.localPosition = new Vector3(offScreenResetPoints / 2.0f, offScreenResetPoints / 2.0f, 10 + (1 * Mathf.Floor((i + 4) / 4)));
            layers[1 + (i * 4)].transform.localPosition = new Vector3(offScreenResetPoints / 2.0f, offScreenResetPoints / 2.0f * -1, 10 + (1 * Mathf.Floor((i + 4) / 4)));
            layers[2 + (i * 4)].transform.localPosition = new Vector3(offScreenResetPoints / 2.0f * -1, offScreenResetPoints / 2.0f, 10 + (1 * Mathf.Floor((i + 4) / 4)));
            layers[3 + (i * 4)].transform.localPosition = new Vector3(offScreenResetPoints / 2.0f * -1, offScreenResetPoints / 2.0f * -1, 10 + (1 * Mathf.Floor((i + 4) / 4)));
        }

        //scale
        for (int i = 0; i < layers.Length; i++)
            layers[i].transform.localScale = new Vector3(offScreenResetPoints, offScreenResetPoints, offScreenResetPoints);
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < layers.Length; i++)
        {
            //update the x and y position based on its movement speed
            Vector3 newPos = new Vector3(layers[i].transform.localPosition.x - HorizontalSpeedAndDirection, layers[i].transform.localPosition.y - VerticalSpeedAndDirection, layers[i].transform.localPosition.z);
            
            //check bounds of the y position
            if (layers[i].transform.localPosition.y < offScreenResetPoints*-1)
                newPos = new Vector3(newPos.x, newPos.y + (offScreenResetPoints * 2), newPos.z);
            if (layers[i].transform.localPosition.y > offScreenResetPoints)
                newPos = new Vector3(newPos.x, newPos.y - (offScreenResetPoints * 2), newPos.z);

            //check bounds of the x position
            if (layers[i].transform.localPosition.x < offScreenResetPoints * -1)
                newPos = new Vector3(newPos.x + (offScreenResetPoints * 2), newPos.y, newPos.z);
            if (layers[i].transform.localPosition.x > offScreenResetPoints)
                newPos = new Vector3(newPos.x - (offScreenResetPoints * 2), newPos.y, newPos.z);

            layers[i].transform.localPosition = newPos;
        }
    }
}
