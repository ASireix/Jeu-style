using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class CharacterStat : ScriptableObject
{
    [Header("Description")]
    public string Name;
    public string Desc;

    [Header("Health")]
    public float Health;
    
    public float Damage_Reduction_Percentage;

    [Header("Magic")]
    public float Energy;

    public float EnergyRegenSpeed;

    [Header("Mobility")]
    public float Speed;
}
