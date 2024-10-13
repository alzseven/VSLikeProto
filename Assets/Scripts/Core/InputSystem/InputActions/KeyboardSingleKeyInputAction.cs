// 0.0.1

using System;
using Core.InputSystem;
using Core.InputSystem.InputActions;
using UnityEngine;

namespace Content.Core.InputSystem.InputActions
{
    [CreateAssetMenu(fileName = "KeyboardSingleKeyInputAction", menuName = "InputActions/KeyboardSingleKeyInputAction", order = 1)]

    public class KeyboardSingleKeyInputAction : BaseInputAction
    {
        public KeyCode keyCode;

        public KeyboardSingleKeyInputAction(KeyCode keyCode) => this.keyCode = keyCode;

        public override bool IsActionInvoked() => GetInputValue().BoolValue;

        public override InputValue GetInputValue() =>
            inputCondition switch
            {
                EInputCondition.Up => new InputValue { BoolValue = UnityEngine.Input.GetKeyUp(keyCode) },
                EInputCondition.Down => new InputValue { BoolValue = UnityEngine.Input.GetKeyDown(keyCode) },
                EInputCondition.Pressing => new InputValue { BoolValue = UnityEngine.Input.GetKey(keyCode) },
                _ => throw new ArgumentOutOfRangeException()
            };
    }
}