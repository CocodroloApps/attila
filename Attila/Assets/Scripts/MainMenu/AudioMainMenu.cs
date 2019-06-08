using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMainMenu : MonoBehaviour
{

    public AudioClip effect1;
    public AudioClip effect2;
    public AudioClip effect3;
    public AudioClip effect4;
    public AudioClip sceneEffect;
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

    public void SceneEffect()
    {
        if (GlobalInfo.soundPlay == true)
        {
            audioSource.volume = 0.8f;
            audioSource.PlayOneShot(sceneEffect);            
        }
    }
}
