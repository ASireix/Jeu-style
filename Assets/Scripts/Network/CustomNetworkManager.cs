using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;

public class CustomNetworkManager : NetworkManager
{
    [SerializeField]
    PlayerObjectController playerObjectPrefab;

    public List<PlayerObjectController> gamePlayers { get; } = new List<PlayerObjectController>();

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        PlayerObjectController playerInstance = Instantiate(playerObjectPrefab);

        playerInstance.ConnectionID = conn.connectionId;
        playerInstance.PlayerIdNumber = gamePlayers.Count + 1;
        playerInstance.PlayerSteamID = (ulong)SteamMatchmaking.GetLobbyMemberByIndex((CSteamID)SteamLobby.Instance.currentLobbyID, gamePlayers.Count);

        NetworkServer.AddPlayerForConnection(conn, playerInstance.gameObject);
    }

    public void StartGame(string scnName)
    {
        ServerChangeScene(scnName);
    }
}
