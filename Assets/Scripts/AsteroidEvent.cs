using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidEvent : MonoBehaviour
{

    public float damageAmount = 80;
    private AudioManager audio;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "Asteroid";
        audio = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioManager>();
        GetComponent<ParticleSystem>().Stop();
    }


    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            //When the event hits the player: do something
            //decrement from fuel based on damageAmount
            audio.Stop(audio.hitAsteroid);
            audio.playAudioOnce(audio.hitAsteroid);
            GetComponent<Collider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<ParticleSystem>().Play();
            GameObject.FindGameObjectWithTag("Player").GetComponent<ShipManager>().fuelRemaining -= damageAmount; //handle a shield?
            //Destroy(gameObject);
        }
    }
}
