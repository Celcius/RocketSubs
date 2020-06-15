using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCurrentSubmarine : MonoBehaviour
{
    [SerializeField]
    private Transform startingPos;
   
   [SerializeField]
    private SubmarineDefinitionVar currentSubmarine;
    void Awake()
    {
        Vector3 position = startingPos == null? Vector3.zero : startingPos.position;
        Quaternion rotation = startingPos == null? Quaternion.identity : startingPos.rotation;
        
        Instantiate(currentSubmarine.Value.Prefab, 
                    position, 
                    rotation);
    }
}
