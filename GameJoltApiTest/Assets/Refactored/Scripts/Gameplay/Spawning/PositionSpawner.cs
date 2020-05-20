using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSpawner : Spawner
{

    public override Vector3 GetNextSpawnPosition()
    {
        return new Vector3(transform.position.x, transform.position.y, 0);
    }
}
