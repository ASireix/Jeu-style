using UnityEditor.UIElements;
using UnityEditor.UI;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(UIButtonCharaSelect))]
public class UIButtonCharaSelectEditor : ButtonEditor
{
    Color color;
    SerializedProperty customCharaData;
    public override void OnInspectorGUI()
    {
        UIButtonCharaSelect targetUIButton = (UIButtonCharaSelect)target;
        //customCharaData = serializedObject.FindProperty("characterData");
        //targetUIButton.bothPlayerSelectColor = EditorGUILayout.ColorField("Both player select",targetUIButton.bothPlayerSelectColor);
        //targetUIButton.p1SelectColor = EditorGUILayout.ColorField("Player 1 select", targetUIButton.p1SelectColor);
        //targetUIButton.p2SelectColor = EditorGUILayout.ColorField("Player 2 select", targetUIButton.p2SelectColor);

        
        //targetUIButton.characterData = (CharacterData)EditorGUILayout.ObjectField("Character Data", targetUIButton.characterData,typeof(CharacterData),true);

        base.OnInspectorGUI();


        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("characterData"), true);
        serializedObject.ApplyModifiedProperties();
    }
}
