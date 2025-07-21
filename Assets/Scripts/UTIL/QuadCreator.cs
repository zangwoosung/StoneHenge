using UnityEngine;
using UnityEngine.UIElements;

public class QuadCreator : MonoBehaviour
{

    [SerializeField] Transform container;
    [SerializeField] GameObject prefabToSpawn;
    [SerializeField] GameObject quad;

    Vector3 position = Vector3.zero;
    Vector3 worldPos;
    float width = 2f;
    float height = 2f;
    private void OnEnable()
    {
        position = quad.transform.position;
    }
    private void Start()
    {       
        CreateQuad();
    }
    public void Setup(float w, float h)
    {
        this.width = w;
        this.height = h;       
    }

   public void CreateQuad()
    {
        GameObject target = GameObject.Find("AreaQuad"); 
        if (target != null)
        {
            Destroy(target);
        }

      var  quad = new GameObject("AreaQuad");
        quad.transform.position = position;
        quad.transform.parent = container;
        quad.transform.up = Vector3.up;

        MeshFilter meshFilter = quad.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = quad.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Standard"));
        meshRenderer.material = new Material(Shader.Find("Unlit/Color")); // Use Unlit to avoid lighting issues
        meshRenderer.material.color = Color.blue; // Set to any color you like


        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4]
        {
            new Vector3(-width / 2f, -height / 2f, 0f),
            new Vector3(width / 2f, -height / 2f, 0f),
            new Vector3(-width / 2f, height / 2f, 0f),
            new Vector3(width / 2f, height / 2f, 0f)
        };
        mesh.vertices = vertices;
        mesh.RecalculateBounds();


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
        quad.transform.rotation = Quaternion.Euler(90f, 0f, 0f);

        width += 1f;
        height += 1f;
        position.x += 0.5f;

    }
    
    public Vector3 GetArea()
    {
        MeshRenderer renderer = quad.GetComponent<MeshRenderer>();
        Vector3 size = renderer.bounds.size;

        Vector3 localPos = new Vector3(
                Random.Range(-size.x, size.x),
                Random.Range(-size.z, size.z),
                0f
            );

        worldPos = quad.transform.TransformPoint(localPos); // C
        return worldPos;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            MeshRenderer renderer = quad.GetComponent<MeshRenderer>();
            Vector3 size = renderer.bounds.size;

            Vector3 localPos = new Vector3(
                Random.Range(-size.x , size.x),
                Random.Range(-size.z , size.z),
                0f
            );

            worldPos = quad.transform.TransformPoint(localPos); // C
            Instantiate(prefabToSpawn, worldPos, Quaternion.identity);
        }
        if (Input.GetKey(KeyCode.C))
        {
            Setup(5, 5);
        }
    }
}
