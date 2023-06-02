using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class ControlManagerUI : MonoBehaviour
{
    [SerializeField] bool ready;
    PlayerInput pi;
    MultiplayerEventSystem multiplayerEventSystem;

    private void Start()
    {
        pi = GetComponent<PlayerInput>();
        multiplayerEventSystem = GetComponent<MultiplayerEventSystem>();
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ready = true;
            pi.actions.FindActionMap("UI").Disable();
        }
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (ready)
            {
                ready = false;
                pi.actions.FindActionMap("UI").Enable();
                CharaSelectMenuManager.instance.UnSetPlayer(multiplayerEventSystem.currentInputModule);
            }
            else
            {
                MenuManager.instance.LoadMainMenu();
            }
        }
    }
        
}
