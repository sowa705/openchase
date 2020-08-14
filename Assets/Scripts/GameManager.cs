using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameState state;
    public CameraFollow follower;
    public GameCanvasController GameCanvas;
    public Canvas MenuCanvas;
    public Canvas DeadCanvas;
    public Player player;
    Camera cam;
    float timer;
    int adcounter;
    Quaternion CameraMenuRotation=Quaternion.Euler(-0.61f,-119.4f,0);
    Vector3 CameraMenuPosition= new Vector3(-3.57f, 1.3f, 3.7f);
    public static GameManager gm;
    public GameObject PlayerPrefab;
    async void Start()
    {
        cam = Camera.main;
        ModSystemTools.GetPartN("Wheel");
#if UNITY_ANDROID
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
#endif
        gm = this;
        try
        {
            ReturnToMenu();

        }
        catch (Exception e)
        {
            Debug.Log($"gm, {e.Message}");
        }

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (state==GameState.Game)
        {
            
            if (timer<5)
            {
                cam.transform.position = Vector3.Lerp(cam.transform.position, follower.transform.position - follower.transform.forward * 10+follower.transform.up*1f, timer / 4f);
                cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, follower.transform.rotation, timer / 4f);
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 70, timer / 4f);
            }
            else
            {
                cam.transform.localPosition = new Vector3(0,1,-10);
            }
            

            if (player.Dead)
            {
                PlayerDeath();
                return;
            }
        }
        if (state==GameState.Menu)
        {
            follower.DisableRotation = false;
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, CameraMenuRotation, timer / 4f);
            cam.transform.position = Vector3.Lerp(cam.transform.position, CameraMenuPosition, timer / 4f);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 25, timer / 4f);
        }
        if (state == GameState.Dead) {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 30, timer / 4f);
        }

    }
    void PlayerDeath()
    {
        state = GameState.Dead;
        GameCanvas.gameObject.SetActive(false);
        DeadCanvas.gameObject.SetActive(true);
        timer = 0;
        Time.timeScale = 0.2f;
    }
    public async void ReturnToMenu()
    {
        state = GameState.Menu;
        if (player!=null)
        {
            Destroy(player.gameObject);
        }
        GameCanvas.gameObject.SetActive(false);
        await CreatePlayer();
        adcounter++;
        follower.playerObject = player.transform.GetChild(0);
        MenuCanvas.gameObject.SetActive(true);
        DeadCanvas.gameObject.SetActive(false);
        timer = 0;
        Time.timeScale = 1f;
    }
    public void StartGame()
    {
        state = GameState.Game;
        follower.transform.position = Vector3.zero;
        player.StartGame();
        GameCanvas.gameObject.SetActive(true);
        MenuCanvas.gameObject.SetActive(false);
        timer = 0;
    }

    public async Task CreatePlayer()
    {
        GameObject gm =  Instantiate(PlayerPrefab);
        gm.transform.position = new Vector3(-22.6f, 2f, -9.9f);
        var modobj = gm.GetComponent<ModObject>();

        player = gm.GetComponent<Player>();

        await modobj.Create(SaveSystem.GetVehicles()[SaveSystem.GetSelectedVehicleIndex()].Type);

        foreach (var item in SaveSystem.GetVehicles()[SaveSystem.GetSelectedVehicleIndex()].InstalledParts)
        {
            modobj.AttachPart(item);
        }
        
        GameCanvas.player = player;
    }
}
public enum GameState
{
    Menu,
    Game,
    Dead
}