using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target = null;      // Assign the projectile here
    public Vector3 offset = new Vector3(0, 2, -5); // Adjust as needed
    public float smoothSpeed = 5f;
    Vector3 originPos;
    Quaternion rotation;

    public bool isFollowTarget = false;
    private void OnEnable()
    {
        originPos = transform.position;
        rotation = transform.rotation;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
        smoothSpeed = 5f;
        isFollowTarget = true;
    }
    public void Restore()
    {
        transform.position= originPos;
        transform.rotation= rotation;
        isFollowTarget= false;
    }

    void LateUpdate()
    {
        if (!isFollowTarget) return;
        Vector3 desiredPosition;
        try
        {
            desiredPosition = target.position + offset;
        }
        catch (System.Exception)
        {            
            desiredPosition = originPos;
            smoothSpeed = 10f;
        }  
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(target);
    }
}

