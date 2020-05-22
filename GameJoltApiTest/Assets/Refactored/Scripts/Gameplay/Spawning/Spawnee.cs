using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnedDirection {
    LEFT,
    RIGHT,
}

public delegate void OnSpawneeDestruction(Spawnee spawnee);

public abstract class Spawnee : MonoBehaviour
{    
    [SerializeField]
    private SpawnedDirection dir = SpawnedDirection.RIGHT;

    private event OnSpawneeDestruction OnDestructionCallback;
    
    public virtual void OnSpawn(SpawnedDirection spawnDir, OnSpawneeDestruction destructionCall)
    {
        OnDestructionCallback += destructionCall;
    }

    public virtual void Unspawn()
    {
        Destroy(this.gameObject);
    }

    protected void OnDestroy()
    {
        OnDestructionCallback?.Invoke(this);
    }
}
