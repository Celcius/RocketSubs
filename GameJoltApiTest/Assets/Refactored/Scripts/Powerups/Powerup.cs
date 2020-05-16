using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : MonoBehaviour
{
    protected abstract void OnCapture();

    [SerializeField]
    private bool destroyOnCollision = true;
    
    private bool caught = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(caught || other.gameObject.tag != "Player")
        {
            return;
        }

        OnCapture();

        if(destroyOnCollision)
        {
            Destroy(gameObject);
        }
    }
}
