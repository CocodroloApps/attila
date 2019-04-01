using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBar : MonoBehaviour {

    private bool loadScene = false;
    public string loadingSceneName;
    public float waitSecondsPrev;
    public float waitSecondsPost;
    public Slider sliderBar;
    
    // Use this for initialization
	void Start ()
    {
        sliderBar.gameObject.SetActive(false);
        Invoke("Loading", waitSecondsPrev);
    }
	
	// Update is called once per frame
	void Loading()
    {
	    if(loadScene == false)
        {
            loadScene = true;
            sliderBar.gameObject.SetActive(true);
            StartCoroutine("LoadNewScene");
        }
	}

    IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(waitSecondsPost);
        AsyncOperation async = SceneManager.LoadSceneAsync(loadingSceneName);

        while (!async.isDone)
        {
            float progress = Mathf.Clamp01(async.progress / 0.9f);
            sliderBar.value = progress;
            yield return null;
        }
    }
}
