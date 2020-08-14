using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPopup : MonoBehaviour
{
    public GameObject PopupPrefab;
    Canvas ca;
    // Start is called before the first frame update
    void Start()
    {
    }
    void OnCollisionEnter(Collision c)
    {
        if (ca==null)
        {
            ca = GameCanvasController.instance.GetComponent<Canvas>();
            return;
        }
        if (c.impulse.sqrMagnitude > 100)
        {
            if (c.other.GetComponentInParent<Vehicle>() == null)
            {
                return;
            }
            Vehicle v = c.other.GetComponentInParent<Vehicle>();
            if (v.HP<0)
            {
                return;
            }
            int points =(int) c.impulse.magnitude / 10;
            if (points>50)
            {
                Spawn(points,c.GetContact(0).point);
            }
        }

    }
    public void Spawn(int points,Vector3 pos)
    {
        if (ca==null)
        {
            return;
        }
        GameObject g= Instantiate(PopupPrefab,ca.transform);
        PointAdd p = g.GetComponent<PointAdd>();
        p.position = pos;
        p.points = points;
        Debug.Log("Spawned");
    }
}
