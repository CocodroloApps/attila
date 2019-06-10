using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAttila : MonoBehaviour
{
    public AudioClip effect1;
    public AudioClip effect2;
    public AudioClip effect3;
    public AudioClip effect4;
    public AudioClip move1;
    public AudioClip move2;
    public AudioClip move3;
    public AudioClip move4;
    public AudioClip move5;
    public AudioClip move6;
    public AudioClip sceneEffect;
    public AudioClip battleEffect;
    public AudioClip victoryEffect;
    public AudioSource audioSource;

    public void ClickEffect()
    {
        if (GlobalInfo.soundPlay == true)
        {
            audioSource.volume = 0.8f;
            int nAux = Mathf.RoundToInt(UnityEngine.Random.Range(1f, 4f));
            if (nAux == 1)
            {
                audioSource.PlayOneShot(effect1);
            }
            if (nAux == 2)
            {
                audioSource.PlayOneShot(effect2);
            }
            if (nAux == 3)
            {
                audioSource.PlayOneShot(effect3);
            }
            if (nAux == 4)
            {
                audioSource.PlayOneShot(effect4);
            }
        }
    }

    public void MoveEffect()
    {
        if (GlobalInfo.soundPlay == true)
        {
            audioSource.volume = 0.8f;
            int nAux = Mathf.RoundToInt(UnityEngine.Random.Range(1f, 6f));
            if (nAux == 1)
            {
                audioSource.PlayOneShot(move1);
            }
            if (nAux == 2)
            {
                audioSource.PlayOneShot(move2);
            }
            if (nAux == 3)
            {
                audioSource.PlayOneShot(move3);
            }
            if (nAux == 4)
            {
                audioSource.PlayOneShot(move4);
            }
            if (nAux == 5)
            {
                audioSource.PlayOneShot(move5);
            }
            if (nAux == 6)
            {
                audioSource.PlayOneShot(move6);
            }
        }
    }

    public void SceneEffect()
    {
        if (GlobalInfo.soundPlay == true)
        {
            audioSource.volume = 0.8f;
            audioSource.PlayOneShot(sceneEffect);
        }
    }

    public void BattleEffect()
    {
        if (GlobalInfo.soundPlay == true)
        {
            audioSource.volume = 0.8f;
            audioSource.PlayOneShot(battleEffect);
        }
    }

    public void VictoryEffect()
    {
        if (GlobalInfo.soundPlay == true)
        {
            audioSource.volume = 0.8f;
            audioSource.PlayOneShot(victoryEffect);
        }
    }
}
