using FunWithFlags.GrabInteractions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorBanderas : MonoBehaviour
{
    private static GameObject[] banderas;
    private static GameObject[] displays;
    private static Quaternion rotacionDeseada;
    private static int cantidadObjetivoBanderas;
    private void Start()
    {
        cantidadObjetivoBanderas = ValoresNivel.MAX_EXITOS;
        rotacionDeseada = GetRotacionDeseada();
        banderas = GetBanderas();
        displays = GetDisplays();
        SeleccionarBanderas();
    }

    private GameObject[] GetBanderas()
    {
        GameObject[] banderas = GameObject.FindGameObjectsWithTag("Bandera");
        if (banderas == null || banderas.Length == 0)
            throw new System.Exception("Banderas no encontradas. No hay ningun objeto con el tag <b>Bandera</b>");

        return banderas;
    }

    private GameObject[] GetDisplays()
    {
        GameObject[] displays = GameObject.FindGameObjectsWithTag("Display");
        
        if (displays == null || displays.Length == 0)
            throw new System.Exception("Displays no encontrados. No hay ningun objeto con el tag <b>Display</b>");

        return displays;
    }

    private Quaternion GetRotacionDeseada()
    {
        GameObject banderaEjemplo = GameObject.Find("BanderaEjemplo");

        if (banderaEjemplo == null)
            throw new System.Exception("BanderaEjemplo no encontrada. No hay ningun objeto con el nombre <b>BanderaEjemplo</b> del cual sacar la rotaci�n");

        return banderaEjemplo.transform.rotation;
    }
    private void SeleccionarBanderas()
    {
        deshabilitaColisionBanderas();
        ArrayList seleccionadas = new ArrayList();
        while (seleccionadas.Count != cantidadObjetivoBanderas)
        {
            int random = Random.Range(0, banderas.Length);
            if (!seleccionadas.Contains(random))
            {
                ReturnToHome teleporter = banderas[random].GetComponent<ReturnToHome>();
                if (!teleporter)
                {
                    throw new System.Exception("ReturnToHome component no existente en la bandera indicada");
                }
                GameObject display = displays[seleccionadas.Count];
                Transform transformObjetivo = new GameObject().transform;
                transformObjetivo.SetPositionAndRotation(display.transform.position, rotacionDeseada);
                teleporter.TeleportToNewHome(transformObjetivo);
                seleccionadas.Add(random);
            }
                
        }
        Invoke(nameof(habilitaColisionbanderas), 1);
    }

    private void deshabilitaColisionBanderas()
    {
        int layerBandera = LayerMask.NameToLayer("Bandera");
        if (layerBandera == -1)
            throw new System.Exception("Layer <b>Bandera</b> no encontrada");
        Physics.IgnoreLayerCollision(layerBandera, layerBandera);
    }

    private void habilitaColisionbanderas()
    {
        int layerBandera = LayerMask.NameToLayer("Bandera");
        if (layerBandera == -1)
            throw new System.Exception("Layer <b>Bandera</b> no encontrada");
        Physics.IgnoreLayerCollision(layerBandera, layerBandera, false);
    }
}
