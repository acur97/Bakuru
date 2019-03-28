using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisolveObject : MonoBehaviour
{
    public enum metodo {Aparecer, Desaparecer};
    public metodo forma;
    public float velDisolucion = 0.5f;
    [Space]
    public Transform trans;
    public DisolveTrigger trigger;
    [Header("Atributos solo al aparecer")]
    public float alphaVal = 112;
    public float emissionPower = 1.25f;

    private Material mat;
    private float firstX;
    private Color32 col;
    private Color32 colE;
    private float HSV_h;
    private float HSV_s;
    private float HSV_v;
    private BoxCollider Bcoll;
    private bool enter;
    private bool exit = true;
    private float speed = 0;

    private void Awake()
    {
        mat = trans.GetComponent<MeshRenderer>().material;
        Bcoll = trans.GetComponent<BoxCollider>();
        col = mat.GetColor("_Color");
        if (forma == metodo.Aparecer)
        {
            colE = mat.GetColor("_EmissionColor");
            Color32 col2 = new Color32(col.r, col.g, col.b, 0);
            Color.RGBToHSV(colE, out HSV_h, out HSV_s, out HSV_v);
            HSV_v = 0;
            Color32 colE2 = Color.HSVToRGB(HSV_h, HSV_s, HSV_v);
            mat.SetColor("_Color", col2);
            mat.SetColor("_EmissionColor", colE2);
        }
        trigger.tagg = "PlayerSphere";
    }

    private void Update()
    {
        if (trigger.stayT)
        {
            if (!enter)
            {
                StartCoroutine(ChangeSpeed(0, 2, velDisolucion));
                if (forma == metodo.Aparecer)
                {
                    Bcoll.enabled = true;
                }
                else
                {
                    Bcoll.enabled = false;
                }
                enter = true;
                exit = false;
            }

            if (forma == metodo.Aparecer)
            {
                Color32 col2 = new Color32(col.r, col.g, col.b, (byte)(speed * alphaVal));
                Color.RGBToHSV(colE, out HSV_h, out HSV_s, out HSV_v);
                HSV_v = speed * emissionPower;
                Color32 colE2 = Color.HSVToRGB(HSV_h, HSV_s, HSV_v);
                mat.SetColor("_Color", col2);
                mat.SetColor("_EmissionColor", colE2);
            }
            else
            {
                Color32 col2 = new Color32(col.r, col.g, col.b, (byte)((speed.Remap(0, 2, 2, 0)) * 127.5f));
                mat.SetColor("_Color", col2);
            }
        }
        else
        {
            if (!exit)
            {
                StartCoroutine(ChangeSpeed(2, 0, velDisolucion));
                exit = true;
                enter = false;
            }
            if (forma == metodo.Aparecer)
            {
                Color32 col2 = new Color32(col.r, col.g, col.b, (byte)(speed * alphaVal));
                Color.RGBToHSV(colE, out HSV_h, out HSV_s, out HSV_v);
                HSV_v = speed * emissionPower;
                Color32 colE2 = Color.HSVToRGB(HSV_h, HSV_s, HSV_v);
                mat.SetColor("_Color", col2);
                mat.SetColor("_EmissionColor", colE2);
                Bcoll.enabled = false;
            }
            else
            {
                Color32 col2 = new Color32(col.r, col.g, col.b, (byte)((speed.Remap(2, 0, 0, 2)) * 127.5f));
                mat.SetColor("_Color", col2);
                Bcoll.enabled = true;
            }
        }
    }
 
    IEnumerator ChangeSpeed(float v_start, float v_end, float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            speed = Mathf.Lerp(v_start, v_end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        speed = v_end;
        yield return new WaitForSeconds(duration);
    }
}