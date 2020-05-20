using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonController : MonoBehaviour {

    [SerializeField]
    GameObject credits;
    [SerializeField]
    GameObject instructions;
    [SerializeField]
    Button creditButton;
    [SerializeField]
    Button instructionsButton;
	// Use this for initialization
	void Start () {
        instructions.SetActive(false);
        credits.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void showInstructions()
    {
        if (!credits.activeInHierarchy)
            instructions.SetActive(true);
        else
            instructionsButton.OnDeselect(null);
    }

    public void hideInstructions()
    {
        instructions.SetActive(false);
    }


    public void showCredits()
    {
        if (!credits.activeInHierarchy)
            credits.SetActive(true);
        else
            creditButton.OnDeselect(null);
    }

    public void hideCredits()
    {
        credits.SetActive(false);
    }

}
