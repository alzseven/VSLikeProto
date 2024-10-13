// 0.0.1

using System;
using UnityEngine;
using UnityEngine.Serialization;
using Vector3 = System.Numerics.Vector3;

namespace Core.InputSystem.InputActions
{
    [CreateAssetMenu(fileName = "NormalizedKeyboardAxisInputAction",
        menuName = "InputActions/NormalizedKeyboardAxisInputAction", order = 2)]
    public class NormalizedKeyboardAxisInputAction : BaseInputAction
    {
        [Header("XAxis")] [SerializeField] private KeyCode horizontalPositiveKeyCode;
        [SerializeField] private KeyCode horizontalNegativeKeyCode;

        [Header("YAxis")] [SerializeField] private KeyCode verticalPositiveKeyCode;
        [SerializeField] private KeyCode verticalNegativeKeyCode;


        public override bool IsActionInvoked() => true;

        public override InputValue GetInputValue()
        {
            return inputCondition switch
            {
                EInputCondition.Down => new InputValue()
                {
                    Vector2Value = new Vector2(
                        // x
                        Input.GetKeyDown(horizontalPositiveKeyCode) ? 1f :
                        Input.GetKeyDown(horizontalNegativeKeyCode) ? -1f : 0f,
                        // y
                        Input.GetKeyDown(verticalPositiveKeyCode) ? 1f :
                        Input.GetKeyDown(verticalNegativeKeyCode) ? -1f : 0f).normalized
                },
                EInputCondition.Up => new InputValue()
                {
                    Vector2Value = new Vector2(
                        // x
                        Input.GetKeyUp(horizontalPositiveKeyCode) ? 1f :
                        Input.GetKeyUp(horizontalNegativeKeyCode) ? -1f : 0f,
                        // y
                        Input.GetKeyUp(verticalPositiveKeyCode) ? 1f :
                        Input.GetKeyUp(verticalNegativeKeyCode) ? -1f : 0f).normalized
                },
                EInputCondition.Pressing => new InputValue()
                {
                    Vector2Value = new Vector2(
                            // x
                            Input.GetKey(horizontalPositiveKeyCode) ? 1f :
                            Input.GetKey(horizontalNegativeKeyCode) ? -1f : 0f,
                            // y
                            Input.GetKey(verticalPositiveKeyCode) ? 1f :
                            Input.GetKey(verticalNegativeKeyCode) ? -1f : 0f)
                        .normalized
                },
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}