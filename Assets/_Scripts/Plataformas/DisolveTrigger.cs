using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisolveTrigger : MonoBehaviour
{
    public bool onlyT;
    public bool stayT;
    public bool stayC;
    public string tagg;
    public string tagg2;
    [Space]
    public bool destruirOtro;
    public bool destruirOtroColecc;
    public Transform puntoObjetos;
    private bool tomarColeccionable;
    private bool moverCollAbajo = true;
    private bool tomarPoder;
    private GameObject obj;
    private float espera = 5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagg))
        {
            onlyT = true;
            if (destruirOtro)
            {
                tomarPoder = true;
                obj = other.gameObject;
            }
        }
        if (destruirOtroColecc && other.CompareTag(tagg2))
        {
            onlyT = true;
            tomarColeccionable = true;
            obj = other.gameObject;
        }
    }

    private void Update()
    {
        if (tomarPoder)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, transform.position, Time.unscaledDeltaTime * 1.5f);

            obj.transform.localScale = new Vector3(obj.transform.localScale.x - 0.025f, obj.transform.localScale.y - 0.025f, obj.transform.localScale.z - 0.025f);
            if (obj.transform.localScale.x <= 0)
            {
                Destroy(obj);
                tomarPoder = false;
            }
        }
        if (tomarColeccionable)
        {
            if (moverCollAbajo)
            {
                obj.transform.position = Vector3.MoveTowards(obj.transform.position, puntoObjetos.position, Time.unscaledDeltaTime * 2.5f);
            }
            else
            {
                obj.transform.position = Vector3.MoveTowards(obj.transform.position, transform.position, Time.unscaledDeltaTime * 3.25f);
                obj.transform.localScale = new Vector3(obj.transform.localScale.x - 0.02f, obj.transform.localScale.y - 0.02f, obj.transform.localScale.z - 0.02f);
                if (obj.transform.localScale.x <= 0)
                {
                    Destroy(obj);
                    tomarColeccionable = false;
                    moverCollAbajo = true;
                }
            }
            espera -= Time.unscaledDeltaTime;
            if (espera <= 0)
            {
                moverCollAbajo = false;
            }
            else
            {
                float valA = obj.transform.localScale.x;
                float val = valA + (Mathf.Sin(Time.unscaledTime * 6) / 75);
                obj.transform.localScale = new Vector3(val, val, val);
            }
        }
    }

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
            onlyT = false;
            //if (destruirOtro)
            //{
            //    tomarPoder = false;
            //    obj = null;
            //}
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