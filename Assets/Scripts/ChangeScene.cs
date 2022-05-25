using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void LoadScene(string dificultad)
    {
        ValoresNivel.DIFICULTAD = dificultad;
        SceneManager.LoadScene("EscenaConMapa");
    }

    public static void LoadMenu()
    {
        SceneManager.LoadScene("MenuInicio");
    }
}
