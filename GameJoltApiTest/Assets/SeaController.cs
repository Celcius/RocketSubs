using UnityEngine;
using System.Collections;


public class SeaController : MonoBehaviour {

    [SerializeField]
    SubmarineController _sub;

    void OnTriggerEnter(Collider other)
    {
        _sub.onSea = true;
        Debug.Log("onSea");
    }

    void OnTriggerExit(Collider other)
    {
        _sub.onSea = false;
        Debug.Log("onAir");
    }
}
