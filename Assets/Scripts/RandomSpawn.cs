using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    [Range(0,1)]
    public float Probability;
    public bool ThisObject;
    void Start()
    {
        if (ThisObject)
        {
            if (Random.value > Probability)
            {
                Destroy(gameObject);
            }
        }
        if (Random.value > Probability)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
