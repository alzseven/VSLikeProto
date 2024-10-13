// 0.0.2

using Core.InputSystem.InputActions;
using UnityEngine;

namespace Core.InputSystem
{
    public abstract class BaseInputAction : ScriptableObject, IInputAction<InputValue>
    {
        public EInputCondition inputCondition;
        public bool canBeInvokedDuringPause;
        
        public virtual bool IsActionInvoked() => default;

        public virtual InputValue GetInputValue() => new();
    }
}