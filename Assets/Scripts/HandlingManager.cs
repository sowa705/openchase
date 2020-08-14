using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlingManager : MonoBehaviour,IModSystemSlot
{
    string spoiler;
    string fspoiler;
    string suspensionh;
    public string drivetrain;

    public void PartAdded(string Type, string Part)
    {
        switch (Type)
        {
            case "Spoiler":
                spoiler = Part;
                break;
            case "FSpoiler":
                fspoiler = Part;
                break;
            case "SuspensionHeight":
                suspensionh = Part;
                break;
            case "Drivetrain":
                drivetrain = Part;
                break;
            default:
                break;
        }

    }

    public void PartNulled(string Type)
    {
        switch (Type)
        {
            case "Spoiler":
                spoiler = "";
                break;
            case "FSpoiler":
                fspoiler = "";
                break;
            case "SuspensionHeight":
                suspensionh = "";
                break;
            default:
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        drivetrain = "null";
    }

    public int GetHandling()
    {
        float handling = 0;
        switch (GetComponentInParent<ModObject>().Name)
        {
            case "bus":
                handling = 50;
                break;
            case "busnew":
                handling = 60;
                break;
            case "muscle":
                handling = 75;
                break;
            case "sedan":
                handling = 80;
                break;
            default:
                break;
        }
        switch (spoiler)
        {
            case "Spoiler2":
                handling *= 1.05f;
                break;
            case "Spoiler3":
                handling *= 1.1f;
                break;
            case "Spoiler4":
                handling *= 1.1f;
                break;
            default:
                break;
        }
        switch (suspensionh)
        {
            case "LowHeight":
                handling *= 1.1f;
                break;
            case "HighHeight":
                handling *= 0.95f;
                break;
            default:
                break;
        }
        switch (fspoiler)
        {
            case "FrontSpoiler1":
                handling *= 1.05f;
                break;
            default:
                break;
        }
        switch (drivetrain)
        {
            case "RWD":
                handling *= 1.00f;
                break;
            case "AWD":
                handling *= 1.20f;
                break;
            case "FWD":
                handling *= 1.1f;
                break;
            default:
                break;
        }

        return (int)handling;
    }
}
