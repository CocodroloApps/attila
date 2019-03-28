using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeScene : MonoBehaviour {

    public string sceneName;
    public float waitSeconds;
    public Image black;
    public Animator anim;

	// Use this for initialization
	void Start ()
    {
        if (sceneName!="")
        {
            StartCoroutine("ExitToNewScene");
        } else
        {
            StartCoroutine("EnterToNewScene");
        }
    }

    IEnumerator ExitToNewScene()
    {
        yield return new WaitForSeconds(waitSeconds);
        anim.SetTrigger("FadeIN");
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator EnterToNewScene()
    {
        yield return new WaitForSeconds(waitSeconds);
        anim.SetTrigger("FadeOUT");
        yield return new WaitUntil(() => black.color.a == 0);
    }
}
