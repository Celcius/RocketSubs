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
    private BoolVar movingForward;

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
        movingForward.OnChange += OnMovingForwardChange;
    }

    public void OnDestroy()
    {
        usingFuel.OnChange -= OnFuelChange;
        onSea.OnChange -= OnSeaChange;
        movingForward.OnChange -= OnMovingForwardChange;
    }

    private void OnSeaChange(bool oldVal, bool newVal)
    {
        UpdateRepresentation();
    }

    private void OnFuelChange(bool oldVal, bool newVal)
    {
        UpdateRepresentation();
        
    }

    private void OnMovingForwardChange(bool oldVal, bool newVal)
    {
        UpdateRepresentation();
    }

    private void UpdateRepresentation()
    {
        frameAnimator.StartPlaying((usingFuel.Value || onSea.Value) && movingForward.Value? usingFuelAnimation : normalAnimation);
    }
}

