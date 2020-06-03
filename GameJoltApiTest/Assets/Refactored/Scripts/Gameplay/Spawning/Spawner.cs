using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Spawnee spawneePrefab;
    public Spawnee SpawneePrefab => spawneePrefab;

    [SerializeField]
    private SpawnStrategy spawnStrategy;
    [SerializeField]
    private SpawnPosition positionDefinition;

    [SerializeField]
    private SpawnedDirection overrideDir;

    private List<Spawnee> spawnees = new List<Spawnee>();
    public int LiveSpawnees => spawnees.Count;

    private bool hasSpawnStart = false;

    private void Awake() 
    {
         StartSpawn();
    }

    private void OnDestroy()
    {
        UnspawnAll();
        StopSpawn();
    }

    public void Spawn()
    {
        if(spawneePrefab == null || spawnStrategy == null)
        {
            Debug.LogError("Cannot spawn from null at " + this.name);
            return;
        }
        Vector3 position = positionDefinition.GetNextSpawnPosition(!hasSpawnStart);
        
        Spawnee directionable = GameObject.Instantiate<Spawnee>(spawneePrefab, position, Quaternion.identity);
        directionable.OnSpawn(this.overrideDir, OnSpawneeDeath);
        spawnees.Add(directionable);

        spawnStrategy.OnSpawn(directionable, this);
    }

    private void OnSpawneeDeath(Spawnee spawnee)
    {
        spawnees.Remove(spawnee);

        spawnStrategy.OnSpawnDeath(spawnee, this);
    }

    private void StartSpawn()
    {
        hasSpawnStart = false;
        spawnStrategy.OnSpawnStart(this);
        hasSpawnStart = true;
    }

    private void StopSpawn(bool unspawn = true)
    {
        spawnStrategy.OnSpawnStop(this);
        if(unspawn)
        {
            UnspawnAll();
        }
    }

    private void UnspawnAll()
    {
        foreach(Spawnee spawnee in spawnees)
        {
            spawnee.Unspawn();
        }
        spawnees.Clear();
        hasSpawnStart = false;
    }

}
