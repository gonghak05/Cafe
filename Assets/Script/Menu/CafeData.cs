using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CafeData
{
    public int cupSize;     //컵 사이즈(20,24,32)
    public int shot;        //샷 크기(0,1,2,3,4)
    public int water;       //물 존재여부(0,1)
    public int ice;         //얼음 존재여부(0,1,2?)

    public CafeData(int cupSize = 24, int shot = 0, int water = 0, int ice = 0)
    {
        this.cupSize = cupSize;
        this.shot = shot;
        this.water = water;
        this.ice = ice;
    }
}
