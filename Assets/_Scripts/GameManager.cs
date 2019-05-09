using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Canvas")]
    public Canvas canvasMenu;
    public Button btn_resumen;
    private static Button Sbtn_resumen;
    public Button btn_checkPoint;
    private static Button Sbtn_checkPoint;
    public Button btn_opciones;
    private static Button Sbtn_opciones;
    public Button btn_coleccionable;
    private static Button Sbtn_coleccionable;
    private bool pausa;
    public PostProcessVolume post;
    private DepthOfField depth;
    [Header("Jugador")]
    public bool checkPoints = true;
    public CharacterControl characterC;
    public Power characterP;

    private void Awake()
    {
        Sbtn_checkPoint = btn_checkPoint;
        Sbtn_resumen = btn_resumen;
        Sbtn_opciones = btn_opciones;
        Sbtn_coleccionable = btn_coleccionable;
        canvasMenu.gameObject.SetActive(true);
        canvasMenu.enabled = false;
        post.profile.TryGetSettings(out depth);
        depth.active = false;
        btn_checkPoint.interactable = false;
        Navigation nav = btn_resumen.navigation;
        nav.selectOnDown = btn_opciones;
        btn_resumen.navigation = nav;
        Navigation nav2 = btn_opciones.navigation;
        nav2.selectOnUp = btn_resumen;
        btn_opciones.navigation = nav2;

    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("RestartCheckPoint", 0);
    }

    private void Start()
    {
        if (checkPoints && PlayerPrefs.HasKey("RestartCheckPoint") && PlayerPrefs.GetInt("RestartCheckPoint") == 1)
        {
            PlayerPrefs.SetInt("RestartCheckPoint", 0);
            Debug.Log("venia de checkpoint");

            string[] pos = PlayerPrefs.GetString("checkPoint_" + SceneManager.GetActiveScene().name + "_position").Split('_');
            characterC.enabled = false;
            characterC.transform.position = new Vector3(float.Parse(pos[0]), float.Parse(pos[1]), 0);
            StartCoroutine(DelayStart());
        }
        if (!checkPoints)
        {
            PlayerPrefs.SetInt("RestartCheckPoint", 0);
        }
    }


    IEnumerator DelayStart()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        characterC.enabled = true;
    }

    #region Game funciones

    public static void CheckPoint(int punto, Vector3 position)
    {
        Debug.Log("CheckPoint #" + punto + "pos: " + position);
        PlayerPrefs.SetInt("checkPoint_" + SceneManager.GetActiveScene().name + "_number", punto);
        PlayerPrefs.SetString("checkPoint_" + SceneManager.GetActiveScene().name + "_position", position.x.ToString() + "_" + position.y.ToString());
        PlayerPrefs.SetInt("RestartCheckPoint", 1);
    }

    public void SiguienteNivel()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            Initiate.Fade("Nivel forest", Color.black, 0.5f);
            //PlayerPrefs
        }
        if (SceneManager.GetActiveScene().name == "Nivel forest")
        {
            Initiate.Fade("Nivel cueva", Color.black, 0.5f);
        }
        if (SceneManager.GetActiveScene().name == "Nivel cueva")
        {
            Initiate.Fade("Nivel montana", Color.black, 0.5f);
        }
    }

    public static void Coleccionable(string nombreCol)
    {
        Sbtn_checkPoint.interactable = true;
        Navigation nav = Sbtn_resumen.navigation;
        nav.selectOnDown = Sbtn_coleccionable;
        Sbtn_resumen.navigation = nav;
        Navigation nav2 = Sbtn_opciones.navigation;
        nav2.selectOnUp = Sbtn_coleccionable;
        Sbtn_opciones.navigation = nav2;
        MenuColeccionables.Coleccionable(nombreCol);
    }

    #endregion

    #region Funciones UI

    public void VolverMenu()
    {
        PlayerPrefs.SetInt("RestartCheckPoint", 0);
        //scenemanger menu
    }

    public void VolverCheckPoint()
    {
        Time.timeScale = 1;
        Initiate.Fade("SampleScene", Color.black, 1);
    }

    public void Pausar()
    {
        Time.timeScale = 0;
        characterC.enabled = false;
        characterP.enabled = false;
        canvasMenu.enabled = true;
        depth.active = true;
        btn_resumen.Select();
        pausa = true;
    }

    public void Reanudar()
    {
        Time.timeScale = 1;
        characterC.enabled = true;
        characterP.enabled = true;
        canvasMenu.enabled = false;
        depth.active = false;
        pausa = false;
    }

    #endregion

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (!pausa)
            {
                Pausar();
            }
            else
            {
                Reanudar();
            }
        }
    }
}
