using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;

public class UIButtonCharaSelect : Button
{
    int numberOfSelected = 0;
    public Color bothPlayerSelectColor;
    public Color p1SelectColor;
    public Color p2SelectColor;

    public CharacterData characterData;

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        CharaSelectMenuManager.instance.UpdateCharacterPreview(eventData.currentInputModule, characterData);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
    }

    public override void OnSubmit(BaseEventData eventData)
    {
        base.OnSubmit(eventData);
        CharaSelectMenuManager.instance.SetPlayer(eventData.currentInputModule, characterData);
        Debug.Log("I am subitting");
    }

    

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        //base.DoStateTransition(state, instant);
        if (state == SelectionState.Selected)
        {
            numberOfSelected++;
        }
        else
        {
            numberOfSelected = (numberOfSelected-- <= 0) ? 0 : numberOfSelected--;
        }

        Color color;
        switch (state)
        {
            case SelectionState.Normal:
                if (numberOfSelected == 0)
                {
                    color = this.colors.normalColor;
                }
                else
                {
                    color = this.colors.selectedColor;
                }
                
                break;
            case SelectionState.Selected:
                if (numberOfSelected > 1)
                {
                    color = CharaSelectMenuManager.instance.bothPlayerSelectColor;
                }
                else
                {
                    color = this.colors.selectedColor;
                }
                break;
            case SelectionState.Highlighted:
                color = this.colors.highlightedColor;
                break;
            case SelectionState.Pressed:
                color = this.colors.pressedColor;
                break;
            case SelectionState.Disabled:
                color = this.colors.disabledColor;
                break;
            default:
                color = Color.black;
                break;
        }

        if (base.gameObject.activeInHierarchy)
        {
            switch (this.transition)
            {
                case Selectable.Transition.ColorTint:
                    ColorTween(color * this.colors.colorMultiplier, instant);
                    break;
            }
        }

    }

    private void ColorTween(Color targetColor, bool instant)
    {
        if (this.targetGraphic == null)
        {
            this.targetGraphic = this.image;
        }

        base.image.CrossFadeColor(targetColor, (!instant) ? this.colors.fadeDuration : 0f, true, true);

    }
}
