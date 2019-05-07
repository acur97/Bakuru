using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public int checkPointCount;
    [Space]
    public GameObject particlesTocado;

    private void Awake()
    {
        particlesTocado.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.CheckPoint(checkPointCount, transform.position);
            particlesTocado.SetActive(true);
        }
    }
}
