using UnityEngine;
using FiremanTrial.Movement;
using FiremanTrial.MovementAnimator;
using FiremanTrial.Sound;
using FiremanTrial.WithArchitecture;

namespace FiremanTrial.Characters.Player
{
    public class PlayerManager : CharacterManager
    {
       [SerializeField] private InteractWithObjects interactWithObjects;

       public void OpenDoor()
       {
           movementHandler.StopMovement();
           //wait for animation
           movementHandler.RestartMovement();
       }

        
    }
}
