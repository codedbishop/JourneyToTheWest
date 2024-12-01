using Unity.Entities;
using UnityEngine;

public class EntitiesReferencesAuthoring : MonoBehaviour
{
    public GameObject hexTileGameObject;


    public class Baker : Baker<EntitiesReferencesAuthoring>
    {


        public override void Bake(EntitiesReferencesAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new EntitiesReferences
            {

                hexTileEntity = GetEntity(authoring.hexTileGameObject, TransformUsageFlags.Dynamic),
            });
        }

    }

}


public struct EntitiesReferences : IComponentData
{
    public Entity hexTileEntity;
}