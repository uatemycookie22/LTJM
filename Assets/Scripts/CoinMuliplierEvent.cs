using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMuliplierEvent : MonoBehaviour
{
    public int coinMultiplierValue = 2;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "Multiplier";
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            //When the event hits the player: do something
            GameObject.FindGameObjectWithTag("Player").GetComponent<ShipManager>().coinMultiplier = coinMultiplierValue;
            //set the number of frame remaining on the multiplier
            //30 frames = 1 second
            GameObject.FindGameObjectWithTag("Player").GetComponent<ShipManager>().multFramesRemaining = 5*30;
            Destroy(gameObject);
        }
    }
}
