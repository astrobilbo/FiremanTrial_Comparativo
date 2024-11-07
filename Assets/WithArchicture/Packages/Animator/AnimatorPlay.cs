using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiremanTrial.WithArchitecture
{
    public class AnimatorPlay : AnimatorBase
    {
        public AnimatorPlay()
        {
            if (animator != null)
            {
                animator.Play(ParamID);
            }
        }
    }
}
