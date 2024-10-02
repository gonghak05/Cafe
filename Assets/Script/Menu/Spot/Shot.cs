using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    //선택할 수 있는 아이템 -> 3개(포터필터, 샷1, 샷2)
    //할 수 있는 활동 -> 샷 내리기
    Move move;
    CurrentSpot currentSpot;
    SpotInfo spotInfo;
    bool isHoldItem;

    float shotSpeed = 20f;                        //샷 내려가는 시간
    bool isShotDown = false;                      //샷 내려가고 있나?

    public List<List<string>> text = new List<List<string>> { new List<string> { "아이템 가져가기", "샷 내리기" }, new List<string> { "포터필터", "왼쪽 샷잔", "오른쪽 샷잔" }, new List<string> { "왼쪽에 2샷", "오른쪽에 2샷", "샷 나눠내리기" } };

    //변수 참조
    private void Start()
    {
        move = GameObject.Find("Player").GetComponent<Move>();
        currentSpot = GameObject.Find("Player").GetComponent<CurrentSpot>();
        spotInfo = GetComponent<SpotInfo>();
    }

    //space바 눌렀을 때 액션 지정
    public void SpaceAction()
    {
        //플레이어가 특정 아이템을 가지고 있나 확인
        isHoldItem = move.myObject != null && ((move.myObject.tag == "portafilter" && spotInfo.myObject[0] == null) || (move.myObject.tag == "shotCup" && (spotInfo.myObject[1] == null || spotInfo.myObject[2] == null)));
        if (isShotDown) return;     //샷 내려가는 중이면 아무 활동도 실행 x

        //포터필터 또는 샷잔을 가지고 접근하면 무조건 장착만 가능하도록
        if (isHoldItem)
        {
            //포터필터 받기
            if (move.myObject.tag == "portafilter" && spotInfo.myObject[0] == null)
            {
                spotInfo.myObject[0] = move.myObject;
                move.myObject = null;
            }
            //샷잔 받기
            else
            {
                if (spotInfo.myObject[1] == null)
                {
                    spotInfo.myObject[1] = move.myObject;
                    move.myObject = null;
                }
                else if (spotInfo.myObject[2] == null)
                {
                    spotInfo.myObject[2] = move.myObject;
                    move.myObject = null;
                }
            }
        }
        else
        {
            //아이템선택 또는 샷 내리기 선택 화면
            if (spotInfo.currentState == 0)
            {
                //아이템 가져가기를 눌렀을 경우
                if (spotInfo.currentChoice == 0)
                {
                    spotInfo.currentState = 1;
                    spotInfo.currentChoice = 0;
                }
                //샷 내리기를 눌렀을 경우
                else if (spotInfo.currentChoice == 1)
                {
                    spotInfo.currentState = 2;
                    spotInfo.currentChoice = 0;
                }
            }
            //아이템 가져가기 선택 화면
            else if (spotInfo.currentState == 1)
            {
                if (spotInfo.myObject[spotInfo.currentChoice] != null && move.myObject == null)     //오브젝트 주기
                {
                    move.myObject = spotInfo.myObject[spotInfo.currentChoice];
                    spotInfo.myObject[spotInfo.currentChoice] = null;
                }
                spotInfo.isUIClear = true;            }
            //샷 내리기 선택 화면
            else if (spotInfo.currentState == 2 && spotInfo.myObject[0].GetComponent<Portafilter>().currentStep == 2)
            {
                if (spotInfo.currentChoice == 0)
                {
                    if (spotInfo.myObject[1] != null)
                    {
                        StartCoroutine("ShotDown", shotSpeed);
                    }
                }
                else if (spotInfo.currentChoice == 1)
                {
                    if (spotInfo.myObject[2] != null)
                    {
                        StartCoroutine("ShotDown", shotSpeed);
                    }
                }
                else if (spotInfo.currentChoice == 2)
                {
                    if (spotInfo.myObject[1] != null)
                    {
                        StartCoroutine("ShotDown", shotSpeed);
                    }
                    if (spotInfo.myObject[2] != null)
                    {
                        StartCoroutine("ShotDown", shotSpeed);
                    }
                }
                spotInfo.isUIClear = true;
            }
        }
    }

    IEnumerator ShotDown(float time)
    {
        isShotDown = true;
        yield return new WaitForSeconds(time);
        isShotDown = false;
        bool isDecaf = spotInfo.myObject[0].GetComponent<Portafilter>().isDecaf == 1;

        switch (spotInfo.currentChoice)
        {
            case 0:
                spotInfo.myObject[1].GetComponent<ShotCup>().currentShot += 2;
                if (isDecaf) spotInfo.myObject[1].GetComponent<ShotCup>().isDecaf = 1;
                break;
            case 1:
                spotInfo.myObject[2].GetComponent<ShotCup>().currentShot += 2;
                if (isDecaf) spotInfo.myObject[2].GetComponent<ShotCup>().isDecaf = 1;
                break;
            case 2:
                if (spotInfo.myObject[1] != null)
                {
                    spotInfo.myObject[1].GetComponent<ShotCup>().currentShot += 1;
                    if (isDecaf) spotInfo.myObject[1].GetComponent<ShotCup>().isDecaf = 1;
                }
                if (spotInfo.myObject[2] != null)
                {
                    spotInfo.myObject[2].GetComponent<ShotCup>().currentShot += 1;
                    if (isDecaf) spotInfo.myObject[2].GetComponent<ShotCup>().isDecaf = 1;
                }
                break;
        }
        spotInfo.myObject[0].GetComponent<Portafilter>().currentStep = 3;
    }
}
