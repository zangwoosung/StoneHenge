using UnityEngine;
using UnityEngine.UIElements;

public class QuadCreatorTest : MonoBehaviour
{


    [SerializeField] GameObject prefabToSpawn;
    public float width = 2f;
    public float height = 3f;
    GameObject quad;
    void Start()
    {

         quad = new GameObject("AreaQuad");
      

        quad.transform.rotation = Quaternion.Euler(90f, 0f, 0f);

        MeshFilter meshFilter = quad.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = quad.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Standard"));

        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4]
        {
            new Vector3(0, 0, 0),
            new Vector3(width, 0, 0),
            new Vector3(0, height, 0),
            new Vector3(width, height, 0)
        };

        int[] triangles = new int[6]
        {
            0, 2, 1,
            2, 3, 1
        };

        Vector3[] normals = new Vector3[4]
        {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward
        };

        Vector2[] uv = new Vector2[4]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.uv = uv;

        meshFilter.mesh = mesh;
    }
    public void GetArea()
    {
        Vector3 localPos = new Vector3(
            Random.Range(0f, width),
            Random.Range(0f, height),
            0f
        );

        Vector3 worldPos = quad.transform.TransformPoint(localPos); // Convert to world space

        Instantiate(prefabToSpawn, worldPos, Quaternion.identity);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            GetArea();
        }
        if (Input.GetKey(KeyCode.C))
        {
           
        }
    }
}
