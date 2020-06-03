using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmoaebaUtils;

public class HeightController : MonoBehaviour
{
    [SerializeField]
    private BoolVar onSea;
    
    [SerializeField]
    private Vector2Var submarinePos;
    [SerializeField]
    private FloatVar maxHeight;

    private void Awake()
    {
        submarinePos.OnChange += OnPosChanged;
    }

    private void OnDestroy()
    {
        submarinePos.OnChange -= OnPosChanged;
    }

    private void OnPosChanged(Vector2 oldValue, Vector2 newValue)
    {
        maxHeight.Value = Mathf.Max(maxHeight.Value, newValue.y);
    }
}
