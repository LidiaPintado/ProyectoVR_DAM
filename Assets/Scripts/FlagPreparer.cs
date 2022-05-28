using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FlagPreparer : MonoBehaviour
{
    XRSocketInteractor socket;
    void Awake()
    {
        this.socket = GetComponents<XRSocketInteractor>()[0];
    }

    private void OnEnable()
    {
        this.socket.selectExited.AddListener(OnSelectExit);
    }

    private void OnSelectExit(SelectExitEventArgs arg0)
    {
        Rigidbody flagRigidBody = arg0.interactable.gameObject.GetComponent<Rigidbody>();
        flagRigidBody.useGravity = true;
    }

}
