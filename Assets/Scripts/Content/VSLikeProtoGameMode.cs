using System;
using Content.Components.Entity;
using Core;
using UnityEngine;

namespace Content
{
    public class VSLikeProtoGameMode : MonoBehaviour
    {
        [SerializeField] private PlayerComponent _playerComponent;
        public static float GameTime;


        public static int KillCount
        {
            get => _killCount;
            set
            {
                _killCount = value;
                OnKillCountChanged?.Invoke(_killCount);
            }
        }

        private static int _killCount;
        public static Action<int> OnKillCountChanged;

        

        //TODO: As minute or secs?
        private static float _timeToSurvive;
        [SerializeField] private float timeToSurvive;
        

        private void Awake()
        {
            _playerComponent.OnPlayerDead += OnGameEnded;
            _timeToSurvive = timeToSurvive;
        }

        private void Start()
        {
            _killCount = 0;
            OnKillCountChanged?.Invoke(_killCount);
        }

        // Update is called once per frame
        void Update()
        {
            GameTime += GameInstance.GameDelta;
        }

        public void OnGameEnded()
        {
            GameInstance.IsGamePaused = true;
            if (GameTime > _timeToSurvive)
            {
                OnVictory();
            }
            else
            {
                OnLose();
            }
            return;
            
            void OnVictory()
            {
                Debug.Log("win");
            }

            void OnLose()
            {
                Debug.Log("lose");
            }
        }
    }
}