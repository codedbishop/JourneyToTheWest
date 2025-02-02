using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Wagon : Unit
{
    public override void GetEnergyLevel()
    {
        

        energyAmount = 60;
        Debug.Log("add energy to wagon to " + energyAmount);
    }
}
