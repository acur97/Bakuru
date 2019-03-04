using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverPorPuntos : MonoBehaviour
{
    public Transform trans;
    public float velocidad = 5;
    public bool suavizado = true;
    [Header("Colocar primer punto en el centro del objeto")]
    public Transform[] puntos;

    private int count;
    private bool subir;
    private float velocidad2;
    private float speed = 1;
    private bool cambio;
    private void Awake()
    {
        trans.position = puntos[count].position;
    }
    private void Update()
    {
        if (suavizado)
        {
            if (!cambio && Vector3.Distance(trans.position, puntos[count].position) <= (velocidad / 5))
            {
                StartCoroutine(ChangeSpeed(1, 0.2f, ((velocidad / 2) * 0.1f)));
                cambio = true;
            }

            velocidad2 = velocidad * speed;
        }
        else
        {
            velocidad2 = velocidad;
        }

        trans.position = Vector3.MoveTowards(trans.position, puntos[count].position, (velocidad2 * Time.unscaledDeltaTime));

        if (trans.position == puntos[count].position)
        {
            StartCoroutine(ChangeSpeed(0.2f, 1, ((velocidad / 2) * 0.1f)));
            cambio = false;

            if (count == 0)
            {
                subir = true;
            }
            if (count == (puntos.Length - 1))
            {
                subir = false;
            }

            if (subir)
            {
                count += 1;
            }
            else
            {
                count -= 1;
            }
        }
    }
    IEnumerator ChangeSpeed(float v_start, float v_end, float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            speed = Mathf.Lerp(v_start, v_end, elapsed / duration);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        speed = v_end;
        yield return new WaitForSeconds(duration);
    }
}