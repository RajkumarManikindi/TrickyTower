using Interfaces;
using UnityEngine;

namespace PieceScripts
{
    public class BasicPiece : IBasicPiece
    {
        protected float DestinationAngle;
        protected Vector2 DestinationPosition;

        public Vector2 HorizontalLimit{ get; set; }
        
        public bool CanControlThePiece { get; set; }
        public float Width { get; }
        public float Height { get; }
        
        public float BoostSpeed { get; set; }
        

        protected BasicPiece(float width, float height, bool canControlThePiece = true)
        {
            Width = width;
            Height = height;
            CanControlThePiece = canControlThePiece;
            BoostSpeed = 1;
        }
        
        public float GetSize()
        {
            return DestinationAngle % 180 == 0 ? Width : Height;
        }
         
        
        public void SetUpBoundariesAndStartPosition(Vector2 pos, float val)
        {
            var xVal = pos.x;
            var horizontalLimit = new Vector2(xVal - val, xVal + val);
            HorizontalLimit = horizontalLimit;
            //DestinationPosition = pos;
        }
         
        
        

        public void ChangeBoostValue(float fallingDownBoostSpeed = 1)
        {
            BoostSpeed = fallingDownBoostSpeed;
        }

       
    }
}
