using System.Collections;
using Interfaces;
using UnityEngine;

namespace SpecialPowers
{
    public class SizeIncreasePower : IPowers
    {
        private readonly IPhysicsPiece _physicsPiece;
        private const float WaitTime = 0.5f;
        private const float MoveUpValue = -5f;
        public SizeIncreasePower(IPhysicsPiece physicsPiece)
        {
            _physicsPiece = physicsPiece;
        }
        
       public IEnumerator ApplyPowers()
       {
           _physicsPiece.MoveVertical(MoveUpValue);
           yield return new WaitForSeconds(WaitTime);
           var piecePhysics = _physicsPiece.GetRigidBody2D();
           if (piecePhysics != null)
           {
               piecePhysics.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);
           }
       }

       public void SetUp()
       {
           throw new System.NotImplementedException();
       }
    }
}