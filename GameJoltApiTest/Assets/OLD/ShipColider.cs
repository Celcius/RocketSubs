using UnityEngine;
using System.Collections;

public class ShipColider : MonoBehaviour {

    [SerializeField]
    Vector3 force;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {

            col.gameObject.GetComponent<Rigidbody>().AddForce(force*30, ForceMode.Impulse);
        }
    }

}
