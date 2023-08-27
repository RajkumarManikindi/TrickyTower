using System;
using Interfaces;
using UnityEngine;

namespace Controls
{
    public class InputController : MonoBehaviour, IInputController
    {
        public event Action<float> MoveHorizontal;
        public event Action<float> MoveVertical;
        public event Action<float> Rotate;
        public event Action<float> ActivateBoost;
        
        
        public float LeftAndRightMoveSpeed { get; set; }
        public float RotateAngle { get; set; }
        public float FallDownSpeed { get; set; }
        public float FallDownBoostSpeed { get; set; }
        

        protected void MoveObjectHorizontal(float multiplier = 1)
        {
            MoveHorizontal?.Invoke(LeftAndRightMoveSpeed * multiplier);
        }

        protected void ActivateBoostFall()
        {
            ActivateBoost?.Invoke(FallDownBoostSpeed);
        }
        protected void DeActivateBoostFall()
        {
            ActivateBoost?.Invoke(1);
        }

        protected void MoveObjectVertical()
        {
            MoveVertical?.Invoke(FallDownSpeed);
        }

        protected void RotateObject()
        {
            Rotate?.Invoke(RotateAngle);
        }
        

    }
}