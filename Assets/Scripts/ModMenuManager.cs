using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ModVehicle;

public class ModMenuManager : MonoBehaviour
{
    public int menuindex;
    public GameObject[] Menus;
    public GameObject BuyMenu;
    public static int CurrentPrice;
    void Start()
    {
        ChangeIndex(0);
    }
    public void ChangeIndex(int index)
    {
        menuindex = index;
        foreach (var item in Menus)
        {
            item.SetActive(false);
        }
        Menus[index].SetActive(true);
    }

    public void ChangeValue(int value)
    {
        var modifications = GameManager.gm.player.GetComponent<ModVehicle>().Modifications;

        switch (menuindex)
        {
            case 0:
                modifications.SelectedPaintColor =(PaintColor) value;
                break;
            case 1:
                modifications.SelectedSpoiler = (Spoiler)value;
                break;
            case 2:
                modifications.SelectedSticker = (Sticker)value;
                break;
            case 3:
                modifications.SelectedPaintType = (PaintType)value;
                break;
            case 4:
                modifications.SelectedFrontSpoiler = (FrontSpoiler)value;
                break;
            case 5:
                modifications.SelectedEngine = (Engine)value;
                break;
            default:
                break;
        }
        CurrentPrice= modifications.CalculateChangePrice();
        
        BuyMenu.SetActive(CurrentPrice>0);
        if (CurrentPrice == 0)
        {
            GameManager.gm.player.GetComponent<ModVehicle>().ApplyChanges(modifications, true);
        }
        else
        {
            GameManager.gm.player.GetComponent<ModVehicle>().ApplyChanges(modifications, false);
        }
    }
    public void Buy()
    {
        var modifications = GameManager.gm.player.GetComponent<ModVehicle>().Modifications;

        int price = modifications.CalculateChangePrice();

        if (price>PlayerPrefs.GetInt("Cash"))
        {
            return;
        }
        BuyMenu.SetActive(false);
        modifications.ApplyChanges();
        PlayerPrefs.SetInt("Cash", PlayerPrefs.GetInt("Cash") - price);
        PlayerPrefs.Save();
        GameManager.gm.player.GetComponent<ModVehicle>().ApplyChanges(modifications, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
