using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Audio;


public class ComprobadorBandera : MonoBehaviour
{

    [Tooltip("El XRGrabInteractable que deseas afectar o vacio para referenciar al objeto que lo contiene")]
    XRSocketInteractor socket;
    public AudioClip exito;
    public AudioClip fallo;

    private void Awake()
    {
        socket = this.GetComponent<XRSocketInteractor>();
        socket.onSelectEntered.AddListener(ValidarBandera);
        socket.onSelectExited.AddListener(LimpiarOutline);

    }

    public void ValidarBandera(XRBaseInteractable interactable)
    {
        Outline outline = interactable.GetComponent<Outline>();
        if (outline == null)
            Debug.LogError("El <b>interactable</b> no contiene un <b>Outline</b>");

        AudioSource source = this.GetComponent<AudioSource>();
        if (source == null)
            Debug.LogError("<b>socket</b> no contiene un <b>AudioSource</b>");

        outline.OutlineWidth = 5f;

        if (name.Equals(interactable.gameObject.name))
        {
            interactable.gameObject.layer = LayerMask.NameToLayer("Non-Interactable");
            outline.OutlineColor = Color.green;
            source.PlayOneShot(exito);
            Puntuacion.Exito();
        }
        else
        {
            outline.OutlineColor = Color.red;
            source.PlayOneShot(fallo);
            Puntuacion.Fallo();
        }
    }

    public void LimpiarOutline(XRBaseInteractable interactable)
    {
        Outline outline = interactable.GetComponent<Outline>();
        if (outline) {
            outline.OutlineWidth = 0f;
        }
    }
}
