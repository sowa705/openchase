using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceList : MonoBehaviour
{
    static string[] assetList;
    void Start()
    {
        if (assetList!=null)
        {
            return;
        }/*
        string text="";
        if (Resources.Load<TextAsset>("AssetList") != null)
        {
            text = Resources.Load<TextAsset>("AssetList").text;
        }
        if (Resources.Load<TextAsset>("AssetList.txt") != null)
        {
            text = Resources.Load<TextAsset>("AssetList").text;
        }*/
        var text = "ModSystem/Parts/LowHeight;ModSystem/Parts/WheelCarbon;ModSystem/Objects/Engine;ModSystem/Parts/Intake1;ModSystem/Objects/Handling;LayeredStickers/DecalLayer/Sowirex;ModSystem/Parts/Engines/25tdi;ModSystem/Objects/muscle;ModSystem/Objects/Shield;ModSystem/Parts/Engines/BiTurbo;Stickers/ModSystem;ModSystem/Parts/Stickers/StickerWhite;ModSystem/Objects/New Avatar Mask;ModSystem/Objects/busI;ModSystem/Parts/Paint/PaintMagenta;ModSystem/Parts/Shield/LightShield;ModSystem/Objects/muscleI;Stickers/Stripes3;ModSystem/Parts/Stickers/StickerPolice;ModSystem/Parts/Engines/50v8;Translations/EN;ModSystem/Parts/Stickers/StickerBlack;ModSystem/Parts/Engines/16d;ModSystem/Parts/Stickers/Sowirex;ModSystem/Objects/bus;ModSystem/Objects/sedan;ModSystem/Parts/PoliceLights;ModSystem/Parts/Paint/PaintGray;ModSystem/Parts/Stickers/StripeWide;ModSystem/Parts/Paint/PaintGreen;ModSystem/Parts/Spoilers/Spoiler2;ModSystem/Parts/Spoilers/Spoiler4;ModSystem/Parts/Stickers/StripeDouble;ModSystem/Parts/Paint/PaintWhite;ModSystem/Parts/Paint/PaintRed;ModSystem/Objects/busnewI;ModSystem/Parts/Stickers/StickerBlue;ModSystem/Parts/Stickers/StickerOrange;ModSystem/Parts/Engines/19tdi;LayeredStickers/BaseLayer/StripeWide;LayeredStickers/DecalLayer/Numbers2;ModSystem/Parts/Engines/72v12;Stickers/Numbers1;Stickers/Stripes2;ModSystem/Objects/DeadFX;ModSystem/Parts/Shield/HeavyShield;ModSystem/Parts/Engines/SingleTurbo;ModSystem/Parts/Spoilers/FrontSpoiler1;ModSystem/Parts/Stickers/Numbers1;ModSystem/Objects/sedanI;Stickers/Empty;ModSystem/Parts/Paint/PaintYellow;LayeredStickers/BaseLayer/StickerPolice;ModSystem/Parts/NormalHeight;ModSystem/Parts/Paint/PaintOrange;ModSystem/Parts/HighHeight;ModSystem/Parts/Engines/36v6;LayeredStickers/DecalLayer/Numbers1;LayeredStickers/BaseLayer/StripeDouble;ModSystem/UI/Button;Stickers/Stripes1;ModSystem/Parts/Wheel;ModSystem/Parts/Spoilers/Spoiler3;ModSystem/Parts/Paint/PaintBlack;ModSystem/Parts/Paint/PaintBlue;ModSystem/Parts/Shield/MediumShield;ModSystem/Objects/busnew;ModSystem/Parts/Stickers/Numbers2;";
        assetList = text.Split(';');

        Debug.Log(assetList);
    }

    public static string[] GetAllAssetsAtPath(string path)
    {
        var list = new List<string>();
        foreach (var item in assetList)
        {
            if (item.StartsWith(path))
            {
                list.Add(item);
            }
        }
        return list.ToArray();
    }
}
