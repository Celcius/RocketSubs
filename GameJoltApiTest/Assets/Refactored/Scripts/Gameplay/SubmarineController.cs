using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SubmarineController : MonoBehaviour 
{
    [SerializeField]
    private SubmarineStats stats;
    
    [SerializeField]
    private BoolVar onSea;
    
    [SerializeField]
    private FloatVar fuel;

    [SerializeField]
    private BoolVar inputForward;
    
    [SerializeField]
    private BoolVar inputLeft;
    
    [SerializeField]
    private BoolVar inputRight;
    
    private Rigidbody2D body;
    public bool wasOnAir = false;
    private Vector2 speed = Vector3.zero;
    private float timeSinceFire = 0.0f;

    private float lastSplashSpawned;
    
    private float loseControlTimer = 0.0f;

    private Vector3 storedForward;

    private float invincibility = 0.0f;
    private float health = 3.0f;
    private float hangTime = 0.0f;
    private bool decel = false;
    private float rotateAccel = 0.0f;

    private float rotateSpeed = 0.0f;

	void Start () {
        //ScoreManager.Instance.sub = this;
        onSea.Value = true;
        fuel.Value = stats.MaxFuel;

        body = GetComponent<Rigidbody2D>();
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

        /*if(ScoreManager.Instance.gameOver)
        {
            if (!onSea)
                Instantiate(exp).transform.position = transform.position;
            else
                Instantiate(waterExp).transform.position = transform.position;
            Destroy(gameObject);    
        }*/

        if (loseControlTimer > 0)
        {
            loseControlTimer -= Time.deltaTime;
            if (loseControlTimer <= 0)
                loseControlTimer = 0;
        }
        UpdateSea();
        UpdateRotation();
        updateSpeed();
        updateLooks();
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "EnemyMissile")
        {
            Destroy(col.gameObject);
            if (invincibility <= 0.0f)
            {
                StartCoroutine(InvincibilityRoutine());
                //ScoreManager.Instance.playHit();
                health -= 1;
                float healthScale = Mathf.Ceil(((int)health/stats.MaxHealth*10.0f)/10.0f);
                //healthBar.localScale = new Vector3(((int)(Mathf.Clamp(health / stats.MaxHealth, 0, 1.0f) * 10.0f)) / 10.0f, healthBar.localScale.y, healthBar.localScale.z);
              
                if(health <=0)
                {
                   // ScoreManager.Instance.gameIsOver();
                }
            }
        }

    }

    private IEnumerator InvincibilityRoutine()
    {
        invincibility = 0.1f;
        while(invincibility > 0)
        { 
            invincibility -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        //animatePain = false;
    }
    public void enemyImpulse()
    {
        if(!onSea.Value)
            body.AddForce(storedForward * 10, ForceMode2D.Impulse);
      //  else
      //      body.AddForce(storedForward * 100, ForceMode.Impulse);

        if(onSea.Value)
            loseControlTimer = 0.0f;
        else
            loseControlTimer = 0.2f;

    }
    void updateFire(Vector3 forward)
    {
        /*timeSinceFire += Time.deltaTime;
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
            
        }*/
    }

    void updateLooks()
    {
        /*
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
         //if (oldScale != fuelPanel.localScale.x && oldScale < fuelPanel.localScale.x)
             //ScoreManager.Instance.playRegen();
             */
    }

    void showSplash()
    {
        /*
          if(lastSplashSpawned > 0.1f )
            {
                lastSplashSpawned = 0;
                Transform splashInst = Instantiate(splash);
                splashInst.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
              if(body.velocity.x < 0)
                  splashInst.localScale = new Vector3(-splash.localScale.x, splash.localScale.y, splash.localScale.z);
            }
            */
    }
    void UpdateSea()
    {
       
       /*
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
        }*/
    }

    public void AddFuel(float inc)
    {
        fuel.Value = Mathf.Clamp(fuel.Value + inc, 0, stats.MaxFuel);
    }
    private void UpdateFuel(bool isMoving, bool onSeaAfterBounce)
    {
        if(onSeaAfterBounce)
        {
            fuel.Value = Mathf.Clamp(fuel.Value + Time.deltaTime * stats.MaxFuel / stats.FuelRecoveryTime, 0, stats.MaxFuel);
        }
        else if(isMoving)
        {
            fuel.Value = Mathf.Clamp(fuel.Value - Time.deltaTime*stats.MaxFuel / stats.FuelConsumptionTime, 0, stats.MaxFuel);
        }   
        //Debug.Log("Fuel at " + fuel);
    }
    
    private Vector2 GetForward()
    {
        return (Vector2)RotatePointAroundPivot(new Vector3(-1, 0, 0), Vector3.zero, transform.localRotation.eulerAngles);
    }
    private bool HandleBounce()
    {
        bool enteringWater = onSea.Value && wasOnAir;
        if(!enteringWater)
        {
            return false;
        }

        Vector2 forward = GetForward();
        float entryAngle = Vector2.Angle(-Vector2.up, forward);
        float entrySpeed = Mathf.Abs(body.velocity.x);

        bool shouldBounce =  (entryAngle >= stats.BounceAngle) && (entryAngle <= stats.MaxBounceAngle) && (entrySpeed > stats.BounceSpeed);

        Debug.Log("Entry Angle: " +  entryAngle + " Entry Speed: " + entrySpeed + " Bounce:" + shouldBounce);
        
        if(shouldBounce)
        {
            rotateSpeed = 0;
            rotateAccel = 0;
            Vector2 bounceVelocity = new Vector2(body.velocity.normalized.x * stats.BounceHorizontalLoss,
                                                -body.velocity.normalized.y * stats.BounceVerticalLoss);
            if(entryAngle < 90.0f)
            {
                body.MoveRotation(body.rotation + (90.0f-entryAngle)*2.0f*Mathf.Sign(body.velocity.normalized.x));
            }
            
            body.velocity = bounceVelocity*body.velocity.magnitude;
        }
        return shouldBounce;
    }

    private void updateSpeed()
    {
        bool bounced = HandleBounce();
        
        if(!bounced)
        {
            body.drag = onSea.Value ? stats.SeaDrag : stats.AirDrag;
        }

        bool movingForward = inputForward.Value && (!bounced && onSea.Value || fuel.Value > 0 && !onSea.Value);
        Vector2 forward = GetForward();
   
        if(movingForward)
        {
            body.velocity += (Vector2)forward * Time.deltaTime * (!bounced && onSea.Value? stats.SeaImpulse : stats.AirImpulse);
        } 
        
        UpdateFuel(movingForward, !bounced && onSea.Value);
        
        body.gravityScale = onSea.Value? stats.SeaGravity : stats.AirGravity;
        body.velocity = body.velocity.normalized * Mathf.Clamp(body.velocity.magnitude, 0, (!bounced && onSea.Value? stats.MaxSeaSpeed : stats.MaxAirSpeed));
        //Debug.Log("Moving at " + body.velocity.magnitude + " Dir: " + body.velocity.normalized);

 
        //updateFire(forward);
        wasOnAir = !onSea.Value || bounced;
        storedForward = forward;
    }

    void UpdateRotation()
    {
       if (inputLeft.Value && loseControlTimer == 0)
        {
            body.angularVelocity = 0;
            if(rotateSpeed < 0)
                rotateSpeed = 0.0f;
            if (rotateAccel < 0)
                rotateAccel = 0;
            rotateAccel += stats.RotateAccelInc * Time.deltaTime;
            rotateAccel = rotateAccel > stats.MaxRotateAccelSpeed ? stats.MaxRotateAccelSpeed : rotateAccel;
            rotateSpeed += rotateAccel;
        }
        else if (inputRight.Value && loseControlTimer == 0)
        {
            body.angularVelocity = 0;
            if (rotateSpeed > 0)
                rotateSpeed = 0.0f;
            if (rotateAccel > 0)
                rotateAccel = 0;
            rotateAccel -= stats.RotateAccelInc * Time.deltaTime;
            rotateAccel = rotateAccel < -stats.MaxRotateAccelSpeed ? -stats.MaxRotateAccelSpeed : rotateAccel;
            rotateSpeed += rotateAccel;
        }
        else if (rotateSpeed != 0)
        {
            if(onSea.Value)
            {
                rotateSpeed = 0;
                rotateAccel = 0;
            }
            else
            { 
                float sign = Mathf.Sign(rotateSpeed);
                rotateAccel += -Mathf.Sign(rotateSpeed) * stats.AngularDrag* Time.deltaTime;
                rotateSpeed += rotateAccel;
                if (Mathf.Abs(rotateSpeed) <= 0.5 || Mathf.Sign(rotateSpeed) != sign)
                {
                    rotateSpeed = 0;
                    rotateAccel = 0;
                }
            }
        }
        rotateSpeed = Mathf.Clamp(rotateSpeed, -stats.MaxRotateSpeed, stats.MaxRotateSpeed);

        body.MoveRotation(body.rotation + rotateSpeed);
        
    }
    
}
