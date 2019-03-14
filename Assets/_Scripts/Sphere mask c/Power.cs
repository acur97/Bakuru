using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class Power : MonoBehaviour
{
    public Slider sli;
    public Slider sli2;
    public GameObject esfera;
    public DisolveTrigger triggerPowerUp;
    public float velocidadSubida;
    public float velocidadBajada;
    [Space]
    public PostProcessVolume post;

    private float poder = 25;
    //private float energia = 25;
    private bool subido;
    //private bool comenzar;
    private bool bajarSpeed;
    private bool bajarPoder;
    private bool ChangeSpeed_UP;
    private bool ChangeSpeed_DOWN;
    private bool enter;
    private bool exit = true;
    private ChromaticAberration chromatic;
    private float normalChromatic;
    private Bloom bloom;
    private float normalBloom;
    private Grain grain;
    private float normalGrain;
    private Transform esferaT;
    private SphereCollider esferaR;

    private int count = 0;
    private float speed = 0;

    private void Awake()
    {
        esferaT = esfera.transform;
        esferaR = esfera.GetComponent<SphereCollider>();
        esferaR.enabled = false;
        esferaT.localScale = new Vector3(0, 0, 0);
        post.profile.TryGetSettings(out chromatic);
        post.profile.TryGetSettings(out bloom);
        post.profile.TryGetSettings(out grain);
        normalChromatic = chromatic.intensity.value;
        normalBloom = bloom.intensity.value;
        normalGrain = grain.intensity.value;
        triggerPowerUp.tagg = "PowerUp";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            count += 1;

            if (count == 1)
            {
                ChangeSpeed_DOWN = false;
                ChangeSpeed_UP = true;
                Debug.Log("activar");
            }
            if (count == 2)
            {
                ChangeSpeed_DOWN = true;
                ChangeSpeed_UP = false;
                count = 0;
                Debug.Log("desactivar");
            }
        }

        if (ChangeSpeed_UP)
        {
            speed += velocidadSubida;
            sli.value = speed;

            if (speed == 25)
            {
                ChangeSpeed_DOWN = true;
                ChangeSpeed_UP = false;
            }
        }

        if (ChangeSpeed_DOWN)
        {
            speed -= velocidadSubida;
            sli.value = speed;

            if (speed == 0)
            {
                ChangeSpeed_DOWN = false;
            }
        }

        Debug.Log(speed);
    }
}