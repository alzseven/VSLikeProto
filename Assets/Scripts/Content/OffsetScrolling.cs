using System;
using Content.Data;
using Core;
using Core.InputSystem;
using Core.InputSystem.InputActions;
using UnityEngine;

namespace Content
{
    public class OffsetScrolling : MonoBehaviour
    {
        [SerializeField] private PlayerStat _playerStat;
        // [SerializeField] private float scrollSpeed;
        private Renderer _renderer;
        // private static readonly int MainTex = Shader.PropertyToID("_MainTex");

        [SerializeField] private InputComponent _inputComponent;
        [SerializeField] private NormalizedKeyboardAxisInputAction moveAction;
        
        private void Awake()
        {
            if (TryGetComponent(out _renderer) == false)
            {
                Debug.LogWarning($"No Renderer on {gameObject.name}");
            }
            
        }

        private void Start()
        {
            _inputComponent.BindAction(moveAction,(value => UpdateMaterial(value.Vector2Value)) );
        }
        
        public void UpdateMaterial(Vector2 dir)
        {
            if (dir.Equals(Vector2.zero)) return;
            
            Vector2 currentTextureOffset = _renderer.material.GetTextureOffset("_MainTex");
            // works for only localscale.x == localscale.y
            Vector2 newOffset = currentTextureOffset + dir * (_playerStat.moveSpeed / transform.localScale.x * GameInstance.GameDelta);
            // float newXOffset = currentTextureOffset.x + scrollSpeed * Time.deltaTime;
            // Vector2 newOffset = new Vector2(newXOffset,
            //     currentTextureOffset.y);
            _renderer.material.SetTextureOffset("_MainTex", newOffset);
        }
    }
}