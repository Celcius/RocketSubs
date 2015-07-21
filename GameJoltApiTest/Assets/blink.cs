using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class blink : MonoBehaviour {

    bool hide = false;
    bool showing = true;
    Text text;
    float elapsedTime = 0.0f;
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
    public void hideText()
    {
        hide = true;
        if(text!= null)
           text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
    }
	// Update is called once per frame
	void Update () {
        if (hide)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
            return;
        }

        elapsedTime += Time.deltaTime;
	    if(showing && elapsedTime > 1.0f)
        {
            showing = false;
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
            elapsedTime = 0;
        }
        else if(!showing && elapsedTime > 0.5f)
        {
            showing = true;
            text.color = new Color(text.color.r, text.color.g, text.color.b, 255);
            elapsedTime = 0;
            ScoreManager.Instance.playRegen();
        }
	}
}
