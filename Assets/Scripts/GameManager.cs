using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Players")]
    public GameObject playerPrefab;
    GameObject StickWhite;
    GameObject StickBlack;
    public AbilityManager PlayerOneAbilityManager;
    public AbilityManager PlayerTwoAbilityManager;

    [Header("Materials")]
    public Material WhiteMat;
    public Material BlackMat;

    public Material GroundWhite;
    public Material GroundBlack;

    public Material WallWhite;
    public Material WallBlack;

    [Header("Input manager")]
    public PlayerInputManager playerInputManager;

    [Header("UI")]
    public UIManager uIManager;
    public GameObject victoryScreen;
    public TextMeshProUGUI victoryText;

    [Header("Starting Pos")]
    public Transform whitePosition;
    public Transform blackPosition;

    [Header("terrain")]
    public ArenaMatManager ground;
    bool isWhite;

    [Header("timer")]
    public float maxCountDown;
    float timeRemainingUntilChange;

    bool hasStarted;

    PlayerController blackController;
    PlayerController whiteController;
    private void Start()
    {
        isWhite = true;
        timeRemainingUntilChange = maxCountDown;

        

        playerInputManager.JoinPlayer(0,-1,"Keyboard");
        playerInputManager.JoinPlayer(1, -1, "Gamepad");
        victoryScreen.SetActive(false);

    }

    private void Update()
    {
        if (timeRemainingUntilChange > 0)
        {
            timeRemainingUntilChange -= Time.deltaTime;
        }
        else
        {
            if (isWhite)
            {
                ground.SetColor(WallBlack, GroundBlack);
                isWhite = false;
            }
            else
            {
                ground.SetColor(WallWhite, GroundWhite);
                isWhite = true;
            }
            
            timeRemainingUntilChange = maxCountDown;
        }

        if (hasStarted && !StickWhite)
        {
            victoryScreen.SetActive(true);
            victoryText.text = "Le blacos a gagné";
        }else if (hasStarted && !StickBlack)
        {
            victoryScreen.SetActive(true);
            victoryText.text = "Le banc gagne";
        }

        
    }

    public void OnPlayerJoined(PlayerInput playerInput) 
    { 
        if (!StickWhite)
        {
            Debug.Log("white join");
            StickWhite = playerInput.gameObject;
            StickWhite.transform.position = whitePosition.position;
        }
        else
        {
            Debug.Log("black join");
            StickBlack = playerInput.gameObject;
            StickBlack.transform.position = blackPosition.position;
        }

        SetPlayer();
    }

    void SetPlayer()
    {
        if (StickBlack && StickWhite)
        {
            blackController = StickBlack.GetComponent<PlayerController>();
            whiteController = StickWhite.GetComponent<PlayerController>();

            StickBlack.GetComponent<Outline>().OutlineColor = Color.white;
            StickWhite.GetComponent<Outline>().OutlineColor = Color.black;

            blackController.mesh.material = BlackMat;
            whiteController.mesh.material = WhiteMat;

            blackController.color = "black";
            whiteController.color = "white";

            StickBlack.layer = LayerMask.NameToLayer("Black");
            StickWhite.layer = LayerMask.NameToLayer("White");

            blackController.abilityManager = PlayerTwoAbilityManager;
            whiteController.abilityManager = PlayerOneAbilityManager;

            hasStarted = true;
        }
    }

    public void RestartGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        blackController.abilityManager.ResetEverything();
        whiteController.abilityManager.ResetEverything();
    }
}
