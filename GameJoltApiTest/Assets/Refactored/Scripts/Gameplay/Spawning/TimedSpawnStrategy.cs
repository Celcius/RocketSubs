using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmoaebaUtils;
public class TimedSpawnStrategy : SpawnStrategy
{
    [SerializeField]
    private float timeToFirstSpawn;

    [SerializeField]
    private float timeBetweenSpawns;

    [SerializeField]
    private float randomTimeBetweenSpawns;


    private IEnumerator timedSpawn = null;


    public override void OnSpawnStart(Spawner spawner)
    {
        OnSpawnStop(spawner);
        
        base.OnSpawnStart(spawner);

        timedSpawn = SpawnRoutine(spawner);
        StartCoroutine(timedSpawn);        
    }

    public override void OnSpawnStop(Spawner spawner)
    {
        spawned = 0;
        if(timedSpawn != null)
        {
            StopCoroutine(timedSpawn);
        }
        timedSpawn = null;
    }

    private IEnumerator SpawnRoutine(Spawner spawner)
    {
        
        if(timeToFirstSpawn > 0)
        {
            yield return new WaitForSeconds(timeToFirstSpawn);
        }
        
        while(true)
        {
            Spawn(spawner);

            yield return new WaitForSeconds(timeBetweenSpawns + Random.Range(0, randomTimeBetweenSpawns));
        }
    }
}
