using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    public CafeData cafeData;

    private void OnEnable()
    {
        cafeData = new CafeData();
    }

    /*
    private void Update()
    {
        Debug.Log(string.Format("얼음 {0}, 물 {1}", cafeData.ice, cafeData.water));
    } */
}
