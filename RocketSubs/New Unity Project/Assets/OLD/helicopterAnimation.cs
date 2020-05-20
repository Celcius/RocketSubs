using UnityEngine;
using System.Collections;

public class helicopterAnimation : MonoBehaviour {
    [SerializeField]
    Sprite[] sprites;
    [SerializeField]
    Vector3 move;

    [SerializeField]
    SubmarineController sub;
    [SerializeField]
    Transform bullet;

    [SerializeField]
    int points = 100;

    [SerializeField]
    Transform explosion;

    float timeToFire;

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
	}
	
	// Update is called once per frame
	void Update () {
        if(sub == null)
        {
            Destroy(gameObject);
            
            Vector3 expPos = transform.position;
            expPos.z = -0.5f;
            Transform exp = Instantiate(explosion);
            exp.position = expPos;
        }
        updateTime += Time.deltaTime;
        timeToFire += Time.deltaTime;
        if (timeToFire > 4.0f + Random.Range(-0.2f, 0.2f))
        {
            timeToFire = 0.0f;
            Missile bulletInstance = Instantiate(bullet).GetComponent<Missile>();
            Missile bulletInstance2 = Instantiate(bullet).GetComponent<Missile>();
            Missile bulletInstance3 = Instantiate(bullet).GetComponent<Missile>();

            bulletInstance.transform.position = new Vector3(transform.position.x, transform.position.y, bulletInstance.transform.position.z) + move;
            bulletInstance2.transform.position =bulletInstance.transform.position+ new Vector3(-3, 0, 0);
            bulletInstance3.transform.position = bulletInstance.transform.position + new Vector3(3, 0, 0);
            bulletInstance.move = new Vector3(0.0f, -1.0f, 0.0f) * 1000.0f * Time.deltaTime;
            bulletInstance2.move = new Vector3(-1.0f, -1.0f, 0.0f) * 1000.0f * Time.deltaTime;
            bulletInstance3.move = new Vector3(1.0f, -1.0f, 0.0f) * 1000.0f * Time.deltaTime;

            Vector3 relativePos = sub.transform.position - bulletInstance.transform.position;
            relativePos.x = 0;
            relativePos.y = 0;
            bulletInstance.transform.Rotate(new Vector3(0, 0, 1), 90);
            bulletInstance2.transform.Rotate(new Vector3(0, 0, 1), 45);
            bulletInstance3.transform.Rotate(new Vector3(0, 0, 1), 135);
            
        }
        if(updateTime > 0.3f)
        {
            index = (index + 1) % sprites.Length;
            renderer.sprite = sprites[index];
            updateTime = 0.0f;
        }
        transform.Translate(move * Time.deltaTime*Random.Range(10,20));
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
            ScoreManager.Instance.increaseMult();

            Vector3 expPos = transform.position;
            expPos.z = -0.5f;
            Transform exp = Instantiate(explosion);
            exp.position = expPos;
            // make Explosion
        } 
        else if(col.gameObject.tag == "Missile")
        {
            Destroy(col.gameObject);
            ScoreManager.Instance.addScore(points/2);
            Destroy(gameObject);
            ScoreManager.Instance.increaseMult();

            Vector3 expPos = transform.position;
            expPos.z = -0.5f;
            Transform exp = Instantiate(explosion);
            exp.position = expPos;
        }
        Debug.Log("Collision" + col.gameObject);
    }
}
