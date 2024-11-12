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
           questDescriptionText.text = "O calor externo, junto com o calor do fogão, fez com que o óleo ficasse tão quente "
                                       + "que passou a pegar fogo. Como não temos um extintor de classe K disponível em casa, que é o "
                                       + "equipamento específico para esse tipo de incêndio, precisamos agir rápido enquanto ele ainda está "
                                       + "em pequenas proporções e não apresenta grandes riscos.\n"
                                       + "Para apagar o fogo, primeiro <b>desligue o gás</b> para remover a fonte de calor. "
                                       + "Em seguida, <b>cubra a frigideira com uma tampa</b> para interromper o fornecimento de oxigênio.";

           KitchenFire.Active();
       }
      
    }
}