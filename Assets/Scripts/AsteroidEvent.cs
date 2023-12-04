using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidEvent : MonoBehaviour
{

    public float damageAmount = 80;
    private AudioManager audio;
    public bool blowUp = false;

    private float targetblowScale;
    private float blowSpeed = 0.4f;

    Vector3 origScale;

    // Start is called before the first frame update
    void Start()
    {
        targetblowScale = transform.localScale.x / 4;
        origScale = transform.localScale;
        gameObject.name = "Asteroid";
        audio = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioManager>();
        GetComponent<ParticleSystem>().Stop();
        blowUp = false;
    }


    private void Update()
    {
        if (blowUp == true) {
            if (transform.localScale.x < targetblowScale)
            {
                //fix the scaling so that the particle system still looks right (it also gets scaled)
                transform.localScale = origScale;

                //When the event hits the player: do something
                //decrement from fuel based on damageAmount
                audio.Stop(audio.hitAsteroid);
                audio.playAudioOnce(audio.hitAsteroid);
                GetComponent<Collider>().enabled = false;
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<ParticleSystem>().Play();
                GameObject.FindGameObjectWithTag("Player").GetComponent<ShipManager>().fuelRemaining -= damageAmount; //handle a shield?
                //Destroy(gameObject);

                blowUp = false;
            } else {
                Vector3 newScale = new Vector3(transform.localScale.x * blowSpeed, transform.localScale.y * blowSpeed, transform.localScale.z * blowSpeed);
                transform.localScale = newScale;
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            blowUp = true;
            ////When the event hits the player: do something
            ////decrement from fuel based on damageAmount
            //audio.Stop(audio.hitAsteroid);
            //audio.playAudioOnce(audio.hitAsteroid);
            //GetComponent<Collider>().enabled = false;
            //GetComponent<MeshRenderer>().enabled = false;
            //GetComponent<ParticleSystem>().Play();
            //GameObject.FindGameObjectWithTag("Player").GetComponent<ShipManager>().fuelRemaining -= damageAmount; //handle a shield?
            ////Destroy(gameObject);
        }
    }
}
