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
            this.assignedInteractable = GetComponents<XRGrabInteractable>()[0];
            if (!returnPoint)
            {
                this.returnPoint = new GameObject().transform;
                this.SetHome(this.transform);
            }
            this.ShouldReturnHome = true;
        }

        private void OnEnable()
        {
            this.assignedInteractable.selectEntered.AddListener(OnSelect);
            this.assignedInteractable.selectExited.AddListener(OnSelectExit);

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
            rigidbody.rotation = this.returnPoint.rotation;
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.Sleep();
            this.transform.SetPositionAndRotation(this.returnPoint.position, this.returnPoint.rotation);

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
            bool isEngaged = socket != null && socket.CanSelect(this.assignedInteractable);
            this.ShouldReturnHome = !isEngaged;

        }
        private void OnTriggerExit(Collider other)
        {
            if (IsController(other.gameObject))
            {
                return;
            }
            this.ShouldReturnHome = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name.Equals("Suelo"))
            {
                Invoke(nameof(this.AttemptReturnHome), this.resetDelayTime);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.name.Equals("Suelo"))
            {
                CancelInvoke(nameof(this.AttemptReturnHome));
            }
        }


        private bool IsController(GameObject collided)
        {
            return collided.GetComponent<XRBaseController>() != null;
        }

        public void SetHome(Transform destination)
        {
            this.returnPoint.SetPositionAndRotation(destination.position, destination.rotation);
        }

        public void TeleportToNewHome(Transform destination)
        {
            this.SetHome(destination);
            this.ReturnHome();
        }
    }
}