using UnityEngine;
using UnityEngine.Events;

namespace FiremanTrial.WithArchitecture
{
    public class OnPlayerRange : SphereOverlapInteractions
    {
        private Collider _collider;
        [SerializeField] UnityEvent OnPlayerRangeHit;
        
        protected override void InteractiveObjectLoop(int numColliders)
        {
            _findTargetObject = false;
            
            for (var i = 0; i < numColliders; i++)
            {
                if (!FindTargetObjectsInRange(i)) continue;
                
                _findTargetObject = true;
                ExecuteInteractions();
            }

            if (!_findTargetObject)
            {
                EndOldInteractions();   
            }        
        }

        protected override bool FindTargetObjectsInRange(int i)
        {
            var findObject = false;
            if (hitColliders[i].CompareTag("Player"))
            {
                findObject = true;
                if (!_collider || _collider != hitColliders[i])
                {
                    _collider = hitColliders[i];
                }
            }
            return findObject;
        }

        protected override void ExecuteInteractions()
        {
            OnObjectPositionRange();
            OnPlayerRangeHit.Invoke();
        }

        protected override void EndOldInteractions()
        {
            if (!_collider) return;
            _collider = null;
        }
    }
}