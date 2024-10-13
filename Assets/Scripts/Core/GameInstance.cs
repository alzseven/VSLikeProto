using UnityEngine;
using UnityEngine.Assertions;

namespace Core
{
    public class GameInstance : MonoBehaviour
    {
        [SerializeField] private int targetFrameRate = 60;
    
        
        public static float GameDelta;
        public static bool IsGamePaused = false;
        private void Awake() => Application.targetFrameRate = targetFrameRate;

        private void Update()
        {
            GameDelta = Time.deltaTime * (IsGamePaused ? 0 : 1);
            
        
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
        }
    }
}