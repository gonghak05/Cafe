using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceHoldManager : MonoBehaviour
{
    public Image image;
    public SpotInfo spotInfo;
    public bool isHold = false;

    private void Update()
    {
        if (isHold)
        {
            if (spotInfo.holdingTime < spotInfo.maxHoldingTime)
            {
                image.fillAmount = spotInfo.holdingTime / spotInfo.maxHoldingTime;
            }
            else
            {
                image.fillAmount = 0;
            }
        }
    }
}
