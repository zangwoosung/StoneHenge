using System;
using UnityEngine;

public class TargetStoneManager : MonoBehaviour
{
    public event Action OnStageClearEvent;
    public GameObject stonePrefab;
    [SerializeField] QuadCreator quadCreator;
    [SerializeField] RaycastAtHeight raycastAtHeight;
    [SerializeField] int count = default(int);
    Vector3 scale = new Vector3(1f, 1f, 1f);
    Vector3 pos;
    float newMass = 1;
    int clearCount = 3;  
    public void OnReset()
    {
        count = 0;
    }
    private void Start()
    {
        raycastAtHeight.OnStoneHasFallenEvent += RaycastAtHeight_OnStoneHasFallenEvent;
    }

    private void RaycastAtHeight_OnStoneHasFallenEvent()
    {
        count++;
        if (count == clearCount)
        {
            
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

    float ww = 4, hh = 4;
    public void CreateOneTargeStone()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Target");
        if (target != null)
            Destroy(target);

        quadCreator.Setup(ww,hh);
        quadCreator.CreateQuad();
        //range 
        pos = quadCreator.GetRandomPoint();
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
        quadCreator.Setup(5f,5f);
        count = 0;
        //size
        scale = new Vector3(scale.x += 0.1f, scale.y += 0.3f, scale.z += 0.3f);       
        newMass += 1;   

    }    

}
