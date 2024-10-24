using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiremanTrial.WithoutArchitecture
{
    public class KitchenGas : MonoBehaviour
    {
        private bool _open=true;
        [SerializeField]Fire fire;
        float fireChanger=0.1f;
        // Start is called before the first frame update
        void Start()
        {
            InvokeRepeating("ChangeFire",1f,1f);
        }

        // Update is called once per frame
        void ChangeFire()
        {
            fire.FireAddons(fireChanger);
        }

        public void CloseGas()
        {
            if (!_open)
            {
                fireChanger = -0.1f;
                _open = false;
            }
        }
        public bool Open(){return _open;}
        
    }
}
