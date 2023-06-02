using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPaperManager : MonoBehaviour
{
    [SerializeField] UIPaper[] uIPapers;
    Dictionary<UIPaperType, UIPaper> dicoPaper;
    [SerializeField] UIPaper currentPaper;
    // Start is called before the first frame update
    void Start()
    {
        dicoPaper = new Dictionary<UIPaperType, UIPaper>();
        for (int i = 0; i < uIPapers.Length; i++)
        {
            dicoPaper.TryAdd(uIPapers[i].paperType, uIPapers[i]);
            uIPapers[i].InstantShow(false);
        }
        currentPaper = dicoPaper[UIPaperType.Main];
        currentPaper.ShowPaper(true);
    }

    public void ClickOnPaper(UIPaper paperType)
    {
        if (currentPaper != paperType)
        {
            currentPaper.ShowPaper(false);
            dicoPaper[paperType.paperType].ShowPaper(true);
            currentPaper = dicoPaper[paperType.paperType];
        }
    }
}
