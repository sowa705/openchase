using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMap : MonoBehaviour
{
    public GameObject[] Straight;
    public GameObject[] Left;
    public GameObject[] Right;

    public GameObject StartSegment;

    public int currentindex=-1;
    public float TileSize;
    public float MaxDistance=1000;
    public float Dist;
    public float MDist;
    public Vector2Int CurrentPos;

    public static DynamicMap instance;

    public List<Segment> Segments=new List<Segment>();
    void Start()
    {
        instance = this;
    }

    public void Reset()
    {
        currentindex = -1;
        foreach (var item in Segments)
        {
            Destroy(item.gm);
        }
        Segments.Clear();
    }

    // Update is called once per frame
    public void AddSegment()
    {
        if (currentindex==-1)
        {
            Segments.Add( InstantiateSegment(Vector2Int.zero,0,SegmentType.Straight));
            currentindex++;
            return;
        }
        Segment s = Segments[currentindex];
        var newType = (SegmentType)Random.Range(0,3);
        Dist = Vector2Int.Distance(Vector2Int.zero, s.position * (int)TileSize);
        if (Dist>MDist)
        {
            MDist = Dist;
        }
        if (Dist > MaxDistance)
        {
            newType = (SegmentType)Random.Range(0, 2);
        }
        int requiredRotation = s.rotation;
        switch (s.type)
        {
            case SegmentType.Left:
                requiredRotation -= 90;
                break;
            case SegmentType.Right:
                requiredRotation += 90;
                break;
        }
        
        if (requiredRotation<0)
        {
            requiredRotation = 360 + requiredRotation;
        }
        requiredRotation = requiredRotation % 360;
        var newPosition = s.position;
        switch (requiredRotation)
        {
            case 0:
                newPosition += Vector2Int.up;
                break;
            case 90:
                newPosition += Vector2Int.right;
                break;
            case 180:
                newPosition += Vector2Int.down;
                break;
            case 270:
                newPosition += Vector2Int.left;
                break;
            default:
                Debug.Log("Invalid rotation :"+requiredRotation.ToString());
                break;
        }

        Segments.Add(InstantiateSegment(newPosition,requiredRotation,newType));
        currentindex++;
        if (Segments.Count > 4)
        {
            currentindex--;
            Destroy(Segments[0].gm);
            Segments.RemoveAt(0);
        }
    }
    Segment InstantiateSegment(Vector2Int position,int rotation,SegmentType type)
    {
        var seg = new Segment();
        GameObject g = null;
        if (currentindex==-1)
        {
            g = Instantiate(StartSegment, new Vector3(position.x * TileSize, 0, position.y * TileSize), Quaternion.Euler(0, rotation+90 , 0));
        }
        else
        {
            g = Instantiate(GetRandomSegment(type), new Vector3(position.x * TileSize, 0, position.y * TileSize), Quaternion.Euler(0, rotation +90, 0));
        }
        
        seg.gm = g;
        seg.rotation = rotation;
        seg.type = type;
        seg.position = position;
        CurrentPos = seg.position;
        return seg;
    }
    public GameObject GetRandomSegment(SegmentType t)
    {
        switch (t)
        {
            case SegmentType.Left:
                return Left[Random.Range(0, Left.Length)];
            case SegmentType.Right:
                return Right[Random.Range(0, Right.Length)];
            case SegmentType.Straight:
                return Straight[Random.Range(0, Straight.Length)];
            default:
                return null;
        }
    }
}
public class Segment
{
    public GameObject gm;
    public Vector2Int position;
    public int rotation;
    public SegmentType type;
}
public enum SegmentType
{
    Left,
    Right,
    Straight
}