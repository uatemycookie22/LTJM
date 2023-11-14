using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    //this speed should increase to reflect the games difficulty speed
    public float speedAndDirection;
    //the first layers should be furthest from the screen
    public GameObject[] layers;

    public float offScreenResetPoints;

    public void Start()
    {
        offScreenResetPoints = gameObject.GetComponent<Camera>().orthographicSize*2;
        for (int i = 0; i < layers.Length; i++)
            if (i % 2 != 0)
                layers[i].transform.localPosition = new Vector3(0, offScreenResetPoints/2.0f, 10 + (1 * Mathf.Floor((i + 2) / 2)));
            else
                layers[i].transform.localPosition = new Vector3(0, offScreenResetPoints/2.0f * -1, 10 + (1 * Mathf.Floor((i + 2) / 2)));
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < layers.Length; i++)
        {
            Vector3 newPos = new Vector3(0, layers[i].transform.localPosition.y - (speedAndDirection*Mathf.Floor((i+2)/2)), 10 + (1 * Mathf.Floor((i + 2) / 2)));

            if (layers[i].transform.localPosition.y < offScreenResetPoints*-1)
                newPos = new Vector3(0, offScreenResetPoints, 10 + (1 * Mathf.Floor((i + 2) / 2)));
            if (layers[i].transform.localPosition.y > offScreenResetPoints)
                newPos = new Vector3(0, offScreenResetPoints * -1, 10 + (1 * Mathf.Floor((i + 2) / 2)));

            layers[i].transform.localPosition = newPos;
        }
    }
}
