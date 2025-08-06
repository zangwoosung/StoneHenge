using UnityEngine;

public class QuadCreator : MonoBehaviour
{
    [SerializeField] GameObject prefabToSpawn;
    [SerializeField] GameObject quad;
    [SerializeField] Transform container;

    Vector3 position = Vector3.zero;  
    float width = 2f;
    float height = 2f;
    void OnEnable()
    {
        position = container.transform.position;
        position.y = 0.1f;
    }

    public void Setup(float w, float h)
    {
        this.position.x +=2;
        this.width = w;
        this.height = h;
    }

    public void CreateQuad()
    {
        GameObject target = GameObject.Find("AreaQuad");
        if (target != null)     
            Destroy(target);
        

        quad = new GameObject("AreaQuad");
        quad.transform.position = position;
        quad.transform.rotation = Quaternion.Euler(90f, 0f, 0f);

        MeshFilter meshFilter = quad.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = quad.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Standard"));
        meshRenderer.material = new Material(Shader.Find("Unlit/Color")); 
        meshRenderer.material.color = Color.blue; 
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4]
        {
            new Vector3(0, 0, 0),
            new Vector3(width, 0, 0),
            new Vector3(0, height, 0),
            new Vector3(width, height, 0)
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

    }

    public Vector3 GetRandomPoint()  
    {                        
        Vector3 localPos = new Vector3(
           Random.Range(0f, width),
           Random.Range(0f, height),
           0f
       );       
        return quad.transform.TransformPoint(localPos);
    }    
}
