using Unity.Entities;
using UnityEngine;

public class StoneAuthoring : MonoBehaviour
{
    public float speed = 100;

    public class StoneBaker : Baker<StoneAuthoring>
    {

        public override void Bake(StoneAuthoring authoring)
        {
            Debug.Log("this.GetName()");
            Debug.Log(this.GetName());

            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new StoneData
            {
                speed = authoring.speed,    
            });

        }

    }
}
