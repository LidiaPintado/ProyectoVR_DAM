using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Personalizada : MonoBehaviour
{
    public Text Banderas;
    public Text Fallos;
    public Slider SelectorBanderas;
    public Slider SelectorFallos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cambiarBanderas()
    {
        Banderas.text = SelectorBanderas.value.ToString();
    }

    public void cambiarFallos()
    {
        if (SelectorFallos.value != 100)
            Fallos.text = SelectorFallos.value.ToString();
        else 
        { 
            Fallos.text = "∞";
        }
    }

    public void continuar()
    {
        ValoresNivel.MAX_EXITOS = (int)SelectorBanderas.value;
        ValoresNivel.FALLOS = 0;
        ValoresNivel.MAX_FALLOS = (int)SelectorFallos.value;
        ValoresNivel.EXITOS = 0;
    }
}
