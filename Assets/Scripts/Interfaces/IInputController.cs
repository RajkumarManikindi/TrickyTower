using System;

namespace Interfaces
{
    public interface IInputController
    {
        public event Action<float> MoveHorizontal;
        public event Action<float> MoveVertical;
        public event Action<float> Rotate;
        public event Action<float> ActivateBoost;

        public float LeftAndRightMoveSpeed { get; set; }
        public float RotateAngle{ get; set; }
        public float FallDownSpeed{ get; set; }   
        public float FallDownBoostSpeed{ get; set; }
    }
}