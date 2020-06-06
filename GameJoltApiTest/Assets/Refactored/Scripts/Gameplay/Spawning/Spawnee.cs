using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmoaebaUtils;

public enum SpawnedDirection {
    LEFT,
    RIGHT,
}

public delegate void OnSpawneeDestruction(Spawnee spawnee);

public abstract class Spawnee : MonoBehaviour
{    
    [SerializeField]
    private SpawnedDirection dir = SpawnedDirection.RIGHT;

    [SerializeField]
    private SpawneeArrayVar spawnees;

    private event OnSpawneeDestruction OnDestructionCallback;
    
    public virtual void OnSpawn(SpawnedDirection spawnDir, OnSpawneeDestruction destructionCall)
    {
        OnDestructionCallback += destructionCall;
        
        if(spawnees != null)
        {
            spawnees.Remove(this);
            spawnees.Add(this);
        }
    }

    public virtual void Unspawn()
    {
        Destroy(this.gameObject);
    }

    protected void OnDestroy()
    {
        if(spawnees != null)
        {
            spawnees.Add(this);
        }
        OnDestructionCallback?.Invoke(this);
    }
}
