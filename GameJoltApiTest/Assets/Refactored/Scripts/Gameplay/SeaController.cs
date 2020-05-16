using UnityEngine;
using System.Collections;


public class SeaController : MonoBehaviour {

    [SerializeField]
    private BoolVar onSea;

    void OnTriggerEnter2D(Collider2D other)
    {
        onSea.Value = true;
        Debug.Log("onSea");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        onSea.Value = false;
        Debug.Log("onAir");
    }
}
