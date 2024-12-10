using System.Collections;
using System.Collections.Generic;
using FiremanTrial.WithArchitecture;
using UnityEngine;

namespace FiremanTrial.WithArchitecture
{
    public class OnObjectRange : SphereOverlapInteractions
    {
        private List<InteractiveObject> _activeInteractiveObject= new List<InteractiveObject>();
        private List<InteractiveObject> _oldInteractiveObject= new List<InteractiveObject>();
        
        protected override void InteractiveObjectLoop(int numColliders)
        {
            loopPreparations();
            
            for (var i = 0; i < numColliders; i++)
            {
                if (!FindTargetObjectsInRange(i)) continue;
                
                _findTargetObject = true;
            }

            if (_findTargetObject)
            {
                ExecuteInteractions();
            }

            EndOldInteractions();
        }

        private void loopPreparations()
        {
            _findTargetObject = false;
            _oldInteractiveObject = _activeInteractiveObject;
            _activeInteractiveObject.Clear();
        }
        
        protected override bool FindTargetObjectsInRange(int i)
        {
            if (!hitColliders[i].transform.TryGetComponent<InteractiveObject>(out var interactiveObject)) return false;
            Debug.Log($"object {interactiveObject.name} is in range of {i}");           
            _activeInteractiveObject.Add(interactiveObject);
            return true;
        }

        protected override void ExecuteInteractions()
        {
            foreach (InteractiveObject interactiveObject in _activeInteractiveObject)
            {
                interactiveObject.OnRange();
            }
            OnObjectPositionRange();
        }

        protected override void EndOldInteractions()
        {
            if (_oldInteractiveObject is null || _oldInteractiveObject.Count==0) return;
            
            _oldInteractiveObject.RemoveAll(obj => _activeInteractiveObject.Contains(obj));

            foreach (var interactiveObject in _oldInteractiveObject)
            {
                Debug.Log($"{interactiveObject.name} is out of range");
                interactiveObject.OutRange();
            }
            _oldInteractiveObject.Clear();
        }
    }
}