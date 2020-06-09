using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmoaebaUtils;

[RequireComponent(typeof(BoxCollider2D))]
public class RangeSpawnPosition : FixedSpawnPosition
{
    public enum RangeType
    {
        Box,
        BoxOutOfScreen,
        Oval
    }
    
    [SerializeField]
    private RangeType type;

    private Vector2 computedRange;
    private Vector2 center;

    [SerializeField]
    private CameraVar mainCamera;

    private bool hasComputedRange = false;

    private Vector2 INVALID_POS = new Vector2(float.MinValue, float.MaxValue);

    private void ComputeRanges()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        computedRange = collider.bounds.extents;
        center = collider.bounds.center;
        hasComputedRange = true;
    }

    public override Vector3 GetNextSpawnPosition(bool isStartSpawn)
    {
        if(!hasComputedRange)
        {
            ComputeRanges();
        }
        switch (type)
        {
            case RangeType.Box:
                return GetBoxPos();
            case RangeType.Oval:
                return center + GeometryUtils.RandomPointInOval(computedRange);
            case RangeType.BoxOutOfScreen:
                return isStartSpawn? GetBoxPos() : GetOutOfBoxPos();
        }
        return GeometryUtils.RandomPointInRectangle(computedRange);
    }

    private Vector2 GetBoxPos()
    {
        return center + GeometryUtils.RandomPointInRectangle(computedRange);
    }
    
    private Vector2 GetOutOfBoxPos()
    {
        bool found = false;
        Vector2 ret = GeometryUtils.PointOutsideInvalidRange(
                new Bounds(center, computedRange*2), 
                new Bounds((Vector2)mainCamera.Value.transform.position,
                UnityEngineUtils.WorldOrthographicSize(mainCamera.Value)),
                out found);
        return found? ret : INVALID_POS;
    }
}
