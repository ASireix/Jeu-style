using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InversionAbility : Ability
{
    public GameObject forceFieldPrefab;

    ForceField forceField;

    public override void Activate(PlayerController player)
    {
        

        if (requiredEnergy <= player.abilityManager.energy)
        {
            base.Activate(player);
            player.anim.SetTrigger(triggerAnimName);
            player.AnimationFunctionToCall = SpawnForceField;
        }
    }

    public override void BeginCooldown(PlayerController player)
    {
        base.BeginCooldown(player);
        


    }

    public void SpawnForceField(PlayerController player)
    {
        player.abilityManager.DecreaseEnergy(player.color, requiredEnergy);
        GameObject tempBullet = Instantiate(forceFieldPrefab, player.transform.position, player.transform.rotation);
    }
}
