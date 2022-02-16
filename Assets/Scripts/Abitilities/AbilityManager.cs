using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class AbilityManager : ScriptableObject
{
    public float maxEnergy = 100;

    public float energy = 100;

    public UnityEvent<string,float> energyChangeEvent;

    public float regenSpeed;
    public float currentRegenSpeed;

    public int penalityDuration;


    private void OnEnable()
    {
        energy = maxEnergy;
        if (energyChangeEvent == null)
        {
            energyChangeEvent = new UnityEvent<string,float>();
        }

        currentRegenSpeed = regenSpeed;
    }

    public async void DecreaseEnergy(string col, int amount)
    {
        energy -= amount;
        energyChangeEvent.Invoke(col,(float)energy/maxEnergy);

        currentRegenSpeed = 0;
        await Task.Delay(penalityDuration*1000);
        currentRegenSpeed = regenSpeed;
    }

    public void IncreaseEnergy(string col, float amount)
    {
        energy += amount;
        energyChangeEvent.Invoke(col, (float)energy / maxEnergy);
    }

    

}
