using Content.Components.Entity;
using Core.InputSystem;
using Core.InputSystem.InputActions;
using UnityEngine;

namespace Content.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerComponent _playerComponent;
        private InputComponent _inputComponent;
        [SerializeField] private NormalizedKeyboardAxisInputAction _keyboardAxisInputAction;
        
        private void Awake()
        {
            _playerComponent = FindObjectOfType<PlayerComponent>();
            _inputComponent = FindObjectOfType<InputComponent>();
        }
        
        private void OnEnable() => _inputComponent.BindAction(_keyboardAxisInputAction,
            value => _playerComponent.MoveToDirection(value.Vector2Value));

        private void OnDisable() =>
            //TODO: should i?
            _inputComponent.TryRemoveBinding(_keyboardAxisInputAction,
                value => _playerComponent.MoveToDirection(value.Vector2Value));
    }
}