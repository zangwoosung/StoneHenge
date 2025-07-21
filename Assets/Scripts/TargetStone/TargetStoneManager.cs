using System;
using UnityEngine;

public class TargetStoneManager : MonoBehaviour
{
    public static event Action OnStageClearEvent;
    public GameObject stonePrefab;
    [SerializeField] QuadCreator quadCreator;
    [SerializeField] int count = default(int);

    Vector3 scale = new Vector3(1f, 1f, 1f);
    Vector3 pos;
    float newMass = 1;
    int clearCount = 3;


    private void OnEnable()
    {
        quadCreator.CreateQuad();

    }
    private void Start()
    {
        pos = quadCreator.GetArea();
        TargetStone.OnKnockDownEvent += TargetStone_OnKnockDownEvent;
    }

    private void TargetStone_OnKnockDownEvent(StoneType obj)
    {
        count++;
        if (count == clearCount)
        {
            count = 0;
            ResetValue();
            OnStageClearEvent?.Invoke();
            return;
        }

        GameObject[] objects = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject item in objects)
        {
            Destroy(item);
        }

        CreateOneTargeStone();
    }

    public void CreateOneTargeStone()
    {
        //range 
        pos = quadCreator.GetArea();
        pos.y = 0.1f;

        //size
        stonePrefab.transform.localScale = scale;

        //mass
        Rigidbody rb = stonePrefab.GetComponent<Rigidbody>();
        if (rb != null) rb.mass = newMass;

        Instantiate(stonePrefab, pos, Quaternion.identity);

    }

    public void ResetValue()
    {
        //size
        scale = new Vector3(scale.x += 0.1f, scale.y += 0.3f, scale.z += 0.3f);

        //mass
        newMass += 1;

        quadCreator.CreateQuad();
        // pos = quadCreator.GetArea();    

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {

            GameObject[] objects = GameObject.FindGameObjectsWithTag("Target");
            foreach (GameObject obj in objects)
            {
                Destroy(obj);
            }
            ResetValue();
            CreateOneTargeStone();

        }
        if (Input.GetKey(KeyCode.Z))
        {
            TargetStone_OnKnockDownEvent(StoneType.Middle);
        }
    }

}
//private void Update()
//{
//    if (Input.GetKey(KeyCode.B))
//    {
//        SpawnInside();
//    }
//    if (Input.GetKey(KeyCode.C))
//    {
//        Setup(5, 5);
//    }
//}