using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using FunWithFlags.GrabInteractions;

public class ComprobadorBandera : MonoBehaviour
{

    [Tooltip("El XRGrabInteractable que deseas afectar o vacio para referenciar al objeto que lo contiene")]
    XRSocketInteractor socket;

    private void Awake()
    {
        socket = GetComponents<XRSocketInteractor>()[0];
        //socket.selectEntered.AddListener(OnSelect);
        //socket.selectExited.AddListener(OnSelectExit);
        socket.onSelectEntered.AddListener(ValidarBandera);
        socket.onSelectExited.AddListener(LimpiarOutline);

    }

    public void ValidarBandera(XRBaseInteractable interactable)
    {
        Outline outline = interactable.GetComponent<Outline>();
        outline.OutlineWidth = 10f;
        if (interactable.gameObject.name.Equals("Cubo Azul"))
        {
            outline.OutlineColor = Color.green;
        } else
        {
            outline.OutlineColor = Color.red;
        }
    }

    public void LimpiarOutline(XRBaseInteractable interactable)
    {
        Outline outline = interactable.GetComponent<Outline>();
        outline.OutlineWidth = 0f;
    }
}
