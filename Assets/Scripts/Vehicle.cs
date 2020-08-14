using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
public class Vehicle : MonoBehaviour
{
    public WheelCollider[] DriveWheels = new WheelCollider[2];
    public WheelCollider[] SteerWheels=new WheelCollider[2];

    public float Throttle;
    public float Brakes;
    public float SteerInput;

    public float Power = 300;
    public float RPM;
    public AnimationCurve ThrottleCurve;

    public int gear=1;

    public int TargetRPM = 4000;
    public int MaxRPM;

    public float[] GearRatios = {-4.2f,4.2f,3.1f,1.8f,1f,0.75f,0.5f};
    public int GearCount;

    public float HP;
    public float MaxHP;
    public float GearboxCooldown;
    public float points;
    public string drivetrain;

    float FDR;
    public float powerupduration;
    public Powerup.PowerupType poweruptype;
    AudioSource audioSource;
    public AudioClip Hit;
    public bool init;
    public GameObject DeadFX;
    public float AirTime;
    public float DefaultMass;
    async void Start()
    {
        VehicleInfo vinfo = await ResourceCache.LoadAsync<VehicleInfo>("ModSystem/Objects/" + GetComponent<ModObject>().Name+"I");
        await Task.Delay(100);
        if (this==null)
        {
            return;
        }
        MaxHP = HP = GetComponentInChildren<HealthManager>().GetHealth();
        Power = map( GetComponentInChildren<EngineManager>().GetPower(),70,300,170,300)*10;
        drivetrain = GetComponentInChildren<HandlingManager>().drivetrain;
        if (drivetrain=="null")
        {
            drivetrain = vinfo.DriveType.ToString();
        }
        Debug.Log($"Initialized vehicle HP={HP} Power={Power}");
        
        GearRatios = new float[]{ -4.2f,4.2f,3.1f,1.8f,1f,0.75f,0.5f};
        GearCount = vinfo.GearCount;
        FDR = vinfo.FinalDriveRatio;
        DefaultMass=GetComponentInChildren<Rigidbody>().mass;

        if (vinfo.Diesel)
        {
            TargetRPM = 3000;
            MaxRPM = 5000;
        }
        switch (drivetrain)
        {
            case "RWD":
                SteerWheels[0] = transform.GetChild(0).Find("Wheels").Find("FL").GetComponentInChildren<WheelCollider>();
                SteerWheels[1] = transform.GetChild(0).Find("Wheels").Find("FR").GetComponentInChildren<WheelCollider>();
                DriveWheels[0] = transform.GetChild(0).Find("Wheels").Find("RL").GetComponentInChildren<WheelCollider>();
                DriveWheels[1] = transform.GetChild(0).Find("Wheels").Find("RR").GetComponentInChildren<WheelCollider>();
                break;
            case "FWD":
                SteerWheels[0] = transform.GetChild(0).Find("Wheels").Find("FL").GetComponentInChildren<WheelCollider>();
                SteerWheels[1] = transform.GetChild(0).Find("Wheels").Find("FR").GetComponentInChildren<WheelCollider>();
                DriveWheels[0] = transform.GetChild(0).Find("Wheels").Find("FL").GetComponentInChildren<WheelCollider>();
                DriveWheels[1] = transform.GetChild(0).Find("Wheels").Find("FR").GetComponentInChildren<WheelCollider>();
                break;
            case "AWD":
                DriveWheels = new WheelCollider[4];
                SteerWheels[0] = transform.GetChild(0).Find("Wheels").Find("FL").GetComponentInChildren<WheelCollider>();
                SteerWheels[1] = transform.GetChild(0).Find("Wheels").Find("FR").GetComponentInChildren<WheelCollider>();
                DriveWheels[0] = transform.GetChild(0).Find("Wheels").Find("RL").GetComponentInChildren<WheelCollider>();
                DriveWheels[1] = transform.GetChild(0).Find("Wheels").Find("RR").GetComponentInChildren<WheelCollider>();
                DriveWheels[2] = transform.GetChild(0).Find("Wheels").Find("FL").GetComponentInChildren<WheelCollider>();
                DriveWheels[3] = transform.GetChild(0).Find("Wheels").Find("FR").GetComponentInChildren<WheelCollider>();
                Power /= 2;
                break;
            default:
                break;

        }
        DeadFX = transform.GetChild(0).Find("DeadFX").gameObject;
        DeadFX.SetActive(false);
        init = true;
    }
    long map(long x, long in_min, long in_max, long out_min, long out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
    // Update is called once per frame
    void Update()
    {
        int wheels=0;
        if (!init)
        {
            return;
        }
        GearboxCooldown -= Time.deltaTime;
        if (audioSource==null)
        {
            audioSource = GetComponentInChildren<AudioSource>();
            return;
        }
        powerupduration -= Time.deltaTime;
        if (powerupduration<0)
        {
            poweruptype = Powerup.PowerupType.None;
        }
        foreach (var item in SteerWheels)
        {
            item.steerAngle = SteerInput * 25;

            WheelVis vis = item.GetComponentInChildren<WheelVis>();
            if (vis.Grounded)
            {
                wheels++;
            }

            if (GetComponentInChildren<Rigidbody>().velocity.magnitude > 15)
            {
                if (Brakes < 0.5f)
                {
                    item.brakeTorque = 0;
                }
                else
                {
                    item.motorTorque = 0;
                    item.brakeTorque = Brakes * 2000;
                }
            }
            else
            {
                item.brakeTorque = 0;
            }
            
        }
        if (Vector3.Dot(transform.GetChild(0).up,Vector3.up)<0.1f)
        {
            GetComponentInChildren<Rigidbody>().AddRelativeTorque(0,0,-SteerInput*900000*Time.deltaTime);
        }

        float rpm = 0;

        if (poweruptype == Powerup.PowerupType.Tank)
        {
            GetComponentInChildren<Rigidbody>().mass = DefaultMass * 5;
        }
        else
        {
            GetComponentInChildren<Rigidbody>().mass = DefaultMass;
        }

        ApplyHealth();

        float adjustedthrottle = Throttle * ThrottleCurve.Evaluate(RPM / 7000);
        if (RPM > MaxRPM)
        {
            adjustedthrottle = 0;
        }
        
        foreach (var item in DriveWheels)
        {
            WheelVis vis = item.GetComponentInChildren<WheelVis>();
            if (vis.Grounded)
            {
                wheels++;
            }
            if (GetComponentInChildren<Rigidbody>().velocity.magnitude<15)
            {
                item.motorTorque = adjustedthrottle * Power;
                item.brakeTorque = 0;
                if (Brakes > 0.5f)
                {
                    item.motorTorque = -adjustedthrottle * Power * 1f;
                    item.brakeTorque = 0;
                }
            }
            else
            {
                if (Brakes < 0.5f)
                {
                    item.motorTorque = adjustedthrottle * Power;
                    item.brakeTorque = 0;
                    if (poweruptype==Powerup.PowerupType.QuadPower||poweruptype==Powerup.PowerupType.Tank)
                    {
                        item.motorTorque *= 4;
                    }
                }
                else
                {
                    item.motorTorque = 0;
                    item.brakeTorque = Brakes * 4000;
                }
            }
            
            rpm += item.rpm;
        }
        rpm /= DriveWheels.Length;
        rpm *= FDR;
        rpm = CalculateGear(rpm);

        if (poweruptype==Powerup.PowerupType.QuadPower)
        {
            rpm /= 2;
        }

        RPM = Mathf.Lerp(RPM, rpm, Time.deltaTime * 10f);

        points += Time.deltaTime * GetComponentInChildren<Rigidbody>().velocity.magnitude * 4;
        audioSource.volume = Mathf.Clamp(rpm / 5000, 0.2f, 0.7f);
        audioSource.pitch = Mathf.Clamp(rpm / 6000, 0.5f, 1.2f) * Time.timeScale;

        if (wheels==0&&!Physics.Raycast(new Ray(transform.GetChild(0).position,Vector3.down),4))
        {
            GetComponentInChildren<Rigidbody>().AddRelativeTorque(0, 0, -SteerInput * 100000 * Time.deltaTime);
            AirTime += Time.deltaTime *300f;
            points += Time.deltaTime *300f;
        }
        else
        {
            AirTime = 0;
        }

        if (transform.GetChild(0).position.y < -20)
        {
            HP -= Time.deltaTime * 50;
            if (HP < 0 && GetComponent<Player>() != null)
            {
                GetComponent<Player>().reason = DeathReason.Fall;
            }
        }
    }

    private void ApplyHealth()
    {
        if (HP < 0)
        {
            Throttle = 0;
            Brakes = 1;
            DeadFX.SetActive(true);
        }
        else
        {
            HP += Time.deltaTime * 1.5f;
            if (poweruptype == Powerup.PowerupType.HPRegen)
            {
                HP += Time.deltaTime * 5f;
            }
            if (HP > MaxHP)
            {
                HP = MaxHP;
            }
        }
    }

    private float CalculateGear(float rpm)
    {
        float best = 99999;
        int bestindex = 0;

        for (int i = 1; i < GearCount+1; i++)
        {
            float v = rpm * GearRatios[i];
            float diff = Mathf.Abs(TargetRPM - v);
            if (diff < best)
            {
                best = diff;
                bestindex = i;
            }
        }
        if (GearboxCooldown<0)
        {
            if (bestindex!=gear)
            {
                GearboxCooldown = 0.5f;
            }
            gear = bestindex;
        }

        if (gear > 0)
        {
            rpm *= GearRatios[gear];
        }

        return rpm;
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.impulse.magnitude > 2000)
        {
            Debug.Log(c.impulse.magnitude);
            if (HP > 0)
            {
                if (poweruptype==Powerup.PowerupType.Tank)
                {
                    HP -= c.impulse.magnitude / 2500;
                }
                else
                {
                    HP -= c.impulse.magnitude / 500;
                }
                if (c.other.GetComponentInParent<Vehicle>() != null)
                {
                    if (HP > 0)
                    {
                        if (c.other.GetComponentInParent<Vehicle>() != null)
                        {
                            points += c.impulse.magnitude / 20;
                        }
                    }
                    else
                    {
                        c.other.GetComponentInParent<Vehicle>().points += 500;
                        if (GetComponent<Player>()!=null)
                        {
                            GetComponent<Player>().reason = DeathReason.Rammed;
                        }
                        if (c.other.GetComponentInParent<PointPopup>() != null)
                        {
                            c.other.GetComponentInParent<PointPopup>().Spawn(500, c.GetContact(0).point);
                        }
                    }
                }
                else
                {
                    if (HP<0&&GetComponent<Player>()!=null)
                    {
                        GetComponent<Player>().reason = DeathReason.Crash;
                    }
                }
            }
            audioSource.PlayOneShot(Hit, Mathf.Clamp01((c.impulse.magnitude / 20000f) - 0.1f));
        }
        
    }
}
