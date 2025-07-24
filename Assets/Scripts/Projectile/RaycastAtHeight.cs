using System;
using Unity.VisualScripting;
using UnityEngine;

public class RaycastAtHeight : MonoBehaviour
{
    public event Action OnStoneHasFallenEvent;
    public static event Action OnNoStoneStandingEvent;
    public float maxDistance = 100f;
    public GameObject objectA;
    public GameObject objectB;
    public float speed = 15;
    public float lapTime = 5;
    float myHeight = 0;
    Color statusColor = Color.blue;
    Vector3 SourcePos;
    bool isLocked = false;

    private void Start()
    {
        SourcePos = objectA.transform.position;
    }
    void GetTargetHeight(Renderer ren)
    {
        Renderer renderer = ren;
        Bounds bounds = renderer.bounds;
        Vector3 origin = new Vector3(
            bounds.center.x,
            bounds.min.y + bounds.size.y * (4f / 5f),
            bounds.center.z
        );
        myHeight = origin.y;

    }

    public void Init(GameObject obj)
    {
       
        objectB = obj;
        GetTargetHeight(objectB.GetComponent<Renderer>());
        isLocked = true;
    }
    public (Vector3 left, Vector3 right) GetEdgePoints()
    {
        Renderer rendererB = objectB.GetComponent<Renderer>();
        Bounds bounds = rendererB.bounds;
        Vector3 leftPoint;
        Vector3 rightPoint;
        leftPoint = bounds.min;
        leftPoint.y = myHeight;
        rightPoint = bounds.max;
        rightPoint.y = myHeight;

        return (leftPoint, rightPoint);
    }

    private void Update()
    {
        Debug.DrawRay(SourcePos, objectA.transform.forward * maxDistance, statusColor);
        if (!isLocked) return;

        int rayCount = 3;
        var (leftPoint, rightPoint) = GetEdgePoints();

        for (int i = 0; i <= rayCount; i++)
        {
            float t = i / (float)rayCount;
            Vector3 pointOnEdge = Vector3.Lerp(leftPoint, rightPoint, t);
            Vector3 direction = (pointOnEdge - SourcePos).normalized;

            if (Physics.Raycast(SourcePos, direction, out RaycastHit hit))
            {
                statusColor = Color.green;
                //Debug.Log($"Ray {i} hit: {hit.collider.name}");
                lapTime = 0;
            }
            else
            {
                lapTime += Time.deltaTime;
                if (lapTime > 6)
                {
                    isLocked = false;
                    lapTime = 0;
                    statusColor = Color.yellow;
                    OnNoStoneStandingEvent?.Invoke();
                    Debug.Log("It has fallen");
                }
               // Debug.Log($"Ray {i} not hit: ");
            }
            Debug.DrawRay(SourcePos, direction * maxDistance, statusColor);
        }

    }



}
