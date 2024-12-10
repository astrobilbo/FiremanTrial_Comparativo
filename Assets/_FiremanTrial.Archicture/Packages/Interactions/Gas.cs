using UnityEngine;

namespace FiremanTrial.WithArchitecture
{
    public class Gas : InteractiveObject
    {
        [SerializeField]private Fire Fire;
        private bool _valveIsOpen = true;
        
        public void CloseValve()
        {
            if (!_valveIsOpen) return;
            
            Fire.IsolateFuel();
            _valveIsOpen = false;
        }
        public void OpenValve()
        {
            if (_valveIsOpen) return;

            Fire.StopIsolation();
            _valveIsOpen = true;
        }
    }
}