using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityHolder : MonoBehaviour
{
    public string type;
    PlayerController playerCtrl;
    public Ability ability;

    [System.NonSerialized]
    public float coolDownTime;

    float activeTime;

    public void SetClips(PlayerController play, string whichOne)
    {
        switch (whichOne)
        {
            case "one":
                ability.triggerAnimName = "AbilityOne";
                play.animatorOverride["AbilityOne"] = ability.startClip;
                if (ability.activeClip)
                {
                    play.animatorOverride["ActiveOne"] = ability.activeClip;
                }
                if (ability.recoveryClip)
                {
                    play.animatorOverride["RecoveryOne"] = ability.recoveryClip;
                }

                break;
            case "two":
                ability.triggerAnimName = "AbilityTwo";
                play.animatorOverride["AbilityTwo"] = ability.startClip;
                if (ability.activeClip)
                {
                    play.animatorOverride["ActiveTwo"] = ability.activeClip;
                }
                if (ability.recoveryClip)
                {
                    play.animatorOverride["RecoveryTwo"] = ability.recoveryClip;
                }
                break;
            default:
                break;
        }
    }
    private void Start()
    {
        playerCtrl = gameObject.GetComponent<PlayerController>();
    }

    enum AbilityState
    {
        ready,
        active,
        cooldown
    }

    AbilityState state = AbilityState.ready;

    public void OnCast(InputAction.CallbackContext context)
    {
        if (context.performed && state == AbilityState.ready)
        {
            ability.Activate(playerCtrl);
            state = AbilityState.active;
            activeTime = ability.activeTime;
        }

    }

    private void Update()
    {
        switch (state)
        {

            case AbilityState.active:
                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    ability.BeginCooldown(playerCtrl);
                    state = AbilityState.cooldown;
                    coolDownTime = ability.cooldDownTime;
                }
                break;
            case AbilityState.cooldown:
                if (coolDownTime > 0)
                {
                    coolDownTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.ready;
                }
                break;
        }
    }


}
