using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public int requiredEnergy;

    public new string name;
    public float cooldDownTime;
    public float activeTime;

    public string triggerAnimName;

    public AnimationClip activeClip;
    public AnimationClip recoveryClip;
    public AnimationClip startClip;

    public virtual void Activate(PlayerController player)
    {
        player.abilityManager.currentRegenSpeed = 0;
    }

    public virtual void BeginCooldown(PlayerController player)
    {
        player.abilityManager.currentRegenSpeed = player.abilityManager.regenSpeed;
    }

}
