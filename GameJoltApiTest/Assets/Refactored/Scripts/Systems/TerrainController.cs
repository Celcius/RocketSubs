using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmoaebaUtils;

public class TerrainController : MonoBehaviour
{
    [SerializeField]
    private TerrainDefinitionVar terrainVar;

    private Transform[] instantiatedTerrains;

    private void Awake() 
    {
        OnTerrainChange(null, terrainVar.Value);
        terrainVar.OnChange += OnTerrainChange;
    }

    private void OnDestroy() 
    {
        terrainVar.OnChange -= OnTerrainChange;
    }

    private void OnTerrainChange(TerrainDefinition oldVar, TerrainDefinition newVar)
    {
        if(newVar != null)
        {
            InstantiateTerrain(newVar);
        }
    }

    private void InstantiateTerrain(TerrainDefinition newVar)
    {

        List<Transform> newTerrains = new List<Transform>();
        
        Transform[] minTerrains = InstantiateTerrains(newVar.EdgeTransforms, -newVar.MaxX + newVar.MinX -newVar.transform.position.x, newVar.transform.position.y);
        Transform[] maxTerrains = InstantiateTerrains(newVar.EdgeTransforms, newVar.MaxX - newVar.MinX -newVar.transform.position.x, newVar.transform.position.y);

        int order = newVar.GetOrderInLayer();

        TerrainDefinition.SetOrderInLayer(order+1, minTerrains);
        TerrainDefinition.SetOrderInLayer(order-1, maxTerrains);

        newTerrains.AddRange(minTerrains);
        newTerrains.AddRange(maxTerrains);
    }

    private Transform[] InstantiateTerrains(Transform[] transforms, float xOffset, float yOffset)
    {
        Transform[] newTerrains = new Transform[transforms.Length];
        
        for(int i = 0; i < transforms.Length; i++)
        {
            Transform t = transforms[i];
            Transform newT = Instantiate<Transform>(t, new Vector3(xOffset, yOffset + t.position.y,0),Quaternion.identity);
            newTerrains[i] = newT;
        }

        return newTerrains;
    }

    private void DestroyTerrains()
    {
        foreach(Transform t in instantiatedTerrains)
        {
            Destroy(t.gameObject);
        }
    }
    
}
