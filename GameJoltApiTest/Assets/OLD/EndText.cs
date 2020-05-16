using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndText : MonoBehaviour {

    [SerializeField]
    Text endText;

    [SerializeField]
    Text endText2;

    [SerializeField]
    blink scoreRecord;

    [SerializeField]
    blink hangRecord;

    float _score;
    float _maxScore;
    float _hang;
    float _maxHang;
    float showingScore;
    float showingHang;

	// Use this for initialization
	void Start () {
       

	}
	
    public void setValues(float score, float maxScore, float hang, float maxHang)
    {
        _score = score;
        _maxScore = maxScore;
        _hang = hang;
        _maxHang = maxHang;

        if (_maxScore > _score)
            scoreRecord.hideText();

        if (_maxHang >_hang )
            hangRecord.hideText();
    }
	// Update is called once per frame
	void Update () {



        endText.text = "SCORE: " + _score.ToString("F0") + "\n" +
            "ALL TIME: " + _maxScore.ToString("F0");
        endText2.text = "MAX HANG: " + _hang.ToString("F0") + "s" + "\n" +
            "ALL TIME: " + _maxHang.ToString("F0") + "s";
	}
}
