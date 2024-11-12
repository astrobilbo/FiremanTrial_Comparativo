using System;
using System.Collections.Generic;
using FiremanTrial.MainMenu;
using FiremanTrial.WithArchitecture;
using UnityEngine;

namespace FiremanTrial.UI
{
    public class InteractiveObjectGUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup interactionCanvasGroup;
        private List<InteractiveObject> _interactionObjects;
        private void Awake()
        {
            CanvasGroupManager.Visible(false, interactionCanvasGroup);
            _interactionObjects = new List<InteractiveObject>(FindObjectsOfType<InteractiveObject>());
        }

        private void OnEnable()
        {
            foreach (var interaction in _interactionObjects)
            {
                interaction.InteractAction += UpdateUI;
            }        
        }

        private void OnDisable()
        {
            foreach (var interaction in _interactionObjects)
            {
                interaction.InteractAction -= UpdateUI;
            }
        }

        private void UpdateUI(bool isInteracting)
        {
            Debug.Log(isInteracting);
            CanvasGroupManager.Visible(isInteracting, interactionCanvasGroup);
        }

    }
}
