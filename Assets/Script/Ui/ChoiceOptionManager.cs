using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ChoiceOptionManager : MonoBehaviour
{
    [SerializeField] GameObject prefabUI;

    public List<GameObject> UI = new List<GameObject>();

    SpotInfo spotInfo; 
    TMP_Text tmp;

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            UI.Add(Instantiate(prefabUI, this.transform));
            if (UI[i] != null) UI[i].SetActive(false);
        }
    }
    public void ChoiceOption(int count, List<string> textList)
    {
        //모든 UI 먼저 비활성화
        for (int i = 0; i < UI.Count; i++)
        {
            if (UI[i] != null) UI[i].SetActive(false);
        }

        //개수만큼 다시 활성화
        for (int i = 0; i < count; i++)
        {
            if (UI[i] != null) UI[i].SetActive(true);
            UI[i].GetComponentInChildren<TMP_Text>().text = textList[i];
        }

        //하이라이트
        ChangeColor(0, count);
    }

    public void ChangeColor(int index, int maxCount)
    {
        for (int i = 0; i < maxCount; i++)
        {
            if (UI[i] != null) UI[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, 33f / 255f);
        }
        if (UI[index] != null) UI[index].GetComponent<Image>().color = new Color(160f / 255f, 160f / 255f, 160f / 255f, 160f / 255f);
    }

    public void HoldSpace(SpotInfo spotInfo, int index)
    {
        SpaceHoldManager space = UI[index].GetComponent<SpaceHoldManager>();
        space.isHold = true;
        space.spotInfo = spotInfo;
    }

    public void DontHoldSpace(int index)
    {
        SpaceHoldManager space = UI[index].GetComponent<SpaceHoldManager>();
        space.image.fillAmount = 0;
        space.isHold = false;
    }

    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(GameObject.Find("Player").transform.position + new Vector3(1f,0f, 0f));
    }
}
