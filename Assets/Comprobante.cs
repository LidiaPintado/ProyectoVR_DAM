using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Comprobante : MonoBehaviour
{
    
    public Text punMax;
    public Text fallMax;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(ValoresNivel.DIFICULTAD);
        switch (ValoresNivel.DIFICULTAD)
        {
            case "Hard":
                changeValues(10, 3);
                break;

            case "Easy":
                changeValues(5, 7);
                break;

            default:

                break;

        }
    }

    private void changeValues(int max_exitos, int fallos)
    {
        ValoresNivel.MAX_EXITOS = max_exitos;
        ValoresNivel.FALLOS = fallos;
        ValoresNivel.EXITOS = 0;
        punMax.text = ValoresNivel.MAX_EXITOS + "";
        fallMax.text = ValoresNivel.FALLOS + "";
    }
}
