using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelVis : MonoBehaviour
{
    WheelCollider c;
    Quaternion startRot;
    float steer;
    public bool Grounded;
    void Start()
    {
        c = GetComponentInParent<WheelCollider>();
        startRot = transform.localRotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        steer = Mathf.Lerp(steer,c.steerAngle,Time.deltaTime*3f);
        RaycastHit hit;
        if (Physics.Raycast(new Ray(transform.parent.position-transform.parent.up*c.suspensionDistance*0.5f, -transform.parent.up),out hit,c.suspensionDistance*0.5f + c.radius))
        {
            Grounded = true;
            transform.localPosition = new Vector3(0.1f,-hit.distance+c.radius-c.suspensionDistance*0.5f);
        }
        else
        {
            Grounded = false;
            transform.localPosition = new Vector3(0.1f, -c.suspensionDistance*0.5f);
        }
        transform.localRotation =Quaternion.Euler(0,startRot.eulerAngles.y+steer, transform.localEulerAngles.z+((c.rpm/60f)*Time.deltaTime*360f));
    }
}
