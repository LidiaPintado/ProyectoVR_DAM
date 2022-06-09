using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Audio;
using UnityEngine.UI;


public class ComprobadorBandera : MonoBehaviour
{

    [Tooltip("El XRGrabInteractable que deseas afectar o vacio para referenciar al objeto que lo contiene")]
    XRSocketInteractor socket;
    public AudioClip exito;
    public AudioClip fallo;
    public Text exitos;
    public Text fallos;

    [Tooltip("Tiempo en segundos antes de que cargue la escena una vez ganes o pierdas")]
    [SerializeField] float loadDelayTime = 2f;

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

        outline.OutlineWidth = 8f;

        if (name.Equals(interactable.gameObject.name))
        {
            interactable.gameObject.layer = LayerMask.NameToLayer("Non-Interactable");
            outline.OutlineColor = Color.green;
            source.PlayOneShot(exito);
            ValoresNivel.EXITOS += 1;
            exitos.text = ValoresNivel.EXITOS + "";
            Debug.Log(ValoresNivel.EXITOS);
            if(ValoresNivel.EXITOS == ValoresNivel.MAX_EXITOS)
            {
                ChangeScene.LoadMenu();
            }
        }
        else
        {
            outline.OutlineColor = Color.red;
            source.PlayOneShot(fallo);
            ValoresNivel.FALLOS += 1;
            if(ValoresNivel.MAX_FALLOS != 100) { 
                ValoresNivel.MAX_FALLOS -= 1;
                fallos.text = ValoresNivel.MAX_FALLOS + "";
                Debug.Log(ValoresNivel.MAX_FALLOS);
                if(ValoresNivel.MAX_FALLOS == 0)
                {
                    ChangeScene.LoadMenu();
                }
            }
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
