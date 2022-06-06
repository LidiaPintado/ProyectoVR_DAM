using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour

{
    public MenuManager instance = null;

    public GameObject menu;
    public GameObject ganador;
    private static readonly Color VERDE = new Color32(87, 217, 120, 255);
    private static readonly Color ROJO = new Color32(238, 91, 91, 255);

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
        Image color = GameObject.Find("/MenuInicial/Resultado/Panel/Color").GetComponent<Image>();
        Text mensajeResultado = GameObject.Find("/MenuInicial/Resultado/Panel/Mensaje").GetComponent<Text>();
        Text banderas = GameObject.Find("/MenuInicial/Resultado/Panel/Banderas").GetComponent<Text>();
        Text banderasMax = GameObject.Find("/MenuInicial/Resultado/Panel/MaxBanderas").GetComponent<Text>();
        Text fallos = GameObject.Find("/MenuInicial/Resultado/Panel/Fallos").GetComponent<Text>();

        Outline msgF1 = GameObject.Find("/MenuInicial/Resultado/Panel/MsgF1").GetComponent<Outline>();
        Outline msgF2 = GameObject.Find("/MenuInicial/Resultado/Panel/MsgF2").GetComponent<Outline>();
        Outline msgF3 = GameObject.Find("/MenuInicial/Resultado/Panel/MsgF3").GetComponent<Outline>();
        Outline msgF4 = GameObject.Find("/MenuInicial/Resultado/Panel/MsgF4").GetComponent<Outline>();
        Outline outlineMsgRes = mensajeResultado.GetComponent<Outline>();
        Outline outlineBanderas = banderas.GetComponent<Outline>();
        Outline outlineBanMax = banderasMax.GetComponent<Outline>();
        Outline outlineFallos = fallos.GetComponent<Outline>();

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
                msgF1.OutlineColor = ROJO;
                msgF2.OutlineColor = ROJO;
                msgF3.OutlineColor = ROJO;
                msgF4.OutlineColor = ROJO;
                outlineMsgRes.OutlineColor = ROJO;
                outlineBanderas.OutlineColor = ROJO;
                outlineBanMax.OutlineColor = ROJO;
                outlineFallos.OutlineColor = ROJO;
            }
            else
            {
                mensajeResultado.text = "Enhorabuena has completado el nivel";
                msgF1.OutlineColor = VERDE;
                msgF2.OutlineColor = VERDE;
                msgF3.OutlineColor = VERDE;
                msgF4.OutlineColor = VERDE;
                outlineMsgRes.OutlineColor = VERDE;
                outlineBanderas.OutlineColor = VERDE;
                outlineBanMax.OutlineColor = VERDE;
                outlineFallos.OutlineColor = VERDE;
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
