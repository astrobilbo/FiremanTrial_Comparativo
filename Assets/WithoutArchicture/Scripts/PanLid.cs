using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiremanTrial.WithoutArchitecture
{
    public class PanLid : MonoBehaviour
    {
        public Transform panPosition;
        private MeshRenderer _rend;
        // Start is called before the first frame update
        void Start()
        {
             _rend = GetComponent<MeshRenderer>();
        }

        public void GoToUserInventory()
        {
            _rend.enabled = false;
        }

        public void PlaceInPan()
        {
            transform.position = panPosition.position;
            _rend.enabled = true;
        }
    }
}
