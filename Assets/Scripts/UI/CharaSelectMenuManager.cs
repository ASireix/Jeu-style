using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;



public class CharaSelectMenuManager : MonoBehaviour
{
    public static CharaSelectMenuManager instance;

    public VersusParam versusParam;

    public Color bothPlayerSelectColor;
    public Color p1Color;
    public Color p2Color;

    public MultiplayerEventSystem player1MInput;
    public MultiplayerEventSystem player2MInput;

    public Image p1Portrait;
    public Image p2Portrait;

    public Transform prisonP1Spawn;
    public Transform prisonP2Spawn;

    bool ready;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        
        try
        {
            Color c = Color.white;
            c.a = 0f;
            p1Portrait.sprite = null;
            p2Portrait.sprite = null;

            p1Portrait.color = c;
            p2Portrait.color = c;
            Destroy(prisonP1Spawn.GetChild(0).gameObject);
            Destroy(prisonP2Spawn.GetChild(0).gameObject);
        }
        catch { Debug.Log("Nothing to destroy"); }
        
    }

    public void UnSetPlayer(BaseInputModule events)
    {
        Color c = Color.white;
        c.a = 0f;
        if (events == player1MInput.currentInputModule)
        {
            
            p1Portrait.sprite = null;
            p1Portrait.color = c;
        }
        else
        {
            p2Portrait.sprite = null;
            p2Portrait.color = c;
        }
    }

    public void SetPlayer(BaseInputModule eventSystem, CharacterData c)
    {
        Color ca = Color.white;
        ca.a = 1f;

        if (eventSystem == player1MInput.currentInputModule)
        {
            versusParam.playersList[0] = c.characterPrefab;
            p1Portrait.sprite = c.portrait;
            p1Portrait.color = ca;
        }
        else
        {
            versusParam.playersList[1] = c.characterPrefab;
            p2Portrait.color = ca;
            p2Portrait.sprite = c.portrait;
        }

        if (p1Portrait.sprite && p2Portrait.sprite)
        {
            MenuManager.instance.LoadVersus();
        }
    }

    public void UpdateCharacterPreview(BaseInputModule eventSystem, CharacterData c)
    {
        GameObject o;

        if (eventSystem == player1MInput.currentInputModule)
        {
            try
            {
                Destroy(prisonP1Spawn.GetChild(0).gameObject);
            }
            catch{}
            
            o = Instantiate(c.prisonPrefab);
            o.transform.SetParent(prisonP1Spawn);
        }
        else
        {
            try
            {
                Destroy(prisonP2Spawn.GetChild(0).gameObject);
            }
            catch{}

            o = Instantiate(c.prisonPrefab);
            o.transform.SetParent(prisonP2Spawn);
        }
        o.transform.localPosition = c.positionOffset;
        o.transform.localScale = Vector3.one;
    }

}
