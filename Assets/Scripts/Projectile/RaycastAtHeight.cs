using UnityEngine;
using static UnityEngine.UI.Image;

public class RaycastAtHeight : MonoBehaviour
{
    public Transform target;      // Assign the target point in the Inspector
    public float maxDistance = 100f;

    Vector3 origin;
    Vector3 direction;

    Vector3 TargetHeight;
    Vector3 GetTargetHeight()
    {      
        Renderer renderer = target.GetComponent<Renderer>();    

        Bounds bounds = renderer.bounds;      
        Vector3 origin = new Vector3(
            bounds.center.x,
            bounds.min.y + bounds.size.y * (4f / 5f),
            bounds.center.z
        );
        myHeight = origin.y;
        return origin;
    }

    float myHeight = 0;

    void Start()
    {
        origin = transform.position;
        TargetHeight= GetTargetHeight();
        origin.y = TargetHeight.y;
        direction = (TargetHeight - origin).normalized;

    }

    float lapTime = 0;
    void Update()
    {
        origin = transform.position;
        origin.y = myHeight;


        Ray ray = new Ray(origin, direction);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            Debug.Log("Hit: " + hit.collider.name);
        }
        else
        {
            lapTime += Time.deltaTime;
            if(lapTime > 1)
            {
                //has fallen down. 

            } 

            Debug.Log("Not Hit: ");
        }

        Debug.DrawRay(origin, direction * maxDistance, Color.red);
    }
}
