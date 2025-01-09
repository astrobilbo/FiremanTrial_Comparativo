using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiremanTrial.Characters
{
    public class Wife : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private void Awake()
        {
            Terrified();
        }

        public void Terrified()
        {
            //set to play the scared anim in the animator
            animator.Play("Terrified");
        }

        public void SillyDancing()
        {
            animator.Play("Silly Dancing");
        }
        
        
    }
}
