using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace FiremanTrial.WithoutArch
{
    public class Fire : MonoBehaviour
    {
        [SerializeField]private string fireClass;
        
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

        private void ReduceFire()
        {
            
        }

        private void IncreaseFire()
        {
            
        }
    }
}
