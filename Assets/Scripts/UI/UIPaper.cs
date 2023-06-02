using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPaper : MonoBehaviour
{
    public UIPaperType paperType;
    Renderer render;
    Material dissolveShader;
    [SerializeField] float fadeSpeed = 3f;
    private void Start()
    {
        if (!render) render = GetComponent<Renderer>();
        dissolveShader = render.material;
        //dissolveShader.SetFloat("_Amount", 0f);
    }
    public void ShowPaper(bool on)
    {
        if (on)
        {
            StartCoroutine(FadePaper(fadeSpeed, 0f));
        }
        else
        {
            StartCoroutine(FadePaper(fadeSpeed, 1f));
        }
    }

    IEnumerator FadePaper(float duration,float endFloat)
    {
        for (float i = 0f; i < 1f; i+=Time.deltaTime/duration)
        {
            dissolveShader.SetFloat("_Amount", Mathf.Lerp(1 - endFloat, endFloat, i));
            yield return null;
        }
        dissolveShader.SetFloat("_Amount", endFloat);
    }

    public void InstantShow(bool on)
    {
        if (!render) render = GetComponent<Renderer>();
        dissolveShader = render.material;
        if (on)
        {
            dissolveShader.SetFloat("_Amount", 0f);
        }
        else
        {
            dissolveShader.SetFloat("_Amount", 1f);
        }
        
    }
}
