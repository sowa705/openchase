using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTable : MonoBehaviour
{
    public Text hardMode;
    public Text normalMode;
    void Start()
    {
        hardMode.text = $"Hard mode: {SaveSystem.GetHighscore(true)}";
        normalMode.text = $"Normal mode: {SaveSystem.GetHighscore(false)}";
    }

    // Update is called once per frame
    void OnEnable()
    {
        hardMode.text = $"Hard mode: {SaveSystem.GetHighscore(true)}";
        normalMode.text = $"Normal mode: {SaveSystem.GetHighscore(false)}";
    }
    void OnDisable()
    {
        hardMode.text = $"Hard mode: {SaveSystem.GetHighscore(true)}";
        normalMode.text = $"Normal mode: {SaveSystem.GetHighscore(false)}";
    }

}
