using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // scriptable object
    public AbilityManager abilityManager;

    public Image clock;

    public Image EnergyBarWhite;
    public Image EnergyBarBlack;

    public Image BlackAbilityOne;
    public Image BlackAbilityTwo;

    public Image WhiteAbilityOne;
    public Image WhiteAbilityTwo;


    private void OnEnable()
    {
        abilityManager.energyChangeEvent.AddListener(UpdateEnergy);
    }
    void UpdateEnergy(string coloeur, int amount)
    {

    }
}
