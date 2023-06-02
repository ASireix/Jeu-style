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

            item.cdProjChangeEvent.AddListener(UpdateCDProj);

            item.cdOneChangeEvent.AddListener(UpdateCDOne);

            item.cdTwoChangeEvent.AddListener(UpdateCDTwo);

            item.cdThreeChangeEvent.AddListener(UpdateCDThree);

            item.healthChangeEvent.AddListener(UpdateHealth);
        }
    }

    void UpdateEnergy(PlayerUI ui, float amount)
    {
        ui.energy.fillAmount = amount;
    }

    void UpdateCDProj(PlayerUI ui, float amount)
    {
        ui.projectile.fillAmount = amount;
    }

    void UpdateCDOne(PlayerUI ui, float amount)
    {
        ui.abilityOne.fillAmount = amount;
    }

    void UpdateCDTwo(PlayerUI ui, float amount)
    {
        ui.abilityTwo.fillAmount = amount;
    }

    void UpdateCDThree(PlayerUI ui, float amount)
    {
        ui.abilityThree.fillAmount = amount;
    }

    void UpdateHealth(PlayerUI ui, float amount)
    {
        ui.health.fillAmount = amount;
    }
}
