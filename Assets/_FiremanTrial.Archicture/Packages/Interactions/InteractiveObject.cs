using System;
using System.Collections.Generic;
using UnityEngine;

namespace FiremanTrial.WithArchitecture
{
    public class InteractiveObject : MonoBehaviour
    {
        public Action<bool> InteractAction;
        public bool IsInteracting { get; private set; }
        [SerializeField] protected Color highlightColor = Color.yellow; 
        protected List<MeshRenderer> MeshRenderers;

        private void Awake()
        {
            MeshRenderers = MeshRendererHelper.GetAllMeshRenderers(gameObject);
        }

        public void StartInteraction()
        {
            if (IsInteracting) return;
            Debug.Log($"{gameObject.name} is interacting", this);
            IsInteracting = true;
            InteractAction?.Invoke(IsInteracting);
        }

        public void EndInteraction()
        {
            if (!IsInteracting) return;
            Debug.Log($"{gameObject.name} not is more interacting",this);
            IsInteracting = false;
            InteractAction?.Invoke(IsInteracting);
        }

        public void OnRange()
        {
            Debug.Log($"{gameObject.name} is on range", this);
            MeshRendererHelper.ApplyEmissionHighlight(MeshRenderers, highlightColor);
        }

        public void OutRange()
        {
            Debug.Log($"{gameObject.name} is out range",this);
            MeshRendererHelper.RemoveEmissionHighlight(MeshRenderers);
        }
    }
}
