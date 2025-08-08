using System;
using System.Collections.Generic;
using UnityEngine;

public struct StoneDataStruct  // 
{
    public float width, height;
    public int mass;
    public Vector3 scale;

}
public class TargetStoneManager : MonoBehaviour
{
    public event Action OnStageClearEvent;
    public GameObject[] stonePrefabs;
    [SerializeField] QuadCreator quadCreator;
    [SerializeField] int count = default(int);

    Vector3 pos;

    int level = 0;
    int stoneIndex = 0;
    int clearCount = 3;
    List<StoneDataStruct> stoneDatas;
    public void OnReset()
    {
        count = 0;
    }

    private void OnEnable()
    {
        stoneDatas = InitStoneData();

    }
    private void Start()
    {
        TargetStone.OnKnockDownEvent += TargetStone_OnKnockDownEvent;

    }
    List<StoneDataStruct> InitStoneData()
    {
        List<StoneDataStruct> output = new List<StoneDataStruct>();
        StoneDataStruct levelOne = new StoneDataStruct
        {
            width = 2.5f,
            height = 2.2f,
            mass = 10,
            scale = new Vector3(4f, 4f, 4f)
        };
        StoneDataStruct levelTwo = new StoneDataStruct
        {
            width = 5.5f,
            height = 5.2f,
            mass = 20,
            scale = new Vector3(6f, 7f, 6f)
        };
        StoneDataStruct levelThree = new StoneDataStruct
        {
            width = 10.5f,
            height = 10.2f,
            mass = 30,
            scale = new Vector3(8f, 9f, 10f)
        };
        output.Add(levelOne);
        output.Add(levelTwo);
        output.Add(levelThree);
        return output;
    }
    private void TargetStone_OnKnockDownEvent(StoneType obj)
    {
        count++;
        if (count == clearCount)
        {
            level++;          
            OnStageClearEvent?.Invoke();
            return;
        }
        CreateOneTargeStone();
    }


    public void CreateOneTargeStone()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Target");
        if (target != null)
            Destroy(target);


        quadCreator.Setup(stoneDatas[level].width, stoneDatas[level].height);
        quadCreator.CreateQuad();
        //range 
        pos = quadCreator.GetRandomPoint();
        pos.y = 0.1f;


        GameObject stonePrefab = stonePrefabs[stoneIndex];
        //size
        stonePrefab.transform.localScale = stoneDatas[level].scale;

        //mass
        Rigidbody rb = stonePrefab.GetComponent<Rigidbody>();
        if (rb != null) rb.mass = stoneDatas[level].mass;

        var clone = Instantiate(stonePrefab, pos, Quaternion.identity);
        stoneIndex++;
        if (stoneIndex >=5 ) stoneIndex = 0;

    }
    public void ResetValue()
    {
        quadCreator.Setup(5f, 5f);
        count = 0;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Z))
        {
            count++;
            if (stoneIndex >= stonePrefabs.Length) stoneIndex = 0;
            if (count == clearCount)
            {
                level++;
                if (level >= 3) level = 0;
                ResetValue();
                OnStageClearEvent?.Invoke();
                return;
            }

            CreateOneTargeStone();
        }
    }





}
