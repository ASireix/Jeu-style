using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public int requiredEnergy;
    public float damage;

    public new string name;
    public float cooldDownTime;
    public float activeTime;

    public string triggerAnimName;

    public AnimationClip activeClip;
    public AnimationClip recoveryClip;
    public AnimationClip startClip;

    public virtual void Activate(PlayerController player)
    {
        Debug.Log("Activate "+name);
        player.currentMPRegen = 0;
    }

    public virtual void BeginCooldown(PlayerController player)
    {
        Debug.Log("CD of " + name);
        player.currentMPRegen = player.characterStat.EnergyRegenSpeed;
    }

    public IEnumerator ChangeRegenSpeed(float duration,float newRegen)
    {
        yield return null;
    }
}
