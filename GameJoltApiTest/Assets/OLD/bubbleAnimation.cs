using UnityEngine;
using System.Collections;

public class bubbleAnimation : MonoBehaviour {

    float targetScale = 0.7f;

	// Use this for initialization
    Rigidbody body;
	void Start () {
        body = GetComponent<Rigidbody>();

	}


	
	// Update is called once per frame

    
	void Update () {

	    if(transform.localScale.x > 0)
        {
            float lerpedVal = Mathf.Lerp(transform.localScale.x,targetScale,Time.deltaTime / (targetScale == 0.7f ? 2 : 5));
            transform.localScale = new Vector3(lerpedVal, lerpedVal, lerpedVal);
            if (transform.localScale.x < 0.75f)
                targetScale = 0.0f;
            if (transform.localScale.x < 0.05f)
                Destroy(gameObject);
        }

        body.AddForce(Vector3.up * Time.deltaTime, ForceMode.Impulse);
        if (transform.position.y >= -0.65f)
            Destroy(gameObject);
        /*
        
        if (transform.position.y >= -0.65f || (transform.position.y <= -13.8f && body.velocity.y < 0))
            Destroy(gameObject);
        */

    }
}
