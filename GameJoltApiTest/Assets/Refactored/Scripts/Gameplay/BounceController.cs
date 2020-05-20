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

    private void Awake()
    {
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
        Debug.Log("Bounce: " + skipCount.Value);
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
