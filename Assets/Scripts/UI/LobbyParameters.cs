using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using TMPro;

public class LobbyParameters : MonoBehaviour
{

    public TMP_InputField password;
    public TMP_InputField roomName;
    public TMP_Dropdown roomType;

    public void CreateLobby()
    {
        
        SteamLobby.Instance.HostLobby(roomType.value.ToString(),roomName.text);
    }
}
