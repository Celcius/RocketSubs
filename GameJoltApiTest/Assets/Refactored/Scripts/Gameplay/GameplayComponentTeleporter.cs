using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmoaebaUtils;

public class GameplayComponentTeleporter : MonoBehaviour
{
    [SerializeField]
    private TerrainDefinitionVar terrainVar; 

    [SerializeField]
    private TransformVar submarine;

    [SerializeField]
    private Vector2Var submarinePos;

    [SerializeField]
    SpawneeArrayVar spawnees;

    private void Awake()
    {
        terrainVar.OnChange += TerrainChanged;
        submarinePos.OnChange += OnSubmarinePosChanged;
    }

    private void OnDestroy()
    {
        terrainVar.OnChange -= TerrainChanged;
        submarinePos.OnChange -= OnSubmarinePosChanged;
    }

    private void TerrainChanged(TerrainDefinition oldTerrain, TerrainDefinition newTerrain)
    {
        if(newTerrain == null)
        {
            return;
        }

        UpdatePositions();
    }
    
    private void OnSubmarinePosChanged(Vector2 oldPos, Vector2 newPos)
    {
        UpdatePositions();
    }

    private void UpdatePositions()
    {
        Vector2 posChange = Vector2.zero;
        if(submarine.Value != null)
        {
            posChange = ClampPos(submarine.Value.transform);  
        }

        foreach (Spawnee spawnee in spawnees.Value)
        {
            if(spawnee != null)
            {
                spawnee.transform.position += (Vector3)posChange;
            }
        }
    }

    private Vector2 ClampPos(Transform t)
    {
        if(terrainVar.Value == null)
        {
           return Vector2.zero;
        }

        Vector2 newPos = t.position;

        float leftAnchor = terrainVar.Value.MinX;
        float rightAnchor = terrainVar.Value.MaxX;
        float endPos = transform.position.x;
        Vector2 retVec = Vector2.zero;

        if(newPos.x < leftAnchor)
        {
            float offset = Mathf.Min((leftAnchor - newPos.x),0);
            endPos =  rightAnchor + offset;
            retVec = Vector2.right * (endPos - t.position.x);
            TeleportTo(t, endPos);
        } 
        else if(newPos.x >  rightAnchor)
        {
            float offset =  Mathf.Max((rightAnchor - newPos.x),0);
            endPos =  leftAnchor + offset;
            retVec = Vector2.right * (endPos - t.position.x);
            TeleportTo(t, endPos);
        }

        return retVec;
    }

    private void TeleportTo(Transform toTeleport, float xPos)
    {
        toTeleport.position = new Vector3(xPos,
                                         toTeleport.position.y, 
                                         toTeleport.position.z);
    }
}
