using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    // public static Timer instanciar;
    public Text Crono;
    private TimeSpan tiempoCronometro;
    private bool timerBool = false;
    public float tiempoTrans;

    private void Start()
    {
        Crono.text = "00:00";
    }

    public void Update()
    {
        if (timerBool)
        {
            tiempoTrans += Time.deltaTime;
            tiempoCronometro = TimeSpan.FromSeconds(tiempoTrans);
            Crono.text = tiempoCronometro.ToString("mm':'ss");
        }
        if (Crono.text == "03:00")
        {
            finTiempo();
            tiempoTrans = 180;
        }
    }
    public void iniciaTiempo()
    {
        timerBool = true;
        tiempoTrans = 0F;

    }
    public void finTiempo()
    {
        timerBool = false;

    }


}
