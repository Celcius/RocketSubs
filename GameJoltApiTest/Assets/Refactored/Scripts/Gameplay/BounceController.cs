using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  AmoaebaUtils
{

public class BounceController : MonoBehaviour
{
    [SerializeField]
    private VoidEvent bounceEvent;

    [SerializeField]
    private BoolVar onSea;

    [SerializeField]
    private IntVar skipCount;

    [SerializeField]
    private IntVar maxSkipCount;

    [SerializeField]
    private Transform skipEffectsPrefab;

    [SerializeField]
    private Vector2Var submarinePos;

    private void Awake()
    {
        skipCount.Value = 0;
        maxSkipCount.Value = 0;
        bounceEvent.OnEvent -= OnBounce;
        onSea.OnChange -= OnSeaChange;

        bounceEvent.OnEvent += OnBounce;
        onSea.OnChange += OnSeaChange;
    }

    private void OnDestroy()
    {
        bounceEvent.OnEvent -= OnBounce;
        onSea.OnChange -= OnSeaChange;
    }

    private void OnBounce()
    {
        skipCount.Value++;
        int newMax = Mathf.Max(maxSkipCount.Value, skipCount.Value);
        bool isNewRecord = (newMax != maxSkipCount.Value);
        maxSkipCount.Value = newMax;
        
        Instantiate(skipEffectsPrefab,submarinePos.Value,Quaternion.identity);

        Debug.Log("Bounce: " + skipCount.Value + " record:" + isNewRecord);
    }

    private void OnSeaChange(bool wasOnSea, bool isOnSea)
    {
        if(isOnSea)
        {
            skipCount.Value = 0;
        }
    }
}
}
