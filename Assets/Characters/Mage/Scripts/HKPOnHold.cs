using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HK Performance On Hold", menuName = "Abilities/Haraki K/Performance On Hold")]
public class HKPOnHold : Ability
{
    bool stopped = false;
    public override void ActivateAbility(PlayerController player, PlayerState pState)
    {
        player.anim.SetTrigger(triggerAnimName);
        player.AnimationFunctionToCall = OnHold;
    }

    void OnHold(PlayerController player)
    {
        player.DecreaseEnergy(requiredEnergy);
        player.GetComponent<ProjectileStock>().InvertProjectileSpeed();
        /*
        if (!stopped)
        {
            player.GetComponent<ProjectileStock>().SetProjectilesMaxSpeed(0f);
            stopped = true;
        }
        else
        {
            stopped = false;
            player.GetComponent<ProjectileStock>().SetProjectilesMaxSpeed(15f);
        }*/
    }
}
