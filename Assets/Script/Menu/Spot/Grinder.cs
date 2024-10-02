using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grinder : MonoBehaviour
{
    [SerializeField] bool isDecaf;

    Move move;
    CurrentSpot currentSpot;
    SpotInfo spotInfo;

    bool isHoldItem;
    public List<List<string>> text = new List<List<string>> { new List<string> { "원두 내리기", "포터필터 가져가기" }};

    float speed = 5f;                                 //원두 내려가는 시간
    bool isPlaying = false;                           //원두 내려가고 있나?

    private void Start()
    {
        //클래스 참조
        move = GameObject.Find("Player").GetComponent<Move>();
        currentSpot = GameObject.Find("Player").GetComponent<CurrentSpot>();
        spotInfo = GetComponent<SpotInfo>();
    }

    public void SpaceAction()
    {
        //플레이어가 특정 아이템을 가지고 있나 확인
        isHoldItem = move.myObject != null && move.myObject.tag == "portafilter";
        if (isPlaying) return;     //원두 내려가는 중이면 아무 활동도 실행 x

        //포터필터 또는 샷잔을 가지고 접근하면 무조건 장착만 가능하도록
        if (isHoldItem)
        {
            //포터필터 받기
            if (move.myObject.tag == "portafilter" && spotInfo.myObject[0] == null)
            {
                spotInfo.myObject[0] = move.myObject;
                move.myObject = null;
            }
        }
        else
        {
            //원두 추출하기
            if (spotInfo.currentChoice == 0)
            {
                if (spotInfo.myObject[0] != null && spotInfo.myObject[0].GetComponent<Portafilter>().currentStep == 0)
                {
                    StartCoroutine("Action");
                }
            }
            //포터필터주기
            else if (spotInfo.currentChoice == 1)
            {
                if (spotInfo.myObject[0] != null && move.myObject == null)     //오브젝트 주기
                {
                    move.myObject = spotInfo.myObject[0];
                    spotInfo.myObject[0] = null;
                }
                spotInfo.isUIClear = true;
            }
            spotInfo.isUIClear = true;
        }
    }

    //원두 추출 액션 구현
    IEnumerator Action()
    {
        isPlaying = true;
        yield return new WaitForSeconds(speed);
        isPlaying = false;

        spotInfo.myObject[0].GetComponent<Portafilter>().currentStep = 1;
        if (isDecaf) spotInfo.myObject[0].GetComponent<Portafilter>().isDecaf = 1;
    }
}
