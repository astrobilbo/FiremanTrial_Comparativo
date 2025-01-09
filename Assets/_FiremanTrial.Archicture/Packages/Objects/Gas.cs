using FiremanTrial.WithArchitecture;
using UnityEngine;

namespace FiremanTrial.Object
{
    [RequireComponent(typeof(InteractiveObject))]
    public class Gas : MonoBehaviour
    {
        [SerializeField] private Fire fire;
        [SerializeField] private float timebetweenFireIntensityReduction;
        private bool _valveIsOpen = true;
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
        public bool CanInteract() => _isInteracting;

        public void CloseValve()
        {
            if (!_isInteracting) return;
            if (!_valveIsOpen) return;
            if (fire.IsBurning()) InvokeRepeating(nameof(IsolateFire), 0f, timebetweenFireIntensityReduction);
            _valveIsOpen = false;
        }

        public void OpenValve()
        {
            if (!_isInteracting) return;
            if (_valveIsOpen) return;
            CancelInvoke();
            fire?.StopIsolation();
            _valveIsOpen = true;
        }

        void IsolateFire()
        {
            fire?.IsolateFuel();
        }

    }
}
