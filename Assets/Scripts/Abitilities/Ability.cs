using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    [Header("Deprecated")]
    public AnimationClip activeClip;
    public AnimationClip recoveryClip;
    public AnimationClip startClip;

    [Header("Good ones")]

    public int requiredEnergy;
    public float damage;

    public new string name;
    public float cooldDownTime;
    public float activeTime;

    public string triggerAnimName;
    [HideInInspector]
    
    public void Activate(PlayerController player, PlayerState pState) // YOU NEED TO DECREASE THE ENERGY MANUALLY IN EITHER ACTIVATE ABILITY OR YOUR ON TRIGGER ON ANIMATION FUNCTION !!!!
    {
        Debug.Log("Activate " + name);
        player.currentMPRegen = 0;
        pState.startup = true;
        ActivateAbility(player, pState);
        // YOU NEED TO ASSIGN A LAYER IN A FUNCTION MANUALLY, GET IT FROM PLAYERCONTROLLER.GAMEOBJECT
    }

    public virtual void ActivateAbility(PlayerController player, PlayerState pState)
    {

    }

    public virtual void EnterActive(PlayerState p)
    {
        p.startup = false;
        p.active = true;
    }

    public virtual void EnterRecovery(PlayerState p)
    {
        p.active = false;
        p.recovery = true;
    }

    public virtual void BeginCooldown(PlayerController player)
    {
        Debug.Log("CD of " + name);
        player.currentMPRegen = player.characterStat.EnergyRegenSpeed;
    }

    public IEnumerator ChangeRegenSpeed(float duration, float newRegen)
    {
        yield return null;
    }
}
