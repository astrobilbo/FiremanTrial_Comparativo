using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FiremanTrial.WithoutArchitecture
{
    public class Dialogue : MonoBehaviour
    {
        [SerializeField]CanvasGroup canvasGroup;
        [SerializeField]TextMeshProUGUI dialogueText;
        [SerializeField]string[] dialogues;
        [SerializeField] private GameObject interactionText;
        int currentDialogue=0;
        bool dialogueActive=false;
        // Start is called before the first frame update
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                NextDialogue();
            }
        }
        [ContextMenu("test")]
        public void StartDialogue()
        {
            canvasGroup.alpha = 1;
            currentDialogue = 0;
            dialogueText.text = dialogues[currentDialogue];
            dialogueActive = true;
            interactionText.SetActive(true);
        }

        // Update is called once per frame
        void NextDialogue()
        {
            if (!dialogueActive)
            {
                return;
            }
            if (currentDialogue < dialogues.Length - 1)
            {
                currentDialogue++;
                dialogueText.text = dialogues[currentDialogue];
            }
            else
            {
                EndDialogue();
            }
        }

        void EndDialogue()
        {
            canvasGroup.alpha = 0;
            dialogueText.text = "";
            dialogueActive = false;
            interactionText.SetActive(false);
        }
    }
}
