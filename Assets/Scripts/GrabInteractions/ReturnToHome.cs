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

        [Tooltip("Tiempo en segundos antes de que el objeto se teletransporte a su posicion inicial")]
        [SerializeField] float resetDelayTime = 2f;
        protected bool ShouldReturnHome { get; set; }

        void Awake()
        {
            assignedInteractable = GetComponents<XRGrabInteractable>()[0];
            if (!returnPoint)
            {
                returnPoint = new GameObject().transform;
                returnPoint.SetPositionAndRotation(transform.position, transform.rotation);
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
                ReturnHome();
            }
        }

        public void ReturnHome()
        {
            PreReturnHome();

            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.zero;
            rigidbody.rotation = returnPoint.rotation;
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.Sleep();
            transform.SetPositionAndRotation(returnPoint.position, returnPoint.rotation);

            PostReturnHome();
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
        private void OnTriggerExit(Collider other)
        {
            if (IsController(other.gameObject))
            {
                return;
            }
            ShouldReturnHome = true;
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


        private bool IsController(GameObject collided)
        {
            return collided.GetComponent<XRBaseController>() != null;
        }
    }
}