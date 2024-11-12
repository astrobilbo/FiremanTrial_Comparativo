using System;
using UnityEngine;
using UnityEngine.Events;

namespace FiremanTrial.WithArchitecture
{
    public class SphereOverlapInteractions : MonoBehaviour
    {
        public Action ObjectInPositionRange;
        public UnityEvent objectInRange;
        public float sphereRange=1f;        
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private TargetType targetType;
        Collider[] hitColliders=new Collider[10];
        private InteractiveObject _activeInteractiveObject;
        private Collider _collider;
        private void FixedUpdate()
        {
            var findInteractiveObject = false;
            var numColliders = Physics.OverlapSphereNonAlloc(this.transform.position, sphereRange, hitColliders, layerMask);
            for (var i = 0; i < numColliders; i++)
            {
                switch (targetType)
                {
                    case TargetType.Player when hitColliders[i].CompareTag("Player"):
                        findInteractiveObject = true;
                        if (!_collider || _collider != hitColliders[i])
                        {
                            Debug.Log("Player find in sphere", this);
                            _collider=hitColliders[i];
                            ObjectInPositionRange?.Invoke();
                            objectInRange?.Invoke();
                        }

                        break;
                    case TargetType.InteractiveObject when hitColliders[i].transform.TryGetComponent<InteractiveObject>(out var interactiveObject):
                        findInteractiveObject = true;
                        if (!_activeInteractiveObject || _activeInteractiveObject.name != interactiveObject.name)
                        {
                            Debug.Log("Interactive object find in sphere", this);
                            _activeInteractiveObject=interactiveObject;
                            interactiveObject.OnPlayerRange();
                            ObjectInPositionRange?.Invoke();
                            objectInRange?.Invoke();
                        }

                        break;
                }
                
            }

            if (!findInteractiveObject && (_activeInteractiveObject || _collider))
            {
                _activeInteractiveObject?.EndInteraction();
                _activeInteractiveObject = null;
                _collider = null;
            }
        }

        private enum TargetType
        {
            Player=0,
            InteractiveObject=1
        }
    }
}
