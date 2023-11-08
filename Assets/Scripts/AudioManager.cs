using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource mainMenuBG;      //generic looping music played during the main menus
    public AudioSource inGameBG;        //generic looping music played during game

    public AudioSource genericClick;    //generic sound when any button is pressed
    public AudioSource purchasedItem;   //cash or coin related sound
    public AudioSource insufiscentFunds;//buzzing noise?
    public AudioSource pause;           //some kind of pause audio
    public AudioSource resume;          //some kind of resume audio

    //In Game
    public AudioSource hitAsteroid;     //crash sound
    public AudioSource hitCoin;         //*coin being dropped into a bag* sound
    public AudioSource hitMultiplier;   //hitting a temporary coin multiplier - coin related sound
    public AudioSource hitFuel;         //hitting a fuel tank power up
    public AudioSource gameOver;        //"game over" or related audio

    public void Start()
    {
        //if the volume has never been set before, then set it to 1 in playerPrefs
        if (PlayerPrefs.HasKey("Volume") == false)
            PlayerPrefs.SetFloat("Volume", 1);


        //start playing some background audio and make sure the backgrounds are set to loop
        if (mainMenuBG != null)
        {
            //set the volume to whatever is stored in PlayerPrefs
            changeVolume(PlayerPrefs.GetFloat("Volume"));

            PlayLoop(mainMenuBG);
        }
    }

    public void Stop(AudioSource audible)
    {
        if (audible != null)
        {
            audible.Stop();
        }
        else
            Debug.LogWarning("Be sure to set all audio sources. Some are still null.");
    }

    public void PlayLoop(AudioSource audible)
    {
        if (audible != null)
        {
            audible.loop = true;
            audible.Play();                
        }
        else
            Debug.LogWarning("Be sure to set all audio sources. Some are still null.");
    }

    public void playAudioOnce(AudioSource audible)
    {
        if (audible != null)
        {
            if (!audible.isPlaying)
                audible.Play();
        } else
            Debug.LogWarning("Be sure to set all audio sources. Some are still null.");
    }

    public void changeVolume(float newVolume)
    {
        mainMenuBG.volume = newVolume/2;
        inGameBG.volume = newVolume/2;

        genericClick.volume = newVolume;
        purchasedItem.volume = newVolume;
        insufiscentFunds.volume = newVolume;
        pause.volume = newVolume;
        resume.volume = newVolume;

        hitAsteroid.volume = newVolume;
        hitCoin.volume = newVolume;
        hitMultiplier.volume = newVolume;
        hitFuel.volume = newVolume;
        gameOver.volume = newVolume;
    }
}
