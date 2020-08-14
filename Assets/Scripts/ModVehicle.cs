using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModVehicle : MonoBehaviour
{
    public MeshRenderer body;
    public MeshRenderer spoiler;
    public GameObject frontspoiler;
    public Texture2D[] Stickers;
    public Modifications Modifications;
    void Start()
    {
        var v = new Modifications();
        if (PlayerPrefs.HasKey("Modifications2"))
        {
            v = JsonUtility.FromJson<Modifications>(PlayerPrefs.GetString("Modifications2"));
        }
        
        ApplyChanges(v,false);
    }

    public void ApplyChanges(Modifications modifications,bool save)
    {
        Modifications = modifications;
        switch ((int)modifications.SelectedPaintColor)
        {
            case 0:
                body.materials[2].color = Color.red;
                break;
            case 1:
                body.materials[2].color = Color.green;
                break;
            case 2:
                body.materials[2].color = Color.blue;
                break;
            case 3:
                body.materials[2].color = Color.yellow;
                break;
            case 4:
                body.materials[2].color = Color.white;
                break;
            case 5:
                body.materials[2].color = Color.black;
                break;
            case 6:
                body.materials[2].color = Color.gray;
                break;
            case 7:
                body.materials[2].color = Color.magenta;
                break;

            default:
                break;
        }
        switch ((int)modifications.SelectedSpoiler)
        {
            case 1:
                spoiler.enabled = true;
                spoiler.material.color = body.materials[2].color;
                break;
            case 0:
                spoiler.enabled = false;
                break;

            default:
                break;
        }
        body.materials[2].SetTexture("_MainTex2",Stickers[(int)modifications.SelectedSticker]);

        switch ((int)modifications.SelectedPaintType)
        {
            case 0:
                body.materials[2].SetFloat("_Glossiness",0.9f);
                body.materials[2].SetFloat("_Metallic", 0.0f);
                spoiler.material.SetFloat("_Glossiness", 0.9f);
                spoiler.material.SetFloat("_Mettallic", 0.0f);
                break;
            case 1:
                body.materials[2].SetFloat("_Glossiness", 0.6f);
                body.materials[2].SetFloat("_Metallic", 0.0f);
                spoiler.material.SetFloat("_Glossiness", 0.6f);
                spoiler.material.SetFloat("_Mettallic", 0.0f);
                break;
            case 2:
                body.materials[2].SetFloat("_Glossiness", 0.1f);
                body.materials[2].SetFloat("_Metallic", 0.0f);
                spoiler.material.SetFloat("_Glossiness", 0.1f);
                spoiler.material.SetFloat("_Mettallic", 0.0f);
                break;
            case 3:
                body.materials[2].SetFloat("_Glossiness", 0.9f);
                body.materials[2].SetFloat("_Metallic", 0.4f);
                spoiler.material.SetFloat("_Glossiness", 0.9f);
                spoiler.material.SetFloat("_Mettallic", 0.4f);
                break;
            case 4:
                body.materials[2].SetFloat("_Glossiness", 0.95f);
                body.materials[2].SetFloat("_Metallic", 1f);
                spoiler.material.SetFloat("_Glossiness", 0.95f);
                spoiler.material.SetFloat("_Mettallic", 1f);
                break;

            default:
                break;
        }

        switch (modifications.SelectedEngine)
        {
            case Engine.V6:
                GetComponent<Vehicle>().Power = 2200;
                break;
            case Engine.V8:
                GetComponent<Vehicle>().Power = 2900;
                break;
            case Engine.V12:
                GetComponent<Vehicle>().Power = 3500;
                break;
            default:
                break;
        }
        switch (modifications.SelectedFrontSpoiler)
        {
            case FrontSpoiler.Disabled:
                frontspoiler.SetActive(false);
                break;
            case FrontSpoiler.Enabled:
                frontspoiler.SetActive(true);
                break;
            default:
                break;
        }
        if (save)
        {
            PlayerPrefs.SetString("Modifications2", JsonUtility.ToJson(modifications));
            PlayerPrefs.Save();
        }
        
    }
    public class SerializableIntArr
    {
        public int[] arr;
        public SerializableIntArr(int[] a)
        {
            arr = a;
        }
    }
}
public class Modifications
{
    public List<PaintType> OwnedPaintTypes=new List<PaintType>();
    public PaintType SelectedPaintType;
    public List<Spoiler> OwnedSpoilers=new List<Spoiler>();
    public Spoiler SelectedSpoiler;
    public List<PaintColor> OwnedPaintColors=new List<PaintColor>();
    public PaintColor SelectedPaintColor;
    public List<Sticker> OwnedStickers=new List<Sticker>();
    public Sticker SelectedSticker;
    public List<FrontSpoiler> OwnedFrontSpoilers = new List<FrontSpoiler>();
    public FrontSpoiler SelectedFrontSpoiler;
    public List<Engine> OwnedEngines = new List<Engine>();
    public Engine SelectedEngine;
    public Modifications()
    {
        OwnedPaintTypes.Add(PaintType.Glossy);
        SelectedPaintType = PaintType.Glossy;

        OwnedSpoilers.Add(Spoiler.Disabled);
        SelectedSpoiler = Spoiler.Disabled;

        OwnedPaintColors.Add(PaintColor.Red);
        SelectedPaintColor = PaintColor.Red;

        OwnedStickers.Add(Sticker.Disabled);
        SelectedSticker = Sticker.Disabled;

        OwnedFrontSpoilers.Add(FrontSpoiler.Disabled);
        SelectedFrontSpoiler = FrontSpoiler.Disabled;

        OwnedEngines.Add(Engine.V6);
        SelectedEngine = Engine.V6;
    }
    public int CalculateChangePrice()
    {
        int price = 0;
        if (!OwnedPaintColors.Contains(SelectedPaintColor))
        {
            price += GetPrice(SelectedPaintColor);
        }
        if (!OwnedPaintTypes.Contains(SelectedPaintType))
        {
            price += GetPrice(SelectedPaintType);
        }
        if (!OwnedSpoilers.Contains(SelectedSpoiler))
        {
            price += GetPrice(SelectedSpoiler);
        }
        if (!OwnedStickers.Contains(SelectedSticker))
        {
            price += GetPrice(SelectedSticker);
        }
        if (!OwnedFrontSpoilers.Contains(SelectedFrontSpoiler))
        {
            price += GetPrice(SelectedFrontSpoiler);
        }
        if (!OwnedEngines.Contains(SelectedEngine))
        {
            price += GetPrice(SelectedEngine);
        }
        return price;
    }
    public void ApplyChanges()
    {
        if (!OwnedPaintColors.Contains(SelectedPaintColor))
        {
            OwnedPaintColors.Add(SelectedPaintColor);
        }
        if (!OwnedPaintTypes.Contains(SelectedPaintType))
        {
            OwnedPaintTypes.Add(SelectedPaintType);
        }
        if (!OwnedSpoilers.Contains(SelectedSpoiler))
        {
            OwnedSpoilers.Add(SelectedSpoiler);
        }
        if (!OwnedStickers.Contains(SelectedSticker))
        {
            OwnedStickers.Add(SelectedSticker);
        }
        if (!OwnedFrontSpoilers.Contains(SelectedFrontSpoiler))
        {
            OwnedFrontSpoilers.Add(SelectedFrontSpoiler);
        }
        if (!OwnedEngines.Contains(SelectedEngine))
        {
            OwnedEngines.Add(SelectedEngine);
        }
    }
    public int GetPrice(PaintColor color)
    {
        switch (color)
        {
            case PaintColor.Red:
                return 1000;
            case PaintColor.Green:
                return 1000;
            case PaintColor.Blue:
                return 1000;
            case PaintColor.Yellow:
                return 1000;
            case PaintColor.White:
                return 4000;
            case PaintColor.Black:
                return 8000;
            case PaintColor.Gray:
                return 6000;
            case PaintColor.Magenta:
                return 1000;
            default:
                return 1000;
        }
    }
    public int GetPrice(PaintType type)
    {
        switch (type)
        {
            case PaintType.Glossy:
                return 500;
            case PaintType.SemiMatte:
                return 1000;
            case PaintType.Matte:
                return 1500;
            case PaintType.Metalic:
                return 5000;
            case PaintType.Chrome:
                return 10000;
            default:
                return 500;
        }
    }
    public int GetPrice(Spoiler spoiler)
    {
        switch (spoiler)
        {
            case Spoiler.Disabled:
                return 0;
            case Spoiler.Standard:
                return 2000;
            default:
                return 0;
        }
    }
    public int GetPrice(Sticker sticker)
    {
        switch (sticker)
        {
            case Sticker.Disabled:
                return 0;
            case Sticker.Numbers1:
                return 1000;
            case Sticker.Stripe1:
                return 1000;
            case Sticker.Stripe2:
                return 2000;
            case Sticker.Stripe3:
                return 4000;
            case Sticker.Police1:
                return 6000;
            default:
                return 0;
        }
    }
    public int GetPrice(FrontSpoiler spoiler)
    {
        switch (spoiler)
        {
            case FrontSpoiler.Disabled:
                return 0;
            case FrontSpoiler.Enabled:
                return 2000;
            default:
                return 0;
        }
    }
    public int GetPrice(Engine engine)
    {
        switch (engine)
        {
            case Engine.V6:
                return 4000;
            case Engine.V8:
                return 8000;
            case Engine.V12:
                return 15000;
            default:
                return 4000;
        }
    }
}
public enum PaintType
{
    Glossy,
    SemiMatte,
    Matte,
    Metalic,
    Chrome
}
public enum Spoiler
{
    Disabled,
    Standard
}
public enum PaintColor
{
    Red,
    Green,
    Blue,
    Yellow,
    White,
    Black,
    Gray,
    Magenta
}
public enum Sticker
{
    Disabled,
    Numbers1,
    Stripe1,
    Stripe2,
    Stripe3,
    Police1
}

public enum FrontSpoiler
{
    Disabled,
    Enabled
}
public enum Engine
{
    V6,
    V8,
    V12
}