using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddCash : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Add);
    }

    // Update is called once per frame
    void Add()
    {
        SaveSystem.AddCash(10000);
    }
}
