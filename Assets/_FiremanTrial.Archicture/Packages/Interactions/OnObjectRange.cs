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
            _oldInteractiveObject = new List<InteractiveObject>(_activeInteractiveObject);
            _activeInteractiveObject.Clear();
        }
        
        protected override bool FindTargetObjectsInRange(int i)
        {
            if (!hitColliders[i].transform.TryGetComponent<InteractiveObject>(out var interactiveObject)) return false;
            _activeInteractiveObject.Add(interactiveObject);
            return true;
        }

        protected override void ExecuteInteractions()
        {
            foreach (InteractiveObject interactiveObject in _activeInteractiveObject)
            {
                if (AlreadyCalledOnRange(interactiveObject)) continue;
                interactiveObject.OnRange();
            }
            OnObjectPositionRange();
        }

        private bool AlreadyCalledOnRange(InteractiveObject interactiveObject) => _oldInteractiveObject.Contains(interactiveObject);

        protected override void EndOldInteractions()
        {
            if (OldInteractionObjectIsNullOrEmpty()) return;
            
            PrepareOutRangeList();

            foreach (var interactiveObject in _oldInteractiveObject)
            {
                interactiveObject.OutRange();
            }
            
            _oldInteractiveObject.Clear();
        }
        
        // tira os objetos que ainda estão no raio de ação da lista.
        private void PrepareOutRangeList() => _oldInteractiveObject.RemoveAll(obj => _activeInteractiveObject.Contains(obj));

        private bool OldInteractionObjectIsNullOrEmpty() => _oldInteractiveObject is null || _oldInteractiveObject.Count==0;
    }
}