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
        [SerializeField] Vector3 returnPosition;
        //[SerializeField] Quaternion returnToPosition;
        [SerializeField] float resetDelayTime;
        protected bool ShouldReturnHome { get; set; }

        void Awake()
        {
            assignedInteractable = GetComponents<XRGrabInteractable>()[0];
            if (!returnPoint)
            {
                returnPoint = new GameObject().transform;
                returnPoint.position = returnPosition;
            }
            ShouldReturnHome = true;
        }

        private void OnEnable()
        {
            assignedInteractable.selectEntered.AddListener(OnSelect);
            assignedInteractable.selectExited.AddListener(OnSelectExit);

        }

        private void OnSelect(SelectEnterEventArgs arg0) => CancelInvoke(nameof(AttemptReturnHome));
        private void OnSelectExit(SelectExitEventArgs arg0) => Invoke(nameof(AttemptReturnHome), resetDelayTime);


        private void AttemptReturnHome()
        {
            if (ShouldReturnHome)
            {
                CancelInvoke(nameof(AttemptReturnHome));
                transform.position = returnPoint.position;
            }
        }

        private void ReturnHome()
        {
            transform.position = returnPosition;
        }

        protected void PreReturnHome()
        {

        }

        protected void PostReturnHome()
        {

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

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name.Equals("Suelo"))
            {
                Invoke(nameof(AttemptReturnHome), resetDelayTime);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.name.Equals("Suelo"))
            {
                CancelInvoke(nameof(AttemptReturnHome));
            }
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