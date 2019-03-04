using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesPatron : MonoBehaviour
{
    public float tiempoEntreCubos;
    public float duracionVivos;
    public float tiempoFade = 0.3f;
    [Header("De menor a mayor")]
    public GameObject[] Cubos;

    private BoxCollider[] colliders;
    private MeshRenderer[] meshes;
    private int count = 0;
    private Color32[] colors;

    private void Awake()
    {
        colliders = new BoxCollider[Cubos.Length];
        meshes = new MeshRenderer[Cubos.Length];
        colors = new Color32[Cubos.Length];

        for (int i = 0; i < Cubos.Length; i++)
        {
            colliders[i] = Cubos[i].GetComponent<BoxCollider>();
            colliders[i].enabled = false;
            meshes[i] = Cubos[i].GetComponent<MeshRenderer>();
            meshes[i].enabled = false;
            colors[i] = meshes[i].material.GetColor("_Color");
            Color32 col = new Color32(colors[i].r, colors[i].g, colors[i].b, 0);
            meshes[i].material.SetColor("_Color", col);
        }
    }

    private void Start()
    {
        StartCoroutine(ActivarCubo());
    }

    IEnumerator ActivarCubo()
    {
        yield return new WaitForSeconds(tiempoEntreCubos);
        StartCoroutine(Act(count));
        count += 1;
        if (count == Cubos.Length)
        {
            count = 0;
        }

        yield return new WaitForSeconds(tiempoEntreCubos);
        StartCoroutine(Act(count));
        count += 1;
        if (count == Cubos.Length)
        {
            count = 0;
        }
        StartCoroutine(ActivarCubo());
    }

    IEnumerator Act(int contador)
    {
        StartCoroutine(ChangeSpeed_UP(0, 255, tiempoFade, contador));
        yield return new WaitForSeconds(duracionVivos);
        StartCoroutine(ChangeSpeed_DOWN(255, 0, tiempoFade, contador));
    }
 
    IEnumerator ChangeSpeed_UP(float v_start, float v_end, float duration, int valor)
    {
        Color32 col;
        float speed;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            speed = Mathf.Lerp(v_start, v_end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;

            if (speed >= 127.5f)
            {
                meshes[valor].enabled = true;
                colliders[valor].enabled = true;
            }

            col = new Color32(colors[valor].r, colors[valor].g, colors[valor].b, (byte)speed);
            meshes[valor].material.SetColor("_Color", col);
        }
        speed = v_end;

        col = new Color32(colors[valor].r, colors[valor].g, colors[valor].b, (byte)speed);
        meshes[valor].material.SetColor("_Color", col);

        yield return new WaitForSeconds(duration);
    }

    IEnumerator ChangeSpeed_DOWN(float v_start, float v_end, float duration, int valor)
    {
        Color32 col;
        float speed;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            speed = Mathf.Lerp(v_start, v_end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;

            if (speed <= 127.5f)
            {
                meshes[valor].enabled = false;
                colliders[valor].enabled = false;
            }

            col = new Color32(colors[valor].r, colors[valor].g, colors[valor].b, (byte)speed);
            meshes[valor].material.SetColor("_Color", col);
        }
        speed = v_end;

        col = new Color32(colors[valor].r, colors[valor].g, colors[valor].b, (byte)speed);
        meshes[valor].material.SetColor("_Color", col);

        yield return new WaitForSeconds(duration);
    }
}