using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelEvent : MonoBehaviour
{

    public float volume = 50;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "Fuel";
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            //When the event hits the player: do something
            GameObject.FindGameObjectWithTag("Player").GetComponent<ShipManager>().fuelRemaining += volume;
            Destroy(gameObject);
        }
    }
}
