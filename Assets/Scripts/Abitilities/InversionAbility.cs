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
        base.Activate(player);

        if (!player.anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot") && requiredEnergy <= player.abilityManager.energy)
        {
            base.Activate(player);
            player.anim.SetTrigger("Shoot");
            int r = Random.Range(0, 2);
            player.anim.SetFloat("ShootingAnim", r);
        }
    }

    public override void BeginCooldown(PlayerController player)
    {
        base.BeginCooldown(player);
        


    }
}
