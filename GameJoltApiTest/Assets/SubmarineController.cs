using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SubmarineController : MonoBehaviour {

    [SerializeField]
    Sprite _normalSub;
    [SerializeField]
    Sprite _rocketSub;

    [SerializeField]
    Sprite _normalPain;

    [SerializeField]
    Sprite _rocketPain;

    Rigidbody body;
    public bool onSea = true;
    [SerializeField]
    float rotateAccelInc = 10.0f;
    float rotateAccel = 0.0f;
    [SerializeField]
    float airGravity = 0.1f;
    [SerializeField]
    float drag = 20.0f;
    float rotateSpeed = 0.0f;
    [SerializeField]
    float maxRotateAccelSpeed = 20.0f;
    [SerializeField]
    float maxRotateSpeed = 1.0f;
    [SerializeField]
    Transform waterExp;
    [SerializeField]
    Transform exp;

    bool animatePain = false;

    float minSpeed = 30.0f;
    float maxSpeed = 100.0f;
    float baseSpeedMult = 30.0f;
    float speedMult = 30.0f;
    Vector3 speed = Vector3.zero;
    Vector3 speedCopy = Vector3.zero;
    float timeSinceFire = 0.0f;
    [SerializeField]
    Transform missile;
    [SerializeField]
    Transform splash;
    [SerializeField]
    Transform bubble;
    float timeToFire = 0.2f;

    float lastSplashSpawned;

    [SerializeField]
    Transform healthBar;

    float maxHealth = 3.0f;
    float health = 3.0f;

    float loseControlTimer = 0.0f;

    Vector3 storedForward;

    float invincibility = 0.0f;


    [SerializeField]
    Transform fuelPanel;

    public float maxFuel = 0.5f;
    float fuel = 0.25f;

    float hangTime = 0.0f;
    bool decel = false;
    SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
        ScoreManager.Instance.sub = this;
        onSea = true;
        body = GetComponent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
    public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        Vector3 dir = point - pivot; // get point direction relative to pivot
        dir = Quaternion.Euler(angles) * dir; // rotate it
        point = dir + pivot; // calculate rotated point
        return point; // return it
    }

	// Update is called once per frame
	void Update () {
        if(invincibility >0)
        {
            invincibility -= Time.deltaTime;
            if (invincibility <= 0)
            { 
                invincibility = 0;
                animatePain = false;
            }
        }

        if(ScoreManager.Instance.gameOver)
        {
            if (!onSea)
                Instantiate(exp).transform.position = transform.position;
            else
                Instantiate(waterExp).transform.position = transform.position;
            Destroy(gameObject);    
        }

        if (loseControlTimer > 0)
        {
            loseControlTimer -= Time.deltaTime;
            if (loseControlTimer <= 0)
                loseControlTimer = 0;
        }
        updateSea();
        updateRotation();
        updateSpeed();
        updateLooks();
	}

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("COLLISION");
        if (col.gameObject.tag == "EnemyMissile")
        {
            Destroy(col.gameObject);
            if (invincibility <= 0.0f)
            {
                animatePain = true;
                invincibility = 0.1f;
                ScoreManager.Instance.playHit();
                health -= 1;
                float healthScale = Mathf.Ceil(((int)health/maxHealth*10.0f)/10.0f);
                healthBar.localScale = new Vector3(((int)(Mathf.Clamp(health / maxHealth, 0, 1.0f) * 10.0f)) / 10.0f, healthBar.localScale.y, healthBar.localScale.z);
              
                if(health <=0)
                {
                    ScoreManager.Instance.gameIsOver();
                }
            }
        }

    }
    public void enemyImpulse()
    {
        if(!onSea)
            body.AddForce(storedForward * 10, ForceMode.Impulse);
      //  else
      //      body.AddForce(storedForward * 100, ForceMode.Impulse);

        if(onSea)
            loseControlTimer = 0.0f;
        else
            loseControlTimer = 0.2f;

    }
    void updateFire(Vector3 forward)
    {
        timeSinceFire += Time.deltaTime;
        if (timeSinceFire > timeToFire && loseControlTimer == 0)
        {
                if (Input.GetKey(KeyCode.Z))
                {
                    Vector3 position = transform.position + forward * 3;
                    timeSinceFire = 0.0f;
                    if (position.y < 0 && position.y > -14.4f)
                    {
                        timeToFire = 0.5f;
                        bubbleAnimation bubbleInstance = Instantiate(bubble).GetComponent<bubbleAnimation>();
                        bubbleInstance.GetComponent<Rigidbody>().AddForce(forward * (30 + Mathf.Abs(body.velocity.magnitude)), ForceMode.Impulse);
                        bubbleInstance.transform.position = new Vector3(position.x, position.y, bubbleInstance.transform.position.z);
                        bubbleInstance.transform.rotation = transform.rotation;
                    }
                    else if(position.y > 0)
                    {
                        timeToFire = 0.2f;
                        Missile missileInstance = Instantiate(missile).GetComponent<Missile>();
                        missileInstance.move = forward*(50+Mathf.Abs(body.velocity.magnitude));
                        missileInstance.transform.position = new Vector3(position.x, position.y, missileInstance.transform.position.z);
                        missileInstance.transform.rotation = transform.rotation;
                    }
                }
            
        }
    }

    void updateLooks()
    {
        if (Input.GetKey(KeyCode.UpArrow) && fuel > 0 && loseControlTimer == 0)
         {
            if(animatePain)
            {
                spriteRenderer.sprite = _rocketPain;
            }
            else
                spriteRenderer.sprite = _rocketSub;
         }
         else
         {
             if (animatePain)
             {
                 spriteRenderer.sprite = _normalPain;
             }
             else
                 spriteRenderer.sprite = _normalSub;
         }

         lastSplashSpawned += Time.deltaTime;
        if(transform.position.y >= 0.1f && transform.position.y < 10.0f && Input.GetKey(KeyCode.UpArrow))
        {
            showSplash();
        }

        Mathf.Ceil(((int)health / maxHealth * 10.0f) / 10.0f);
        float oldScale = fuelPanel.localScale.x;
         fuelPanel.localScale = new Vector3(((int)(Mathf.Clamp(fuel / maxFuel, 0, 1.0f) * 10.0f)) / 10.0f, fuelPanel.localScale.y, fuelPanel.localScale.z);
         if (oldScale != fuelPanel.localScale.x && oldScale < fuelPanel.localScale.x)
             ScoreManager.Instance.playRegen();
    }

    void showSplash()
    {
          if(lastSplashSpawned > 0.1f )
            {
                lastSplashSpawned = 0;
                Transform splashInst = Instantiate(splash);
                splashInst.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
              if(body.velocity.x < 0)
                  splashInst.localScale = new Vector3(-splash.localScale.x, splash.localScale.y, splash.localScale.z);
            }
    }
    void updateSea()
    {
       
        if(onSea && transform.position.y > 0)
        {
            onSea = false;
            Debug.Log(body.velocity.magnitude);
            if(body.velocity.magnitude > 7)
            {
                showSplash();
            }
        }
        else if(!onSea && transform.position.y < 0)
        {
            hangTime = 0;
            onSea = true;
            Debug.Log(body.velocity.magnitude);
            if (body.velocity.magnitude > 7)
            {
                showSplash();
            }
        }

        if (!onSea)
            hangTime += Time.deltaTime;
        else
        { 
            float tempFuel = ((int)fuel *10)/10;
            fuel = Mathf.Clamp(fuel + Time.deltaTime * 5, 0, maxFuel);
        
        }
    }

    public float getHangTime()
    {
        return hangTime;
    }

    public void addFuel(float add)
    {
        fuel = Mathf.Clamp(fuel + add, 0, maxFuel);
    }

    void updateSpeed()
    {
        
        Vector3 forward = RotatePointAroundPivot(new Vector3(-1, 0, 0), Vector3.zero, transform.localRotation.eulerAngles);
        if(onSea)
        {
            body.drag = 10.0f;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(new Vector3(0, 0, 0));
                speed = forward * speedMult * Time.deltaTime;
                speedCopy = speed;
                body.velocity = Vector3.zero;
                body.useGravity = false;
            }
            else
            {
                float waterDrag = 0.5f;
                speed -= speed * Time.deltaTime;
                body.useGravity = true;
            }
            decel = false;
        }
        else
        {
            body.drag = 0;
            body.useGravity = true;

            if (Input.GetKey(KeyCode.UpArrow) )
            {

                if (fuel > 0)
                {
                    if(!decel)
                    {
                        body.AddForce(forward * Time.deltaTime * 500, ForceMode.Impulse);
                        decel = true;
                    }
                    body.AddForce(forward * Time.deltaTime*50, ForceMode.Impulse);
                    fuel = Mathf.Clamp(fuel -= Time.deltaTime*5, 0, maxFuel);
                    ScoreManager.Instance.playBoost();
                }
                Debug.Log("Air Impulse");

            }
            else
                decel = false;
        }

        if(speed != Vector3.zero)
        { 
          body.MovePosition(transform.position+speed);
        }

        if(transform.position.y >= 39)
            body.AddForce(new Vector3(0,-1,0)*20.0f, ForceMode.Impulse);


        if (body.velocity.magnitude > 50)
            body.velocity = body.velocity.normalized * 50;

        updateFire(forward);

        storedForward = forward;
    }

    void updateRotation()
    {

        if (Input.GetKey(KeyCode.LeftArrow) && loseControlTimer == 0)
        {
            body.angularVelocity = Vector3.zero;
            if(rotateSpeed < 0)
                rotateSpeed = 0.0f;
            if (rotateAccel < 0)
                rotateAccel = 0;
            rotateAccel += rotateAccelInc * Time.deltaTime;
            rotateAccel = rotateAccel > maxRotateAccelSpeed ? maxRotateAccelSpeed : rotateAccel;
            rotateSpeed += rotateAccel;
        }
        else
            if (Input.GetKey(KeyCode.RightArrow) && loseControlTimer == 0)
            {
                body.angularVelocity = Vector3.zero;
                if (rotateSpeed > 0)
                    rotateSpeed = 0.0f;
                if (rotateAccel > 0)
                    rotateAccel = 0;
                rotateAccel -= rotateAccelInc * Time.deltaTime;
                rotateAccel = rotateAccel < -maxRotateAccelSpeed ? -maxRotateAccelSpeed : rotateAccel;
                rotateSpeed += rotateAccel;
            }
            else if (rotateSpeed != 0)
            {
                if(onSea)
                {
                    rotateSpeed = 0;
                    rotateAccel = 0;
                }
                else
                { 
                float sign = Mathf.Sign(rotateSpeed);
                rotateAccel += -Mathf.Sign(rotateSpeed) * drag * Time.deltaTime;
                rotateSpeed += rotateAccel;
                if (Mathf.Abs(rotateSpeed) <= 0.5 || Mathf.Sign(rotateSpeed) != sign)
                {
                    rotateSpeed = 0;
                    rotateAccel = 0;
                }
                }
            }
        rotateSpeed = Mathf.Clamp(rotateSpeed, -maxRotateSpeed, maxRotateSpeed);

        body.MoveRotation(body.rotation*Quaternion.Euler(new Vector3(0.0f, 0.0f, 1.0f) * rotateSpeed));
        
    }
    
}
