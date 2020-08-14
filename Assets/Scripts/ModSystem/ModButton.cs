using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModButton : MonoBehaviour
{
    public string Name;
    public string TranslationName="";
    public Image selection;
    public Text MainText;
    public Text StatusText;
    void Start()
    {
        MainText.text = Name;
        if (TranslationName != "")
        {
            MainText.text = TranslationSystem.GetString(TranslationName);
        }
    }

    public void ChangeStatusText(string text)
    {
        StatusText.text = text;
    }

    // Update is called once per frame
    public void Select()
    {
        selection.color = new Color(0, 0.5f, 1, 1);
    }
    public void Deselect()
    {
        selection.color = new Color(0, 0, 0, 0);
    }
}