using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class maskPos : MonoBehaviour
{
    public Material mat;
    public float radiusOffset;

    private Vector4 vec;

    private void Update()
    {
        vec = new Vector4(transform.position.x, transform.position.y, transform.position.z, 0);
        mat.SetFloat("_Radius", transform.localScale.x + radiusOffset);
        mat.SetVector("_Position", vec);
    }
}