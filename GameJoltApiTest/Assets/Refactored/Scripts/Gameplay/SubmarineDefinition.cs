using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineDefinition : ScriptableObject
{
    [SerializeField]
    private string name;
    public string Name => name;

    [SerializeField]
    private SubmarineController prefab;

    public SubmarineController Prefab => prefab;

    [SerializeField]
    private Sprite representationUI;
    public Sprite RepresentationUI => representationUI;

    [SerializeField]
    private SubmarineStats stats;
    public SubmarineStats Stats => stats;

}
