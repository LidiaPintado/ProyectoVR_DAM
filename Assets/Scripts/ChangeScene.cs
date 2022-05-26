using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void LoadScene(string dificultad)
    {
        ValoresNivel.DIFICULTAD = dificultad;
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
        SceneManager.LoadScene("EscenaConMapa");
    }

    public static void LoadMenu()
    {
        SceneManager.LoadScene("MenuInicio");
    }

    private void changeValues(int max_exitos, int fallos)
    {
        ValoresNivel.MAX_EXITOS = max_exitos;
        ValoresNivel.FALLOS = fallos;
        ValoresNivel.MAX_FALLOS = fallos;
        ValoresNivel.EXITOS = 0;
        
    }
}
