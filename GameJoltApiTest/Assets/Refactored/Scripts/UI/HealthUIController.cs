using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : PipUIController
{
    [SerializeField]
    private FloatVar health;
    
    [SerializeField]
    private FloatVar maxHealth;

    private void Start()
    {
        CreateNewPips((int)Mathf.Ceil(maxHealth.Value));
        health.OnChange += OnHealthChange;
        maxHealth.OnChange += OnHealthChange;
    }

    private void OnDestroy()
    {
        health.OnChange -= OnHealthChange;
        maxHealth.OnChange -= OnHealthChange;     
    }

    private void OnHealthChange(float oldHealth, float newHealth)
    {
        SetVisibleAmount(newHealth/maxHealth.Value);
    }

}
