using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupSpawn : MonoBehaviour
{
    [SerializeField] GameObject cup;
    [SerializeField] List<GameObject> pool;
    [SerializeField] Transform parentTransform;

    Move move;
    SpotInfo spotInfo;

    public List<List<string>> text = new List<List<string>> { new List<string> { "20온즈", "24온즈", "32온즈" }};

    private void Start()
    {
        pool = new List<GameObject>();

        move = GameObject.Find("Player").GetComponent<Move>();
        spotInfo = GetComponent<SpotInfo>();
    }

    public void SpaceAction()
    {
        //플레이어가 빈 손인 경우에만 아이템 받을 수 있음
        if (move.myObject != null) return;

        GameObject cupObject = GetObjectFromPool();

        //선택한 옵션별로 컵 사이즈 다르게 설정
        if (spotInfo.currentChoice == 0)
        {
            cupObject.GetComponent<Cup>().cafeData.cupSize = 20;
        }
        else if (spotInfo.currentChoice == 1)
        {
            cupObject.GetComponent<Cup>().cafeData.cupSize = 24;
        }
        else
        {
            cupObject.GetComponent<Cup>().cafeData.cupSize = 32;
        }

        //컵 제공
        move.myObject = cupObject;
        spotInfo.isUIClear = true;
    }

    //오브젝트풀링으로 컵 구현
    public GameObject GetObjectFromPool()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // 만약 모든 오브젝트가 사용 중이라면 새로 생성하여 풀에 추가
        GameObject newObj = Instantiate(cup, parentTransform);
        newObj.SetActive(true);
        pool.Add(newObj);
        return newObj;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
}
