using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesDestruye : MonoBehaviour
{
    public float tiempoEspera;
    [Space]
    public GameObject cube;
    private MeshRenderer[] meshes;
    private Material[] mats;

    private bool enter;
    public DisolveTrigger trigger;
    //private Material mat;
    private Color32 col;
    private float HSV_h;
    private float HSV_s;
    private float HSV_v;
    private Color32 col2;
    private bool caer;
    private float sumandoCaida = 0;

    private void Start()
    {
        //trigger = cube.GetComponent<DisolveTrigger>();
        //mat = cube.GetComponent<MeshRenderer>().material;
        meshes = GetComponentsInChildren<MeshRenderer>();
        mats = new Material[meshes.Length];
        for (int i = 0; i < meshes.Length; i++)
        {
            mats[i] = meshes[i].material;
        }
        col = mats[1].GetColor("_Color");
        Color.RGBToHSV(col, out HSV_h, out HSV_s, out HSV_v);
        trigger.tagg = "Player";
    }

    private void Update()
    {
        if (trigger.stayT)
        {
            if (!enter)
            {
                enter = true;

                StartCoroutine(DelayCaida());
            }
        }

        if (caer)
        {
            sumandoCaida += 0.002f;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - sumandoCaida, transform.localPosition.z);
        }
    }

    IEnumerator DelayCaida()
    {
        StartCoroutine(ChangeSpeed(HSV_v, (HSV_v - 0.5f), tiempoEspera));
        yield return new WaitForSeconds(tiempoEspera);
        caer = true;
    }

    IEnumerator ChangeSpeed(float v_start, float v_end, float duration)
    {
        float speed;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            speed = Mathf.Lerp(v_start, v_end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
            
            HSV_v = speed;
            col2 = Color.HSVToRGB(HSV_h, HSV_s, HSV_v);
            for (int i = 0; i < mats.Length; i++)
            {
                mats[i].SetColor("_Color", col2);
            }
            //mat.SetColor("_Color", col2);
        }
        speed = v_end;
        
        HSV_v = speed;
        col2 = Color.HSVToRGB(HSV_h, HSV_s, HSV_v);
        //mat.SetColor("_Color", col2);
        for (int i = 0; i < mats.Length; i++)
        {
            mats[i].SetColor("_Color", col2);
        }

        yield return new WaitForSeconds(duration);
    }
}
