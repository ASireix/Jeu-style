using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void LoadCharacterSelect()
    {
        SceneManager.LoadScene("CharacterSelect");
    }

    public void LoadVersus()
    {
        SceneManager.LoadScene("PrisonLevel");
    }
}
