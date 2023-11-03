using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GUISkin sliderStyle;
    public GUIStyle defaultStyle;

    public GUIStyle unPauseButton;
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
    public GUIStyle leaderboardStyle;
    public GUIStyle creditStyle;
    public GUIStyle coinGraphic;

    public string currMenu;
    private float volumeSlider = 0.5f;

    //this will be used to add some distance between the edges of the button and the screen
    int buf = Screen.width / 100;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 50;
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
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        //For debugging, show the currMenu at the top of the screen.
        GUI.Label(new Rect(Screen.width / 15, Screen.width / 15, Screen.width, Screen.width / 15), currMenu);

        //Change the defaults. This will fix some slider sizing issues.
        GUI.skin = sliderStyle;

        if (currMenu == "MAIN MENU")
        {
            GUI.Box(new Rect(Screen.width / 10, Screen.width / 10, Screen.width / 10 * 8, Screen.width / 10 * 8), "ROCKET\nRUN", titleLogo);

            if (GUI.Button(new Rect(Screen.width / 4, Screen.height / 2 + (Screen.height / 13 * 1), Screen.width / 2, Screen.height / 15), "PLAY", playButton))
            {
                gameObject.GetComponent<PlayfieldManager>().StartRun();
                currMenu = "INGAME";
            }
            if (GUI.Button(new Rect(Screen.width / 4, Screen.height / 2 + (Screen.height / 13 * 2), Screen.width / 2, Screen.height / 15), "SHOP", shopButton))
            {
                currMenu = "SHOP";
            }
            if (GUI.Button(new Rect(Screen.width / 4, Screen.height / 2 + (Screen.height / 13 * 3), Screen.width / 2, Screen.height / 15), "SETTINGS", profileButton))
            {
                currMenu = "SETTINGS";
            }
            if (GUI.Button(new Rect(Screen.width / 4, Screen.height / 2 + (Screen.height / 13 * 4), Screen.width / 2, Screen.height / 15), "INSTRUCTIONS", instructionsButton))
            {
                currMenu = "INSTRUCTIONS";
            }
            if (GUI.Button(new Rect(Screen.width / 4, Screen.height / 2 + (Screen.height / 13 * 5), Screen.width / 2, Screen.height / 15), "LEADERBOARD", scoreButton))
            {
                currMenu = "LEADERBOARD";
            }
        }

        if (currMenu == "PROFILE")
        {
            //Back Button
            if (GUI.Button(new Rect(Screen.width / 10 * 9 - buf, buf, Screen.width / 10, Screen.width / 10), "", backButton))
            {
                currMenu = "MAIN MENU";
            }


        }

        if (currMenu == "INGAME")
        {
            //Back Button
            if (GUI.Button(new Rect(Screen.width / 10 * 9 - buf, buf, Screen.width / 10, Screen.width / 10), "", pauseButton))
            {
                currMenu = "PAUSE";
            }
            
            //show coin count
            GUI.Box(new Rect(Screen.width / 100, Screen.height / 120, Screen.width / 8, Screen.width / 8), PlayerPrefs.GetInt("Total Coins") + "\nCoins", coinGraphic);

            //show fuel level

            //show elevation

            //show pause button

        }

        //this menu is called from the Playfield Manager
        if(currMenu == "POST GAME")
        {
            //go to the main menu until stuff is added to this screen
            currMenu = "MAIN MENU";
        }

        //Pause the game by disabling the playfield manager script
        if (currMenu == "PAUSE")
        {
            //cover screen with a tint
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "", pauseBG);

            GetComponent<PlayfieldManager>().enabled = false;
            if (GUI.Button(new Rect(Screen.width / 10 * 9 - buf, buf, Screen.width / 10, Screen.width / 10), "", unPauseButton))
            {
                currMenu = "INGAME";
                GetComponent<PlayfieldManager>().enabled = true;
            }
            //quit to menu
            if (GUI.Button(new Rect(Screen.width / 4, Screen.height / 11 * 2, Screen.width / 2, Screen.height / 15), "Quit", defaultStyle))
            {
                //tell the playfield manager to clear all objects
                GetComponent<PlayfieldManager>().enabled = true;
                gameObject.GetComponent<PlayfieldManager>().EndRun();

                currMenu = "MAIN MENU";
            }

            //restart
            if (GUI.Button(new Rect(Screen.width / 4, Screen.height / 11 * 4, Screen.width / 2, Screen.height / 15), "Restart", defaultStyle))
            {
                //Do something with the playfield manager here
                GetComponent<PlayfieldManager>().enabled = true;
                gameObject.GetComponent<PlayfieldManager>().EndRun();
                gameObject.GetComponent<PlayfieldManager>().StartRun();
                currMenu = "INGAME";
            }
        }

        if(currMenu == "SHOP")
        {
            //Back Button
            if (GUI.Button(new Rect(Screen.width / 10 * 9 - buf, buf, Screen.width / 10, Screen.width / 10), "", backButton))
            {
                currMenu = "MAIN MENU";
            }

            GUI.Box(new Rect(Screen.width / 10, Screen.height / 15, Screen.width - (Screen.width / 10 * 2), Screen.height / 15), "Shop Title", shopButton);

            //sliders are not user controlled. They will use the +- buttons and the slider will move when they buy something
            int shopItemsCount = 5;
            sliderStyle.horizontalSlider.fixedWidth = Screen.width / 5 * 2;
            sliderStyle.horizontalSlider.fixedHeight = Screen.height / (shopItemsCount * 2) /3;
            sliderStyle.horizontalSliderThumb.fixedWidth = Screen.height / (shopItemsCount * 2) /3;
            sliderStyle.horizontalSliderThumb.fixedHeight = Screen.height / (shopItemsCount * 2) /3;

            PlayerPrefs.SetInt("Shield Level", 5);
            //Shields slider
            float shieldsSlider = GUI.HorizontalSlider(new Rect(Screen.width / 5, (Screen.height / (shopItemsCount * 2 + 1)) * 3, sliderStyle.horizontalSlider.fixedWidth, sliderStyle.horizontalSlider.fixedHeight), PlayerPrefs.GetInt("Shield Level"), 0.0f, 10.0f);
            GUI.Label(new Rect(Screen.width / 5, (Screen.height / (shopItemsCount * 2 + 1)) * 3 - sliderStyle.horizontalSlider.fixedHeight, sliderStyle.horizontalSlider.fixedWidth, sliderStyle.horizontalSlider.fixedHeight), "Slider Value: " + shieldsSlider.ToString("F0"), defaultStyle);

            PlayerPrefs.SetInt("Max Fuel", 5);
            //Fuel Tank Slider
            float fuelSlider = GUI.HorizontalSlider(new Rect(Screen.width / 5, (Screen.height / (shopItemsCount * 2 + 1)) * 5, sliderStyle.horizontalSlider.fixedWidth, sliderStyle.horizontalSlider.fixedHeight), PlayerPrefs.GetInt("Max Fuel"), 0.0f, 10.0f);
            GUI.Label(new Rect(Screen.width / 5, (Screen.height / (shopItemsCount * 2 + 1)) * 5 - sliderStyle.horizontalSlider.fixedHeight, sliderStyle.horizontalSlider.fixedWidth, sliderStyle.horizontalSlider.fixedHeight), "Slider Value: " + fuelSlider.ToString("F0"), defaultStyle);

            //Coin Magnet Slider
            PlayerPrefs.SetInt("Coin Magnet Chance", 5);
            float magnetSlider = GUI.HorizontalSlider(new Rect(Screen.width / 5, (Screen.height / (shopItemsCount * 2 + 1)) * 7, sliderStyle.horizontalSlider.fixedWidth, sliderStyle.horizontalSlider.fixedHeight), PlayerPrefs.GetInt("Magnet Level"), 0.0f, 10.0f);
            GUI.Label(new Rect(Screen.width / 5, (Screen.height / (shopItemsCount * 2 + 1)) * 7 - sliderStyle.horizontalSlider.fixedHeight, sliderStyle.horizontalSlider.fixedWidth, sliderStyle.horizontalSlider.fixedHeight), "Slider Value: " + magnetSlider.ToString("F0"), defaultStyle);
        }

        if(currMenu == "SETTINGS")
        {
            //Back Button
            if (GUI.Button(new Rect(Screen.width / 10 * 9 - buf, buf, Screen.width / 10, Screen.width / 10), "", backButton))
            {
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
            //Back Button
            if (GUI.Button(new Rect(Screen.width / 10 * 9 - buf, buf, Screen.width / 10, Screen.width / 10), "", backButton))
            {
                currMenu = "MAIN MENU";
            }

            tempStyle.alignment = TextAnchor.MiddleCenter;
            GUI.Box(new Rect(Screen.width / 6, Screen.height / 15, Screen.width/6*4, Screen.height / 15), "LEADERBOARD", tempStyle);

            //get the leader names and score from the playerprefs memory
            List<float> highScoreList = new List<float>();
            List<string> highScoreNameList = new List<string>();
            string tempString = "High";
            for (int i = 0; i < 10; i++)
            {
                //get name here and create parallel arrays
                highScoreList.Add(PlayerPrefs.GetFloat(tempString + (i + 1).ToString()));
                highScoreNameList.Add(PlayerPrefs.GetString(tempString + (i + 1).ToString()));
            }

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
            //Back Button
            if (GUI.Button(new Rect(Screen.width / 10 * 9 - buf, buf, Screen.width / 10, Screen.width / 10), "", backButton))
            {
                currMenu = "MAIN MENU";
            }


            GUI.Box(new Rect(Screen.width / 10, Screen.height / 15, Screen.width - (Screen.width / 10 * 2), Screen.height / 15), "How-To Title", defaultStyle);
            GUI.Box(new Rect(Screen.width / 10, Screen.height / 15*3, Screen.width - (Screen.width / 10 * 2), Screen.height/15 *11), "How-To text", defaultStyle);
            //show how to play
        }

        if(currMenu == "CREDITS")
        {
            //Back Button
            if (GUI.Button(new Rect(Screen.width / 10 * 9 - buf, buf, Screen.width / 10, Screen.width / 10), "", backButton))
            {
                currMenu = "MAIN MENU";
            }

            //Show Creater info
            GUI.Box(new Rect(Screen.width / 5, Screen.height / 9, Screen.width / 5 * 3, Screen.height / 9 * 7), "Credit Text HERE", creditStyle);
        }
    }
}