using System;
using UnityEngine;

public class FlyingStone : MonoBehaviour
{
    public static event Action OnMissionComplete;    
   
    private void Start()
    {
        TrailRenderer trail = gameObject.AddComponent<TrailRenderer>();
        trail.material = new Material(Shader.Find("Sprites/Default"));
        trail.startColor = Color.red;
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
        SoundManager.Instance.PlaySFXByIndex(2);

        if (collision.gameObject.CompareTag("Ground"))
        {
           // SoundManager.Instance.PlaySFXByIndex(1);
            Destroy(gameObject, 3);
        }

        if (collision.gameObject.CompareTag("Target"))
        {
           // SoundManager.Instance.PlaySFXByIndex(3);
            //OnMissionComplete?.Invoke();
            Destroy(gameObject, 3);
        }
    }

    
}
