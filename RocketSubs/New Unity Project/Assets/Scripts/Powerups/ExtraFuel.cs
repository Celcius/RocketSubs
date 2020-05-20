using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmoaebaUtils;

public class ExtraFuel : Powerup
{
    [SerializeField]
    private FloatVar fuel; 
    
    [SerializeField]
    private SubmarineStats stats; 
    protected override void OnCapture()
    {
        fuel.Value = stats.MaxFuel;
    }
}
