using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//현재 위치값 저장 & 행동 관리
public class CurrentSpot : MonoBehaviour
{
    [SerializeField] ChoiceOptionManager choiceUI;      //UI 띄우는 역할
    [SerializeField] Move move;

    public int currentSpotIndex;                        //현재 플레이어의 위치 데이터
    public GameObject currentSpot;                      //현재 플레이어의 위치 오브젝트
    SpotInfo spotInfo = null;

    int spotCount = 0;                                  //콜라이더 밖에서 UI 띄우지 않게 하기 위한 변수

    //위치 접근했을 때 처리 -> 현재 위치 갱신 & 데이터초기화
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Spot")
        {
            //spot 데이터 초기화
            spotInfo = collision.GetComponent<SpotInfo>();
            spotInfo.ResetData();

            //위치, 오브젝트 데이터 받아옴
            currentSpotIndex = spotInfo.currentSpot;
            currentSpot = collision.gameObject;
            spotCount += 1;

            //초반 UI 띄우기... 특정 아이템 가지고 있으면 따로 UI 띄움
            if (TagCheck(spotInfo)) choiceUI.ChoiceOption(1, new List<string> { "아이템 장착" });
            else choiceUI.ChoiceOption(spotInfo.choiceCount[spotInfo.currentState], spotInfo.text[spotInfo.currentState]);

            //UI Hold 초기화(스페이스 누른 상태로 위치 전환 예외 처리)
            choiceUI.DontHoldSpace(spotInfo.currentChoice);
        }
    }

    //UI 뜨는거 관리
    private void OnTriggerExit2D(Collider2D collision)
    {
        //콜라이더 밖일 때 UI관리
        if (collision.tag == "Spot") spotCount -= 1;
        if (spotCount == 0)
        {
            UIDelete();
        }
    }

    private void Update()
    {
        //currentChoice 값 변경 알고리즘.. 최대값 넘지 안도록 조절(choiceCount에 각각의 state의 최대값 저장되어있음)
        if (Input.GetKeyDown(KeyCode.E) && spotCount != 0)
        {
            //변경 시 기존 홀드 중단
            choiceUI.DontHoldSpace(spotInfo.currentChoice);

            //전환
            if (spotInfo.choiceCount[spotInfo.currentState] - 1 == spotInfo.currentChoice)
            {
                spotInfo.currentChoice = 0;
            }
            else spotInfo.currentChoice += 1;

            //지금 선택된 UI 강조
            choiceUI.ChangeColor(spotInfo.currentChoice, spotInfo.choiceCount[spotInfo.currentState]);
        }

        //액션 알고리즘
        if (Input.GetKeyDown(KeyCode.Space) && spotCount != 0)
        {
            spotInfo.SpaceAction();                                     //액션 실행
            choiceUI.ChoiceOption(spotInfo.choiceCount[spotInfo.currentState], spotInfo.text[spotInfo.currentState]);   //실행 후 UI 초기화
            choiceUI.ChangeColor(spotInfo.currentChoice, spotInfo.choiceCount[spotInfo.currentState]);

            //끝나는 액션이면 UI 지우기
            if (spotInfo.isUIClear)
            {
                UIDelete();

                //만약 아이템 가져가는 액션이면 바로 장착 UI 띄우기
                if (TagCheck(spotInfo)) choiceUI.ChoiceOption(1, new List<string> { "아이템 장착" });

                spotInfo.isUIClear = false;
            }

            //스페이스바 홀딩 액션 있는 장소면 홀딩 UI 시작
            if (spotInfo.isHolding) choiceUI.HoldSpace(spotInfo, spotInfo.currentChoice);
        }

        //스페이스바 홀딩 기능 구현
        if (Input.GetKey(KeyCode.Space) && spotCount != 0 && spotInfo.isHolding)
        {
            spotInfo.holdingTime += Time.deltaTime;
        }
        //홀딩 기능 종료 구현
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (spotInfo != null) spotInfo.holdingTime = 0;
            choiceUI.DontHoldSpace(spotInfo.currentChoice);
        }
    }

    //해당하는 테그 있는지 확인
    bool TagCheck(SpotInfo spotInfo)
    {
        bool result = false;
        if (move.myObject == null)
        {

        }
        else
        {
            int i = 0;
            foreach (string tagName in spotInfo.tagList)
            {
                if (move.myObject.tag == tagName && currentSpot.GetComponent<SpotInfo>().myObject[i] == null) result = true;
                i += 1;
            }
        }
        return result;
    }

    public void UIDelete()
    {
        choiceUI.ChoiceOption(0, null);
    }
}
