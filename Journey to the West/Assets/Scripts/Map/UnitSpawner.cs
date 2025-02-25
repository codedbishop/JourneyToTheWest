using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] Transform WagonPrefab;
    [SerializeField] Transform HorsePrefab;

    public void SpawnUnits()
    {
        SpawnWagon();
        SpawnHorse();
    }

    public void SpawnWagon()
    {
        GridPosition spawnPosition = new GridPosition(2, 2);
        Transform newGameObject = Instantiate(WagonPrefab, LevelSystem.Instance.GetHexWorldPositionWithGridPosition(spawnPosition), Quaternion.identity).transform;
        newGameObject.GetComponent<Unit>().SetHexTileOn(spawnPosition);
        LevelSystem.Instance.GetHexTile(spawnPosition).AddActionToTile(newGameObject.GetComponent<UnitActions>());
    }

    public void SpawnHorse()
    {
        GridPosition spawnPosition = new GridPosition(1, 1);
        Transform newGameObject = Instantiate(HorsePrefab, LevelSystem.Instance.GetHexWorldPositionWithGridPosition(spawnPosition), Quaternion.identity).transform;
        newGameObject.GetComponent<Unit>().SetHexTileOn(spawnPosition);
        LevelSystem.Instance.GetHexTile(spawnPosition).AddActionToTile(newGameObject.GetComponent<UnitActions>());
        UnitsOnMap.Instance.AddUnitToMap(newGameObject.GetComponent<Unit>());
    }
}
