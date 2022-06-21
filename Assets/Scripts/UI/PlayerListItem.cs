using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;
using TMPro;

public class PlayerListItem : MonoBehaviour
{
    public string playerName;
    public int connectionID;
    public ulong playerSteamID;

    bool AvatarReceived;

    public TextMeshProUGUI playerNameText;
    public RawImage playerIcon;
    public Image playerReadyIcon;
    public bool ready;

    protected Callback<AvatarImageLoaded_t> ImageLoaded;

    private void Start()
    {
        ImageLoaded = Callback<AvatarImageLoaded_t>.Create(OnImageLoaded);
    }

    public void ChangeReadyStatus()
    {
        if (ready)
        {
            playerReadyIcon.color = Color.green;
        }
        else
        {
            playerReadyIcon.color = Color.yellow;
        }
    }

    private Texture2D GetSteamImageAsTexture(int iImage)
    {
        Texture2D texture = null;

        bool isValid = SteamUtils.GetImageSize(iImage, out uint width, out uint height);
        if (isValid)
        {
            byte[] image = new byte[width * height * 4];

            isValid = SteamUtils.GetImageRGBA(iImage, image, (int)(width * height * 4));

            if (isValid)
            {
                texture = new Texture2D((int)width, (int)height, TextureFormat.RGBA32, false, true);
                texture.LoadRawTextureData(image);
                texture.Apply();
            }
        }
        AvatarReceived = true;
        return texture;
    }

    public void SetPlayerValues()
    {
        playerNameText.text = playerName;
        ChangeReadyStatus();
        if (!AvatarReceived)
        {
            GetPlayerIcon();
        }
    }

    void GetPlayerIcon()
    {
        int imageId = SteamFriends.GetLargeFriendAvatar((CSteamID)playerSteamID);
        if (imageId == -1)
        {
            return;
        }
        playerIcon.texture = GetSteamImageAsTexture(imageId);
    }

    void OnImageLoaded(AvatarImageLoaded_t callback)
    {
        if (callback.m_steamID.m_SteamID == playerSteamID)
        {
            playerIcon.texture = GetSteamImageAsTexture(callback.m_iImage);
        }
        else
        {
            return;
        }
    }
}
