using UnityEngine;

public class EdgeDistanceCalculator : MonoBehaviour
{
    public static float GetDistanceToNearestEdge(MeshCollider meshCollider, Vector3 worldHitPoint)
    {
        if (meshCollider == null || meshCollider.sharedMesh == null)
        {
            Debug.LogWarning("Invalid MeshCollider or mesh");
            return -1f;
        }

        Mesh mesh = meshCollider.sharedMesh;

        // Convert world hit point to local space
        Vector3 localHitPoint = meshCollider.transform.InverseTransformPoint(worldHitPoint);
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        float minDistance = float.MaxValue;

        // Loop through triangle edges
        for (int i = 0; i < triangles.Length; i += 3)
        {
            Vector3 v0 = vertices[triangles[i]];
            Vector3 v1 = vertices[triangles[i + 1]];
            Vector3 v2 = vertices[triangles[i + 2]];

            float d0 = DistancePointToSegment(localHitPoint, v0, v1);
            float d1 = DistancePointToSegment(localHitPoint, v1, v2);
            float d2 = DistancePointToSegment(localHitPoint, v2, v0);

            float min = Mathf.Min(d0, d1, d2);
            if (min < minDistance)
            {
                minDistance = min;
            }
        }

        return minDistance * meshCollider.transform.lossyScale.x; // scale-adjusted back to world
    }

    // Utility: returns distance from point to segment
    private static float DistancePointToSegment(Vector3 point, Vector3 a, Vector3 b)
    {
        Vector3 ab = b - a;
        Vector3 ap = point - a;

        float t = Vector3.Dot(ap, ab) / Vector3.Dot(ab, ab);
        t = Mathf.Clamp01(t);

        Vector3 closestPoint = a + t * ab;
        return Vector3.Distance(point, closestPoint);
    }
}
