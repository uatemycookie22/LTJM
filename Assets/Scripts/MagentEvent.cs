using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagentEvent : MonoBehaviour
{

    private GameObject userShip;
    //30 frames per second
    public int totalFrames = 30 * 6;
    private AudioManager audio;

    // Start is called before the first frame update
    void Start()
    {
        userShip = GameObject.FindGameObjectWithTag("Player");
        gameObject.name = "Magnet";
        audio = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            //When the event hits the player: do something
            audio.Stop(audio.hitMagnet);
            audio.playAudioOnce(audio.hitMagnet);
            userShip.GetComponent<ShipManager>().magFramesRemaining = totalFrames;
            Destroy(gameObject);
        }
    }
}
