using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class LobbyController : MonoBehaviour
{
    public static LobbyController Instance;

    public TextMeshProUGUI lobbyNameText;

    public GameObject playerListViewContent;
    public GameObject playerListItemPrefab;
    public GameObject localPlayerObject;

    public ulong currentLobbyID;
    public bool playerItemCreated = false;
    List<PlayerListItem> playerListItems = new List<PlayerListItem>();
    public PlayerObjectController localPlayerController;

    public Button startGameButton;

    CustomNetworkManager manager;

    CustomNetworkManager Manager
    {
        get
        {
            if (manager != null)
            {
                return manager;
            }
            return manager = CustomNetworkManager.singleton as CustomNetworkManager;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        
    }

    public void ReadyPlayer()
    {
        localPlayerController.ChangeReady();
    }

    public void CheckIfAllReady()
    {
        bool AllReady = false;

        foreach (PlayerObjectController player in Manager.gamePlayers)
        {
            if (player.Ready)
            {
                AllReady = true;
            }
            else
            {
                AllReady = false;
                break;
            }
        }

        if (AllReady)
        {
            if (localPlayerController.PlayerIdNumber == 1)
            {
                startGameButton.interactable = true;
            }
            else
            {

            }
        }
        else
        {
            startGameButton.interactable = false;
        }
    }

    public void UpdateLobbyName()
    {
        currentLobbyID = Manager.GetComponent<SteamLobby>().currentLobbyID;
        lobbyNameText.text = SteamMatchmaking.GetLobbyData(new CSteamID(currentLobbyID), "name");
    }

    public void UpdatePlayerList()
    {
        if (!playerItemCreated)
        {
            CreateHostPlayerItem();
        }

        if (playerListItems.Count < Manager.gamePlayers.Count)
        {
            CreateClientPlayerItem();
        }

        if (playerListItems.Count > Manager.gamePlayers.Count)
        {
            RemovePlayerItem();
        }

        if (playerListItems.Count == Manager.gamePlayers.Count)
        {
            UpdatePlayerItem();
        }
    }

    public void FindLocalPlayer()
    {
        localPlayerObject = GameObject.Find("LocalGamePlayer");
        localPlayerController = localPlayerObject.GetComponent<PlayerObjectController>();
    }

    public void CreateHostPlayerItem()
    {
        foreach (PlayerObjectController player in Manager.gamePlayers)
        {
            GameObject newPlayerItem = Instantiate(playerListItemPrefab) as GameObject;
            PlayerListItem newPlayerItemScript = newPlayerItem.GetComponent<PlayerListItem>();

            newPlayerItemScript.playerName = player.PlayerName;
            newPlayerItemScript.connectionID = player.ConnectionID;
            newPlayerItemScript.playerSteamID = player.PlayerSteamID;
            newPlayerItemScript.ready = player.Ready;
            newPlayerItemScript.SetPlayerValues();

            newPlayerItem.transform.SetParent(playerListViewContent.transform);
            newPlayerItem.transform.localScale = Vector3.one;

            playerListItems.Add(newPlayerItemScript);
        }
        playerItemCreated = true;
    }

    public void CreateClientPlayerItem()
    {
        foreach (PlayerObjectController player in Manager.gamePlayers)
        {
            if (!playerListItems.Any(b => b.connectionID == player.ConnectionID))
            {
                GameObject newPlayerItem = Instantiate(playerListItemPrefab) as GameObject;
                PlayerListItem newPlayerItemScript = newPlayerItem.GetComponent<PlayerListItem>();

                newPlayerItemScript.playerName = player.PlayerName;
                newPlayerItemScript.connectionID = player.ConnectionID;
                newPlayerItemScript.playerSteamID = player.PlayerSteamID;
                newPlayerItemScript.ready = player.Ready;
                newPlayerItemScript.SetPlayerValues();

                newPlayerItem.transform.SetParent(playerListViewContent.transform);
                newPlayerItem.transform.localScale = Vector3.one;

                playerListItems.Add(newPlayerItemScript);
            }
        }
    }
    public void UpdatePlayerItem()
    {
        foreach (PlayerObjectController player in Manager.gamePlayers)
        {
            foreach (PlayerListItem playerListItemScript in playerListItems)
            {
                if (playerListItemScript.connectionID == player.ConnectionID)
                {
                    playerListItemScript.playerName = player.PlayerName;
                    playerListItemScript.ready = player.Ready;
                    playerListItemScript.SetPlayerValues();
                }
            }
        }
        CheckIfAllReady();
    }

    public void RemovePlayerItem()
    {
        List<PlayerListItem> playerListItemToRemove = new List<PlayerListItem>();

        foreach (PlayerListItem item in playerListItems)
        {
            if (!Manager.gamePlayers.Any(b=> b.ConnectionID == item.connectionID))
            {
                playerListItemToRemove.Add(item);
            }
        }

        if (playerListItemToRemove.Count > 0)
        {
            foreach (PlayerListItem item in playerListItemToRemove)
            {
                GameObject objectToRemove = item.gameObject;
                playerListItems.Remove(item);
                Destroy(objectToRemove);
                objectToRemove = null;
            }
        }
    }

    public void StartGame(string scnName)
    {
        localPlayerController.CanStartGame(scnName);
    }
}
