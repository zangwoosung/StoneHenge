using Controller;

using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;


public class AnimalController : MonoBehaviour
{

    [SerializeField] List<GameObject> animals;
    [SerializeField] Transform container;
    [SerializeField] EffectOnAnimal effectOnAnimal;
    public float spacing = 2f; // Dist   
    Transform player;
    int animalCount = 5;
    Vector2 vec2 = new Vector2(-5, -5);

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Initialize();
    }

    public void Initialize()
    {
        SpawnAnimals();
        EnableAnimals();
    }

    void CollectAnimals()
    {
        List<Transform> allObjects = new List<Transform>();

        foreach (Transform child in container)
        {
            allObjects.Add(child);
        }


        List<Transform> closestObjects = allObjects
            .OrderBy(obj => Vector3.Distance(player.position, obj.position))
            .Take(5)
            .ToList();


        foreach (Transform obj in closestObjects)
        {
            Debug.Log("Close object: " + obj.name);
            effectOnAnimal.Disappear(obj.transform.position);
            Destroy(obj.gameObject);

        }
    }
    public void SpawnAnimals()
    {

        int gridSize = Mathf.CeilToInt(Mathf.Pow(animalCount, 1f / 3f)); // Cube root for 3D grid
        int y = 1;

        for (int x = 0; x < animalCount; x++)
        {
            for (int z = 0; z < animalCount; z++)
            {
                quaternion rot = container.transform.rotation;
                Vector3 position = new Vector3(x, y, z) * spacing;
                GameObject clone = Instantiate(animals[UnityEngine.Random.Range(0, animals.Count)], position, rot);
                clone.transform.SetParent(container, false);
                clone.GetComponent<MovePlayerInput>().enabled = false;


            }
        }
    }
    public void EnableAnimals()
    {
        foreach (Transform child in container)
        {
            child.GetComponent<MovePlayerInput>().enabled = true;
        }

    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            foreach (Transform child in container)
            {
                Vector2 vec2 = new Vector2(-10, -10);
                child.GetComponent<MovePlayerInput>().SetSpeed(vec2);
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            foreach (Transform child in container)
            {
                Vector2 vec2 = new Vector2(10, 10);
                child.GetComponent<MovePlayerInput>().SetSpeed(vec2);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (Transform child in container)
            {
                Vector2 vec2 = new Vector2(0, 1);
                child.GetComponent<MovePlayerInput>().GatherInputSample(vec2, true, false, player.transform.position);
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            CollectAnimals();

        }

    }
}
