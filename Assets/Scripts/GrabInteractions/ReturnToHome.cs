using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace FunWithFlags.GrabInteractions
{
    public class ReturnToHome : MonoBehaviour
    {
        [Tooltip("Un Transform especifico al que volver o vacio para que coja la posicion especifica del objeto asignado al iniciar")]
        public Transform returnPoint;

        [Tooltip("El XRGrabInteractable que deseas afectar o vacio para referenciar al objeto que lo contiene")]
        XRGrabInteractable assignedInteractable;
        [SerializeField] Vector3 returnToPosition;
        [SerializeField] float resetDelayTime;
        protected bool ShouldReturnHome { get; set; }

        void Awake()
        {
            assignedInteractable = GetComponents<XRGrabInteractable>()[0];
            if (returnPoint)
            {
                returnToPosition = returnPoint.transform.position;
            } else
            {
                returnToPosition = this.transform.position;
            }
            ShouldReturnHome = true;
        }

        private void OnEnable()
        {
            assignedInteractable.selectEntered.AddListener(OnSelect);
            assignedInteractable.selectExited.AddListener(OnSelectExit);

        }

        private void OnSelect(SelectEnterEventArgs arg0) => CancelInvoke(nameof(ReturnHome));
        private void OnSelectExit(SelectExitEventArgs arg0) => Invoke(nameof(ReturnHome), resetDelayTime);


        protected virtual void ReturnHome()
        {
            if (ShouldReturnHome)
            {
                transform.position = returnToPosition;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (IsController(other.gameObject))
            {
                return;
            }

            var socket = other.gameObject.GetComponent<XRSocketInteractor>();
            bool isEngaged = socket != null && socket.CanSelect(assignedInteractable);
            ShouldReturnHome = !isEngaged;

        }

        private void OnTriggerExit(Collider other)
        {
            if (IsController(other.gameObject))
            {
                return;
            }
            ShouldReturnHome = true;
        }

        private bool IsController(GameObject collided)
        {
            return collided.GetComponent<XRBaseController>() != null;
        }
    }
}