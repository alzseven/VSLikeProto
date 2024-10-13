// 0.0.1

using System;
using Content.Core.InputSystem.InputActions;
using UnityEngine;

namespace Core.InputSystem.InputActions
{
    [CreateAssetMenu(fileName = "MouseInputAction", menuName = "InputActions/MouseInputAction", order = 0)]

    public class MouseInputAction : BaseInputAction
    {
        public int mouseButton;

        public MouseInputAction(int mouseButton) => this.mouseButton = mouseButton;

        public override bool IsActionInvoked() => GetInputValue().BoolValue;

        public override InputValue GetInputValue() =>
            inputCondition switch
            {
                EInputCondition.Up => new InputValue { BoolValue = UnityEngine.Input.GetMouseButtonUp(mouseButton) },
                EInputCondition.Down => new InputValue { BoolValue = UnityEngine.Input.GetMouseButtonDown(mouseButton) },
                EInputCondition.Pressing => new InputValue { BoolValue = UnityEngine.Input.GetMouseButton(mouseButton) },
                _ => throw new ArgumentOutOfRangeException()
            };
    }
}
