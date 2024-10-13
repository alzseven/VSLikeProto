// 0.0.1

using System;
using System.Collections.Generic;
using Content.Core.InputSystem.InputActions;
using Core.InputSystem.InputActions;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.InputSystem
{
    public class InputComponent : MonoBehaviour
    {
        [SerializeField] private List<BaseInputAction> _inputActions = new ();
        private Dictionary<BaseInputAction, Action<InputValue>> _inputActionMapping = new();
        // public List<BaseInputAction> InputActions => _inputActions;
        
#if UNITY_EDITOR
        
        // private void OnValidate()
        // {
        //     if (_inputActions.Count > 0) return;
        //     LoadInputActions();
        // }

        [ContextMenu(nameof(LoadInputActions))]
        private void LoadInputActions()
        {
            _inputActions = new List<BaseInputAction>();
            foreach (var action in Resources.LoadAll<BaseInputAction>("Data")) _inputActions.Add(action);
            
            EditorUtility.SetDirty(this);
        }
#endif
        private void Awake()
        {
            Assert.IsTrue(_inputActions.Count >= 1);
            // _inputActionMapping = new Dictionary<BaseInputAction, Action<InputValue>>();
            
        }

        public void BindAction(BaseInputAction action, Action<InputValue> callback)
        {
            if (_inputActions.Contains(action) == false)
            {
                // _inputActions.Add(action);
                throw new ArgumentException();
            }

            if (_inputActionMapping.TryAdd(action, callback) == false)
            {
                _inputActionMapping[action] += callback;
            }
        }
        
        public bool TryRemoveBinding(BaseInputAction action, Action<InputValue> callback)
        {
            if (!_inputActionMapping.TryGetValue(action, out _)) return false;
            _inputActionMapping[action] -= callback;
            return true;
        }
        
        private void Update()
        {
            // foreach (var action in _inputActions)
            // {
            //     if (action.IsActionInvoked() &&
            //         ((GameInstance.IsGamePaused && action.canBeInvokedDuringPause == false) == false)) 
            //         _inputActionMapping[action]?.Invoke(action.GetInputValue());
            // }
            foreach (var action in _inputActions)
            {
                if (action.IsActionInvoked()) 
                    _inputActionMapping[action]?.Invoke(action.GetInputValue());
            }
        }
    }
}