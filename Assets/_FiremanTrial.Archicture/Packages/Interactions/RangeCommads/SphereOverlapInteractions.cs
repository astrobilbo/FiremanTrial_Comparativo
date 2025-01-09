using System;
using UnityEngine;

namespace FiremanTrial.WithArchitecture
{
    public abstract class SphereOverlapInteractions : MonoBehaviour
    {
        public event Action ObjectInRange;
        [SerializeField] protected float sphereRange=1f;        
        [SerializeField] private LayerMask layerMask;
        protected Collider[] hitColliders=new Collider[10];
        protected bool _findTargetObject;
        private void FixedUpdate() => InteractiveObjectLoop(PhysicsSphereOverlap());

        private int PhysicsSphereOverlap()
        {
             return Physics.OverlapSphereNonAlloc(transform.position, sphereRange, hitColliders, layerMask);
        }

        protected abstract void InteractiveObjectLoop(int numColliders);
        protected abstract bool FindTargetObjectsInRange(int i);
        protected abstract void ExecuteInteractions();
        protected abstract void EndOldInteractions();

        protected void OnObjectPositionRange() => ObjectInRange?.Invoke();
    }
}
