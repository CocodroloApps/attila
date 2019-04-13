using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ToGameScene()
    {
        SceneManager.LoadScene("Attila");
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
