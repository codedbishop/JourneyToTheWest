using UnityEngine;

public class TurnController : MonoBehaviour
{
    public static TurnController Instance;

    public enum TurnStat { playerTurn, enemyTurn}
    public TurnStat turnStat;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        turnStat = TurnStat.playerTurn;
    }

    public void CheckTurnStat()
    {
        Debug.Log("Check Turn Stat");
        if (!UnitsOnMap.Instance.CheckIfUnitsHaveEnergy())
        {
            Debug.Log("Out Of Moves");
            turnStat = TurnStat.enemyTurn;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(turnStat == TurnStat.enemyTurn)
        {
            Debug.Log("EnemyTurn");
            turnStat = TurnStat.playerTurn;
            ResetUnitsEnergy();
        }
    }

    public void ResetUnitsEnergy()
    {
        UnitsOnMap.Instance.ResetUnitsEnergy();
    }
}
