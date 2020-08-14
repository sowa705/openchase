using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageRotater : MonoBehaviour
{
    Quaternion target;
    public float Angle;
    public static float FOV=60;
    Camera childCam;
    // Start is called before the first frame update
    void Start()
    {
        childCam = GetComponentInChildren<Camera>();
        target = Quaternion.Euler(Angle, 215, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)&&!Input.GetKeyDown(KeyCode.Mouse0)&&Input.mousePosition.y>200&&Input.mousePosition.y<Screen.height-100)
        {
            target = Quaternion.Euler(Angle,target.eulerAngles.y+ Input.GetAxis("Mouse X") * 6, 0);
        }
        childCam.fieldOfView = Mathf.Lerp(childCam.fieldOfView,FOV,Time.deltaTime*5f);
        transform.rotation = target;
    }
}
