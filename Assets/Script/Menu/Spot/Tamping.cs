using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tamping : MonoBehaviour
{
    //기본으로 필요한 컴포넌트 가져옴
    Move move;
    SpotInfo spotInfo;

    public List<List<string>> text = new List<List<string>> { new List<string> { "탬핑 또는 찌꺼기 버리기"} };


    private void Start()
    {
        //클래스 참조
        move = GameObject.Find("Player").GetComponent<Move>();
        spotInfo = GetComponent<SpotInfo>();
    }

    private void Update()
    {
        if (spotInfo.holdingTime > spotInfo.maxHoldingTime)
        {
            if (move.myObject != null && move.myObject.tag == "portafilter")
            {
                Portafilter portafilter = move.myObject.GetComponent<Portafilter>();
                if (portafilter.currentStep == 1)
                {
                    portafilter.currentStep = 2;
                }
                else if (portafilter.currentStep == 3)
                {
                    portafilter.currentStep = 0;
                }
            }
        }
    }

    public void SpaceAction()
    {
        
    }
}
