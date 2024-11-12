using System;
using System.Collections.Generic;
using FiremanTrial.WithArchitecture.Commands;
using UnityEngine;
using UnityEngine.Events;

namespace FiremanTrial.WithArchitecture.Inputs
{
    public class GetKeyInputCommand : MonoBehaviour
    {
        public KeyCode keyCode;
        public UnityEvent onKeyDown;
        public UnityEvent onKey;
        public UnityEvent onKeyUp;

        private void Update()
        {

            if (Input.GetKeyDown(keyCode))
            {
                onKeyDown?.Invoke();
            }

            if (Input.GetKey(keyCode))
            {
                onKey?.Invoke();
            }

            if (Input.GetKeyUp(keyCode))
            {
                onKeyUp?.Invoke();
            }

        }
    }
}