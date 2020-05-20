using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField]
    private SpawnerDefinition definition;

    [SerializeField]
    private SpawnedDirection overrideDir;

    private List<Spawnee> spawnees = new List<Spawnee>();

    public abstract Vector3 GetNextSpawnPosition();

    private void Spawn()
    {
        if(definition == null || definition.SpawneePrefab == null)
        {
            Debug.LogError("Cannot spawn from null at " + this.name);
            return;
        }
        Vector3 position = GetNextSpawnPosition();
        Spawnee directionable = GameObject.Instantiate<Spawnee>(definition.SpawneePrefab, position, Quaternion.identity);
        directionable.OnSpawn(this.overrideDir, OnSpawneeDeath);
        spawnees.Remove(directionable);
    }

    private void OnSpawneeDeath(Spawnee spawnee)
    {
        spawnees.Remove(spawnee);
    }

    private void UnspawnAll()
    {
        foreach(Spawnee spawnee in spawnees)
        {
            spawnee.Unspawn();
        }
        spawnees.Clear();
    }

}
