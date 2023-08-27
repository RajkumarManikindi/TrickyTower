using UnityEngine;

namespace Interfaces
{
     public interface IBasicPiece 
     {
          float GetSize();
          float Width { get; }
          float Height { get; }
          
          float BoostSpeed { get; set; }
          bool CanControlThePiece { get; set; }
          Vector2 HorizontalLimit { get; set; }
     }
}
