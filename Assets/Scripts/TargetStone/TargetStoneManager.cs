using System;
using UnityEngine;

public class TargetStoneManager : MonoBehaviour
{
    public GameObject stonePrefab;

    Vector3 pos;
    
    public float minX, maxX;
   
    public float  minZ, maxZ; 


    private void Start()
    {
        TargetStone.OnKnockDownEvent += TargetStone_OnKnockDownEvent;
    }

    private void TargetStone_OnKnockDownEvent(StoneType obj)
    {
        CreateOneTargeStone();
    }

    public void CreateOneTargeStone()
    {
       
        pos.x = UnityEngine.Random.Range(minX, maxX);
        pos.z = UnityEngine.Random.Range(minZ, maxZ );
        pos.y = 3;
        Instantiate(stonePrefab, pos, Quaternion.identity);

    }


}
