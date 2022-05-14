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
        socket = GetComponents<XRSocketInteractor>()[0];
        socket.onSelectEntered.AddListener(ValidarBandera);
        socket.onSelectExited.AddListener(LimpiarOutline);

    }

    public void ValidarBandera(XRBaseInteractable interactable)
    {
        Outline outline = interactable.GetComponent<Outline>();
        outline.OutlineWidth = 10f;
        if (name.Equals(interactable.gameObject.name))
        {
            // TODO: agregar sonido de exito y efecto confeti, fuegos artificiales o similar
            //SoundManager.PlayOneShotMusic(this.GetComponent<AudioSource>(),);
            //Puntuacion.Exito();
            this.GetComponent<AudioSource>().PlayOneShot(exito);
            outline.OutlineColor = Color.green;
        } else
        {
            //Puntuacion.Fallo();
            this.GetComponent<AudioSource>().PlayOneShot(fallo);
            outline.OutlineColor = Color.red;
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
