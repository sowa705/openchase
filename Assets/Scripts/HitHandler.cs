using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitHandler : MonoBehaviour
{
    public AudioClip Hit;
    public AudioClip Engine;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource aus= gameObject.AddComponent<AudioSource>();
        aus.clip = Engine;
        aus.loop = true;
        
        if (GetComponentInParent<Player>()!=null)
        {
            aus.Play();
        }
    }

    void OnCollisionEnter(Collision c)
    {
        try
        {
            if (c.impulse.magnitude > 4000)
            {
                //Debug.Log(c.impulse.magnitude);
                if (GetComponentInParent<Vehicle>().HP > 0)
                {
                    if (GetComponentInParent<Vehicle>().poweruptype==Powerup.PowerupType.Tank)
                    {
                        GetComponentInParent<Vehicle>().HP -= c.impulse.magnitude / 2400;
                    }
                    else
                    {
                        GetComponentInParent<Vehicle>().HP -= c.impulse.magnitude / 800;
                    }
                    if (c.other.GetComponentInParent<Vehicle>() != null)
                    {
                        if (GetComponentInParent<Vehicle>().HP > 0)
                        {
                            if (c.other.GetComponentInParent<Vehicle>() != null)
                            {
                                GetComponentInParent<Vehicle>().points += c.impulse.magnitude / 10;
                            }
                        }
                        else
                        {
                            c.other.GetComponentInParent<Vehicle>().points += 500;
                            if (GetComponent<Player>() != null)
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
                        if (GetComponentInParent<Vehicle>().HP < 0 && GetComponent<Player>() != null)
                        {
                            GetComponent<Player>().reason = DeathReason.Crash;
                        }
                    }
                }
                GetComponent<AudioSource>().PlayOneShot(Hit, Mathf.Clamp01((c.impulse.magnitude / 20000f) - 0.1f));
            }
        }
        catch { }
    }
}
