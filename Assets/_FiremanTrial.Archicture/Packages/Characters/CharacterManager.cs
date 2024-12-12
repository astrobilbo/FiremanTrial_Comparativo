using System;
using FiremanTrial.Movement;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TextCore.Text;

namespace FiremanTrial.Characters
{
    [RequireComponent(typeof(MovementHandler))]
    [RequireComponent(typeof(Gravity))]
    [RequireComponent(typeof(MoveAnimator))]
    [RequireComponent(typeof(MovementSoundFX))]
    [RequireComponent(typeof(AudioSource))]
    public class CharacterManager : MonoBehaviour
    {
        protected MovementHandler MovementHandler;
        protected Gravity Gravity;
        protected MoveAnimator MovementAnimator;
        protected MovementSoundFX MovementSoundFX;
        protected AudioSource AudioSource;
        [SerializeField] protected Animator animator;
        [SerializeField] protected AudioClip moveClip;
        
        private void Awake()
        {
            MovementHandler = GetComponent<MovementHandler>();
            Gravity = GetComponent<Gravity>();
            MovementAnimator = GetComponent<MoveAnimator>();
            MovementSoundFX = GetComponent<MovementSoundFX>();
            AudioSource = GetComponent<AudioSource>();
            MovementAnimator.Initialize(MovementHandler,animator);
            MovementSoundFX.Initialize(MovementHandler,AudioSource,moveClip);
        }
    }
}
