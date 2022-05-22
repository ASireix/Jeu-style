using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class AbilityManager : ScriptableObject
{
    public float maxEnergy = 100;

    public float energy = 100;

    [System.NonSerialized]
    public UnityEvent<string,float> energyChangeEvent;

    [System.NonSerialized]
    public UnityEvent<string, float> cdOneChangeEvent;

    [System.NonSerialized]
    public UnityEvent<string, float> cdTwoChangeEvent;

    public float regenSpeed;

    [System.NonSerialized]
    public float currentRegenSpeed;

    AbilityHolder abilityOne;
    AbilityHolder abilityTwo;

    public void SetAbilities(AbilityHolder ab1, AbilityHolder ab2)
    {
        abilityOne = ab1;
        abilityTwo = ab2;
    }
    private void OnEnable()
    {
        energy = maxEnergy;
        if (energyChangeEvent == null)
        {
            energyChangeEvent = new UnityEvent<string,float>();
        }

        if (cdOneChangeEvent == null)
        {
            cdOneChangeEvent = new UnityEvent<string, float>();
        }

        if (cdTwoChangeEvent == null)
        {
            cdTwoChangeEvent = new UnityEvent<string, float>();
        }

        currentRegenSpeed = regenSpeed;
    }

    public void ResetEverything()
    {
        energy = maxEnergy;
        currentRegenSpeed = regenSpeed;
    }

    public void DecreaseEnergy(string col, int amount)
    {
        energy -= amount;
        energyChangeEvent.Invoke(col,(float)energy/maxEnergy);

    }

    public void IncreaseEnergy(string col, float amount)
    {
        energy += amount;
        energyChangeEvent.Invoke(col, (float)energy / maxEnergy);
    }

    
    public void UpdateCDOne(string col,float cd)
    {
        cdOneChangeEvent.Invoke(col,cd);
    }

    public void UpdateCDTwo(string col,float cd)
    {
        cdTwoChangeEvent.Invoke(col,cd);
    }
}
