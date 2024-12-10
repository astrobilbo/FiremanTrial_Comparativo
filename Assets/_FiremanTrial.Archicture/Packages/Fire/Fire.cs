using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace FiremanTrial.WithArchitecture
{
    public class Fire : MonoBehaviour
    {
        [SerializeField] private float intensity;
        [SerializeField] private FireClass fireClass;
        
        [SerializeField] private float growthRate= 0.1f;
        [SerializeField] private float penaltySpreadRateForWrongExtinguisher = 0.1f;
        [SerializeField] private float spreatRateReduction=0.1f;
        [SerializeField] private float maxIntensity = 2f;
        
        private bool _isCooling=false;
        private bool _isMuffling=false;
        private bool _isIsolating=false;

        public bool IsBurning() => intensity > 0;

        
        public void StartFire()
        {
            
            InvokeRepeating(nameof(Burn),1f,1f);
        }
        
        private void Burn()
        {
            if (!IsBurning()) return;


            if (_isCooling || _isMuffling || _isIsolating) return;
            if (intensity > maxIntensity)
            {
                Failed();
                CancelInvoke();
                return;
            }

            intensity += Time.deltaTime * growthRate;
        }

        public void Cooling(FireClass extinguisherFor)
        {
            if (!IsBurning()) return;
            
            if (extinguisherFor.HasFlag(fireClass))
            {
                intensity -= spreatRateReduction;
                _isCooling=true;
            }
            else
            {
                intensity += penaltySpreadRateForWrongExtinguisher;
            }
        }
        
        public void MuffleTheOxygen()
        {
            if (!IsBurning()) return;
            _isMuffling = true;
            intensity = 0;
        }
        
        public void IsolateFuel()
        {
            if (!IsBurning()) return;
            _isIsolating = true;
            intensity -= spreatRateReduction;
        }

        public void StopCooling() => _isCooling = false;
        public void StopMuffleing() => _isMuffling = false;
        public void StopIsolation() => _isIsolating = false;

        private void Failed()
        {
            Debug.Log($"{nameof(Fire)} failed");
        }
    }

    [Flags] public enum FireClass
    {
        None = 0,
        A = 1 << 0,
        B = 1 << 1,
        C = 1 << 2,
        D = 1 << 3,
        K = 1 << 4
    }
}