using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace FiremanTrial.WithoutArchitecture
{
    public class Fire : MonoBehaviour
    {
        [SerializeField]private string fireClass;
        [SerializeField] private ParticleSystem particle;
        private ParticleSystem.EmissionModule emissionModule;
        private void Start()
        {
            emissionModule = particle.emission;
            emissionModule.enabled = true;
            emissionModule.rateOverDistance = 1f; 
        }
        public void TryReduceFire(Extinguisher extinguisher)
        {
            if (extinguisher.fireClass.Contains(fireClass))
            {
                ReduceFire();
            }
            else
            {
                IncreaseFire();
            }
        }

        public void extinguishFire()
        {
            particle.Stop();
        }
        public void FireAddons(float changer)
        {
            emissionModule.rateOverDistance = new ParticleSystem.MinMaxCurve(emissionModule.rateOverDistance.constant + changer);
        }

        private void ReduceFire()
        {
            emissionModule.rateOverDistance = new ParticleSystem.MinMaxCurve(Mathf.Max(0, emissionModule.rateOverDistance.constant - 0.1f));
        }

        private void IncreaseFire()
        {
            emissionModule.rateOverDistance = new ParticleSystem.MinMaxCurve(emissionModule.rateOverDistance.constant + 0.1f);
        }

        public void Active()
        {
            particle.Play();
        }
    }
}
