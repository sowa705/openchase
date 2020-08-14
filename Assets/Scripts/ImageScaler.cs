using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageScaler : MonoBehaviour
{
    Image img;
    RectTransform rectTransform;
    void Start()
    {
        img = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();

        float ratio = ((float)img.sprite.texture.width) / ((float)img.sprite.texture.height);

        float screenRatio = ((float)Screen.width) / ((float)Screen.height);

        if (ratio<screenRatio)
        {
            rectTransform.sizeDelta = new Vector2(Screen.width,Screen.width/ratio);
        }
        else
        {
            rectTransform.sizeDelta = new Vector2(Screen.height*ratio, Screen.height);
        }
    }
}
