using UnityEngine;
using System.Collections;

public class SplashAnimate : MonoBehaviour {

    [SerializeField]
    Sprite[] sprites;
    SpriteRenderer renderer;
    float elapseTime = 0;
    int index = 0;
	// Use this for initialization
	void Start () {
	    renderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        elapseTime += Time.deltaTime;
	    if(elapseTime > 0.5f)
        {
            elapseTime = 0.0f;
            index++;
            if (index < sprites.Length)
                renderer.sprite = sprites[index];
            else
                Destroy(gameObject);
        }
            
	}
}
