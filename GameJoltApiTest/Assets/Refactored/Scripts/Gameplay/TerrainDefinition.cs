using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TerrainDefinition : MonoBehaviour
{
    public float MinX;
    public float MaxX;

    public float Length => Mathf.Abs(MinX) + Mathf.Abs(MaxX);

    public Transform[] ShapeTransforms;
    public Transform[] EdgeTransforms;

#if UNITY_EDITOR
    public void UpdateDefinition()
    {
        MinX = float.MaxValue;
        MaxX = float.MinValue;

        Vector2 anchor = new Vector2(float.MaxValue, float.MinValue);
        List<Transform> shapes = new List<Transform>();
        List<Transform> edges = new List<Transform>();
        SpriteShapeController[] spriteShapes = GetComponentsInChildren<SpriteShapeController>();
        
        foreach (SpriteShapeController shape in spriteShapes)
        {
            int pointCount = shape.spline.GetPointCount();
            for(int i = 0; i < pointCount; i++)
            {
                Vector3 point = shape.spline.GetPosition(i);

                MinX = Mathf.Min(MinX, point.x);
                MaxX= Mathf.Max(MaxX, point.x);
            }

            shapes.Add(shape.transform);
            edges.Add(shape.transform); // Redundant for now, may optimize in the future
        }

        ShapeTransforms = shapes.ToArray();
        EdgeTransforms = edges.ToArray();
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(TerrainDefinition))]
public class TerrainDefinitionEditor : Editor
{
    private TerrainDefinition definition = null;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        if (GUILayout.Button("Update"))
        {
            UpdateDefinition();
        }
    }

    private void OnEnable() 
    {
        definition = (TerrainDefinition)target;
        UpdateDefinition();
    }
    
    private void OnDisable() 
    {
        UpdateDefinition();
    }

    private void UpdateDefinition()
    {
        if(definition == null 
           || Application.isPlaying 
           || EditorApplication.isPlayingOrWillChangePlaymode)
        {
            return;
        }

        definition.UpdateDefinition();
        AssetDatabase.SaveAssets();
    }
}
#endif