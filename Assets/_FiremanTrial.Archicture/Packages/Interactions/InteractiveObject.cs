using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace FiremanTrial.WithArchitecture
{
    public abstract class InteractiveObject : MonoBehaviour
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
            IsInteracting = true;
            InteractAction?.Invoke(IsInteracting);
        }

        public void EndInteraction()
        {
            if (!IsInteracting) return;
            IsInteracting = false;
        }

        public void OnRange()
        {
            print("On Range");
            MeshRendererHelper.ApplyEmissionHighlight(MeshRenderers, highlightColor);
        }

        public void OutRange()
        {
            print("Out Range");
            MeshRendererHelper.RemoveEmissionHighlight(MeshRenderers);
        }
    }
}
