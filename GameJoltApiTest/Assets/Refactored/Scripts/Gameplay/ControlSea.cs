using UnityEngine;
using System.Collections;

public class ControlSea : MonoBehaviour {

    [SerializeField]
    SubmarineController sub;

    [SerializeField]
    Transform sea1;

    [SerializeField]
    Transform sea2;

    [SerializeField]
    Transform wave1;

    [SerializeField]
    Transform wave2;

    float widthSea;
    float widthWaves;
    int index = 0;

	// Use this for initialization
	void Start () {
        widthSea = sea2.gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        widthWaves = wave2.gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
	}


	// Update is called once per frame
	void Update () {
        if (sub == null)
        {
            return;
        }
        Transform currentSea = index == 0? sea1 : sea2;
        Transform currentWater = index == 0? wave1 : wave2;

        Transform otherSea = index != 0? sea1 : sea2;
        Transform otherWater = index != 0? wave1 : wave2;

        if ((sub.transform.position.x < currentSea.position.x && otherSea.position.x > currentSea.position.x)
            || (sub.transform.position.x > currentSea.position.x && otherSea.position.x < currentSea.position.x))
       {
           float mod = otherSea.position.x > currentSea.position.x ? -1 : 1;
           otherSea.position = new Vector3(currentSea.position.x + mod * widthSea, otherSea.position.y, otherSea.position.z);
           otherWater.position = new Vector3(currentWater.position.x + mod * widthWaves- mod*1.183f, otherWater.position.y, otherWater.position.z);
       }

       if(sub.transform.position.x < currentSea.position.x - widthSea/2 ||
           sub.transform.position.x > currentSea.position.x + widthSea/2)
       {
           index = (index + 1) % 2;
       }
	}

}
