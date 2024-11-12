using System;
using UnityEngine;

namespace FiremanTrial.UI
{
    [CreateAssetMenu(fileName = "CellphoneBackgroundImages", menuName = "FiremanTrial/Background Images")]
    public class CellphoneBackgroundImages: ScriptableObject
    {
        public Sprite backgroundSprite;
        public Color backgroundColor;
    }
}