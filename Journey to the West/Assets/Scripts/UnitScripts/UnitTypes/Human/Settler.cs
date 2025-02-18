using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Settler : Human
{
   


    public override void GetEnergyLevel()
    {
        Debug.Log("Checking settler energy");
        if (moral >= 90)
        {
            energyAmount = 80;
        }
        else if (moral >= 70)
        {
            energyAmount = 75;
        }
        else if (moral >= 50)
        {
            energyAmount = 60;
        }
        else if (moral >= 30)
        {
            energyAmount = 40;
        }
        else
        {
            energyAmount = 30;
        }

    }

}
