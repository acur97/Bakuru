using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuColeccionables : MonoBehaviour
{
    public string coll1Name = "Tunjo";
    private static string Scoll1Name;
    public Image icon1;
    public Sprite sprite1;
    public static Sprite Ssprite1;
    private static Image Sicon1;
    [Space]
    public string coll2Name = "collar";
    private static string Scoll2Name;
    public Image icon2;
    public Sprite sprite2;
    public static Sprite Ssprite2;
    private static Image Sicon2;
    [Space]
    public string coll3Name = "jarra";
    private static string Scoll3Name;
    public Image icon3;
    public Sprite sprite3;
    public static Sprite Ssprite3;
    private static Image Sicon3;
    [Space]
    public string coll4Name = "totuma";
    private static string Scoll4Name;
    public Image icon4;
    public Sprite sprite4;
    public static Sprite Ssprite4;
    private static Image Sicon4;

    private void Awake()
    {
        Scoll1Name = coll1Name;
        Sicon1 = icon1;
        Scoll2Name = coll2Name;
        Sicon2 = icon2;
        Scoll3Name = coll3Name;
        Sicon3 = icon3;
        Scoll4Name = coll4Name;
        Sicon4 = icon4;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString(coll1Name, "off");
        PlayerPrefs.SetString(coll2Name, "off");
        PlayerPrefs.SetString(coll3Name, "off");
        PlayerPrefs.SetString(coll4Name, "off");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") >= 0.75f)
        {
            Derechazo();
        }
        if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") <= -0.75f)
        {
            Izquierdazo();
        }
    }

    public void Derechazo()
    {
        Debug.Log("derechazo");
    }

    public void Izquierdazo()
    {
        Debug.Log("izquierdazo");
    }

    public static void Coleccionable(string nombre)
    {
        if (nombre == Scoll1Name)
        {
            PlayerPrefs.SetString(Scoll1Name, "on");
            Sicon1.sprite = Ssprite1;
        }
        if (nombre == Scoll2Name)
        {
            PlayerPrefs.SetString(Scoll2Name, "on");
            Sicon2.sprite = Ssprite2;
        }
        if (nombre == Scoll3Name)
        {
            PlayerPrefs.SetString(Scoll3Name, "on");
            Sicon3.sprite = Ssprite3;
        }
        if (nombre == Scoll4Name)
        {
            PlayerPrefs.SetString(Scoll4Name, "on");
            Sicon4.sprite = Ssprite4;
        }
    }
}