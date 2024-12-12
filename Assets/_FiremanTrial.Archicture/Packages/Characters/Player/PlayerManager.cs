
namespace FiremanTrial.Characters.Player
{
    public class PlayerManager : CharacterManager
    {

       public void InteractionWithDoor(float time)
       {
           MovementHandler.StopMovement();
       }

       public void EndInteractionWithDoor()
       {
           MovementHandler.RestartMovement();
       }

        
    }
}
