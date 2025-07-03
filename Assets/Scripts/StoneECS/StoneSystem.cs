using System.Diagnostics;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;


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
