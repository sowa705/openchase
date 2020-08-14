using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public GameObject PauseScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Pause()
    {
        Time.timeScale = 0.000001f;
        PauseScreen.SetActive(true);
    }
    public void Return()
    {
        Time.timeScale = 1f;
        PauseScreen.SetActive(false);
        GameManager.gm.ReturnToMenu();
    }
    public void Unpause()
    {
        Time.timeScale = 1f;
        PauseScreen.SetActive(false);
    }
}
