using System;
using UnityEngine;

namespace FiremanTrial.Movement
{
    public interface IMovementDirectionNotifier
    {
        event Action<Vector3> DirectionObserver;
    }
}