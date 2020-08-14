using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointAdd : MonoBehaviour
{
    public Vector3 position;
    Camera cam;
    float timer;
    public AnimationCurve Alpha;
    public int points;
    public Vector3 randomOffset;
    void Start()
    {
        GetComponent<Text>().text = $"+{points}";
        transform.localScale = Vector3.one * 2*((points/2000f)+0.5f);
        cam = Camera.main;
        randomOffset = new Vector3(Random.Range(-1f,1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    // Update is called once per frame
    void Update()
    {
        timer+=Time.deltaTime;
        if (timer>2f)
        {
            Destroy(gameObject);
        }
        GetComponent<RectTransform>().position = cam.WorldToScreenPoint(position)+randomOffset;
        GetComponent<Text>().color = new Color(1,1,1,Alpha.Evaluate(timer));
    }
}
