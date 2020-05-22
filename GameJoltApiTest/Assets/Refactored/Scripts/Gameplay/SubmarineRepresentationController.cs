using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using AmoaebaUtils;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AnimateFrames))]
public class SubmarineRepresentationController : MonoBehaviour
{
    [SerializeField]
    private BoolVar usingFuel;

    [SerializeField]
    private BoolVar onSea;

    [SerializeField]
    private Sprite[] usingFuelAnimation;

    [SerializeField]
    private Sprite[] normalAnimation;

    private SpriteRenderer renderer;
    private AnimateFrames frameAnimator;

    public void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        frameAnimator = GetComponent<AnimateFrames>();
        Assert.IsNotNull(usingFuel, "Using Fuel not assigned to " + this.name);

        usingFuel.OnChange += OnFuelChange;
        onSea.OnChange += OnSeaChange;
    }

    public void OnDestroy()
    {
        usingFuel.OnChange -= OnFuelChange;
        onSea.OnChange -= OnSeaChange;
    }

    private void OnSeaChange(bool oldVal, bool newVal)
    {
        if(oldVal == newVal)
        {
            return;
        }

        OnFuelChange(false,newVal? true : usingFuel.Value);
    }

    private void OnFuelChange(bool oldVal, bool newVal)
    {
        if(oldVal == newVal || onSea.Value)
        {
            return;
        }

        frameAnimator.StartPlaying(newVal? usingFuelAnimation : normalAnimation);
    }
}

