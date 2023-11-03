using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidEvent : MonoBehaviour
{

    public float damageAmount = 50;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "Asteroid";
    }


    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            //When the event hits the player: do something
            //decrement from fuel based on damageAmount
            GameObject.FindGameObjectWithTag("Player").GetComponent<ShipManager>().fuelRemaining -= damageAmount; //handle a shield?
            Destroy(gameObject);
        }
    }
}
