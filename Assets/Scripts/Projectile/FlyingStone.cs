using System;
using UnityEngine;

public class FlyingStone : MonoBehaviour
{
    public static event Action OnMissionComplete;    
    // 발행인이 돌
    // 리스너: MainUI.  Why button 과 Slider를 Unlock. 

    private void Start()
    {
        TrailRenderer trail = gameObject.AddComponent<TrailRenderer>();
        trail.material = new Material(Shader.Find("Sprites/Default"));
        trail.startColor = Color.yellow;
        trail.endColor = Color.clear;
        trail.time = 0.5f; 
        trail.startWidth = 0.2f;
        trail.endWidth = 0f;
    }

    private void OnDisable()
    {
        OnMissionComplete?.Invoke();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //OnMissionComplete?.Invoke();
            Destroy(gameObject, 3);
        }

        if (collision.gameObject.CompareTag("Target"))
        {
            //OnMissionComplete?.Invoke();
            Destroy(gameObject, 3);
        }
    }

    
}
