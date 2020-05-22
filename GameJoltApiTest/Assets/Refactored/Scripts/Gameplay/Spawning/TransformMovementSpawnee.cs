using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using AmoaebaUtils;

public class TransformMovementSpawnee : Spawnee
{
    public override void OnSpawn(SpawnedDirection spawnDir, OnSpawneeDestruction destructionCall)
    {
        base.OnSpawn(spawnDir, destructionCall);
    
        TransformMovement movement = GetComponent<TransformMovement>();
        Assert.IsNotNull(movement, "No movement assigned to Transform MovementSpawnee " + this.name);
        movement.AxisMultipliers = new Vector3(GetMultiplierForDirection(spawnDir),1,1);
    }

    private float GetMultiplierForDirection(SpawnedDirection spawnDir)
    {
        switch (spawnDir)
        {
            case SpawnedDirection.LEFT:
                return -1.0f;
            case SpawnedDirection.RIGHT:
                return 1.0f;    
        }
        return 1.0f;
    }
}
