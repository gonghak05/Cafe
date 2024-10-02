using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//오브젝트의 위치, 선택지 정보 저장하는 스크립트
public class SpotInfo : MonoBehaviour
{
    //값 인스펙터 창에서 변경해야하는 변수들
    [Header("SpotSettings")]
    public int currentSpot;                 //현재위치
    [SerializeField] Component component;   //각 오브젝트의 고유 컴포넌트
    public bool isHolding;                  //스페이스바를 누르고 있어야 하는 행동?
    public float maxHoldingTime;             //스페이스바 누르는 시간

    public List<GameObject> myObject;       //현재 가지고있는 아이템
    public List<int> choiceCount;           //각 state별로 선택지 개수 몇개인지 저장 -> 이 개수로 최대 state 개수 확인 가능
    public List<string> tagList;            //무슨 아이템 장착 가능한지 리스트(장착할 수 있는 아이템 칸수에 맞춰서 적기)

    [HideInInspector] public int currentState;          //현재 어떤 선택 단계인지
    [HideInInspector] public int currentChoice;         //선택 단계에서 무엇을 골랐는지

    [HideInInspector] public bool isUIClear = false;    //마지막 행동인지 -> 마지막이면 UI 더이상 안띄워도 되기 때문

    [HideInInspector] public float holdingTime = 0;     //스페이스바 홀딩 액션 구현시 이용 -> 몇 초동안 홀딩하고 있었는지

    [HideInInspector] public List<List<string>> text;   //텍스트 데이터 저장


    //컴포넌트와 텍스트 데이터 리셋
    //오브젝트 추가될때마다 수정해야함
    public void ResetData()
    {
        if (currentSpot == 10 || currentSpot == 11)
        {
            Grinder mainComponent = component as Grinder;
            text = mainComponent.text;
        }
        else if (currentSpot == 12)
        {
            Tamping mainComponent = component as Tamping;
            text = mainComponent.text;
        }
        else if (currentSpot == 13 || currentSpot == 14)
        {
            Shot mainComponent = component as Shot;
            text = mainComponent.text;
        }
        else if (currentSpot == 16)
        {
            CupSpawn mainComponent = component as CupSpawn;
            text = mainComponent.text;
        }
        else if (currentSpot == 17)
        {
            Ice mainComponent = component as Ice;
            text = mainComponent.text;
        }
        currentState = 0;
        currentChoice = 0;
    }

    public void SpaceAction()
    {
        if (currentSpot == 10 || currentSpot == 11)
        {
            Grinder mainComponent = component as Grinder;
            mainComponent.SpaceAction();
        }
        else if (currentSpot == 12)
        {
            Tamping mainComponent = component as Tamping;
            mainComponent.SpaceAction();
        }
        else if (currentSpot == 13 || currentSpot == 14)
        {
            Shot mainComponent = component as Shot;
            mainComponent.SpaceAction();
        }
        else if (currentSpot == 16)
        {
            CupSpawn mainComponent = component as CupSpawn;
            mainComponent.SpaceAction();
        }
        else if (currentSpot == 17)
        {
            Ice mainComponent = component as Ice;
            mainComponent.SpaceAction();
        }
    }
}
