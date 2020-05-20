using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerDefinition : ScriptableObject
{
    [SerializeField]
    private Spawnee spawneePrefab;
    public Spawnee SpawneePrefab => spawneePrefab;
}
