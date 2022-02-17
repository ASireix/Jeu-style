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

    public float regenSpeed;

    [System.NonSerialized]
    public float currentRegenSpeed;


    private void OnEnable()
    {
        energy = maxEnergy;
        if (energyChangeEvent == null)
        {
            energyChangeEvent = new UnityEvent<string,float>();
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

    

}
