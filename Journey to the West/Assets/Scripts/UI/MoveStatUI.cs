using TMPro;
using UnityEngine;

public class MoveStatUI : MonoBehaviour
{
    [SerializeField] private TMP_Text turnsNeeded;

    public void SetTurnsNeeded(int numberOfTurnsNeeded)
    {
        turnsNeeded.text = numberOfTurnsNeeded.ToString();
        this.gameObject.SetActive(true);
    }

    public void SetTurnNeedsToInactive()
    {
        this.gameObject.SetActive(false);
    }
}
