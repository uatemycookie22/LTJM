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
        mainMenuBG.Play();
        mainMenuBG.loop = true;
        inGameBG.loop = true;
    }

    public void playAudioOnce(AudioSource audible)
    {
        if (!audible.isPlaying)
            audible.Play();
    }
}
