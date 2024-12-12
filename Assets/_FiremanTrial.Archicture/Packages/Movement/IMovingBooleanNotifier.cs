using System;

namespace FiremanTrial.Movement
{
    public interface IMovingBooleanNotifier
    {
        event Action<bool> BooleanObserver;
    }
}