using Unity.Entities;
using UnityEngine;

public class TileSpawnerAuthoring : MonoBehaviour
{
    public int mapWidth;
    public int mapHight;
    public int cellSize;


    public class Baker : Baker<TileSpawnerAuthoring>
    {

        public override void Bake(TileSpawnerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new TileSpawner
            {
                mapWidth = authoring.mapWidth,
                mapHight = authoring.mapHight,
                cellSize = authoring.cellSize,
                tilesSpawned = false,
            });
        }
    }
}


public struct TileSpawner : IComponentData
{
    public int mapWidth;
    public int mapHight;
    public int cellSize;
    public bool tilesSpawned;
}
