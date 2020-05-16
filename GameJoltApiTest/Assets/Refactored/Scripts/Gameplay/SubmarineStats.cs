using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineStats : ScriptableObject
{

    public float RotateAccelInc = 10.0f;
    public float AngularDrag = 20.0f;
    public float MaxRotateAccelSpeed = 20.0f;
    public float MaxRotateSpeed = 1.0f;
    public float TimeToFire = 0.2f;
    public float MaxHealth = 3.0f;
    public float MaxFuel = 0.25f;
    public float FuelConsumptionTime = 5.0f;
    public float FuelRecoveryTime = 10.0f;

    public float MaxAirSpeed = 75.0f;
    public float MaxSeaSpeed = 30.0f;
    public float AirGravity = 0.1f;
    public float SeaGravity = 0.1f;
    public float AirImpulse = 100.0f;
    public float SeaImpulse = 50.0f;
    public float SeaDrag = 0.0f;
    public float AirDrag = 20.0f;

    public float BounceSpeed = 25.0f;
    public float BounceAngle = 60.0f;
    public float MaxBounceAngle = 90;

    public float BounceHorizontalLoss = 0.75f;
    public float BounceVerticalLoss = 0.9f;
}
