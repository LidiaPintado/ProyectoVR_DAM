using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GripPressed : MonoBehaviour
{
    private ActionBasedController controller;
    private bool isPressed;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ActionBasedController>();

        bool isPressed = controller.selectAction.action.ReadValue<bool>();

        controller.selectAction.action.performed += Action_performed;

    }

    private void Action_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Pressing Grip Button");
    }


    // Update is called once per frame
    void Update()
    {
        if (isPressed)
        {

        }
    }
}
