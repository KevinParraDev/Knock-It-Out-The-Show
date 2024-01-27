using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Temporizador")]
    [SerializeField] private TMP_Text temporizadorUI;
    [SerializeField] private float startTime;
    private float temporizador;
    private bool relojEncendido = false;

    void Start()
    {
        ReiniciarCronometro();
        EncenderCronometro(true);
    }

    public void EncenderCronometro(bool estado)
    {
        relojEncendido = estado;
    }

    public void ReiniciarCronometro()
    {
        temporizador = startTime;
        temporizadorUI.text = startTime.ToString();
    }

    public void FinDelJuego()
    {
        GameManager.Instance.ReloadLevel();
    }

    void Update()
    {
        if (relojEncendido)
        {
            temporizador -= Time.deltaTime;
            int seg = Mathf.FloorToInt(temporizador);
            temporizadorUI.text = seg.ToString();

            if(seg == 0)
            {
                EncenderCronometro(false);
                temporizadorUI.text = "0";
                FinDelJuego();
            }
        }
    }
}
