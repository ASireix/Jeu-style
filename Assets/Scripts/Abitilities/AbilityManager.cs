using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class AbilityManager : ScriptableObject
{
    public int maxEnergy = 100;

    public int energy = 100;

    public UnityEvent<string,int> energyChangeEvent;


    private void OnEnable()
    {
        energy = maxEnergy;
        if (energyChangeEvent == null)
        {
            energyChangeEvent = new UnityEvent<string,int>();
        }
    }

    public void DecreaseEnergy(string col, int amount)
    {
        energy -= amount;
        energyChangeEvent.Invoke(col,energy);
    }
    
}
