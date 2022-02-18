using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityHolder : MonoBehaviour
{
    PlayerController playerCtrl;
    public Ability ability;

    float coolDownTime;
    float activeTime;

    public void SetClips(PlayerController play)
    {

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
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.ready;
                }
                break;
        }
    }


}
