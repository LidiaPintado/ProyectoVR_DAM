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
        punMax.text = ValoresNivel.MAX_EXITOS + "";
        if(ValoresNivel.MAX_FALLOS != 100)
            fallMax.text = ValoresNivel.MAX_FALLOS + "";
        else
        {
            fallMax.text = "∞";
        }
    }
}
