using UnityEngine;

using System;

public class VFXController : MonoBehaviour
{
    private Action<Collision> collisionCallback;

    public void SetCallback(Action<Collision> callback)
    {
        collisionCallback = callback;
    }

    void OnCollisionEnter(Collision collision)
    {
        collisionCallback?.Invoke(collision);
    }
}
