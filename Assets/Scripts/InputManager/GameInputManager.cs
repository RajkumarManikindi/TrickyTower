using Controls;
using UnityEngine;

namespace InputManager
{
    public class GameInputManager : MonoBehaviour
    {
        private static InputController Controller { get; set; }
        private static AIController AIController { get; set; }

        private void Awake()
        {
#if UNITY_EDITOR
            Controller = GetComponentInChildren<KeyBoardController>();
#elif UNITY_IOS || UNITY_ANDROID
            Controller = GetComponentInChildren<MobileController>();
#endif
            AIController = GetComponentInChildren<AIController>();
        }
        
        public static InputController GetController()
        {
            return Controller;
        }
    
        public static AIController GetAIController()
        {
            return AIController;
        }
    }
}