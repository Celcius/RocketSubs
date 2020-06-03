using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnPosition : MonoBehaviour
{
    public abstract Vector3 GetNextSpawnPosition(bool isStartSpawn);
}
