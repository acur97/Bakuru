using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDescubrirEspiritual : MonoBehaviour
{
    public Power poder;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            poder.TriggerPoderExterno();
        }
    }
}
