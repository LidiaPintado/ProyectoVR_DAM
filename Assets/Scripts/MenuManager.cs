using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour

{
    public MenuManager instance = null;

    public GameObject menu;
    public GameObject ganador;
    private static readonly Color VERDE = Color.green;
    private static readonly Color ROJO = Color.red;

    private void Awake()
    {
        if (instance == null)
            instance = this; // Set instance to this object
        else
            Destroy(gameObject); // Kill yo self
    }
    // Start is called before the first frame update
    void Start()
    {
        string ruta = "/MenuInicial/Resultado/Panel/";
        Image color = GameObject.Find(ruta + "Color").GetComponent<Image>();
        TextMeshProUGUI mensajeResultado = GameObject.Find(ruta + "Mensaje").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI banderas = GameObject.Find(ruta + "Banderas").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI banderasMax = GameObject.Find(ruta + "MaxBanderas").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI fallos = GameObject.Find(ruta + "Fallos").GetComponent<TextMeshProUGUI>();

        TextMeshProUGUI msgF1 = GameObject.Find(ruta + "MsgF1").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI msgF2 = GameObject.Find(ruta + "MsgF2").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI msgF3 = GameObject.Find(ruta + "MsgF3").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI msgF4 = GameObject.Find(ruta + "MsgF4").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI outlineMsgRes = GameObject.Find(ruta + "Mensaje").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI outlineBanderas = GameObject.Find(ruta + "Banderas").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI outlineBanMax = GameObject.Find(ruta + "MaxBanderas").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI outlineFallos = GameObject.Find(ruta + "Fallos").GetComponent<TextMeshProUGUI>();

        Debug.Log("Esta vacio? " + (mensajeResultado == null));

        this.menu = GameObject.Find("Menu");
        this.ganador = GameObject.Find("Resultado");

        ganador.SetActive(false);
        menu.SetActive(false); 

        if(Ganador())
        {
            
            
            banderas.text = ValoresNivel.EXITOS + "";
            banderasMax.text = ValoresNivel.MAX_EXITOS + "";
            fallos.text = ValoresNivel.FALLOS + "";

            if (ValoresNivel.MAX_FALLOS == 0)
            {
                mensajeResultado.text = "No has completado el nivel. Suerte en la próxima";
                outlineMsgRes.outlineColor = ROJO;
                msgF1.outlineColor = ROJO;
                msgF2.outlineColor = ROJO;
                msgF3.outlineColor = ROJO;
                msgF4.outlineColor = ROJO;     
                outlineBanderas.outlineColor = ROJO;
                outlineBanMax.outlineColor = ROJO;
                outlineFallos.outlineColor = ROJO;
            }
            else
            {
                mensajeResultado.text = "Enhorabuena has completado el nivel";
                msgF1.outlineColor = VERDE;
                msgF2.outlineColor = VERDE;
                msgF3.outlineColor = VERDE;
                msgF4.outlineColor = VERDE;
                outlineMsgRes.outlineColor = VERDE;
                outlineBanderas.outlineColor = VERDE;
                outlineBanMax.outlineColor = VERDE;
                outlineFallos.outlineColor = VERDE;
            }
            ganador.SetActive(true);
        }
        else
        {
            CargarMenu();
        }
    }

    public void CargarMenu()
    {
        ganador.SetActive(false);
        menu.SetActive(true);
    }

    private bool Ganador()
    {
        return (ValoresNivel.MAX_FALLOS == 0) || (ValoresNivel.EXITOS == ValoresNivel.MAX_EXITOS);
    }

}
