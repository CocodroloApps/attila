using UnityEngine;
using UnityEngine.UI;

public class AppVersion : MonoBehaviour {

    public Text TextVersion; 

	// Use this for initialization
	void Start ()
    {
        TextVersion.text = "Version: " + Application.version;
    }
}
