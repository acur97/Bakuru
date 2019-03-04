using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesDestruye : MonoBehaviour
{
    public float tiempoEspera;
    [Space]
    public GameObject cube;

    private bool enter;
    private DisolveTrigger trigger;
    private Material mat;
    private Color32 col;
    private float HSV_h;
    private float HSV_s;
    private float HSV_v;
    private Color32 col2;
    private Rigidbody rb;

    private void Awake()
    {
        trigger = cube.GetComponent<DisolveTrigger>();
        mat = cube.GetComponent<MeshRenderer>().material;
        col = mat.GetColor("_Color");
        Color.RGBToHSV(col, out HSV_h, out HSV_s, out HSV_v);
        rb = cube.GetComponent<Rigidbody>();
        trigger.tagg = "Player";
    }

    private void Update()
    {
        if (trigger.stayC)
        {
            if (!enter)
            {
                enter = true;

                StartCoroutine(DelayCaida());
            }
        }
    }

    IEnumerator DelayCaida()
    {
        StartCoroutine(ChangeSpeed(HSV_v, (HSV_v - 0.5f), tiempoEspera));
        yield return new WaitForSeconds(tiempoEspera);
        rb.isKinematic = false;
        rb.useGravity = true;
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
            mat.SetColor("_Color", col2);
        }
        speed = v_end;
        
        HSV_v = speed;
        col2 = Color.HSVToRGB(HSV_h, HSV_s, HSV_v);
        mat.SetColor("_Color", col2);

        yield return new WaitForSeconds(duration);
    }
}
