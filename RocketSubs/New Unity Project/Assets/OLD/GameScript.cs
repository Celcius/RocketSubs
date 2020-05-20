using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameScript : MonoBehaviour {

    /*
    
    float _score = 0;

    [SerializeField]
    Text _text;
	// Use this for initialization
	void Start () {
     //   new GameJolt.API.Objects.User("Celcius", "53ca14").SignIn((bool success) => {
       //     if (success)
                GameJolt.API.DataStore.Get("SCORE", false, (string value) =>
                {
                    if (value != null)
                    {
                        _score = float.Parse(value);
                    }
                });
         //});

        
	}
	
	// Update is called once per frame
	void Update () {
	    if(_text.text != ""+_score)
            _text.text = "" + _score;
	}

    public void click()
    {
        _score++;
        _text.text = "" + _score;

        if (_score >= 10)
        {
            int trophyID = 34030;
            GameJolt.API.Trophies.Get(trophyID, (GameJolt.API.Objects.Trophy trophy) =>
            {
                if (trophy != null && !trophy.Unlocked)
                {
                    GameJolt.API.Trophies.Unlock(trophyID);
                }
            });
        }

        // Saving Score
        GameJolt.API.Scores.Add((int)_score, "Score", 80762);
    }

    public void save()
    {
        GameJolt.API.DataStore.Set("SCORE", "" + _score, false, (bool success) => { if (success) saveCallBack(); });
    }

    public void saveCallBack()
    {
        Debug.Log("Saved");
    }

    public void trophies()
    {
        GameJolt.UI.Manager.Instance.ShowTrophies();
    }

    public void leaderboards()
    {
        GameJolt.UI.Manager.Instance.ShowLeaderboards();
    }

    public void rest()
    {
        Sprite sprite = Resources.LoadAssetAtPath("Assets/Resources/logo.png", typeof(Sprite)) as Sprite;
        Debug.Log(sprite);
        if(_score %2 == 0)
            GameJolt.UI.Manager.Instance.QueueNotification("You Rested Evenly",sprite) ;
        else
            GameJolt.UI.Manager.Instance.QueueNotification("You Rested Oddly");
    }*/
}
