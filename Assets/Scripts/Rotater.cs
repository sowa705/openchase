using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    public float RotSpeed;
    public Transform playerObject;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerObject = GameManager.gm.player.transform.GetChild(0);
        if (playerObject==null)
        {
            return;
        }
        transform.position = Vector3.Lerp(transform.position, playerObject.position, Time.deltaTime * 20f);
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(25f, playerObject.rotation.eulerAngles.y, 0), Time.deltaTime * 5f);
        transform.Rotate(0,RotSpeed*Time.deltaTime,0);
    }
}
