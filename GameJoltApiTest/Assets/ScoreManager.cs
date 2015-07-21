using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour {

    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text hangText;

    AudioSource []sources;
    public bool gameOver = false;

    [SerializeField]
    Text altitudeText;

    [SerializeField]
    Color special;

    [SerializeField]
    Color normal;

    [SerializeField]
    Transform endText;

    [SerializeField]
    AudioClip[] sounds;

    float countToEnd = 3.0f;

    float timeSinceLastBoost = 0.0f;

    const int SCORE_TAG  = 80762;
    const int HANG_TAG  = 83386;
    const int ALTITUDE_TAG = 83387;

    private float highScore = 0;
    private float score = 0;

    private float hangTime = 0;
    private float maxLocalHangtime = 0;
    private float hangTimeHighScore;
        
    private float timer = 60;
    
    private static ScoreManager instance;

    bool checked9000 = false;
    bool checkedZeroG = false;

    private ScoreManager() { }

    [SerializeField]
    Text multText;
    int multiplier = 1;
    int maxMultiplier = 10;
    [SerializeField]
    public SubmarineController sub;

    

    public static ScoreManager Instance
   {
      get 
      {
         if (instance == null)
         {
             instance = new ScoreManager();
         }
         return instance;
      }
   }
	// Use this for initialization
	void Start () {
        if (instance == null)
            instance = this;

        sources = new AudioSource[sounds.Length];
        for (int i = 0; i < sounds.Length;i ++ )
            sources[i] = gameObject.AddComponent<AudioSource>();

         GameJolt.API.DataStore.Get("SCORE", false, (string value) =>
             {
                    if (value != null)
                    {
                        highScore = float.Parse(value);
                    }
                });

                GameJolt.API.DataStore.Get("HANG", false, (string value) =>
                {
                    if (value != null)
                    {
                        hangTimeHighScore = float.Parse(value);
                    }
                });
        
	}

    public void gameIsOver()
    {
        gameOver = true;

        if (maxLocalHangtime >= hangTimeHighScore)
            GameJolt.API.Scores.Add((int)maxLocalHangtime, maxLocalHangtime.ToString("F0") + " seconds", HANG_TAG);
        if(score >= highScore)
             GameJolt.API.Scores.Add((int)score, score + " points", SCORE_TAG);
    }
	
    public void addScore(float add)
    {
        score += add*multiplier;
        if(score > highScore)
        {
            highScore = score;
            
            GameJolt.API.DataStore.Set("SCORE", "" + highScore, false, (bool success) => { });
        }
    }

    public void increaseMult()
    {
        multiplier = Mathf.Clamp(multiplier+1, 1, maxMultiplier);
        multText.text = "x" + multiplier;
    }
	// Update is called once per frame
	void Update () {
        timeSinceLastBoost -= Time.deltaTime;
        if (sub == null && ! gameOver)
            return;
        if(gameOver)
        {
            if(countToEnd > 0)
            { 
               countToEnd -= Time.deltaTime;
              if(countToEnd <= 0)
             {
                 endText.gameObject.SetActive(true);
                 endText.GetComponent<EndText>().setValues(score, highScore, maxLocalHangtime, hangTimeHighScore);
             }
            }
            else if(Input.GetKey(KeyCode.R))
            {
                Application.LoadLevel(1);
            }
            return;
        }
        if(sub != null && sub.onSea)
        {
            multiplier = 1;
            multText.text = "x" + multiplier;
        }

        float prevVal = hangTime;

        hangTime = sub.getHangTime();

        if (prevVal > maxLocalHangtime)
            maxLocalHangtime = prevVal;

        if (hangTime == 0 && prevVal != 0 && prevVal > hangTimeHighScore)
        {
            hangTimeHighScore = prevVal;

            GameJolt.API.DataStore.Set("HANG","" + prevVal, false, (bool success) => { });
        }

        //Debug.Log(hangTime);
   /*     prevVal = heightScore;
        heightScore = sub.transform.position.y;
        if (prevVal > 0 && prevVal > heightHihScore)
        {
            heightHihScore = prevVal;
            GameJolt.API.Scores.Add((int)heightHihScore, "Altitude", ALTITUDE_TAG);
            Debug.Log("Altitude HighScore: " + heightHihScore + "" + prevVal);
        }
        //Debug.Log(heightScore);
    * */

     /*   timer = Mathf.Clamp(timer - Time.deltaTime, 0, timer);
        altitudeText.text = "00:" + (timer < 10.0f ? "0" :"")+ timer.ToString("F0");

        if(timer <= 0.0f)
        {
            Application.LoadLevel(1);
        }
      * */
        scoreText.text = ""+score;
       
        hangText.text = hangTime.ToString("F0") + "s";

        if (multiplier == maxMultiplier)
            multText.color = special;
        else
            multText.color = normal;

        if (score == highScore && score != 0)
            scoreText.color = special;
        else
            scoreText.color = normal;

        if(!checked9000 && score > 9000)
        {
            checked9000 = true;
            attemptUnlock(35675);
        }

        if (!checkedZeroG && hangTime > 60)
        {
            checkedZeroG = true;
            attemptUnlock(35676);
        }


        if(hangTime >= hangTimeHighScore && hangTime != 0)
            hangText.color = special;
        else
            hangText.color = normal;
	}

    void attemptUnlock(int trophyID)
    {
        GameJolt.API.Trophies.Get(trophyID, (GameJolt.API.Objects.Trophy trophy) =>
        {
            if (trophy != null && !trophy.Unlocked)
            {
                GameJolt.API.Trophies.Unlock(trophyID);
            }
        });
    }

    public void playWater()
    {
        play(0);
    }

    public void playXP()
    {
        play(1);
    }

    public void playHit()
    {
        play(2);
    }

    public void playShoot()
    {
        play(3);
    }

    public void playEnemyShoot()
    {
        play(4);
    }

    public void playRegen()
    {
        play(5);
    }


    public void playBoost()
    {
        if(timeSinceLastBoost<=0)
        { 
          //  play(6);
            timeSinceLastBoost = 0.29f;
        }
        
    }


    void play(int index)
    {
        sources[index].clip = sounds[index];
        sources[index].Play();
    }

}
