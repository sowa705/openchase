using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public Text Reason;
    public Text Points;
    public Text Highscore;
    public Text Cash;
    int highscore;
    void Start()
    {
        highscore = SaveSystem.GetHighscore(HardMode.Mode);
    }

    // Update is called once per frame
    void OnEnable()
    {
        switch (GameManager.gm.player.reason)
        {
            case DeathReason.Rammed:
                Reason.text = "You have been rammed by police";
                break;
            case DeathReason.Fall:
                Reason.text = "You fell out of map";
                break;
            case DeathReason.Crash:
                Reason.text = "You crashed";
                break;
            case DeathReason.Caught:
                Reason.text = "You have been caught by police";
                break;
            default:
                break;
        }
        highscore = SaveSystem.GetHighscore(HardMode.Mode);
        Points.text = GameManager.gm.player.Points.ToString();
        if (((int)GameManager.gm.player.Points)>highscore)
        {
            highscore = (int)GameManager.gm.player.Points;

            SaveSystem.SetHighscore(highscore, HardMode.Mode);
            SaveSystem.AddLog(new Save.LogEntry("EndScreen", $"New highscore Hardmode={HardMode.Mode}, {highscore} pts"));
            Highscore.text = $"New highscore: {highscore}";
        }
        else
        {
            Highscore.text = $"Highscore: {highscore}";
        }
        
        if (HardMode.Mode)
        {
            Cash.text = $"{(int)GameManager.gm.player.Points /10} $";
            SaveSystem.AddCash((int)GameManager.gm.player.Points/10);
            SaveSystem.AddLog(new Save.LogEntry("EndScreen", $"Added cash: $ {(int)GameManager.gm.player.Points / 10}"));
        }
        else
        {
            Cash.text = $"{(int)GameManager.gm.player.Points / 20} $";
            SaveSystem.AddCash((int)GameManager.gm.player.Points / 20);
            SaveSystem.AddLog(new Save.LogEntry("EndScreen", $"Added cash: $ {(int)GameManager.gm.player.Points / 20}"));
        }
    }
    
}
