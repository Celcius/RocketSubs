using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FauxSub : MonoBehaviour {

    [SerializeField]
    Sprite _normalSub;
    [SerializeField]
    Sprite _rocketSub;

    Rigidbody body;
    public bool onSea = false;
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

    float maxHealth = 5.0f;
    float health = 5.0f;

    float loseControlTimer = 0.0f;

    Vector3 storedForward;

    public float maxFuel = 0.5f;
    float fuel = 0.5f;

    float hangTime = 0.0f;
    bool decel = false;
    SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {

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
        if (transform.position.y < -23.5f)
            transform.position = new Vector3(transform.position.x, 40.0f, transform.position.z);
	}

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("COLLISION");
        if (col.gameObject.tag == "EnemyMissile")
        {
            health -= 1;
            float healthScale = Mathf.Ceil(((int)health/maxHealth*10.0f)/10.0f);
            healthBar.localScale = new Vector3(((int)(Mathf.Clamp(health / maxHealth, 0, 1.0f) * 10.0f)) / 10.0f, healthBar.localScale.y, healthBar.localScale.z);
            Destroy(col.gameObject);
            if(health <=0)
            {
                Application.LoadLevel(0);
            }
        }

    }
    public void enemyImpulse()
    {
        if(!onSea)
            body.AddForce(storedForward * 10, ForceMode.Impulse);
        else
            body.AddForce(storedForward * 100, ForceMode.Impulse);

        if(onSea)
            loseControlTimer = 0.3f;
        else
            loseControlTimer = 0.1f;

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
      
             spriteRenderer.sprite = _normalSub;


         lastSplashSpawned += Time.deltaTime;
        if(transform.position.y >= 0.1f && transform.position.y < 10.0f && Input.GetKey(KeyCode.UpArrow))
        {
            showSplash();
        }

        Mathf.Ceil(((int)health / maxHealth * 10.0f) / 10.0f);
      
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
            fuel = Mathf.Clamp(fuel + Time.deltaTime * 5, 0, maxFuel);
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

           
                decel = false;
        }

        if(speed != Vector3.zero)
        { 
          body.MovePosition(transform.position+speed);
        }

        if(transform.position.y >= 39)
            body.AddForce(new Vector3(0,-1,0)*40.0f, ForceMode.Impulse);


        if (body.velocity.magnitude > 50)
            body.velocity = body.velocity.normalized * 50;

        updateFire(forward);

        storedForward = forward;
    }

    void updateRotation()
    {


        if (rotateSpeed != 0)
        {
            if (onSea)
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
