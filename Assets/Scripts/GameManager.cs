using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [Header("Players")]
    public GameObject playerPrefab;
    GameObject StickWhite;
    GameObject StickBlack;

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
    public TextMeshProUGUI timerText;

    [Header("CD Managers")]
    public CDHelper whiteCD;
    public CDHelper blackCD;

    bool hasStarted;
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

        timerText.text = (int)timeRemainingUntilChange+"";
    }

    public void OnPlayerJoined(PlayerInput playerInput) { 
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
            StickBlack.GetComponent<Outline>().OutlineColor = Color.white;
            StickWhite.GetComponent<Outline>().OutlineColor = Color.black;

            StickBlack.GetComponent<PlayerController>().mesh.material = BlackMat;
            StickWhite.GetComponent<PlayerController>().mesh.material = WhiteMat;

            StickBlack.GetComponent<PlayerController>().color = "black";
            StickWhite.GetComponent<PlayerController>().color = "white";

            StickBlack.layer = LayerMask.NameToLayer("Black");
            StickWhite.layer = LayerMask.NameToLayer("White");

            StickBlack.GetComponent<PlayerAttacks>().cDHelper = blackCD;
            StickWhite.GetComponent<PlayerAttacks>().cDHelper = whiteCD;

            hasStarted = true;
        }
    }
}
