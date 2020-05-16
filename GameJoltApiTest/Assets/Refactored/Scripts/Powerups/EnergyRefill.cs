using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyRefill : Powerup
{
    [SerializeField]
    private FloatVar energy; 
    
    [SerializeField]
    private SubmarineStats stats; 

   protected override void OnCapture()
    {
        energy.Value = 1.0f;
    }
}
