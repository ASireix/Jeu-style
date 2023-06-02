using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum GameStates
{
    Intro,
    RoundStart,
    Game,
    RoundEnd,
    EndGame
}
public class GameManager : MonoBehaviour
{
    [Header("Parameters")]
    public VersusParam versusParam;

    List<GameObject> playersPrefabs;
    [SerializeField] GameStates gameStates;
    [SerializeField] float endRound_slowdownDuration = 3f;
    [SerializeField] float slowForce;
    [SerializeField] float currentSlowDuration;

    [SerializeField]
    private List<GameObject> players;

    [Header("Starting Pos")]
    [SerializeField]
    List<Transform> spawns;

    PlayerInputManager playerInputManager;

    [Header("UI")]
    public UIManager uIManager;
    public GameObject victoryScreen;
    public TextMeshProUGUI victoryText;

    [SerializeField]
    Canvas canvas;
    public static GameManager instance;
    bool hasStarted;
    [Header("Ultimates")]
    [SerializeField]
    float[] gauges = new float[4];

    [SerializeField]
    float amountRequiredForUlt;
    bool ultIncoming;

    [SerializeField]
    float ultimateLength;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
        gauges[0] = 0; gauges[1] = 0; gauges[2] = 0; gauges[3] = 0;
        playersPrefabs = versusParam.playersList;
        players = new List<GameObject>();
        if (playersPrefabs.Count <= 0)
        {
            Debug.Log("No players selected");
        }
        else
        {
            int i = 0;
            foreach (var p in playersPrefabs)
            {

                playerInputManager.playerPrefab = p;
                playerInputManager.JoinPlayer(i, -1);
                i++;
            }
            SetPlayers();
            hasStarted = true;
        }
        victoryScreen.SetActive(false);
    }

    private void Update()
    {
        switch (gameStates)
        {
            case GameStates.Intro:
                break;
            case GameStates.RoundStart:
                break;
            case GameStates.Game:
                if (ultimateLength > 0)
                {
                    ultimateLength -= Time.deltaTime;
                }
                if (players.Count <= 1)
                {
                    gameStates = GameStates.RoundEnd;
                    currentSlowDuration = 0f;
                    Time.timeScale = slowForce;
                }
                break;
            case GameStates.RoundEnd:
                currentSlowDuration += Time.deltaTime;
                if (currentSlowDuration > endRound_slowdownDuration)
                {
                    currentSlowDuration = 0f;
                    gameStates = GameStates.EndGame;
                    Time.timeScale = 1f;
                }
                break;
            case GameStates.EndGame:
                //MenuManager.instance.LoadMainMenu();
                break;
            default:
                break;
        }
        //
        //victoryScreen.SetActive(true);
        //victoryText.text = players[0].GetComponent<PlayerController>().characterStat.Name+" Win";

        
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log("A Player spawn in the game");
        players.Add(playerInput.gameObject);
    }

    void SetPlayers()
    {
        string[] PlayerNumberString = System.Enum.GetNames(typeof(PlayerNumber));
        List<PlayerController> playersControllers = new List<PlayerController>();
        Debug.Log("setting up " + players.Count + "players");
        PlayerController playerController;
        for (int i = 0; i < players.Count; i++)
        {
            players[i].transform.position = spawns[i].position;

            playerController = players[i].GetComponent<PlayerController>();

            GameObject pUi = Instantiate(playerController.ui);

            playerController.Setup((PlayerNumber)System.Enum.Parse(typeof(PlayerNumber), PlayerNumberString[i]), 7 + i, pUi.GetComponent<PlayerUI>(), versusParam.playersPalettes[i]);

            pUi.transform.SetParent(canvas.transform);

            playersControllers.Add(playerController);

        }
        gameStates = GameStates.Game;
        foreach (var item in playersControllers)
        {
            item.liveChangeEvent.AddListener(UpdateLivePlayers);

            RectTransform rectTransform;
            rectTransform = item.playerUI.GetComponent<RectTransform>();
            rectTransform.localScale = new Vector3(1, 1, 1);
            switch (item.playerNumber)
            {
                case PlayerNumber.PlayerOne:
                    rectTransform.SetAnchor(AnchorPresets.BottomLeft);
                    rectTransform.SetPivot(PivotPresets.BottomLeft);
                    item.gameObject.layer = LayerMask.NameToLayer("TeamOne");
                    item.gameObject.name = "Player 1";
                    break;
                case PlayerNumber.PlayerTwo:
                    rectTransform.SetAnchor(AnchorPresets.BottomRight);
                    rectTransform.SetPivot(PivotPresets.BottomRight);
                    item.gameObject.layer = LayerMask.NameToLayer("TeamTwo");
                    item.gameObject.name = "Player 2";
                    break;
                case PlayerNumber.PlayerThree:
                    rectTransform.SetAnchor(AnchorPresets.TopLeft);
                    rectTransform.SetPivot(PivotPresets.TopLeft);
                    item.gameObject.layer = LayerMask.NameToLayer("TeamThree");
                    item.gameObject.name = "Player 3";
                    break;
                case PlayerNumber.PlayerFour:
                    rectTransform.SetAnchor(AnchorPresets.TopRight);
                    rectTransform.SetPivot(PivotPresets.TopRight);
                    item.gameObject.layer = LayerMask.NameToLayer("TeamFour");
                    item.gameObject.name = "Player 4";
                    break;
                default:
                    break;
            }

            uIManager.playersUIs.Add(item);
            uIManager.SetListeners();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        foreach (var item in playersPrefabs)
        {
            item.GetComponent<PlayerController>().ResetEverything();
        }
    }

    void UpdateLivePlayers(GameObject p)
    {
        players.Remove(p);
    }

    public void UpdateGauge(float amount, PlayerNumber pNumber)
    {
        if (ultimateLength <= 0)
        {
            gauges[(int)pNumber] += amount;
            if (gauges[(int)pNumber] >= amountRequiredForUlt)
            {
                ultimateLength = players[(int)pNumber].GetComponent<Ultimate>().Activate();
                ultIncoming = true;
                gauges[0] = 0; gauges[1] = 0; gauges[2] = 0; gauges[3] = 0;
            }
        }
    }


}
