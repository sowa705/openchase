using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forwarder : MonoBehaviour,IModSystemSlot
{
    public Component Target;

    public void PartAdded(string Type, string Part)
    {
        Target.GetComponent<IModSystemSlot>().PartAdded(Type,Part);
    }

    public void PartNulled(string Type)
    {
        Target.GetComponent<IModSystemSlot>().PartNulled(Type);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
