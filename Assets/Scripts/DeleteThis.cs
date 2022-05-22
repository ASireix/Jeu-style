using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteThis : MonoBehaviour
{
    public Image image;
    public float maxCountDown;
    float timeRemainingUntilChange;

    // Update is called once per frame
    void Update()
    {
        if (timeRemainingUntilChange > 0)
        {
            timeRemainingUntilChange -= Time.deltaTime;
        }
        image.fillAmount = timeRemainingUntilChange/10;
        if (timeRemainingUntilChange <= 0)
        {
            timeRemainingUntilChange = maxCountDown;
        }
    }
}
