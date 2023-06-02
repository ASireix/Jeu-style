using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Data/CharacterData")]
public class CharacterData : ScriptableObject
{
    public GameObject prisonPrefab;
    public Sprite portrait;
    public GameObject characterPrefab;
    public Vector3 positionOffset;
    public List<ColorPalette> colorPalettes;

    public CharacterData(GameObject _prisonPrefab, Sprite _portrait, GameObject _characterPrefab, Vector3 offset, List<ColorPalette> colPal)
    {
        prisonPrefab = _prisonPrefab;
        portrait = _portrait;
        characterPrefab = _characterPrefab;
        positionOffset = offset;
        colorPalettes = colPal;
    }
}