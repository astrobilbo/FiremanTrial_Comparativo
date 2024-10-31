using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiremanTrial.WithArchitecture.Player
{
    using Interactions;
    public class PlayerInteractions : MonoBehaviour
    {
        [SerializeField] private float maxDistanceToTakeObjects = 1f;
        [SerializeField] private LayerMask layerMask;
        private readonly Vector3 _middleScreen = new Vector3(0.5f, 0.5f, 0);
        private bool _active = true;
        private Camera _camera;
        private RaycastHit[] _raycastResults;
        
        public bool Active { get => _active; set => _active = value; }
        private void Start()
        {
            _camera = Camera.main;
            _raycastResults= new RaycastHit[10];
        }

        private void Update()
        {
            if (!_active) return;
            
            var ray = _camera.ViewportPointToRay(_middleScreen);
            Physics.RaycastNonAlloc(ray, _raycastResults, maxDistanceToTakeObjects, layerMask);
            Debug.DrawRay(ray.origin, ray.direction * maxDistanceToTakeObjects, Color.red);

            foreach (var raycastHit in _raycastResults)
            {
                if (!raycastHit.transform) continue;
                
                if (raycastHit.transform.TryGetComponent<IInteraction>(out IInteraction interaction))
                {
                    interaction.Interact(this.gameObject);
                    break; 
                }
                
                Debug.Log("Objeto encontrado, mas não é interativo.", raycastHit.collider.gameObject);
            }
        }
    }
}
