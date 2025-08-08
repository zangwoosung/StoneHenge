using Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
public class AnimalController : MonoBehaviour
{
    [SerializeField] List<GameObject> animals;
    [SerializeField] Transform container;
    [SerializeField] EffectOnAnimal effectOnAnimal;
    [SerializeField] AudioSource animalRunningSound;
    public float spacing = 2f; // Dist   
    Transform player;
    int animalCount = 5;

    private void Start()
    {
        TargetStone.OnKnockDownToAnimalEvent += TargetStone_OnKnockDownToAnimalEvent;
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    private void TargetStone_OnKnockDownToAnimalEvent(Vector3 obj)
    {
        RemoveCloseAnimals();
    }

    public void Initialize()
    {
        SpawnAnimalsCor();
        EnableAnimals();
    }

    public void TakeBreak()
    {

    }

    void RemoveCloseAnimals()
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
    public void SpawnAnimalsCor()
    {
        RemoveAllAnimals();
        int gridSize = Mathf.CeilToInt(Mathf.Pow(animalCount, 1f / 3f)); // Cube root for 3D grid
        int y = 1;

        for (int x = 0; x < animalCount; x++)
        {
            for (int z = 0; z < animalCount; z++)
            {
                Quaternion rot = container.transform.rotation;
              
                Vector3 position = new Vector3(x, y, z) * spacing;
                GameObject clone = Instantiate(animals[UnityEngine.Random.Range(0, animals.Count)], position, rot);
                clone.transform.SetParent(container);
                clone.transform.localPosition = position;
                clone.transform.eulerAngles = new Vector3(0f, -90f, 0f);

                clone.GetComponent<MovePlayerInput>().enabled = false;
               
            }
        }
     
    }

    public void RemoveAllAnimals()
    {
        container.Clear();
        Debug.Log("all removed!");
    }

    public void EnableAnimals()
    {
        foreach (Transform child in container)
        {
            child.GetComponent<MovePlayerInput>().enabled = true;
        }
    }
   
    
    public void RunToPlayer()
    {

        foreach (Transform child in container)
        {
            Vector2 vec2 = new Vector2(0, 1);
            child.GetComponent<MovePlayerInput>().GatherInputSample(vec2, true, false, player.transform.position);
        }
    }
    public void SetSpeed(Vector2 vec2)
    {
        foreach (Transform child in container)
        {
            child.GetComponent<MovePlayerInput>().SetSpeed(vec2);
        }
    }
}

