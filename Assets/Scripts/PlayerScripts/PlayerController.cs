using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MagicaCloth;

public class PlayerController : MonoBehaviour
{
    public CharacterStat characterStat;
    public PlayerNumber playerNumber;
    [Header("Optional")]

    public MagicaAreaWind wind;

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
    public AbilityHolder playerAbilityThree;

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
    bool alive = true;
    // Events

    [System.NonSerialized]
    public UnityEvent<PlayerUI, float> energyChangeEvent;

    [System.NonSerialized]
    public UnityEvent<PlayerUI, float> cdProjChangeEvent;

    [System.NonSerialized]
    public UnityEvent<PlayerUI, float> cdOneChangeEvent;

    [System.NonSerialized]
    public UnityEvent<PlayerUI, float> cdTwoChangeEvent;

    [System.NonSerialized]
    public UnityEvent<PlayerUI, float> cdThreeChangeEvent;

    [System.NonSerialized]
    public UnityEvent<PlayerUI, float> healthChangeEvent;

    [System.NonSerialized]
    public UnityEvent<GameObject> liveChangeEvent;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        animatorOverride = new AnimatorOverrideController(anim.runtimeAnimatorController);
        anim.runtimeAnimatorController = animatorOverride;

        playerAbilityOne.SetClips(this,"one");
        playerAbilityTwo.SetClips(this,"two");
        playerAbilityThree.SetClips(this, "three");

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

        if (cdProjChangeEvent == null)
        {
            cdProjChangeEvent = new UnityEvent<PlayerUI, float>();
        }

        if (cdThreeChangeEvent == null)
        {
            cdThreeChangeEvent = new UnityEvent<PlayerUI, float>();
        }

        if (healthChangeEvent == null)
        {
            healthChangeEvent = new UnityEvent<PlayerUI, float>();
        }

        if (liveChangeEvent == null)
        {
            liveChangeEvent = new UnityEvent<GameObject>();
        }
    }

    private void Update()
    {
        if (currentEnergy < characterStat.Energy)
        {
            IncreaseEnergy(currentMPRegen * Time.deltaTime);
        }
        else
        {
            currentEnergy = characterStat.Energy;
        }

        UpdateCDProj(1 - (shootAbility.coolDownTime / shootAbility.ability.cooldDownTime));

        UpdateCDOne(1 - (playerAbilityOne.coolDownTime / playerAbilityOne.ability.cooldDownTime));

        UpdateCDTwo(1 - (playerAbilityTwo.coolDownTime / playerAbilityTwo.ability.cooldDownTime));

        UpdateCDThree(1 - (playerAbilityThree.coolDownTime / playerAbilityThree.ability.cooldDownTime));


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

    public void DecreaseEnergy(int amount)
    {
        currentEnergy -= amount;
        energyChangeEvent.Invoke(playerUI, currentEnergy / characterStat.Energy);
    }

    public void IncreaseEnergy(float amount)
    {
        currentEnergy += amount;
        //ui.energy.fillAmount = currentEnergy / characterStat.Energy;
        energyChangeEvent.Invoke(playerUI, currentEnergy / characterStat.Energy);
    }

    public void UpdateCDProj(float cd)
    {
        cdProjChangeEvent.Invoke(playerUI, cd);
    }

    public void UpdateCDOne(float cd)
    {
        cdOneChangeEvent.Invoke(playerUI, cd);
    }

    public void UpdateCDTwo(float cd)
    {
        cdTwoChangeEvent.Invoke(playerUI, cd);
    }

    public void UpdateCDThree(float cd)
    {
        cdThreeChangeEvent.Invoke(playerUI, cd);
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth += amount;
        if (currentHealth > characterStat.Health)
        {
            currentHealth = characterStat.Health;
        }
        healthChangeEvent.Invoke(playerUI, (float)currentHealth / characterStat.Health);
    }

    public void DecreaseHealth(float amount)
    {
        if (!alive) { return; }
        currentHealth -= amount;
        healthChangeEvent.Invoke(playerUI, (float)currentHealth / characterStat.Health);

        if (currentHealth <= 0)
        {
            liveChangeEvent.Invoke(gameObject);
            Die();
            alive = false;
        }
    }
    void Die()
    {
        anim.SetTrigger("Die");
    }

    public void Setup(PlayerNumber team, int layer, PlayerUI pUI, int paletteIndex)
    {
        playerNumber = team;
        gameObject.layer = layer;
        playerUI = pUI;
        GetComponent<ColorPicker>().SetColors(paletteIndex);
    }

}
