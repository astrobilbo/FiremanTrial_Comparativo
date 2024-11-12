using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiremanTrial.WithArchitecture
{
    public class PlayerInteractions : MonoBehaviour
    {
        
        [SerializeField] private float maxDistanceToTakeObjects = 1f;
        [SerializeField] private LayerMask layerMask;
        private readonly Vector3 _middleScreen = new Vector3(0.5f, 0.5f, 0);
        private Camera _camera;
        private RaycastHit[] _raycastResults;
        private InteractiveObject _activeInteractiveObject;
        
        private void Awake()
        {
            _camera = Camera.main;
            _raycastResults= new RaycastHit[10];
        }

        private void FixedUpdate()
        {
            
            var findInteractiveObject = false;
            var ray = _camera.ViewportPointToRay(_middleScreen);
            var hitCount = Physics.RaycastNonAlloc(ray, _raycastResults, maxDistanceToTakeObjects, layerMask);
            Debug.DrawRay(ray.origin, ray.direction * maxDistanceToTakeObjects, Color.red);

            for (var index = 0; index < hitCount; index++)
            {
                var raycastHit = _raycastResults[index];
                
                if (raycastHit.transform.CompareTag("Wall")) break;
                if (!raycastHit.transform.TryGetComponent<InteractiveObject>(out var interactiveObject))
                    continue;
                
                findInteractiveObject = true;
                if (!_activeInteractiveObject || _activeInteractiveObject.name != interactiveObject.name)
                {
                    Debug.Log("Start new Player Interaction", this);
                    _activeInteractiveObject?.EndInteraction();
                    _activeInteractiveObject = interactiveObject;
                    _activeInteractiveObject.StartInteraction(this);
                }
                break;
            }

            if (!findInteractiveObject && _activeInteractiveObject)
            {
                Debug.Log("End Player Interaction", this);
                _activeInteractiveObject?.EndInteraction();
                _activeInteractiveObject = null;
            }
        }
    }
}
