using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AmountSpawnStrategy : SpawnStrategy
{
    [SerializeField]
    private bool respawnOnDeath = true;
    
    private bool isSpawning = false;

    public override void OnSpawnStart(Spawner spawner)
    {
        spawned = 0;
        isSpawning = true;
        spawned = 0;
        int startCount = Mathf.Max(startSpawns.Evaluate(0), maxSpawns.Evaluate(0));
        SpawnMultiple(spawner, startCount);
        Application.quitting += () => OnSpawnStop(spawner);
    }

    public override void OnSpawnStop(Spawner spawner)
    {
        isSpawning = false;
    }

    public override void OnSpawnDeath(Spawnee spawnee, Spawner spawner)
    {
        base.OnSpawnDeath(spawnee, spawner);
        if(isSpawning && respawnOnDeath)
        {
            base.Spawn(spawner);
        }
    }

}
