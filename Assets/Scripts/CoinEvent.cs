using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinEvent : MonoBehaviour
{

    public int coinAmount;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "Coin";
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            //When the event hits the player: do something
            GameObject.FindGameObjectWithTag("Player").GetComponent<ShipManager>().addCoin(coinAmount);
            Destroy(gameObject);
        }
    }
}
