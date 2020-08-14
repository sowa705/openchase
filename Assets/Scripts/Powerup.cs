using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public enum PowerupType
    {
        None,
        HPIncrease,
        HPRegen,
        QuadPower,
        Tank
    }
    public PowerupType Type;
    // Start is called before 1the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Vehicle>())
        {
            var v= other.GetComponentInParent<Vehicle>();
            if (Type==PowerupType.HPIncrease)
            {
                v.HP += 50;
                Destroy(gameObject);
                return;
            }
            v.powerupduration = 15f;
            v.poweruptype = Type;
            Destroy(gameObject);
        }
    }
}
