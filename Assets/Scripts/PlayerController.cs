using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public CharacterStat characterStat;
    public PlayerNumber playerNumber;
    public PlayerStats playerStats;

    [Header("Animations")]

    public Animator anim;
    public AnimatorOverrideController animatorOverride;
    public AnimationClip testClip;

    public delegate void TestDelegate(PlayerController playerController); // This defines what type of method you're going to call.
    public TestDelegate AnimationFunctionToCall;

    [Header("Abilities")]
    public AbilityHolder shootAbility;

    public AbilityHolder playerAbilityOne;
    public AbilityHolder playerAbilityTwo;

    [Header("Specifics positions")]
    public Transform shootingPos;

    [Header("UI")]
    public GameObject ui;
    [System.NonSerialized]
    public PlayerUI playerUI;

    [Header("Stats")]
    public float currentHealth;
    public float currentDmgReduc;
    public float currentEnergy;
    public float currentMPRegen;
    public float currentSpeed;

    // Events

    [System.NonSerialized]
    public UnityEvent<PlayerUI, float> energyChangeEvent;

    [System.NonSerialized]
    public UnityEvent<PlayerUI, float> cdOneChangeEvent;

    [System.NonSerialized]
    public UnityEvent<PlayerUI, float> cdTwoChangeEvent;

    [System.NonSerialized]
    public UnityEvent<PlayerUI, float> healthChangeEvent;


    private void Awake()
    {
        
        animatorOverride = new AnimatorOverrideController(anim.runtimeAnimatorController);
        anim.runtimeAnimatorController = animatorOverride;

        playerAbilityOne.SetClips(this,"one");
        playerAbilityTwo.SetClips(this,"two");

        ResetEverything();
        

        if (energyChangeEvent == null)
        {
            energyChangeEvent = new UnityEvent<PlayerUI, float>();
        }

        if (cdOneChangeEvent == null)
        {
            cdOneChangeEvent = new UnityEvent<PlayerUI, float>();
        }

        if (cdTwoChangeEvent == null)
        {
            cdTwoChangeEvent = new UnityEvent<PlayerUI, float>();
        }
    }

    private void Update()
    {
        if (currentEnergy < characterStat.Energy)
        {
            IncreaseEnergy(playerUI, currentMPRegen * Time.deltaTime);
        }
        else
        {
            currentEnergy = characterStat.Energy;
        }

        UpdateCDOne(playerUI,1 - (playerAbilityOne.coolDownTime / playerAbilityOne.ability.cooldDownTime));
        
        UpdateCDTwo(playerUI,1 - (playerAbilityTwo.coolDownTime / playerAbilityTwo.ability.cooldDownTime));

    }

    public void TriggerAnimFunction()
    {
        AnimationFunctionToCall(this);
    }

    public void ResetEverything()
    {
        currentEnergy = characterStat.Energy;
        currentDmgReduc = characterStat.Damage_Reduction_Percentage;
        currentHealth = characterStat.Health;
        currentMPRegen = characterStat.EnergyRegenSpeed;
        currentSpeed = characterStat.Speed;
        gameObject.GetComponent<PlayerMovement>().speed = currentSpeed;
    }

    public void DecreaseEnergy(PlayerUI ui, int amount)
    {
        currentEnergy -= amount;
        energyChangeEvent.Invoke(ui, currentEnergy / characterStat.Energy);
    }

    public void IncreaseEnergy(PlayerUI ui, float amount)
    {
        currentEnergy += amount;
        //ui.energy.fillAmount = currentEnergy / characterStat.Energy;
        energyChangeEvent.Invoke(ui, currentEnergy / characterStat.Energy);
    }


    public void UpdateCDOne(PlayerUI ui, float cd)
    {
        cdOneChangeEvent.Invoke(ui, cd);
    }

    public void UpdateCDTwo(PlayerUI ui, float cd)
    {
        cdTwoChangeEvent.Invoke(ui, cd);
    }

    public void IncreaseHealth(PlayerUI ui, float amount)
    {
        currentHealth += amount;
        if (currentHealth > characterStat.Health)
        {
            currentHealth = characterStat.Health;
        }
        healthChangeEvent.Invoke(ui, (float)currentHealth / characterStat.Health);
    }

    public void DecreaseHealth(PlayerUI ui, float amount)
    {
        currentHealth -= amount;
        healthChangeEvent.Invoke(ui, (float)currentHealth / characterStat.Health);
    }
}
