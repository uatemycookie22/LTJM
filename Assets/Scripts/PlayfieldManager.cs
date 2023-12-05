using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayfieldManager : MonoBehaviour
{
    private float gravitySpeed;
    public float moveSpeed;
    public int maxX;

    public int spawnY;
    public int deathY;

    public GameObject shipToSpawn;
    public GameObject[] eventPrefabs;

    //an event can be any object that the user will interact with (asteroids, coins, power-ups)
    private GameObject[] obj;
    private int maxEvents = 20;
    private float minProximity = 3.0f;
    private bool inGame = false;
    private GameObject userShip;
    private AudioManager audio;

    //this is modified externaly from the shipManager script
    public float moveAngle;

    private void Start()
    {

        audio = gameObject.GetComponent<AudioManager>();
        gravitySpeed = moveSpeed;
    }

    public void StartRun()
    {
        gravitySpeed = moveSpeed;

        //spawn the ship
        userShip = Instantiate(shipToSpawn);

        //randomly spawn all the events in a field above the screen.
        for(int i = 0; i < maxEvents; i++)
        {
            obj = GameObject.FindGameObjectsWithTag("Event");
            CreateNewEvent();
        }

        //start in game music
        audio.Stop(audio.mainMenuBG);
        audio.PlayLoop(audio.inGameBG);


        //This variable is set true by the
        inGame = true;
    }

    public void EndRun()
    {
        //remove all the playfield objects
        obj = GameObject.FindGameObjectsWithTag("Event");
        foreach (GameObject o in obj)
            Destroy(o);

        //handle the score - check for high score
        CheckHighScore(userShip.GetComponent<ShipManager>().score);

        //destroy the ship
        Destroy(userShip);

        //reset the paralax background speed
        GetComponent<ParallaxBackground>().ResetBackground();


        //change the screen to post game
        GetComponent<MainMenu>().currMenu = "POST GAME";

        //end game audio
        audio.playAudioOnce(audio.gameOver);
        audio.Stop(audio.inGameBG);
        audio.PlayLoop(audio.mainMenuBG);

        inGame = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        //this block of code should only run during game play (refactoring)
        if (inGame && GameObject.FindGameObjectWithTag("Player").GetComponent<ShipManager>().firstTouch)
        {
            //increase difficulty - at a constant rate?
            gravitySpeed += 0.0005f;
            //also increase the speed for the parallax background
            GetComponent<ParallaxBackground>().VerticalSpeedAndDirection += 0.0004f;

            //an event can be any object that the user will interact with (asteroids, coins, power-ups)
            obj = GameObject.FindGameObjectsWithTag("Event");

            //if there are less than maxEvents, then instantiate new events
            if (obj.Length < maxEvents && inGame)
            {
                CreateNewEvent();
            }

            Vector3 velocity = new Vector3(0, 0, 0);
            if (userShip != null)            //make sure the ship exists first
                velocity = userShip.GetComponent<ShipManager>().getVelocity();

            // Move game objects in the negative direction of the ship 
            foreach (GameObject o in obj)
            {
                o.transform.position -= velocity * gravitySpeed;
            }

            //check to see if an event object has gone out of bounds
            foreach (GameObject o in obj)
            {
                //if the event position is too far to the left, the move it to the right side
                if (o.transform.position.x < 0 - maxX)
                    o.transform.position = new Vector3(o.transform.position.x + (maxX * 2), o.transform.position.y, o.transform.position.z);
                //if the event position is too far to the right, the move it to the left side
                if (o.transform.position.x > 0 + maxX)
                    o.transform.position = new Vector3(o.transform.position.x - (maxX * 2), o.transform.position.y, o.transform.position.z);
                //if out of lower bounds (off screen), destroy
                if (o.transform.position.y < deathY)
                    Destroy(o);
            }
        }

        //draw a debug box around the area of randomised spawning (above playable area)
        Debug.DrawLine(new Vector3(maxX * -1, spawnY, 0), new Vector3(maxX * -1, spawnY + (spawnY - deathY), 0));
        Debug.DrawLine(new Vector3(maxX, spawnY, 0), new Vector3(maxX, spawnY + (spawnY - deathY), 0));
        Debug.DrawLine(new Vector3(maxX, spawnY + (spawnY - deathY), 0), new Vector3(maxX * -1, spawnY + (spawnY - deathY), 0));

        //draw a debug box around the playable space (below the area of randomised spawning)
        Debug.DrawLine(new Vector3(maxX*-1, spawnY, 0), new Vector3(maxX*-1, deathY, 0));
        Debug.DrawLine(new Vector3(maxX, spawnY, 0), new Vector3(maxX, deathY, 0));
        Debug.DrawLine(new Vector3(maxX, spawnY, 0), new Vector3(maxX*-1, spawnY, 0));
        Debug.DrawLine(new Vector3(maxX, deathY, 0), new Vector3(maxX*-1, deathY, 0));
    }
    

    void CreateNewEvent()
    {
        int randomEvent = Random.Range(0, eventPrefabs.Length);
        GameObject newEvent = Instantiate(eventPrefabs[randomEvent]);

        //the y spawning position should me randomised
        //an object should not spawn on the screen (therfore off screen spawning)
        //the random y range should be eaqual to the playable space (spawnY - deathY) the playable space is outline with the lower debug box lines
        newEvent.transform.position = new Vector3(Random.Range(maxX * -1*100, maxX * 100)/100.0f, Random.Range(spawnY * 100, (spawnY * 100) + ((spawnY - deathY) * 100)) / 100.0f, newEvent.transform.position.z);
        //check to make sure its not too close to an exisitng event
        while(ProximityCheck(newEvent) == false)
            newEvent.transform.position = new Vector3(Random.Range(maxX * -1 * 100, maxX * 100)/100.0f, Random.Range(spawnY * 100, (spawnY * 100) + ((spawnY - deathY) * 100))/100.0f, newEvent.transform.position.z);

        //update the list of existing events
        obj = GameObject.FindGameObjectsWithTag("Event");
    }

    //proximityCheck will make sure that an event does not spawn too close to another one that already exists
    bool ProximityCheck(GameObject newEvent)
    {
        //how close is too close? in units
        foreach (GameObject o in obj) {
            if (Vector3.Distance(o.transform.position, newEvent.transform.position) < minProximity)
                return false;
            if (newEvent.transform.position.x < maxX*-1 + (minProximity/2))
                return false;
            if (newEvent.transform.position.x > maxX - (minProximity/2))
                return false;
        }
        return true;
    }

    //update the high score the hard way
    private void CheckHighScore(float score)
    {
        Debug.Log("Checking against score " + score);
        //if the score is able to make the top 10...
        if (PlayerPrefs.GetFloat("High10") < score)
        {
            PlayerPrefs.SetFloat("High10", score);
            PlayerPrefs.SetString("Name" + "High10", PlayerPrefs.GetString("User Name"));
        }
        if (PlayerPrefs.GetFloat("High9") < score)
        {
            //move this player back a spot to insert the new score
            PlayerPrefs.SetFloat("High10", PlayerPrefs.GetFloat("High9"));
            PlayerPrefs.SetString("Name" + "High10", PlayerPrefs.GetString("High9"));
            //insert player at current high score spot
            PlayerPrefs.SetFloat("High9", score);
            PlayerPrefs.SetString("Name" + "High9", PlayerPrefs.GetString("User Name"));
        }
        if (PlayerPrefs.GetFloat("High8") < score)
        {
            //move this player back a spot to insert the new score
            PlayerPrefs.SetFloat("High9", PlayerPrefs.GetFloat("High8"));
            PlayerPrefs.SetString("Name" + "High9", PlayerPrefs.GetString("High8"));
            //insert player at current high score spot
            PlayerPrefs.SetFloat("High8", score);
            PlayerPrefs.SetString("Name" + "High8", PlayerPrefs.GetString("User Name"));
        }
        if (PlayerPrefs.GetFloat("High7") < score)
        {
            //move this player back a spot to insert the new score
            PlayerPrefs.SetFloat("High8", PlayerPrefs.GetFloat("High7"));
            PlayerPrefs.SetString("Name" + "High8", PlayerPrefs.GetString("High7"));
            //insert player at current high score spot
            PlayerPrefs.SetFloat("High7", score);
            PlayerPrefs.SetString("Name" + "High7", PlayerPrefs.GetString("User Name"));
        }
        if (PlayerPrefs.GetFloat("High6") < score)
        {
            //move this player back a spot to insert the new score
            PlayerPrefs.SetFloat("High7", PlayerPrefs.GetFloat("High6"));
            PlayerPrefs.SetString("Name" + "High7", PlayerPrefs.GetString("High6"));
            //insert player at current high score spot
            PlayerPrefs.SetFloat("High6", score);
            PlayerPrefs.SetString("Name" + "High6", PlayerPrefs.GetString("User Name"));
        }
        if (PlayerPrefs.GetFloat("High5") < score)
        {
            //move this player back a spot to insert the new score
            PlayerPrefs.SetFloat("High6", PlayerPrefs.GetFloat("High5"));
            PlayerPrefs.SetString("Name" + "High6", PlayerPrefs.GetString("High5"));
            //insert player at current high score spot
            PlayerPrefs.SetFloat("High5", score);
            PlayerPrefs.SetString("Name" + "High5", PlayerPrefs.GetString("User Name"));
        }
        if (PlayerPrefs.GetFloat("High4") < score)
        {
            //move this player back a spot to insert the new score
            PlayerPrefs.SetFloat("High5", PlayerPrefs.GetFloat("High4"));
            PlayerPrefs.SetString("Name" + "High5", PlayerPrefs.GetString("High4"));
            //insert player at current high score spot
            PlayerPrefs.SetFloat("High4", score);
            PlayerPrefs.SetString("Name" + "High4", PlayerPrefs.GetString("User Name"));
        }
        if (PlayerPrefs.GetFloat("High3") < score)
        {
            //move this player back a spot to insert the new score
            PlayerPrefs.SetFloat("High4", PlayerPrefs.GetFloat("High3"));
            PlayerPrefs.SetString("Name" + "High4", PlayerPrefs.GetString("High3"));
            //insert player at current high score spot
            PlayerPrefs.SetFloat("High3", score);
            PlayerPrefs.SetString("Name" + "High3", PlayerPrefs.GetString("User Name"));
        }
        if (PlayerPrefs.GetFloat("High2") < score)
        {
            //move this player back a spot to insert the new score
            PlayerPrefs.SetFloat("High3", PlayerPrefs.GetFloat("High2"));
            PlayerPrefs.SetString("Name" + "High3", PlayerPrefs.GetString("High2"));
            //insert player at current high score spot
            PlayerPrefs.SetFloat("High2", score);
            PlayerPrefs.SetString("Name" + "High2", PlayerPrefs.GetString("User Name"));
        }
        if (PlayerPrefs.GetFloat("High1") < score)
        {
            //move this player back a spot to insert the new score
            PlayerPrefs.SetFloat("High2", PlayerPrefs.GetFloat("High1"));
            PlayerPrefs.SetString("Name" + "High2", PlayerPrefs.GetString("High1"));
            //insert player at current high score spot
            PlayerPrefs.SetFloat("High1", score);
            PlayerPrefs.SetString("Name" + "High1", PlayerPrefs.GetString("User Name"));
        }
    }

        public float getAltitude()
    {
        return userShip.GetComponent<ShipManager>()
            .getAltitude();
    }
    
    public float getFuel()
    {
        return userShip.GetComponent<ShipManager>()
            .getFuel();
    }
}
