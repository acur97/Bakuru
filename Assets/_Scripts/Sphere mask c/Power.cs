using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class Power : MonoBehaviour
{
    //public Slider sli;
    public Transform rootPower1;
    public Transform rootPower2;
    //public Slider sli2;
    public GameObject esfera;
    public DisolveTrigger triggerPowerUp;
    public float velocidadSubida;
    public float velocidadBajada;
    [Space]
    public PostProcessVolume post;

    private float energia = 25;
    private float poder = 0;
    private bool subirPoder;
    private bool puedeComprobarPoder;
    private bool gastarEnergia;
    private bool estabaGastandoEnergia;
    private float limitCantidadSubirEnergia;
    private bool subirEnergia;

    private ChromaticAberration chromatic;
    private float normalChromatic;
    private Transform esferaT;
    private SphereCollider esferaR;

    private int count = 0;
    private bool enter;

    private void Awake()
    {
        esferaT = esfera.transform;
        esferaR = esfera.GetComponent<SphereCollider>();
        esferaT.localScale = new Vector3(0, 0, 0);
        post.profile.TryGetSettings(out chromatic);
        normalChromatic = chromatic.intensity.value;
        triggerPowerUp.tagg = "PowerUp";
        triggerPowerUp.tagg2 = "Coleccion";

        //sli.value = 25;
        //sli2.value = 0;

        rootPower1.localScale = new Vector3(1, 1, 1);
        rootPower2.localScale = new Vector3(1, 1, 1);
    }

    private void Start()
    {
        DelayStart();
    }

    IEnumerator DelayStart()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        esfera.SetActive(false);
    }

    public void CargarEnergia(int cantidadEnergia)
    {
        if (energia < 25)
        {
            subirEnergia = true;
            float suma = energia + cantidadEnergia;
            limitCantidadSubirEnergia = Mathf.Clamp(suma, 0, 25);
        }
    }

    public void TriggerPoderExterno()
    {
        if (energia > 0)
        {
            count += 1;
        }

        if (count == 1)
        {
            gastarEnergia = true;
            subirPoder = true;
            Debug.Log("activado");
        }
        if (count == 2)
        {
            gastarEnergia = false;
            subirPoder = false;
            count = 0;
            Debug.Log("desactivado");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            CargarEnergia(5);
        }

        if (Input.GetButtonDown("Power"))
        {
            if (energia > 0)
            {
                count += 1;
            }

            if (count == 1)
            {
                gastarEnergia = true;
                subirPoder = true;
                Debug.Log("activado");
            }
            if (count == 2)
            {
                gastarEnergia = false;
                subirPoder = false;
                count = 0;
                Debug.Log("desactivado");
            }
        }

        if (subirEnergia)
        {
            Debug.Log("power");
            if (gastarEnergia == true)
            {
                estabaGastandoEnergia = true;
                gastarEnergia = false;
            }
            if (energia < limitCantidadSubirEnergia)
            {
                energia += velocidadSubida;
                //sli.value = energia;
                rootPower1.localScale = new Vector3(1, Mathf.Clamp01(energia / 25), 1);
                rootPower2.localScale = new Vector3(1, Mathf.Clamp01(energia / 25), 1);
            }
            if (energia >= limitCantidadSubirEnergia)
            {
                if (estabaGastandoEnergia)
                {
                    gastarEnergia = true;
                    estabaGastandoEnergia = false;
                }
                subirEnergia = false;
            }
        }

        if (gastarEnergia)
        {
            if (energia >= 0)
            {
                energia -= velocidadBajada;
                //sli.value = energia;
                rootPower1.localScale = new Vector3(1, (energia / 25), 1);
                rootPower2.localScale = new Vector3(1, (energia / 25), 1);
            }
            if (energia <= 0)
            {
                gastarEnergia = false;
                count = 0;
                subirPoder = false;
                Debug.Log("desactivado");
            }
        }

        if (subirPoder && poder <= energia)
        {
            poder += velocidadSubida;
            //sli2.value = poder;
            esferaT.localScale = new Vector3(poder, poder, poder);
            chromatic.intensity.value = normalChromatic + Mathf.Clamp01(poder / 50);
            transform.eulerAngles = new Vector3(0, 0, 0);
            esfera.SetActive(true);
            puedeComprobarPoder = true;
        }

        if (puedeComprobarPoder && poder >= energia && energia >= 0)
        {
            poder = energia;
            //sli2.value = poder;
            esferaT.localScale = new Vector3(poder, poder, poder);
            chromatic.intensity.value = normalChromatic + Mathf.Clamp01(poder / 50);
            transform.eulerAngles = new Vector3(0, 0, 0);
            esfera.SetActive(true);
        }

        if (!subirPoder && poder >= 0)
        {
            poder -= velocidadSubida;
            //sli2.value = poder;
            esferaT.localScale = new Vector3(poder, poder, poder);
            chromatic.intensity.value = normalChromatic + Mathf.Clamp01(poder / 50);
            transform.eulerAngles = new Vector3(0, 0, 0);
            esfera.SetActive(true);
            puedeComprobarPoder = false;
        }

        if (poder <= 0)
        {
            esferaT.localScale = new Vector3(0, 0, 0);
            chromatic.intensity.value = normalChromatic;
            esfera.SetActive(false);
        }

        if (triggerPowerUp.onlyT)
        {
            if (!enter)
            {
                CargarEnergia(5);
                enter = true;
            }
        }
        else
        {
            enter = false;
        }
    }
}