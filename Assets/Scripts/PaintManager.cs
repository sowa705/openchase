using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintManager : MonoBehaviour,IModSystemSlot
{
    public MeshRenderer Body;
    public int MatIndex=2;
    public List<MeshRenderer> AdditionalRenderers;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void PartAdded(string Type, string Part)
    {
        if (Type=="BodyPaint")
        {
            switch (Part)
            {
                case "PaintRed":
                    Body.materials[MatIndex].color = Color.red;
                    break;
                case "PaintBlue":
                    Body.materials[MatIndex].color = Color.blue;
                    break;
                case "PaintWhite":
                    Body.materials[MatIndex].color = Color.white;
                    break;
                case "PaintBlack":
                    Body.materials[MatIndex].color = Color.black;
                    break;
                case "PaintGray":
                    Body.materials[MatIndex].color = Color.gray;
                    break;
                case "PaintMagenta":
                    Body.materials[MatIndex].color = Color.magenta;
                    break;
                case "PaintGreen":
                    Body.materials[MatIndex].color = Color.green;
                    break;
                case "PaintYellow":
                    Body.materials[MatIndex].color = Color.yellow;
                    break;
                case "PaintOrange":
                    Body.materials[MatIndex].color = new Color(1,0.5f,0);
                    break;
                default:
                    break;
            }
        }
        if (Type == "StickerPaint")
        {
            switch (Part)
            {
                case "StickerBlack":
                    Body.materials[MatIndex].SetColor( "_Color2",Color.black);
                    break;
                case "StickerWhite":
                    Body.materials[MatIndex].SetColor("_Color2", Color.white);
                    break;
                case "StickerOrange":
                    Body.materials[MatIndex].SetColor("_Color2", new Color(1,0.5f,0));
                    break;
                case "StickerBlue":
                    Body.materials[MatIndex].SetColor("_Color2", Color.blue);
                    break;
                default:
                    break;
            }
        }
        if (Type=="BaseSticker")
        {
            if (Part=="null")
            {
                var stk = Resources.Load<Texture2D>("Stickers/Empty");
                Body.materials[MatIndex].SetTexture("_MainTex2", stk);
                return;
            }
            var sticker = Resources.Load<Texture2D>("LayeredStickers/BaseLayer/"+Part);
            Body.materials[MatIndex].SetTexture("_MainTex2", sticker);
        }
        if (Type == "DecalSticker")
        {
            if (Part == "null")
            {
                var stk = Resources.Load<Texture2D>("Stickers/Empty");
                Body.materials[MatIndex].SetTexture("_MainTex3", stk);
                return;
            }
            var sticker = Resources.Load<Texture2D>("LayeredStickers/DecalLayer/" + Part);
            Body.materials[MatIndex].SetTexture("_MainTex3", sticker);
        }
        foreach (var item in AdditionalRenderers)
        {
            item.material = Body.materials[MatIndex];
        }
    }

    public void PartNulled(string Type)
    {
        if (Type == "BaseSticker")
        {
            var stk = Resources.Load<Texture2D>("Stickers/Empty");
            Body.materials[MatIndex].SetTexture("_MainTex2", stk);
            return;
        }
        if (Type == "DecalSticker")
        {
            var stk = Resources.Load<Texture2D>("Stickers/Empty");
            Body.materials[MatIndex].SetTexture("_MainTex3", stk);
            return;
        }
    }
}
