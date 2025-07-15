using System.Diagnostics;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;


[BurstCompile]
public partial struct CubeSpawnerSystem:ISystem
{
    [BurstCompile]
    public void onUpdate(ref SystemState state)
    {
        if (SystemAPI.TryGetSingletonEntity<CubeSpawnerSystem>(out Entity spawnEntity))
            return;

        RefRW<GravityComponent> spawner = SystemAPI.GetComponentRW<GravityComponent>(spawnEntity);

        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.Temp);
        
        //if(spawner.ValueRO.nextSpawnTime < SystemAPI.Time.ElapsedTime)
        //{

        //}
    }




}
public partial class StoneSystem : SystemBase
{
    protected override void OnUpdate()
    {

        Debug.WriteLine("onupdate");

        float deltaTime = SystemAPI.Time.DeltaTime;

        foreach(var (stoneData, transform) in SystemAPI.Query<RefRO<StoneData>, RefRW<LocalTransform>>()){

            transform.ValueRW = transform.ValueRO.RotateY(math.radians(stoneData.ValueRO.speed *  deltaTime)); 
        }
    }
}
