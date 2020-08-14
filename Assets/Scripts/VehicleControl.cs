using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleControl : MonoBehaviour
{
    Vehicle vehicle;
    // Start is called before the first frame update
    void Start()
    {
        vehicle = GetComponent<Vehicle>();
    }

    // Update is called once per frame
    void Update()
    {
        vehicle.Throttle = 1;
        if (!TouchControl.Left&&!TouchControl.Right)
        {
            vehicle.Brakes = Mathf.Clamp01(-Input.GetAxis("Vertical"));
            vehicle.SteerInput = Input.GetAxis("Horizontal");
        }
        else
        {
            float steer = 0;
            float brake = 0;
            if (TouchControl.Left)
            {
                steer = -1;
            }
            if (TouchControl.Right)
            {
                steer = 1;
            }
            if (TouchControl.Left&&TouchControl.Right)
            {
                steer = 0;
                brake = 1;
            }
            vehicle.SteerInput = steer;
            vehicle.Brakes = brake;
        }
    }
}
