using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAI : MonoBehaviour
{
    Vehicle v;
    public Transform target;
    public float Angle;
    public float TargetSpeed;
    public float Distance;
    public float CurrentSpeed;
    public bool Failed;
    float timer;
    float randomsteer;
    void Start()
    {
        v = GetComponent<Vehicle>();
        target = GameManager.gm.player.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!v.init)
        {
            return;
        }
        if (target==null)
        {
            Destroy(gameObject);
            return;
        }
        timer += Time.deltaTime;
        if (timer>3f)
        {
            randomsteer = Random.Range(-0.2f,0.2f);
            timer = 0;
        }
        CurrentSpeed = GetComponentInChildren<Rigidbody>().velocity.magnitude*3.6f;
        Distance = Vector3.Distance(transform.GetChild(0).position,target.position);

        v.Throttle = 1.2f;
        v.Brakes = 0f;
        if (target.GetComponentInChildren<Rigidbody>().velocity.magnitude<20f)
        {
            //v.Throttle = 0.8f;
        }
        
        if (target.GetComponentInChildren<Rigidbody>().velocity.magnitude*3.6f+10<CurrentSpeed)
        {
            //v.Throttle = 0.6f;
        }

        if (Distance>CurrentSpeed/1.5f+50)
        {
            Failed = true;
        }

        Vector3 toVector = target.position - transform.GetChild(0).position;
        Angle = Mathf.Clamp( Vector3.SignedAngle(transform.GetChild(0).forward, toVector,Vector3.up)/100,-1,1);
        v.SteerInput = Angle+randomsteer;
    }
}
