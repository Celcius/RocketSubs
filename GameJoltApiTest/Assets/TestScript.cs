using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Screen.SetResolution(800, 600, Screen.fullScreen);
        GameJolt.UI.Manager.Instance.ShowSignIn((bool success) => {
            if (success)
                successSignIn(); 
            else 
                failureSignIn();
        });
       
     //  GameJolt.UI.Manager.Instance.ShowLeaderboards();
	}

    void OnApplicationQuit()
    {
        PlayerPrefs.Save();
        if(GameJolt.API.Manager.Instance.CurrentUser != null)
        {
            GameJolt.API.Manager.Instance.CurrentUser.SignOut();
            Debug.Log("Signing Out");
        }
    }

    void successSignIn()
    {
        Application.LoadLevel(1);
    }

    void failureSignIn()
    {

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
