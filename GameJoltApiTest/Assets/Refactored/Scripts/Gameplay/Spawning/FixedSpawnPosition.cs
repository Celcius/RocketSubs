using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedSpawnPosition : SpawnPosition
{
    public override Vector3 GetNextSpawnPosition(bool isStartSpawn)
    {
        return new Vector3(transform.position.x, transform.position.y, 0);
    }
}
