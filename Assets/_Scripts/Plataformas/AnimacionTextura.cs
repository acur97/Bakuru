using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionTextura : MonoBehaviour {

    public Vector2 Offset;
    public float Velocidad;
    [Space]
    public Material mat;

    private void Update()
    {
        mat.mainTextureOffset = new Vector2((Offset.x * Time.time * Velocidad), (Offset.y * Time.time * Velocidad));
    }
}
