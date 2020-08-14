using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineManager : MonoBehaviour,IModSystemSlot
{
    public int Power;
    string engine;
    string turbo;
    public void PartAdded(string Type, string Part)
    {
        if (Type == "Engine")
        {
            engine = Part;
        }
        if (Type=="Turbo")
        {
            turbo = Part;
        }
    }

    public void PartNulled(string Type)
    {
        turbo = "";
    }
    public int GetPower()
    {
        float power = 0;
        switch (engine)
        {
            case "16d":
                power = 100;
                break;
            case "19tdi":
                power = 130;
                break;
            case "25tdi":
                power = 170;
                break;
            case "36v6":
                power = 200;
                break;
            case "50v8":
                power = 250;
                break;
            case "72v12":
                power = 300;
                break;
            default:
                break;
        }
        switch (turbo)
        {
            case "SingleTurbo":
                power *= 1.15f;
                break;
            case "BiTurbo":
                power *= 1.3f;
                break;
            default:
                break;
        }
        return (int)power;
    }
}
