using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ReturnToHome : MonoBehaviour
{
    [Tooltip("The Transform that the object will return to")]

    XRGrabInteractable m_GrabInteractable;
    [SerializeField] Vector3 returnToPsition;
    [SerializeField] float resetDelayTime;
    protected bool shouldReturnHome { get; set; }

    void Awake()
    {
        m_GrabInteractable = GetComponents<XRGrabInteractable>()[0];
        Debug.Log("Awake" + m_GrabInteractable.name);
        returnToPsition = this.transform.position;
        shouldReturnHome = true;
    }

    private void OnEnable()
    {
        m_GrabInteractable.selectEntered.AddListener(OnSelect);
        m_GrabInteractable.selectExited.AddListener(OnSelectExit);

    }

    private void OnSelect(SelectEnterEventArgs arg0) => CancelInvoke("ReturnHome");
    private void OnSelectExit(SelectExitEventArgs arg0) => Invoke(nameof(shouldReturnHome), resetDelayTime);

    protected virtual void ReturnHome()
    {
        if (shouldReturnHome)
        {
            transform.position = returnToPsition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsController(other.gameObject))
        {
            return;
        }

        var socket = other.gameObject.GetComponent<XRSocketInteractor>();
        bool isEngaged = socket != null && socket.CanSelect(m_GrabInteractable);
        shouldReturnHome = !isEngaged;

    }

    private void OnTriggerExit(Collider other)
    {
        if (IsController(other.gameObject))
        {
            return;
        }
        shouldReturnHome = true;
    }

    private bool IsController(GameObject collided)
    {
        return collided.gameObject.GetComponent<XRBaseController>() != null;
    }
}