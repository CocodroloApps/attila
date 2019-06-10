using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioWinner : MonoBehaviour
{
    public AudioClip victoryMusic;
    public AudioSource audioSource;

    public void WinnerMusic()
    {
        if (GlobalInfo.soundPlay == true)
        {
            audioSource.volume = 0.8f;            
            audioSource.clip = victoryMusic;
            audioSource.Play();
        }
    }
}
