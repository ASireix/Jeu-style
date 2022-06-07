using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public List<PlayerController> playersUIs;

    public Image clock;

    public void SetListeners()
    {
        foreach(var item in playersUIs)
        {

            item.energyChangeEvent.AddListener(UpdateEnergy);

            item.cdOneChangeEvent.AddListener(UpdateCDOne);

            item.cdTwoChangeEvent.AddListener(UpdateCDTwo);

            item.healthChangeEvent.AddListener(UpdateHealth);
        }
    }

    void UpdateEnergy(PlayerUI ui, float amount)
    {
        Debug.Log("marche");
        ui.energy.fillAmount = amount;
    }

    void UpdateCDOne(PlayerUI ui, float amount)
    {
        ui.abilityOne.fillAmount = amount;
    }

    void UpdateCDTwo(PlayerUI ui, float amount)
    {
        ui.abilityTwo.fillAmount = amount;
    }

    void UpdateHealth(PlayerUI ui, float amount)
    {
        ui.health.fillAmount = amount;
    }
}
