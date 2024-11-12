using System;
using UnityEngine;
using UnityEngine.Events;

namespace FiremanTrial.WithArchitecture
{
    public abstract class InteractiveObject : MonoBehaviour
    {
        public Action<bool> InteractAction;
        public bool IsInteracting { get; private set; }
        private PlayerInteractions _player;
        public virtual void StartInteraction(PlayerInteractions player)
        {
            if (IsInteracting) return;
            Debug.Log("Starting interaction",this);
            IsInteracting = true;
            _player=player;
            InteractAction?.Invoke(IsInteracting);        }

        public virtual void EndInteraction()
        {
            if (!IsInteracting) return;
            Debug.Log("Ending interaction",this);
            IsInteracting=false;
            _player = null;
            InteractAction?.Invoke(IsInteracting);
        }
       public virtual void OnPlayerRange()
       {
           
       }
       
    }
}
