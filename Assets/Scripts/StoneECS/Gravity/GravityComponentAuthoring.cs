using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class GravityComponentAuthoring : MonoBehaviour
{
    public float Weight;
    public float Velocity;
    public float3 TargetPos;
    public Transform Player;

    private void Awake()
    {
        TargetPos = Player.transform.position; 
    }

    class Baker : Baker<GravityComponentAuthoring> {

        public override void Bake(GravityComponentAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new GravityComponent()
            {

                Weight = authoring.Weight,
                Velocity = authoring.Velocity,
                TargetPos = authoring.Player.transform.position //TargetPos
            });

        }

    }
}

public struct GravityComponent : IComponentData
{
    public float Weight;
    public float Velocity;
    public float3 TargetPos;
}

public partial struct GravitySystem:ISystem
{

    public void OnUpdate(ref SystemState state)
    {

        float dt = SystemAPI.Time.DeltaTime;

        foreach (var (transform, gravity) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<GravityComponent>>())
        {
            //gravity.ValueRW.Velocity -= gravity.ValueRO.Weight * dt;
            //transform.ValueRW.Position.y += gravity.ValueRO.Velocity * dt;

           /* transform.ValueRW.Position.x += 0.01f;// gravity.ValueRO.TargetPos.x; 
            transform.ValueRW.Position.y += 0.011f;//gravity.ValueRO.TargetPos.y;
            transform.ValueRW.Position.z += 0.011f;//gravity.ValueRO.TargetPos.z;
           */
            transform.ValueRW.Position = Vector3.MoveTowards(transform.ValueRW.Position, gravity.ValueRO.TargetPos,5 * dt);
                   

            /*void Update()
            {
                float speed = 5f;
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    target.position,
                    speed * Time.deltaTime
                );
            }*/

        }



    }
}
