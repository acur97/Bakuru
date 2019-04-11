using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotarColeccionable : MonoBehaviour
{
    public float frecuencia = 1;
    public float potencia = 10;

    private float currentY;
    private float val;

    private void Awake()
    {
        currentY = transform.localEulerAngles.y;
    }

    void Update()
    {
        val = currentY + (Mathf.Sin(Time.unscaledTime * frecuencia) * potencia);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, val, transform.localEulerAngles.z);
    }
}
