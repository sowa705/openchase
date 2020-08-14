using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControl : MonoBehaviour
{
    public static bool Left;
    public static bool Right;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Left = false;
        Right = false;

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
            {
                if (touch.position.x<Screen.width/2)
                {
                    Left = true;
                }
                else
                {
                    Right = true;
                }
            }
        }
    }
    
}
