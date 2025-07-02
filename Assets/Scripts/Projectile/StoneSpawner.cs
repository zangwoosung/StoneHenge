using UnityEngine;

public class StoneSpawner : MonoBehaviour
{
    public GameObject cubePrefab;
    public int cubeCount = 200;
    public float spacing = 2f; // Distance between cubes

    void Start()
    {
        int gridSize = Mathf.CeilToInt(Mathf.Pow(cubeCount, 1f / 3f)); // Cube root for 3D grid
        int count = 0;

        for (int x = 0; x < gridSize && count < cubeCount; x++)
        {
            for (int y = 0; y < gridSize && count < cubeCount; y++)
            {
                for (int z = 0; z < gridSize && count < cubeCount; z++)
                {
                    Vector3 position = new Vector3(x, y, z) * spacing;
                    Instantiate(cubePrefab, position, Quaternion.identity);
                    count++;
                }
            }
        }
    }
}

