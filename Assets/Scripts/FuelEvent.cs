using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelEvent : MonoBehaviour
{

    public float volume = 50;
    private AudioManager audio;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "Fuel";
        audio = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioManager>();
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            audio.Stop(audio.hitFuel);
            audio.playAudioOnce(audio.hitFuel);
            //When the event hits the player: do something
            GameObject.FindGameObjectWithTag("Player").GetComponent<ShipManager>().fuelRemaining += volume;
            Destroy(gameObject);
        }
    }
}
