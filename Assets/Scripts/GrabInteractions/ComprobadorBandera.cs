using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using FunWithFlags.GrabInteractions;

public class ComprobadorBandera : MonoBehaviour
{

    [Tooltip("El XRGrabInteractable que deseas afectar o vacio para referenciar al objeto que lo contiene")]
    XRSocketInteractor socket;

    /* Cuando una bandera entra en el socket comprobamos que sea la bandera correcta
     * en caso de correcta (ALGO)
     *      Outline Verde? ¿Permanente/Tiempo?
     *      Sonido de exito?
     * en caso de incorrecta ReturnHome
     *      Sonido de error?
     *      Outline rojo?-¿cuanto Tiempo?
     */


    private void Awake()
    {
        socket = GetComponents<XRSocketInteractor>()[0];
        //socket.selectEntered.AddListener(OnSelect);
        //socket.selectExited.AddListener(OnSelectExit);
        socket.onSelectEntered.AddListener(ValidarBandera);

    }

    //private void OnSelect(SelectEnterEventArgs arg0) => ValidarBandera(arg0.interactable);


    //private void OnSelectExit(SelectExitEventArgs arg0) => Invoke(nameof(AttemptReturnHome), resetDelayTime);

    public void ValidarBandera(XRBaseInteractable interactable)
    {
        if (interactable.gameObject.name.Equals("Bola Azul"))
        {
            Debug.Log("BOLA AZUL AGARRADA!!!!!!!");
        } else
        {
            socket.recycleDelayTime = 1f;
            interactable.gameObject.GetComponent<ReturnToHome>().ReturnHome();
        }
    }
}
