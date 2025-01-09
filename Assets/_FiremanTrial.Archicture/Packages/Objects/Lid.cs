using FiremanTrial.WithArchitecture;
using UnityEngine;


namespace FiremanTrial.Object
{
    [RequireComponent(typeof(InteractiveObject))]
    public class Lid : MonoBehaviour
    {
        
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Transform initialPositionHolder;
        private InteractiveObject _interactiveObject;
        private bool _isInteracting;

        private void Awake()
        {
            _interactiveObject = GetComponent<InteractiveObject>();
        }

        private void OnEnable() => SetObserver();
        private void OnDisable() => RemoveObserver();

        private void SetObserver()
        {
            if (_interactiveObject is null) return;
            _interactiveObject.InteractAction += IsInteracting;
        }

        private void RemoveObserver()
        {
            if (_interactiveObject is null) return;
            _interactiveObject.InteractAction -= IsInteracting;
        }
        private void IsInteracting(bool isInteracting) => _isInteracting = isInteracting;
        
        void GetLid()
        {
            if (!_interactiveObject) return;
            meshRenderer.enabled = false;
        }

        void PlaceLid(Transform holder)
        {
            if (!_interactiveObject) return;
            gameObject.transform.parent = holder.transform;
            gameObject.transform.position = Vector3.zero;
            meshRenderer.enabled = true;
        }

        void ReturnLidToInitialPosition()
        {
            if (!_interactiveObject) return;
            gameObject.transform.parent = initialPositionHolder.transform;
            gameObject.transform.position = Vector3.zero;
            meshRenderer.enabled = true;
        }
    }
}
