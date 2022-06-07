using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Players")]
    public List<GameObject> playersPrefabs;
    [SerializeField]
    private List<GameObject> players;

    [Header("Starting Pos")]
    public List<Transform> spawns;

    [Header("Input manager")]
    public PlayerInputManager playerInputManager;

    [Header("UI")]
    public UIManager uIManager;
    public GameObject victoryScreen;
    public TextMeshProUGUI victoryText;

    public Canvas canvas;

    

    bool hasStarted;
    private void Start()
    {
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
        if (hasStarted && players.Count<=1)
        {
            victoryScreen.SetActive(true);
            victoryText.text = players[0].GetComponent<PlayerController>().characterStat.Name+" Win";
        }
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

        PlayerController playerController;
        for (int i = 0; i < players.Count; i++)
        {
            players[i].transform.position = spawns[i].position;

            playerController = players[i].GetComponent<PlayerController>();

            
            playerController.playerNumber = (PlayerNumber)System.Enum.Parse(typeof(PlayerNumber), PlayerNumberString[i]);

            GameObject pUi = Instantiate(playerController.ui);
            playerController.playerUI = pUi.GetComponent<PlayerUI>();
            pUi.transform.SetParent(canvas.transform);

            playersControllers.Add(playerController);
            
        }

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
                    break;
                case PlayerNumber.PlayerTwo:
                    rectTransform.SetAnchor(AnchorPresets.BottomRight);
                    rectTransform.SetPivot(PivotPresets.BottomRight);
                    item.gameObject.layer = LayerMask.NameToLayer("TeamTwo");
                    break;
                case PlayerNumber.PlayerThree:
                    rectTransform.SetAnchor(AnchorPresets.TopLeft);
                    rectTransform.SetPivot(PivotPresets.TopLeft);
                    item.gameObject.layer = LayerMask.NameToLayer("TeamThree");
                    break;
                case PlayerNumber.PlayerFour:
                    rectTransform.SetAnchor(AnchorPresets.TopRight);
                    rectTransform.SetPivot(PivotPresets.TopRight);
                    item.gameObject.layer = LayerMask.NameToLayer("TeamFour");
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
}
