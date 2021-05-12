using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class pasarVid : MonoBehaviour
{
    public VideoPlayer player;

    private bool comprobar;
    private bool mandando;

    private void Start()
    {
        StartCoroutine(delayStart());
    }

    private void Update()
    {
        if (comprobar && !player.isPlaying)
        {
            pasar();
        }
    }

    public void pasar()
    {
        if (!mandando)
        {
            Initiate.Fade("SampleScene", Color.black, 1);
            mandando = true;
        }
    }

    IEnumerator delayStart()
    {
        yield return new WaitForSecondsRealtime(5);
        comprobar = true;
    }
}
