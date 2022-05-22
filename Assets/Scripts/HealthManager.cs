using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class HealthManager : ScriptableObject
{
    public float maxHealth;
    public float health;

    [System.NonSerialized]
    public UnityEvent<string, float> healthChangeEvent;
}
