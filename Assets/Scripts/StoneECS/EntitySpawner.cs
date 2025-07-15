using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    public GameObject prefab;
    public int entityCount = 10;

    void Start()
    {
        //var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        //// Convert the GameObject prefab into an Entity prefab
        //var entityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(prefab, GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null));

        //for (int i = 0; i < entityCount; i++)
        //{
        //    var entity = entityManager.Instantiate(entityPrefab);

        //    // Example: Position each entity with an offset
        //    entityManager.SetComponentData(entity, new Translation
        //    {
        //        Value = new float3(i * 2, 0, 0)
        //    });
        //}
    }
}
