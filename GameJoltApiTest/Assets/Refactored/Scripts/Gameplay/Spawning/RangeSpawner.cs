using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmoaebaUtils;

[RequireComponent(typeof(BoxCollider2D))]
public class RangeSpawner : Spawner
{
    public enum RangeType
    {
        Box,
        Oval
    }

    [SerializeField]
    private RangeType type;

    private Vector2 computedRange;
    private Vector2 center;

    private void Awake()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        computedRange = collider.bounds.extents;
        center = collider.bounds.center;
    }

    public override Vector3 GetNextSpawnPosition()
    {
        switch (type)
        {
            case RangeType.Box:
                return center + GeometryUtils.RandomPointInRectangle(computedRange);
            case RangeType.Oval:
                return center + GeometryUtils.RandomPointInRectangle(computedRange);
        }
        return GeometryUtils.RandomPointInRectangle(computedRange);
    }
}
