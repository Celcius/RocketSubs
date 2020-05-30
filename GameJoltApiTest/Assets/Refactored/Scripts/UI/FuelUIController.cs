using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelUIController : PipUIController
{
    [SerializeField]
    private FloatVar fuel;

    [SerializeField]
    private int pipAmount = 10;

    void Start()
    {
        CreateNewPips(pipAmount);
        fuel.OnChange += OnFuelChange;        
    }

    private void OnDestroy()
    {
        fuel.OnChange += OnFuelChange;   
    }

    private void OnFuelChange(float oldFuel, float newFuel)
    {
        SetVisibleAmount(newFuel);
    }
}
