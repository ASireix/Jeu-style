using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;
using TMPro;

public class SteamLobby : MonoBehaviour
{
    public static SteamLobby Instance;

    // Callback
    protected Callback<LobbyCreated_t> lobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> joinRequest;
    protected Callback<LobbyEnter_t> lobbyEntered;

    public ulong currentLobbyID;
    const string hostAdressKey = "HostAdress";
    CustomNetworkManager networkManager;
    public string lobbyName;

    private void Start()
    {
        lobbyName = "";
        if (Instance == null)
        {
            Instance = this;
        }

        if (!SteamManager.Initialized)
        {
            return;
        }

        networkManager = GetComponent<CustomNetworkManager>();

        lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        joinRequest = Callback<GameLobbyJoinRequested_t>.Create(OnJoinRequest);
        lobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
    }

    public void HostLobby(string lobbyType, string lobbyname)
    {
        ELobbyType type = ELobbyType.k_ELobbyTypePublic;
        switch (lobbyType)
        {
            case "Public":
                type = ELobbyType.k_ELobbyTypePublic;
                break;
            case "Private":
                type = ELobbyType.k_ELobbyTypePrivate;
                break;
            case "Friend Only":
                type = ELobbyType.k_ELobbyTypeFriendsOnly;
                break;
            default:
                break;
        }
        lobbyName = lobbyname;
        SteamMatchmaking.CreateLobby(type, networkManager.maxConnections);
    }

    void OnLobbyCreated(LobbyCreated_t callback)
    {
        if (callback.m_eResult != EResult.k_EResultOK)
        {
            return;
        }
        Debug.Log("Lobby created");

        networkManager.StartHost();

        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), hostAdressKey, SteamUser.GetSteamID().ToString());
        Debug.Log(string.IsNullOrEmpty(lobbyName));
        if (!string.IsNullOrEmpty(lobbyName))
        {
            SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "name", lobbyName);
        }
        else
        {
            SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "name", SteamFriends.GetPersonaName() + "'s Lobby");
        }
        Debug.Log("Lobby ID : " + callback.m_ulSteamIDLobby);
    }

    void OnJoinRequest(GameLobbyJoinRequested_t callback)
    {
        Debug.Log("Resquest to join lobby");
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }

    void OnLobbyEntered(LobbyEnter_t callback)
    {
        // All
        currentLobbyID = callback.m_ulSteamIDLobby;
        //lobbyNameText.gameObject.SetActive(true);
        //lobbyNameText.text = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "name");

        // Clients
        if (NetworkServer.active)
        {
            return;
        }

        networkManager.networkAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), hostAdressKey);

        networkManager.StartClient();
    }

    
}
