using Interfaces;

namespace PieceScripts
{ 
    public class PhysicsPieceController : PhysicsPiece
    {
        private readonly IInputController _controller;

        public PhysicsPieceController(IInputController controller, PhysicsPieceObject physicsPieceObject ) : base(physicsPieceObject, controller.FallDownSpeed)
        {
            _controller = controller;
            _controller.MoveHorizontal += MoveHorizontally;
            _controller.MoveVertical += MoveVertical;
            _controller.Rotate += Rotate;
            _controller.ActivateBoost += ChangeBoostValue;
        }

        protected override void OnCollisionToObject()
        {
            base.OnCollisionToObject();
            _controller.MoveHorizontal -= MoveHorizontally;
            _controller.MoveVertical -= MoveVertical;
            _controller.Rotate -= Rotate;
            _controller.ActivateBoost -= ChangeBoostValue;
        }
        
        
    }
}