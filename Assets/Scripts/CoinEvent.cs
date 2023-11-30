using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinEvent : MonoBehaviour
{

    public int coinAmount = 1;
    private AudioManager audio;

    private GameObject userShip;

    // Start is called before the first frame update
    void Start()
    {
        userShip = GameObject.FindGameObjectWithTag("Player");
        gameObject.name = "Coin";
        audio = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (userShip.GetComponent<ShipManager>().magFramesRemaining > 0)
        {
            if (Vector3.Distance(transform.position, userShip.transform.position) < 6)
            {
                Vector3 newPosition = Vector3.Lerp(transform.position, userShip.transform.position, 0.07f);

                // Update the object's position
                transform.position = newPosition;
            }
        }
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
