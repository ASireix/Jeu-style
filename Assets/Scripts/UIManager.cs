using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // scriptable object
    public AbilityManager abilitiesPOne;
    public AbilityManager abilitiesPTwo;

    public Image clock;

    public Image EnergyBarWhite;
    public Image EnergyBarBlack;

    public Image BlackAbilityOne;
    public Image BlackAbilityTwo;

    public Image WhiteAbilityOne;
    public Image WhiteAbilityTwo;


    private void OnEnable()
    {
        abilitiesPOne.energyChangeEvent.AddListener(UpdateEnergy);
        abilitiesPTwo.energyChangeEvent.AddListener(UpdateEnergy);
    }
    void UpdateEnergy(string coloeur, float amount)
    {
        switch (coloeur)
        {
            case "white":
                EnergyBarWhite.fillAmount = amount;
                break;
            case "black":
                EnergyBarBlack.fillAmount = amount;
                break;
            default:
                break;
        }
        
    }
}
