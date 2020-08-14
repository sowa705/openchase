using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public uint Points;
    public bool Dead;
    public bool Started;
    public Vehicle v;
    public VehicleSpawner vs;
    public VehicleControl vc;

    public DeathReason reason;

    public float Caught;
    public int WantedLevel;
    float timer0;
    float timer1;

    public float AirTime;
    void Start()
    {
        //Time.timeScale = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateNumVehicles();

        if (Started)
        {
            timer0 += Time.deltaTime;
            if (timer0>5f)
            {
                if (GetComponentInChildren<Rigidbody>().velocity.magnitude<1f)
                {
                    Caught += Time.deltaTime*0.2f;
                    if (Caught>1)
                    {
                        v.HP -= Time.deltaTime * 80f;
                        if (v.HP < 0)
                        {
                            reason = DeathReason.Caught;
                        }
                    }
                }
                else
                {
                    Caught -= Time.deltaTime*2f;
                    if (Caught<0)
                    {
                        Caught = 0;
                    }
                }
            }

        }

        if (v.HP < 0)
        {
            Dead = true;
        }
        else
        {
            Points = (uint)v.points;
        }
    }
    void UpdateNumVehicles()
    {
        WantedLevel = 1;
        vs.numVehicles = 3;
        if (Points > 4000)
        {
            vs.numVehicles = 4;
            WantedLevel = 2;
        }
        if (Points > 8000)
        {
            vs.numVehicles = 7;
            WantedLevel = 3;
        }
        if (Points > 16000)
        {
            vs.numVehicles = 13;
            WantedLevel = 4;
        }
        if (Points > 30000)
        {
            vs.numVehicles = 20;
            WantedLevel = 5;
        }
        if (Points > 50000)
        {
            vs.numVehicles = 35;
            WantedLevel = 6;
        }

        if (HardMode.Mode)
        {
            vs.numVehicles *=3 ;
        }
    }
    public void StartGame()
    {
        vc.enabled = true;
        vs.enabled = true;
        Started = true;
    }
}
public enum DeathReason
{
    Rammed,
    Fall,
    Crash,
    Caught
}