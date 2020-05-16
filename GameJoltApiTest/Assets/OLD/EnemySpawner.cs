using UnityEngine;
using System.Collections;


public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    Transform[] waves;

    [SerializeField]
    float xMod = -65;
    [SerializeField]
    Transform basePosition;

    [SerializeField]
    Rigidbody sub;

    [SerializeField]
    float baseTimeToSpawn = 8.0f;

    [SerializeField]
    float modTimeToSpawn = 6.0f;

    [SerializeField]
    float rangeVarMin = -10.0f;
    [SerializeField]
    float rangeVarMax = 13.5f;

    float rate=0;

    float timeToSpawn = 2.0f;
    float spawnTimer = 8.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (sub == null)
            return;
        rate += Time.deltaTime / 50;
        timeToSpawn = baseTimeToSpawn - Mathf.Clamp(modTimeToSpawn * sub.velocity.magnitude / 50.0f + Mathf.Clamp(rate, 0.0f, baseTimeToSpawn - 1.0f), 0, baseTimeToSpawn - 1.0f);
   
        spawnTimer += Time.deltaTime;
           if(spawnTimer > timeToSpawn)
           {
               spawnTimer = 0.0f;
               Transform wave = Instantiate(waves[0]);
               wave.position = basePosition.transform.position;
               wave.position = new Vector3(transform.position.x + xMod, wave.position.y + Random.Range(rangeVarMin, rangeVarMax), wave.position.z);
           }
	
	}
}
