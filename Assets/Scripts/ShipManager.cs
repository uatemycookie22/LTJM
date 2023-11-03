using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{

    //direction will range from -1 to 1 based on the position of the finger (or mouse)
    public float direction;
    public float maxTurnAngle = 45;

    private int screenCenter;
    private float touchPosition;

    public float fuelUsageRate; //per frame
    public float fuelRemaining;
    private float defaultFuelAmount = 800.0f;

    public float score = 0;

    public int coinMultiplier = 1;
    public int multFramesRemaining = 0;
    private readonly Vector3 initVelocity = new Vector3(0, 1, 0);
    private Vector3 shipVelocity;

    // Start is called before the first frame update
    void Start()
    {
        screenCenter = Screen.width / 2;
        //Go to the PlayerPrefs and get the max fuel amount
        fuelRemaining = PlayerPrefs.GetFloat("Max Fuel");
        score = 0;
        multFramesRemaining = 0;
        shipVelocity = initVelocity;

        //if its the first time playing, then the fuel will be 0 to start
        //change the default memory amount of 0 fuel to starting amount
        if (fuelRemaining <= 0) { 
            fuelRemaining = defaultFuelAmount;
            PlayerPrefs.GetFloat("Max Fuel", defaultFuelAmount);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //~~~COIN MULIPLIER~~~
        //if the multiplier is more than 0 frames remaining, then deduct 1 frame - else - set muliplier back to 1
        if (multFramesRemaining > 0)
            multFramesRemaining--;
        else
            coinMultiplier = 1;

        //~~~FUEL USAGE~~~
        //if we run out of fuel, then end the game
        fuelRemaining -= fuelUsageRate;
        if(fuelRemaining <= 0)
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayfieldManager>().EndRun();

        //~~~SCORE HANDLER~~~
        //score is incremented once per frame
        //high score handling is not done by the ship. It is done by the playfield manager at the end of the game.
        score += 0.1f;

        //~~~SHIP CONTROLS~~~
        //update the direction variable based on the mouse position. 
        //direction is based on the mouse position, left or right of the screen center (range -1 to 1)
        touchPosition = Input.mousePosition.x;
        direction = (1 - (touchPosition / screenCenter)) * -1;

        //rotate the ship based on the maximum (45 degree) angle that the user can travel at
        float shipAngle = Math.Clamp(direction * maxTurnAngle * -1, -maxTurnAngle, maxTurnAngle);
        transform.eulerAngles = new Vector3(0, 0, shipAngle);

        //update the playfield manager with the new direction of the ship
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayfieldManager>().moveAngle = direction * -1;

        shipVelocity = Quaternion.AngleAxis(shipAngle, transform.forward) * initVelocity;
    }

    //do a fuel slider
    private void OnGUI()
    {
        
    }

    //hit a coin; called by the coin itself
    public void addCoin(int coinAmount)
    {
        //write to memory that a coin has been added to the pot
        //multiply coinAmount by coin multiplier
        PlayerPrefs.SetInt("Total Coins", PlayerPrefs.GetInt("Total Coins") + (coinAmount * coinMultiplier));
    }

    public Vector3 getVelocity()
    {
        return shipVelocity;
    }
}
