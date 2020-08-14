using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour
{
    float timer;
    public string PlayerID;
    public Text idtxt;
    bool Stopped;
    public static GameLoader instance;

    public GameObject Dialog;
    public Text DialogText;
    string scheduled="";
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        PlayerID = SaveSystem.SaveObject.PlayerID;
        idtxt.text = $"PlayerID: {PlayerID}";
    }
    public void ShowDialog(string text)
    {
        scheduled = text;
    }
    public void StopDialog()
    {
        Dialog.SetActive(false);
        Stopped = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!Stopped)
        {
            timer += Time.deltaTime;
        }
        if (timer>1&&scheduled!="")
        {
            Dialog.SetActive(true);
            Stopped = true;
            DialogText.text =scheduled;
            scheduled = "";
        }
        if (timer > 2)
        {
            timer = float.MinValue;
            SceneManager.LoadSceneAsync("GameScene");
        }
    }

    internal static readonly char[] chars =
            "ABCDEFGHJKLMNPQRSTUVWXYZ123456789".ToCharArray();

    public static string GetUniqueKey(int size)
    {
        byte[] data = new byte[4 * size];
        using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
        {
            crypto.GetBytes(data);
        }
        StringBuilder result = new StringBuilder(size);
        for (int i = 0; i < size; i++)
        {
            var rnd = BitConverter.ToUInt32(data, i * 4);
            var idx = rnd % chars.Length;

            result.Append(chars[idx]);
        }

        return result.ToString();
    }
}
