using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    Move move;
    SpotInfo spotInfo;

    public List<List<string>> text = new List<List<string>> { new List<string> { "얼음 넣기", "물 넣기" } };

    private void Start()
    {
        //클래스 참조
        move = GameObject.Find("Player").GetComponent<Move>();
        spotInfo = GetComponent<SpotInfo>();
    }

    private void Update()
    {
        if (spotInfo.holdingTime > spotInfo.maxHoldingTime && move.myObject != null && move.myObject.tag == "cup")
        {
            //홀딩 후 할 행동
            if (spotInfo.currentChoice == 0)
            {
                //얼음 넣기
                move.myObject.GetComponent<Cup>().cafeData.ice = 1;
            }
            else
            {
                //물 넣기
                move.myObject.GetComponent<Cup>().cafeData.water = 1;
            }
        }
    }

    public void SpaceAction()
    {
        //빈 공간
    }
}
