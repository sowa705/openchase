using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerObject;
    public bool DisableRotation;
    void Update()
    {
        if (playerObject==null)
        {
            return;
        }
        transform.position = Vector3.Lerp(transform.position, playerObject.position,Time.deltaTime*10f);
        if (!DisableRotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(25f, playerObject.rotation.eulerAngles.y, 0), Time.deltaTime * 5f);
        }
       
    }
}
