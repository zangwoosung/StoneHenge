using System;
using TMPro;
using UnityEngine;

public class TargetStoneManager : MonoBehaviour
{
    public event Action OnStageClearEvent;
    public GameObject stonePrefab;
    [SerializeField] QuadCreator quadCreator;
    [SerializeField] RaycastAtHeight raycastAtHeight;
    [SerializeField] int count = default(int);
    [SerializeField] GameObject worldUIPrefab;
    Vector3 scale = new Vector3(1f, 1f, 1f);
    Vector3 pos;
    float newMass = 1;
    int clearCount = 3;

    TextMeshProUGUI label01;
    TextMeshProUGUI label02;
    public void OnReset()
    {

        count = 0;
    }
    private void Start()
    {
        quadCreator.CreateQuad();
        TargetStone.OnKnockDownEvent += TargetStone_OnKnockDownEvent;
        raycastAtHeight.OnStoneHasFallenEvent += RaycastAtHeight_OnStoneHasFallenEvent;
    }

    private void RaycastAtHeight_OnStoneHasFallenEvent()
    {
        TargetStone_OnKnockDownEvent(StoneType.High);
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
        quadCreator.CreateQuad();
        //range 
        pos = quadCreator.GetArea();
        pos.y = 0.1f;

        //size
        stonePrefab.transform.localScale = scale;

        //mass
        Rigidbody rb = stonePrefab.GetComponent<Rigidbody>();
        if (rb != null) rb.mass = newMass;

        var clone = Instantiate(stonePrefab, pos, Quaternion.identity);
        raycastAtHeight.Init(clone);


    
    }

    public void ResetValue()
    {
        //size
        scale = new Vector3(scale.x += 0.1f, scale.y += 0.3f, scale.z += 0.3f);

        //mass
        newMass += 1;
        quadCreator.CreateQuad();

    }

}
