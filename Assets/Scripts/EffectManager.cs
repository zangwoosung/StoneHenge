using System;
using UnityEngine;
using UnityEngine.UIElements;

public class EffectManager : MonoBehaviour
{
   
    public GameObject particlePrefab; // Assign your prefab in Inspector
    public GameObject firefab; // Assign your prefab in Inspector

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TargetStone.OnHitDistanceEvent += OnHitDistanceEvent;
        TargetStone.OnHitContactEvent += OnHitContactEvent;
    }

    public  void OnHitContactEvent(Vector3 pos)
    {

        GameObject psInstance = Instantiate(particlePrefab, pos, Quaternion.identity);
        ParticleSystem ps = psInstance.GetComponent<ParticleSystem>();
        ps.Play();

        Destroy(psInstance, ps.main.duration + ps.main.startLifetime.constant);
    }

    public void OnHitDistanceEvent(Transform obj, float degree)
    {

    }





    public void SpawnParticles(Vector3 position)
    {
        // Optional cleanup
    }
}




