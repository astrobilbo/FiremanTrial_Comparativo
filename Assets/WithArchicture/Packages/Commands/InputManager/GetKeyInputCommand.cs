using System;
using System.Collections.Generic;
using FiremanTrial.WithArchitecture.Commands;
using UnityEngine;

namespace FiremanTrial.WithArchitecture.Inputs
{
    public class GetKeyInputCommand : MonoBehaviour
    {
        [SerializeField] private List<KeyInputCommands> keyCommands;

        // Update is called once per frame
        private void Update()
        {
            foreach (var command in keyCommands)
            {
                if (Input.GetKeyDown(command.keyCode))
                {
                    command.onKeyDown?.Execute();
                }

                if (Input.GetKey(command.keyCode))
                {
                    command.onKey?.Execute();
                }

                if (Input.GetKeyUp(command.keyCode))
                {
                    command.onKeyUp?.Execute();
                }
            }
        }
    }
}
[Serializable]
public class KeyInputCommands
{
    public string name;
    public KeyCode keyCode;
    public Command onKeyDown; 
    public Command onKey; 
    public Command onKeyUp; 
}