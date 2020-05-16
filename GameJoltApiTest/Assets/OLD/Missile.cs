using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {
    [SerializeField]
    Transform splash;
    Camera main;
    public Vector3 move = Vector3.zero;
	// Use this for initialization

    Rigidbody body;

    SubmarineController sub;
	void Start () {
        main = Camera.main;
        body = GetComponent<Rigidbody>();
        sub = ScoreManager.Instance.sub;
	}
	
	// Update is called once per frame
	void Update () {
        if (sub == null)
            Destroy(gameObject);
        body.MovePosition(transform.position + move*Time.deltaTime);


        if (transform.position.x > main.transform.position.x + 130 || transform.position.x < main.transform.position.x - 130 || transform.position.y > 150)
        {
            Destroy(gameObject);
        }

        if(transform.position.y <=0)
        {
            // waterxplosion
            
            Transform splashInstance = Instantiate(splash);
            splashInstance.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
            Debug.Log(transform.rotation);
            if (transform.rotation.z < 0)
                splashInstance.localScale = new Vector3(-splash.localScale.x, splash.localScale.y, splash.localScale.z);
            Destroy(gameObject);
            

        }
	}
}
