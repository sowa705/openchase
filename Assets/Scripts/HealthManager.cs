using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour,IModSystemSlot
{
    string shield;

    public void PartAdded(string Type, string Part)
    {
        switch (Type)
        {
            case "Shield":
                shield = Part;
                break;
            default:
                break;
        }

    }

    public void PartNulled(string Type)
    {
        shield = "";
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public int GetHealth()
    {
        float health = 0;
        switch (GetComponentInParent<ModObject>().Name)
        {
            case "bus":
                health = 130;
                break;
            case "busnew":
                health = 100;
                break;
            case "muscle":
                health = 75;
                break;
            case "sedan":
                health = 60;
                break;
            default:
                break;
        }
        switch (shield)
        {
            case "LightShield":
                health *= 1.10f;
                break;
            case "MediumShield":
                health *= 1.20f;
                break;
            case "HeavyShield":
                health *= 1.30f;
                break;
            default:
                break;
        }
        return (int)health;
    }
}
