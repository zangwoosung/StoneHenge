using System;
using UnityEngine;

public class TargetStoneManager : MonoBehaviour
{
    public GameObject stonePrefab;
    Vector3 pos;


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
        Debug.Log("stone create");
        pos.x = UnityEngine.Random.Range(2, 7);
        pos.z = UnityEngine.Random.Range(-10, 10);
        pos.y = 3;
        Instantiate(stonePrefab, pos, Quaternion.identity);

    }


}
