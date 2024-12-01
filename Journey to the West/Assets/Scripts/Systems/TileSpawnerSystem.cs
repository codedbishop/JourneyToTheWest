using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

partial struct TileSpawnerSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<EntitiesReferences>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        const float HEX_VERTICAL_OFFSET_MULTIPLIER = .75f;

        EntitiesReferences entitiesReferences = SystemAPI.GetSingleton<EntitiesReferences>();


        foreach ((
          RefRO<LocalTransform> localTransform,
          RefRW<TileSpawner> tileSpawner)
          in SystemAPI.Query<
              RefRO<LocalTransform>,
              RefRW<TileSpawner>>())
        {
            if (tileSpawner.ValueRO.tilesSpawned == false)
            {
                for (int x = 0; x < tileSpawner.ValueRO.mapWidth; x++)
                {
                    for (int y = 0; y < tileSpawner.ValueRO.mapHight; y++)
                    {
                        Entity hexTile = state.EntityManager.Instantiate(entitiesReferences.hexTileEntity);

                        float3 tilePosition = new float3(x, 0, 0) * tileSpawner.ValueRO.cellSize +
                             new float3(0, 0, y) * tileSpawner.ValueRO.cellSize * HEX_VERTICAL_OFFSET_MULTIPLIER +
                             (((y % 2) == 1) ? new float3(1, 0, 0) * tileSpawner.ValueRO.cellSize * .5f : float3.zero);

                        SystemAPI.SetComponent(hexTile, LocalTransform.FromPosition(tilePosition));
                    }
                }
                tileSpawner.ValueRW.tilesSpawned = true;
            }

        }
    }
}
