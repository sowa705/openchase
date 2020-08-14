using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMapVehicle : MonoBehaviour
{
    Vector2Int LastPos;
    DynamicMap map;
    bool ret;
    void Start()
    {
        map = DynamicMap.instance;
        DynamicMap.instance.Reset();
        map.AddSegment();
        map.AddSegment();
        map.AddSegment();
        LastPos = new Vector2Int(Mathf.RoundToInt(transform.GetChild(0).position.x / map.TileSize), Mathf.RoundToInt(transform.GetChild(0).position.z / map.TileSize));
    }

    // Update is called once per frame
    void Update()
    {
        var pos=new Vector2Int(Mathf.RoundToInt(transform.GetChild(0).position.x / map.TileSize), Mathf.RoundToInt(transform.GetChild(0).position.z / map.TileSize));

        if (LastPos.x!=pos.x||LastPos.y!=pos.y)
        {
            if (ret)
            {
                ret = false;
                LastPos = pos;
                return;
            }
            if (Vector2Int.Distance(LastPos,map.CurrentPos)>Vector2Int.Distance(pos,map.CurrentPos))
            {
                map.AddSegment();
            }
            else
            {
                ret = true;
            }
        }
        
        LastPos = pos;
    }
}
