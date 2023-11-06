using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMuliplierEvent : MonoBehaviour
{
    public int coinMultiplierValue = 2;
    private AudioManager audio;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "Multiplier";
        audio = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioManager>();
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            audio.hitMultiplier.Stop();
            audio.playAudioOnce(audio.hitMultiplier);
            //When the event hits the player: do something
            GameObject.FindGameObjectWithTag("Player").GetComponent<ShipManager>().coinMultiplier = coinMultiplierValue;
            //set the number of frame remaining on the multiplier
            //30 frames = 1 second
            GameObject.FindGameObjectWithTag("Player").GetComponent<ShipManager>().multFramesRemaining = 5*30;
            Destroy(gameObject);
        }
    }
}
