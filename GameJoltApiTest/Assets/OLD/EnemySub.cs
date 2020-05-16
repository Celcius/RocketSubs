using UnityEngine;
using System.Collections;

public class EnemySub : MonoBehaviour {
    [SerializeField]
    Sprite[] sprites;
    [SerializeField]
    Vector3 move;

    [SerializeField]
    SubmarineController sub;

    [SerializeField]
    Transform bullet;

    [SerializeField]
    Transform explosion;

    [SerializeField]
    int points = 1000;

    float timeToFire = 10.0f;

    bool rush = false;
    Rigidbody body;
    Camera main;

    SpriteRenderer renderer;
    int index = 0;
    float updateTime = 0.0f;
	// Use this for initialization
	void Start () {
        renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = sprites[0];
        main = Camera.main;
        sub = ScoreManager.Instance.sub;
        body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        if (sub == null)
        {
            Destroy(gameObject);
            
            Vector3 expPos = transform.position;
            expPos.z = -0.5f;
            Transform exp = Instantiate(explosion);
            exp.position = expPos;
        }

       if (timeToFire == 0.0f)
            timeToFire -= Random.Range(-0.5f, 0.5f);
        updateTime += Time.deltaTime;
        timeToFire += Time.deltaTime;

        bool canSee = true;// (move.x < 0 && (sub.transform.position.x + 5) < transform.position.x)
            //|| (move.x > 0 && (sub.transform.position.x - 5) > transform.position.x);

        if (canSee && timeToFire > 5.0f+ Random.Range(-0.2f,0.2f))
        {
            Debug.Log("FIRE");
            timeToFire = 0.0f;
           /* SubMissile bulletInstance = Instantiate(bullet).GetComponent<SubMissile>();

            bulletInstance.transform.position = new Vector3(transform.position.x, transform.position.y, bulletInstance.transform.position.z) + move;
            bulletInstance.transform.localScale = new Vector3( bulletInstance.transform.localScale.x, bulletInstance.transform.localScale.y, bulletInstance.transform.localScale.z);
            Vector3 relativePos = sub.transform.position - bulletInstance.transform.position;
            relativePos.x = 0;
            relativePos.y = 0;
            bulletInstance.transform.Rotate(new Vector3(0, 0, 1), 90);*/

            SubMissile bulletInstance = Instantiate(bullet).GetComponent<SubMissile>();
            bulletInstance.transform.position = new Vector3(transform.position.x, transform.position.y, bulletInstance.transform.position.z) + move;
          
            bulletInstance.move = move* 1200.0f * Time.deltaTime;

            if(move.x > 0)
                bulletInstance.transform.Rotate(new Vector3(0, 0, 1), 180);
            
        }
        if((!rush && updateTime > 2.0f) || (rush && updateTime > 0.5f))
        {
            if (!rush)
            {
                updateTime = 0.0f;
                renderer.sprite = sprites[1];
                rush = true;
            }
            else
            {
                
                renderer.sprite = sprites[0];
                rush = false;
            }
        }
        if (rush)
            body.AddForce(move * Time.deltaTime * Random.Range(10, 20), ForceMode.Impulse);
        body.velocity = move * Mathf.Clamp(body.velocity.magnitude, 0.0f, 5.0f);


        if (transform.position.x > main.transform.position.x + 130 || transform.position.x < main.transform.position.x - 130)
        {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {

            Vector3 impulse = col.transform.position-transform.position;
            impulse = impulse.normalized;
            impulse.y = 1.0f;
            ScoreManager.Instance.addScore(points);
            SubmarineController sub = col.gameObject.GetComponent<SubmarineController>();
            //sub.addFuel(sub.maxFuel/10);
            sub.enemyImpulse();
            Destroy(gameObject);
            // make Explosion

            Vector3 expPos = transform.position;
            expPos.z = -0.5f;
            Transform exp = Instantiate(explosion);
            exp.position = expPos;
        } 
        else if(col.gameObject.tag == "Missile")
        {
            Destroy(col.gameObject);
            ScoreManager.Instance.addScore(points/2);
            Destroy(gameObject);

            Vector3 expPos = transform.position;
            expPos.z = -0.5f;
            Transform exp = Instantiate(explosion);
            exp.position = expPos;

        }
        Debug.Log("Collision" + col.gameObject);
    }
}
