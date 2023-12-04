using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GUISkin sliderStyle;
    public GUISkin FuelSliderStyle;
    public GUIStyle defaultStyle;

    public GUIStyle titleLogo;
    public GUIStyle playButton;
    public GUIStyle shopButton;
    public GUIStyle profileButton;
    public GUIStyle instructionsButton;
    public GUIStyle scoreButton;
    public GUIStyle backButton;
    public GUIStyle graphicsButton;
    public GUIStyle zoomButton;
    public GUIStyle languageButton;
    public GUIStyle pauseBG;
    public GUIStyle pauseButton;
    public GUIStyle unPauseButton;
    public GUIStyle settingsButton;
    public GUIStyle leaderboardStyle;
    public GUIStyle creditStyle;
    public GUIStyle menuBackground;
    public GUIStyle coinGraphic;
    public GUIStyle altitudeLabel;
    public GUIStyle plusButton;
    public GUIStyle shieldIcon;
    public GUIStyle fuelIcon;
    public GUIStyle magnetIcon;
    public GUIStyle coinLabel;
    public GUIStyle instructionsGraphic;
    public GUIStyle scoresStyle;

    int xPos = 2;

    public int coinsCollected = 0;
    public int altitude = 0;
    public string currMenu;
    private float volumeSlider = 0.5f;

    private float menuBgAspectRatio = 1.21f;
    //this will be used to add some distance between the edges of the button and the screen
    int buf = Screen.width / 100;

    private AudioManager audio;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Shield Level", 1);
        PlayerPrefs.SetInt("Fuel Level", 1);
        PlayerPrefs.SetInt("Magnet Level", 1);
        //PlayerPrefs.SetInt("Total Coins", 1000);
        
        Application.targetFrameRate = 30;
        currMenu = "MAIN MENU";

        //set all the font sizes based on screen size
        defaultStyle.alignment = TextAnchor.MiddleCenter;
        defaultStyle.fontSize = Screen.width / 17;
        playButton.alignment = TextAnchor.MiddleCenter;
        playButton.fontSize = Screen.width / 17;
        shopButton.alignment = TextAnchor.MiddleCenter;
        shopButton.fontSize = Screen.width / 17;
        profileButton.alignment = TextAnchor.MiddleCenter;
        profileButton.fontSize = Screen.width / 17;
        instructionsButton.alignment = TextAnchor.MiddleCenter;
        instructionsButton.fontSize = Screen.width / 17;
        scoreButton.alignment = TextAnchor.MiddleCenter;
        scoreButton.fontSize = Screen.width / 17;
        creditStyle.alignment = TextAnchor.MiddleCenter;
        creditStyle.fontSize = Screen.width / 17;
        coinGraphic.alignment = TextAnchor.MiddleCenter;
        coinGraphic.fontSize = Screen.width / 25;
        titleLogo.alignment = TextAnchor.MiddleCenter;
        titleLogo.fontSize = Screen.width / 6;
        titleLogo.border.left = Screen.width / 20;
        titleLogo.border.right = Screen.width / 20;
        titleLogo.border.top = Screen.width / 20;
        titleLogo.border.bottom = Screen.width / 20;
        settingsButton.alignment = TextAnchor.UpperRight;
        altitudeLabel.fontSize = Screen.width / 20;
        altitudeLabel.alignment = TextAnchor.MiddleCenter;
        leaderboardStyle.fontSize = Screen.width / 17;
        coinLabel.normal.textColor = Color.black;
        coinLabel.fontSize = Screen.width / 25;
        coinLabel.alignment = TextAnchor.MiddleLeft;
        instructionsGraphic.alignment = TextAnchor.MiddleCenter;
        scoresStyle.fontSize = Screen.width / 17;
        scoresStyle.alignment = TextAnchor.MiddleCenter;
        scoresStyle.normal.textColor = Color.white;

        audio = gameObject.GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        xPos = xPos*xPos;

        shopButton = playButton;
        profileButton = playButton;
        scoreButton = playButton;
        instructionsButton = playButton;
    }

    private void OnGUI()
    {
        //For debugging, show the currMenu at the top of the screen. This line is not mobile friendly.
        
        //currMenu displayed on the screen for debugging purposes. Do not let this get built to an apk or aab.
        //GUI.Label(new Rect(Screen.width / 15, Screen.width / 15, Screen.width, Screen.width / 15), currMenu);

        //Change the defaults. This will fix some slider sizing issues.

        if (currMenu == "MAIN MENU")
        {
            GUI.Box(new Rect(-Screen.width / 2, 0, Screen.height * menuBgAspectRatio, Screen.height), "", menuBackground);
            GUI.Box(new Rect(Screen.width / 10, Screen.width / 10, Screen.width / 10 * 8, Screen.width / 10 * 8), "ROCKET\nRUN", titleLogo);
            
            if (GUI.Button(new Rect(Screen.width / 4, Screen.height / 2 + (Screen.height / 12 * 1), Screen.width / 2, Screen.height / 15), "PLAY", playButton))
            {
                gameObject.GetComponent<PlayfieldManager>().StartRun();
                audio.playAudioOnce(audio.genericClick);
                currMenu = "INGAME";
            }
            if (GUI.Button(new Rect(Screen.width / 4, Screen.height / 2 + (Screen.height / 12 * 2), Screen.width / 2, Screen.height / 15), "SHOP", shopButton))
            {
                audio.playAudioOnce(audio.genericClick);
                currMenu = "SHOP";
            }
            //if (GUI.Button(new Rect(Screen.width - Screen.width / 8 - 10, 10, Screen.width / 8, Screen.width / 8), "", settingsButton))
            //{
            //    audio.playAudioOnce(audio.genericClick);
            //    currMenu = "SETTINGS";
            //}
            if (GUI.Button(new Rect(Screen.width / 4, Screen.height / 2 + (Screen.height / 12 * 3), Screen.width / 2, Screen.height / 15), "INSTRUCTIONS", instructionsButton))
            {
                audio.playAudioOnce(audio.genericClick);
                currMenu = "INSTRUCTIONS";
            }
            if (GUI.Button(new Rect(Screen.width / 4, Screen.height / 2 + (Screen.height / 12 * 4), Screen.width / 2, Screen.height / 15), "LEADERBOARD", scoreButton))
            {
                audio.playAudioOnce(audio.genericClick);
                currMenu = "LEADERBOARD";
            }
        }

        if (currMenu == "PROFILE")
        {
            GUI.Box(new Rect(-Screen.width / 2, 0, Screen.height * menuBgAspectRatio, Screen.height), "", menuBackground); // Background
            //Back Button
            if (GUI.Button(new Rect(Screen.width / 10 * 9 - buf, buf, Screen.width / 10, Screen.width / 10), "", backButton))
            {
                audio.playAudioOnce(audio.genericClick);
                currMenu = "MAIN MENU";
            }

        }

        if (currMenu == "INGAME")
        {
            GUI.skin = FuelSliderStyle;

            audio.Stop(audio.inGameBG);
            audio.PlayLoop(audio.inGameBG);

            //Back Button
            if (GUI.Button(new Rect(Screen.width / 10 * 9 - buf, Screen.height / 25 + buf, Screen.width / 10, Screen.width / 10), "", pauseButton))
            {
                audio.playAudioOnce(audio.pause);
                currMenu = "PAUSE";
            }

            //show coin count
            coinLabel.alignment = TextAnchor.MiddleCenter;
            coinLabel.wordWrap = true;
            GUI.Box(new Rect(Screen.width / 100, Screen.height - (Screen.width/7.0f) - (Screen.width / 100), Screen.width / 7.0f, Screen.width / 7.0f), "", coinGraphic);
            GUI.Label(new Rect(Screen.width / 100, Screen.height - (Screen.width/7.0f) - (Screen.width / 100), Screen.width / 7.0f, Screen.width / 7.0f), PlayerPrefs.GetInt("Total Coins") + " Coins", coinLabel);
            
            //show fuel level
            float fuel = GameObject.FindGameObjectWithTag("MainCamera")
                .GetComponent<PlayfieldManager>().getFuel();

            fuel = 1 - (fuel / PlayerPrefs.GetFloat("Max Fuel"));

            FuelSliderStyle.horizontalSliderThumb.fixedWidth = Screen.width / 16;
            float fuelSlider = GUI.HorizontalSlider(new Rect(0, 0, Screen.width, Screen.height/25), fuel, 0.0f, 1.0f);

            //show altitude
            altitude = (int)GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayfieldManager>().getAltitude();
            altitude /= 10;
            
            GUI.Box(new Rect(Screen.width / 7, Screen.height - (Screen.height / 18), Screen.width/7*5 , Screen.height/18), "Altitude: " + altitude, altitudeLabel);

            //show pause button

        }

        //this menu is called from the Playfield Manager
        if(currMenu == "POST GAME")
        {
            float boxTop = (Screen.height - (Screen.height / 2.5f)) /2;
            float boxLeft = (Screen.width - (Screen.height / 2.5f)) / 2;
            float boxWidthHeight = Screen.height / 2.5f;

            //box
            GUI.Box(new Rect(boxLeft, boxTop, boxWidthHeight, boxWidthHeight), "", titleLogo);

            //coins collected
            GUI.Box(new Rect(boxLeft + (boxWidthHeight / 4), boxTop + (boxWidthHeight / 5 * 2), boxWidthHeight / 2, boxWidthHeight / 6), "COINS COLLECTED: " + coinsCollected.ToString(), scoresStyle);

            //alltitude reached
            GUI.Box(new Rect(boxLeft + (boxWidthHeight / 4), boxTop + (boxWidthHeight / 5 * 1), boxWidthHeight / 2, boxWidthHeight / 6), "ALTITUDE: " + altitude.ToString(), scoresStyle);

            //continue button
            if (GUI.Button(new Rect(boxLeft + (boxWidthHeight/4), boxTop + (boxWidthHeight/4*3), boxWidthHeight/2, boxWidthHeight/6), "CONTINUE", playButton))
            {
                currMenu = "MAIN MENU";
                audio.playAudioOnce(audio.genericClick);
            }

            audio.Stop(audio.inGameBG);
            audio.playAudioOnce(audio.gameOver);
            audio.PlayLoop(audio.mainMenuBG);
            GUI.Box(new Rect(-Screen.width / 2, 0, Screen.height * menuBgAspectRatio, Screen.height), "", menuBackground); // Background
        }

        //Pause the game by disabling the playfield manager script
        if (currMenu == "PAUSE")
        {
            //cover screen with a tint
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "", pauseBG);

            GetComponent<PlayfieldManager>().enabled = false;
            if (GUI.Button(new Rect(Screen.width / 10 * 9 - buf, buf, Screen.width / 10, Screen.width / 10), "", unPauseButton))
            {
                audio.playAudioOnce(audio.resume);
                currMenu = "INGAME";
                GetComponent<PlayfieldManager>().enabled = true;
            }
            //quit to menu
            if (GUI.Button(new Rect(Screen.width / 4, Screen.height / 11 * 2, Screen.width / 2, Screen.height / 15), "Quit", defaultStyle))
            {
                //tell the playfield manager to clear all objects
                GetComponent<PlayfieldManager>().enabled = true;
                gameObject.GetComponent<PlayfieldManager>().EndRun();
                audio.playAudioOnce(audio.genericClick);

                currMenu = "MAIN MENU";
            }

            //restart
            if (GUI.Button(new Rect(Screen.width / 4, Screen.height / 11 * 4, Screen.width / 2, Screen.height / 15), "Restart", defaultStyle))
            {
                //Do something with the playfield manager here
                GetComponent<PlayfieldManager>().enabled = true;
                gameObject.GetComponent<PlayfieldManager>().EndRun();
                gameObject.GetComponent<PlayfieldManager>().StartRun();
                audio.playAudioOnce(audio.genericClick);
                currMenu = "INGAME";
            }
        }

        if(currMenu == "SHOP")
        {
            GUI.skin = sliderStyle;

            GUI.Box(new Rect(-Screen.width / 2, 0, Screen.height * menuBgAspectRatio, Screen.height), "", menuBackground); // Background
            //show coin count
            GUI.Box(new Rect(Screen.width / 2  - (Screen.width / 100) * 22, Screen.height / 1.25f, Screen.width / 10, Screen.width / 10), "", coinGraphic);
            GUI.Label(new Rect(Screen.width / 2 - (Screen.width / 100) * 8, Screen.height / 1.25f, Screen.width / 10, Screen.width / 10), PlayerPrefs.GetInt("Total Coins") + " Coins", coinLabel);
            
            //Back Button
            if (GUI.Button(new Rect(Screen.width / 10 * 9 - buf, buf, Screen.width / 10, Screen.width / 10), "", backButton))
            {
                audio.playAudioOnce(audio.genericClick);
                currMenu = "MAIN MENU";
            }

            GUI.Box(new Rect(Screen.width / 4, Screen.height / 15, Screen.width / 2, Screen.height / 15), "SHOP", shopButton);

            //sliders are not user controlled. They will use the +- buttons and the slider will move when they buy something
            int shopItemsCount = 5;
            sliderStyle.horizontalSlider.fixedWidth = Screen.width / 5 * 2;
            sliderStyle.horizontalSlider.fixedHeight = Screen.height / (shopItemsCount * 2) /3;
            sliderStyle.horizontalSlider.alignment = TextAnchor.MiddleCenter;
            sliderStyle.horizontalSliderThumb.fixedWidth = Screen.height / (shopItemsCount * 2) /3;
            sliderStyle.horizontalSliderThumb.fixedHeight = Screen.height / (shopItemsCount * 2) /3;

            float xSlider = Screen.width / 2 - sliderStyle.horizontalSlider.fixedWidth / 2;
            float xButton = xSlider - sliderStyle.horizontalSlider.fixedHeight*1.5f;
            float xIcon = Screen.width / 2 + sliderStyle.horizontalSlider.fixedWidth / 2 + sliderStyle.horizontalSlider.fixedHeight*0.5f;
            float iconSize = sliderStyle.horizontalSliderThumb.fixedHeight * 2f;
            
            //Shields slider
            float shieldsSlider = GUI.HorizontalSlider(new Rect(xSlider, (Screen.height / (shopItemsCount * 2 + 1)) * 3, sliderStyle.horizontalSlider.fixedWidth, sliderStyle.horizontalSlider.fixedHeight), PlayerPrefs.GetInt("Shield Level"), 1.0f, 10.0f, sliderStyle.horizontalSlider, sliderStyle.horizontalSliderThumb);
            GUI.Box(new Rect(xIcon, (Screen.height / (shopItemsCount * 2 + 1)) * 3 - iconSize/2, iconSize, iconSize), "", shieldIcon); // Icon
            GUI.Label(new Rect(xButton - iconSize / 8, (Screen.height / (shopItemsCount * 2 + 1)) * 4 - sliderStyle.horizontalSlider.fixedHeight, iconSize/2, iconSize/2), "", coinGraphic);
            GUI.Label(new Rect(xButton + iconSize / 2, (Screen.height / (shopItemsCount * 2 + 1)) * 4 - sliderStyle.horizontalSlider.fixedHeight, iconSize/2, iconSize/2), "" + levelToCost(PlayerPrefs.GetInt("Shield Level")), coinLabel);
            GUI.Label(new Rect(xSlider, (Screen.height / (shopItemsCount * 2 + 1)) * 3 - sliderStyle.horizontalSlider.fixedHeight, sliderStyle.horizontalSlider.fixedWidth, sliderStyle.horizontalSlider.fixedHeight), "Shield Level", defaultStyle);
            //Upgrade shield button
            if (GUI.Button(new Rect(xButton, (Screen.height / (shopItemsCount * 2 + 1)) * 3, sliderStyle.horizontalSlider.fixedHeight, sliderStyle.horizontalSlider.fixedHeight), "", plusButton))
            {
                upgradeLevel("Shield Level");
            }
            
            
            //Fuel Tank Slider
            float fuelSlider = GUI.HorizontalSlider(new Rect(xSlider, (Screen.height / (shopItemsCount * 2 + 1)) * 5, sliderStyle.horizontalSlider.fixedWidth, sliderStyle.horizontalSlider.fixedHeight), PlayerPrefs.GetInt("Fuel Level"), 1.0f, 10.0f);
            GUI.Box(new Rect(xIcon, (Screen.height / (shopItemsCount * 2 + 1)) * 5 - iconSize/2, iconSize, iconSize), "", fuelIcon); // Icon
            GUI.Label(new Rect(xButton - iconSize / 8, (Screen.height / (shopItemsCount * 2 + 1)) * 6 - sliderStyle.horizontalSlider.fixedHeight, iconSize/2, iconSize/2), "", coinGraphic);
            GUI.Label(new Rect(xButton + iconSize / 2, (Screen.height / (shopItemsCount * 2 + 1)) * 6 - sliderStyle.horizontalSlider.fixedHeight, iconSize/2, iconSize/2), "" + levelToCost(PlayerPrefs.GetInt("Fuel Level")), coinLabel);
            GUI.Label(new Rect(xSlider, (Screen.height / (shopItemsCount * 2 + 1)) * 5 - sliderStyle.horizontalSlider.fixedHeight, sliderStyle.horizontalSlider.fixedWidth, sliderStyle.horizontalSlider.fixedHeight), "Fuel Level", defaultStyle);
            //Upgrade fuel button
            if (GUI.Button(new Rect(xButton, (Screen.height / (shopItemsCount * 2 + 1)) * 5, sliderStyle.horizontalSlider.fixedHeight, sliderStyle.horizontalSlider.fixedHeight), "", plusButton))
            {
                upgradeLevel("Fuel Level");
            }
            
            //Coin Magnet Slider
            
            float magnetSlider = GUI.HorizontalSlider(new Rect(xSlider, (Screen.height / (shopItemsCount * 2 + 1)) * 7, sliderStyle.horizontalSlider.fixedWidth, sliderStyle.horizontalSlider.fixedHeight), PlayerPrefs.GetInt("Magnet Level"), 1.0f, 10.0f);
            GUI.Box(new Rect(xIcon, (Screen.height / (shopItemsCount * 2 + 1)) * 7 - iconSize/2, iconSize, iconSize), "", magnetIcon); // Icon
            GUI.Label(new Rect(xButton - iconSize / 8, (Screen.height / (shopItemsCount * 2 + 1)) * 8 - sliderStyle.horizontalSlider.fixedHeight, iconSize/2, iconSize/2), "", coinGraphic);
            GUI.Label(new Rect(xButton + iconSize / 2, (Screen.height / (shopItemsCount * 2 + 1)) * 8 - sliderStyle.horizontalSlider.fixedHeight, iconSize/2, iconSize/2), "" + levelToCost(PlayerPrefs.GetInt("Magnet Level")), coinLabel);
            GUI.Label(new Rect(xSlider, (Screen.height / (shopItemsCount * 2 + 1)) * 7 - sliderStyle.horizontalSlider.fixedHeight, sliderStyle.horizontalSlider.fixedWidth, sliderStyle.horizontalSlider.fixedHeight), "Magnet Level", defaultStyle);
            //Upgrade magnet button
            if (GUI.Button(new Rect(xButton, (Screen.height / (shopItemsCount * 2 + 1)) * 7, sliderStyle.horizontalSlider.fixedHeight, sliderStyle.horizontalSlider.fixedHeight), "", plusButton))
            {
                upgradeLevel("Magnet Level");
            }
        }

        if(currMenu == "SETTINGS")
        {
            GUI.Box(new Rect(-Screen.width / 2, 0, Screen.height * menuBgAspectRatio, Screen.height), "", menuBackground); // Background
            //Back Button
            if (GUI.Button(new Rect(Screen.width / 10 * 9 - buf, buf, Screen.width / 10, Screen.width / 10), "", backButton))
            {
                audio.playAudioOnce(audio.genericClick);
                currMenu = "MAIN MENU";
            }

            int settingsCount = 5;
            //Graphics options
            if(GUI.Button(new Rect(Screen.width/4, (Screen.height/(settingsCount*2+1)) * 1, Screen.width/2, Screen.height/(settingsCount*2)), "Graphics Level: x1", graphicsButton))
            {

            }

            //Volume Control slider
            volumeSlider = GUI.HorizontalSlider(new Rect(Screen.width / 4, (Screen.height / (settingsCount * 2 + 1)) * 3, Screen.width / 2, Screen.height / (settingsCount * 2)), volumeSlider, 0.0f, 1.0f);
            GUI.Label(new Rect(Screen.width/4, (Screen.height / (settingsCount * 2 + 1)) * 3 - 30, 200, 20), "Slider Value: " + volumeSlider.ToString("F2"));

            //Language accessibility
            if (GUI.Button(new Rect(Screen.width / 4, (Screen.height / (settingsCount * 2 + 1)) * 5, Screen.width / 2, Screen.height / (settingsCount * 2)), "Language: English", languageButton))
            {

            }

            //Language accessibility
            if (GUI.Button(new Rect(Screen.width / 4, (Screen.height / (settingsCount * 2 + 1)) * 7, Screen.width / 2, Screen.height / (settingsCount * 2)), "Zoom: x1", zoomButton))
            {

            }


            //5th setting: Access to profile?
            if (GUI.Button(new Rect(Screen.width / 4, (Screen.height / (settingsCount * 2 + 1)) * 9, Screen.width / 2, Screen.height / (settingsCount * 2)), "Profile", profileButton))
            {
                currMenu = "PROFILE";
            }       
        }

        GUIStyle tempStyle = new GUIStyle(leaderboardStyle);
        if(currMenu == "LEADERBOARD")
        {
            GUI.Box(new Rect(-Screen.width / 2, 0, Screen.height * menuBgAspectRatio, Screen.height), "", menuBackground); // Background
            //Back Button
            if (GUI.Button(new Rect(Screen.width / 10 * 9 - buf, buf, Screen.width / 10, Screen.width / 10), "", backButton))
            {
                audio.playAudioOnce(audio.genericClick);
                currMenu = "MAIN MENU";
            }

            tempStyle.alignment = TextAnchor.MiddleCenter;
            GUI.Box(new Rect(Screen.width / 6, Screen.height / 15, Screen.width/6*4, Screen.height / 15), "LEADERBOARD", tempStyle);

            //get the leader names and score from the playerprefs memory
            List<float> highScoreList = new List<float>();
            List<string> highScoreNameList = new List<string>();
            highScoreList.Add(PlayerPrefs.GetFloat("High1"));
            highScoreList.Add(PlayerPrefs.GetFloat("High2"));
            highScoreList.Add(PlayerPrefs.GetFloat("High3"));
            highScoreList.Add(PlayerPrefs.GetFloat("High4"));
            highScoreList.Add(PlayerPrefs.GetFloat("High5"));
            highScoreList.Add(PlayerPrefs.GetFloat("High6"));
            highScoreList.Add(PlayerPrefs.GetFloat("High7"));
            highScoreList.Add(PlayerPrefs.GetFloat("High8"));
            highScoreList.Add(PlayerPrefs.GetFloat("High9"));
            highScoreList.Add(PlayerPrefs.GetFloat("High10"));
            highScoreNameList.Add(PlayerPrefs.GetString("Name" + "High1"));
            highScoreNameList.Add(PlayerPrefs.GetString("Name" + "High2"));
            highScoreNameList.Add(PlayerPrefs.GetString("Name" + "High3"));
            highScoreNameList.Add(PlayerPrefs.GetString("Name" + "High4"));
            highScoreNameList.Add(PlayerPrefs.GetString("Name" + "High5"));
            highScoreNameList.Add(PlayerPrefs.GetString("Name" + "High6"));
            highScoreNameList.Add(PlayerPrefs.GetString("Name" + "High7"));
            highScoreNameList.Add(PlayerPrefs.GetString("Name" + "High8"));
            highScoreNameList.Add(PlayerPrefs.GetString("Name" + "High9"));
            highScoreNameList.Add(PlayerPrefs.GetString("Name" + "High10"));

            leaderboardStyle.alignment = TextAnchor.MiddleLeft;
            for (int i = 0; i < 10; i++)
                GUI.Box(new Rect(Screen.width / 10, Screen.height / 15*3 + (Screen.height/13*i), Screen.width - (Screen.width / 10 * 2), Screen.height / 15), "  #" + (i+1) + "  " + highScoreNameList[i], leaderboardStyle);

            tempStyle.alignment = TextAnchor.MiddleRight;
            tempStyle.normal.background = null;
            for (int i = 0; i < 10; i++)
                GUI.Box(new Rect(Screen.width / 10, Screen.height / 15*3 + (Screen.height/13*i), Screen.width - (Screen.width / 10 * 2), Screen.height / 15), highScoreList[i] + "  ", tempStyle);
            
        }

        if (currMenu == "INSTRUCTIONS")
        {
            GUI.Box(new Rect(-Screen.width / 2, 0, Screen.height * menuBgAspectRatio, Screen.height), "", menuBackground); // Background
            //Back Button
            if (GUI.Button(new Rect(Screen.width / 10 * 9 - buf, buf, Screen.width / 10, Screen.width / 10), "", backButton))
            {
                audio.playAudioOnce(audio.genericClick);
                currMenu = "MAIN MENU";
            }
            
            //show how to play
            GUI.Box(new Rect(Screen.width/16, Screen.height/14, Screen.width - (Screen.width / 10), Screen.height - (Screen.height / 10)), "", instructionsGraphic);
        }

        if(currMenu == "CREDITS")
        {
            GUI.Box(new Rect(-Screen.width / 2, 0, Screen.height * menuBgAspectRatio, Screen.height), "", menuBackground); // Background
            //Back Button
            if (GUI.Button(new Rect(Screen.width / 10 * 9 - buf, buf, Screen.width / 10, Screen.width / 10), "", backButton))
            {
                audio.playAudioOnce(audio.genericClick);
                currMenu = "MAIN MENU";
            }

            //Show Creater info
            GUI.Box(new Rect(Screen.width / 5, Screen.height / 9, Screen.width / 5 * 3, Screen.height / 9 * 7), "Credit Text HERE", creditStyle);
        }
    }

    private int levelToCost(float level)
    {
        return (int)Math.Pow(level, 2);
    }

    private void upgradeLevel(string key)
    {
        int cost = levelToCost(PlayerPrefs.GetInt(key));
        int totalCoins = PlayerPrefs.GetInt("Total Coins");
        int currentLevel = PlayerPrefs.GetInt(key);

        if (cost > totalCoins)
        {
            return;
        }

        if (currentLevel >= 10)
        {
            return;
        }
        
        PlayerPrefs.SetInt(key, currentLevel + 1);
        PlayerPrefs.SetInt("Total Coins",  totalCoins - cost);
    }
}
