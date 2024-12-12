using System;
using UnityEngine;

namespace FiremanTrial.WithArchitecture
{
    [RequireComponent(typeof(InteractiveObject))]
    public class Gas : MonoBehaviour
    {
        [SerializeField]private Fire Fire;
        private bool _valveIsOpen = true;
        private InteractiveObject _interactiveObject;

        private void Awake()
        {
            _interactiveObject = GetComponent<InteractiveObject>();
        }

        public void CloseValve()
        {
            if (!_valveIsOpen) return;
            
            Fire?.IsolateFuel();
            _valveIsOpen = false;
        }
        public void OpenValve()
        {
            if (_valveIsOpen) return;

            Fire?.StopIsolation();
            _valveIsOpen = true;
        }
    }
}