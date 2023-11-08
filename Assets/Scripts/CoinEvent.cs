using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinEvent : MonoBehaviour
{

    public int coinAmount = 1;
    private AudioManager audio;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "Coin";
        audio = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioManager>();
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            //When the event hits the player: do something
            audio.Stop(audio.hitCoin);
            audio.playAudioOnce(audio.hitCoin);
            GameObject.FindGameObjectWithTag("Player").GetComponent<ShipManager>().addCoin(coinAmount);
            Destroy(gameObject);
        }
    }
}
