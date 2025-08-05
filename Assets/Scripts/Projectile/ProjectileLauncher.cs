using System;
using UnityEngine;
public class ProjectileLauncher : MonoBehaviour
{
    public static event Action<Vector3> OnContactEvent;
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform launchPoint;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] ProjectileSO projectileSO;

    [Header("Trajectory Display")]
    [SerializeField] float launchSpeed = 10f;
    [SerializeField] int linePoints = 200;
    [SerializeField] float timeIntervalInPoints = 0.01f;


    public bool isDrawing = true;

    private void Start()
    {
        FlyingStone.OnMissionComplete += OnMissionComplete;
    }

    private void OnMissionComplete()
    {
        gameManager.Restore();
        lineRenderer.enabled = true;
    }

    void Update()
    {
        if (!isDrawing) return;
        DrawTrajectory();
        CheckEndPoint();
    }

    public void ThrowStone()
    {
        //lineRenderer.enabled = false;
        Quaternion rot = launchPoint.rotation;
        rot.x = projectileSO.angleX;
        rot.y = projectileSO.angleY;
        rot.z = projectileSO.angleZ;
        var _projectile = Instantiate(projectilePrefab, launchPoint.position, rot);
        _projectile.GetComponent<Rigidbody>().linearVelocity = projectileSO.speed * launchPoint.up;
        _projectile.GetComponent<Rigidbody>().mass = projectileSO.mass;
        float spinForce = 0.1f;
        _projectile.GetComponent<Rigidbody>().AddTorque(Vector3.up * spinForce, ForceMode.Impulse);
        gameManager.SetTarget(_projectile.transform);


    }

    void DrawTrajectory()
    {
        Vector3 origin = launchPoint.position;
        Vector3 startVelocity = projectileSO.speed * launchPoint.up;

        lineRenderer.positionCount = linePoints;
        float time = 0;
        for (int i = 0; i < linePoints; i++)
        {
            var x = (startVelocity.x * time) + (Physics.gravity.x / 2 * time * time);
            var y = (startVelocity.y * time) + (Physics.gravity.y / 2 * time * time);
            var z = (startVelocity.z * time) + (Physics.gravity.z / 2 * time * time);
            Vector3 point = new Vector3(x, y, z);
            lineRenderer.SetPosition(i, origin + point);
            time += timeIntervalInPoints;
        }
    }

    void CheckEndPoint()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        int pointCount = lineRenderer.positionCount;

        for (int i = pointCount - 2; i < pointCount - 1; i++)
        {
            Vector3 start = lineRenderer.GetPosition(i);
            Vector3 end = lineRenderer.GetPosition(i + 1);
            Vector3 direction = (end - start).normalized;
            float distance = Vector3.Distance(start, end);

            RaycastHit hit;
            if (Physics.Raycast(start, direction, out hit, distance))
            {
                OnContactEvent?.Invoke(hit.point);
            }
        }
    }
}