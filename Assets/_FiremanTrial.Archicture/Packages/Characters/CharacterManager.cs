using FiremanTrial.Movement;
using FiremanTrial.MovementAnimator;
using FiremanTrial.Sound;
using UnityEngine;
using UnityEngine.Serialization;

namespace FiremanTrial.Characters
{
    public class CharacterManager : MonoBehaviour
    {
        [SerializeField] protected MovementHandler movementHandler;
        [SerializeField] protected Gravity gravity;
        [SerializeField] protected MoveAnimator movementAnimator;
        [FormerlySerializedAs("soundFXPlayer")] [SerializeField] protected MovementSoundFX movementSoundFX;
        
    }
}
