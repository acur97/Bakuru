using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisolveTrigger : MonoBehaviour
{
    public bool stayT;
    public bool stayC;
    public string tagg;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(tagg))
        {
            stayT = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagg))
        {
            stayT = false;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag(tagg))
        {
            stayC = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(tagg))
        {
            stayC = false;
        }
    }
}