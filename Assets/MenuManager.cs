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

        this.menu = GameObject.Find("Menu");
        this.ganador = GameObject.Find("Resultado");

        ganador.SetActive(false);
        menu.SetActive(false); 

        if(Ganador())
        {
            
            banderas.text = ValoresNivel.EXITOS + "";
            banderasMax.text = ValoresNivel.MAX_EXITOS + "";
            fallos.text = (ValoresNivel.MAX_FALLOS - ValoresNivel.FALLOS) + "";

            if (ValoresNivel.FALLOS == 0)
            {
                mensajeResultado.text = "No has completado el nivel. Suerte en la próxima";
                color.color = ROJO;
            }
            else
            {
                mensajeResultado.text = "Enhorabuena has completado el nivel";
                color.color = VERDE;
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
        return (ValoresNivel.FALLOS == 0) || (ValoresNivel.EXITOS == ValoresNivel.MAX_EXITOS);
    }

}
