using TMPro;
using UnityEngine;

public class MoveStatUI : MonoBehaviour
{
    [SerializeField] private TMP_Text turnsNeeded;
    [SerializeField] private Transform selected;
    [SerializeField] private Transform cantMove;

    public void SetTurnsNeeded(int numberOfTurnsNeeded, int energyHas)
    {
        turnsNeeded.text = numberOfTurnsNeeded.ToString();
        this.gameObject.SetActive(true);

        if(energyHas >= numberOfTurnsNeeded)
        {
            CanMovetoTile();
        }
        else
        {
            CantMoveTotile();
        }
    }

    public void CantMoveTotile()
    {
        selected.gameObject.SetActive(false);
        cantMove.gameObject.SetActive(true);

    }

    public void CanMovetoTile()
    {
        cantMove.gameObject.SetActive(false);

        selected.gameObject.SetActive(true);
    }

    public void SetTurnNeedsToInactive()
    {
        this.gameObject.SetActive(false);
    }
}
