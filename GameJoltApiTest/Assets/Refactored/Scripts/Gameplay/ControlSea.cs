using UnityEngine;
using System.Collections;

public class ControlSea : MonoBehaviour {

    [SerializeField]
    SubmarineController sub;

    [SerializeField]
    Transform wave1;

    [SerializeField]
    Transform wave2;

    float widthWaves;
    int index = 0;

	// Use this for initialization
	void Start () {
        widthWaves = wave2.gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
	}


	// Update is called once per frame
	void Update () {
        if (sub == null)
        {
            return;
        }
        Transform currentWater = index == 0? wave1 : wave2;
        Transform otherWater = index != 0? wave1 : wave2;

        if ((sub.transform.position.x < currentWater.position.x && otherWater.position.x > currentWater.position.x)
            || (sub.transform.position.x > currentWater.position.x && otherWater.position.x < currentWater.position.x))
       {
           float mod = otherWater.position.x > currentWater.position.x ? -1 : 1;
           
           otherWater.position = new Vector3(currentWater.position.x + mod * widthWaves- mod*1.183f, otherWater.position.y, otherWater.position.z);
       }

       if(sub.transform.position.x < currentWater.position.x - widthWaves/2 ||
           sub.transform.position.x > currentWater.position.x + widthWaves/2)
       {
           index = (index + 1) % 2;
       }
	}

}
