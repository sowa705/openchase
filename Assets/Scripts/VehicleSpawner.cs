using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
    public int numVehicles=3;
    public GameObject VehiclePrefab;
    public GameObject HPBarPrefab;
    public AnimationCurve SpeedCompensation;
    Canvas canvas;

    public List<VehicleAI> SpawnedVehicles=new List<VehicleAI>();
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        //canvas = GameCanvasController.instance.GetComponent<Canvas>();
    }

    // Update is called once per frame
    async void Update()
    {
        timer -= Time.deltaTime;
        if (canvas == null)
        {
            canvas = GameCanvasController.instance.GetComponent<Canvas>();
            return;
        }
        if (timer<0)
        {
            if (HardMode.Mode)
            {
                timer = 0.10f;
                if ((SpawnedVehicles.Count+3)<numVehicles)
                {
                    timer = 0.15f;
                }
            }
            else
            {
                timer = 0.3f + Random.value *0.3f;
            }
            
            if (SpawnedVehicles.Count<numVehicles)
            {
                await SpawnVehicle();
                
            }
        }
        foreach (var item in SpawnedVehicles)
        {
            if (item.Failed)
            {
                ReuseVehicle(item.gameObject);
                break;
            }
        }
        
    }
    Vector3 GetRandomPos(Vector3 playerpos)
    {
        return playerpos+new Vector3(Random.Range(-14,14),3, Random.Range(-14, 14)) -transform.GetChild(0).forward * 30;
    }
    void ReuseVehicle(GameObject gm)
    {
        gm.transform.GetChild(0).position = GetRandomPos(transform.GetChild(0).position);
        gm.transform.GetChild(0).rotation = transform.GetChild(0).rotation;
        gm.GetComponent<VehicleAI>().Failed = false;
        gm.GetComponent<Vehicle>().HP = gm.GetComponent<Vehicle>().MaxHP;
        gm.GetComponentInChildren<Rigidbody>().velocity = GetComponentInChildren<Rigidbody>().velocity * 1.3f;
        gm.GetComponentInChildren<Rigidbody>().angularVelocity = Vector3.zero;
        gm.GetComponent<Vehicle>().DeadFX.SetActive(false);
    }
    async Task SpawnVehicle()
    {
        GameObject g = Instantiate(VehiclePrefab,GetRandomPos(transform.GetChild(0).position), transform.GetChild(0).rotation);

        var vtypes = new List<string> { "busnew","sedan","muscle"};

        await g.GetComponent<ModObject>().Create(vtypes[Random.Range(0,vtypes.Count)]);

        g.GetComponent<ModObject>().AttachPart("Wheel");
        g.GetComponent<ModObject>().AttachPart("PaintBlack");
        g.GetComponent<ModObject>().AttachPart("StickerWhite");
        g.GetComponent<ModObject>().AttachPart("NormalHeight");
        g.GetComponent<ModObject>().AttachPart("PoliceLights");
        g.GetComponent<ModObject>().AttachPart("50v8");
        g.GetComponent<ModObject>().AttachPart("Police");

        g.GetComponentInChildren<Rigidbody>().velocity = GetComponentInChildren<Rigidbody>().velocity*1.3f;
        g.GetComponentInChildren<Rigidbody>().angularVelocity = Vector3.zero;

        VehicleAI a = g.GetComponent<VehicleAI>();
        a.target = transform;
        GameObject bar = Instantiate(HPBarPrefab,canvas.transform);
        bar.GetComponent<HPBar>().vehicle = a.GetComponent<Vehicle>();
        bar.GetComponent<HPBar>().camera = Camera.main;

        a.target = transform.GetChild(0);
        SpawnedVehicles.Add(a);
    }
}
