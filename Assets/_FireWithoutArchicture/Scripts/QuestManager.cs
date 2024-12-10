using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FiremanTrial.WithoutArchitecture
{
    public class QuestManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI questTitleText;
        [SerializeField] private TextMeshProUGUI questDescriptionText;
        [SerializeField] private Animator doorAnimator;
        [SerializeField] private Fire KitchenFire;
        [SerializeField] private Dialogue dialogue;
        [SerializeField] private Animator wife;
        private void Start()
        {
            questTitleText.text = "Vá até a cozinha.";
            questDescriptionText.text = "Sua esposa está te chamando, abra a porta do quarto e ajude-a.";
            dialogue.StartDialogue();
        }

        public void OpenDoor()
        {
            doorAnimator.SetTrigger("Open");
            ActiveQuest2();
        }
        
        public bool DoorIsOpen()
        {
           return doorAnimator.GetBool("Open");
        }
        
       
       private void ActiveQuest2()
       {
           questTitleText.text = "Apague o incêndio de classe K";
           questDescriptionText.text = 
               "O calor fez com que o óleo ficasse tão quente que passou a pegar fogo. " +
               "Como não temos um extintor de classe K disponível em casa, equipamento específico para esse tipo de incêndio, " +
               "precisamos agir rápido enquanto ele ainda está em pequenas proporções e não apresenta grandes riscos.\n\n" +
               "Para apagar o fogo, primeiro <b><size=130%><color=#FF4500>desligue o gás</color></size></b> " +
               "para remover a fonte de calor. Em seguida, <b><size=130%><color=#FF4500>cubra a frigideira com uma tampa</color></size></b> " +
               "para interromper o fornecimento de oxigênio.";

           KitchenFire.Active();
       }

       public void EndFire()
       {
           wife.Play("Silly Dancing");
       }
      
    }
}