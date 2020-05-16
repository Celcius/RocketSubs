using UnityEngine;
using System.Collections;

public class FollowSub : MonoBehaviour {

    [SerializeField]
    Transform _submarine;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(_submarine != null)
        {
            transform.position = new Vector3(_submarine.position.x, transform.position.y, transform.position.z);
        }
	}
}
