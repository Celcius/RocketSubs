using UnityEngine;
using System.Collections;
using AmoaebaUtils;

public class SeaController : MonoBehaviour {

    [SerializeField]
    private BoolEvent onSeaDetected;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            onSeaDetected.Invoke(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            onSeaDetected.Invoke(false);
        }
    }
}
