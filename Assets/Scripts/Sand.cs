using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sand : MonoBehaviour
{
    WheelCollider wc;
    bool isPlayer;
    // Start is called before the first frame update
    void Start()
    {
        wc = GetComponentInParent<WheelCollider>();
        if (GetComponentInParent<Player>()!=null)
        {
            isPlayer = true;
        }
    }
#pragma warning disable 
    // Update is called once per frame
    void Update()
    {
        if (!isPlayer)
        {
            return;
        }
        RaycastHit hit;
        if (Physics.Raycast(new Ray(transform.position,-transform.up),out hit,0.5f))
        {
            if (hit.collider.tag=="Sand")
            {
                GetComponent<ParticleSystem>().startColor = new Color(0.7f,0.6f,0.2f);
                if (isPlayer)
                {
                    GetComponent<ParticleSystem>().emissionRate = 100;
                }
                else
                {
                    GetComponent<ParticleSystem>().emissionRate = 30;
                }
                
                GetComponent<ParticleSystem>().Play();
            }
            else
            {
                WheelHit whit;
                if (wc.GetGroundHit(out whit))
                {
                    GetComponent<ParticleSystem>().startColor = new Color(0.6f, 0.6f, 0.6f);
                    
                    if (isPlayer)
                    {
                        GetComponent<ParticleSystem>().emissionRate = (whit.sidewaysSlip + Mathf.Abs(whit.forwardSlip)) * 65f;
                    }
                    else
                    {
                        GetComponent<ParticleSystem>().emissionRate = (whit.sidewaysSlip + Mathf.Abs(whit.forwardSlip)) * 50f;
                    }
                    GetComponent<ParticleSystem>().Play();
                }
                else
                {
                    GetComponent<ParticleSystem>().Stop();
                }
               
            }
        }
        else
        {
            GetComponent<ParticleSystem>().Stop();
        }
        if (GetComponentInParent<Rigidbody>().velocity.magnitude < 1f)
        {
            GetComponent<ParticleSystem>().Stop();
        }
    }
#pragma warning restore
}
