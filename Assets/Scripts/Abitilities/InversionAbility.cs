using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InversionAbility : Ability
{
    public GameObject forceFieldPrefab;

    GameObject currentField;
    public int maxS;
    public float gSpeed;
    public float sSpeed;


    public override void Activate(PlayerController player)
    {
        

        if (requiredEnergy <= player.currentEnergy && player.anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") || player.anim.GetCurrentAnimatorStateInfo(0).IsName("Move"))
        {
            base.Activate(player);
            player.anim.SetTrigger(triggerAnimName);
            player.AnimationFunctionToCall = SpawnForceField;
        }
    }

    public override void BeginCooldown(PlayerController player)
    {
        base.BeginCooldown(player);
        if (currentField)
        {
            currentField.GetComponent<ForceField>().Shrinkage();
        }
        

    }

    public void SpawnForceField(PlayerController player)
    {
        player.DecreaseEnergy(requiredEnergy);
        GameObject forceFieldEntitie = Instantiate(forceFieldPrefab, player.transform.position, player.transform.rotation);
        forceFieldEntitie.transform.parent = player.gameObject.transform;
        ForceField tempsForce = forceFieldEntitie.GetComponent<ForceField>();
        tempsForce.maxSize = maxS;
        tempsForce.growSpeed = gSpeed;
        tempsForce.shrinkSpeed = sSpeed;
        currentField = forceFieldEntitie;
    }
}
