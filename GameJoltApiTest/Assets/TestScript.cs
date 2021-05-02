using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Screen.SetResolution(800, 600, Screen.fullScreen);

       
     //  GameJolt.UI.Manager.Instance.ShowLeaderboards();
	}

    void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }

    void successSignIn()
    {
        Application.LoadLevel(1);
    }

    void failureSignIn()
    {

    }
	
	// Update is called once per frame
}
