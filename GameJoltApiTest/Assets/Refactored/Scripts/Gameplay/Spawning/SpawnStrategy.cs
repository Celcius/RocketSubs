using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmoaebaUtils;

public abstract class SpawnStrategy : MonoBehaviour
{
    [SerializeField]
    protected BooledInt maxSpawns = new BooledInt(false,0);

    [SerializeField]
    protected BooledInt startSpawns = new BooledInt(false,0);

    protected int spawned = 0;

    public virtual void OnSpawnStart(Spawner spawner)
    {
        spawned = 0;
        int startCount = Mathf.Min(startSpawns.Evaluate(0), maxSpawns.Evaluate(int.MaxValue));
        SpawnMultiple(spawner, startCount);
    }

    protected void SpawnMultiple(Spawner spawner, int amount)
    {
        for(int i = 0; i < amount; i++)
        {  
            Spawn(spawner);
        }
    }

    protected virtual void Spawn(Spawner spawner)
    {
        if(spawned < maxSpawns.Evaluate(int.MaxValue))
        {
            spawner.Spawn();
        }
    }

    public abstract void OnSpawnStop(Spawner spawner);

    public virtual void OnSpawn(Spawnee spawnee, Spawner spawner) 
    {
        spawned++;
    }

    public virtual void OnSpawnDeath(Spawnee spawnee, Spawner spawner)
    {
        spawned = Mathf.Max(--spawned,0);
    }

}
