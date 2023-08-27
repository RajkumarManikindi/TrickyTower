using System.Collections;
using Interfaces;
using UnityEngine;

namespace SpecialPowers
{
    public class BombPower : IPowers
    {
        private readonly IPhysicsPiece _physicsPiece;
        private readonly Tower.Tower _tower;
        public BombPower(IPhysicsPiece physicsPiece, Tower.Tower tower)
        {
            _tower = tower;
            _physicsPiece = physicsPiece;
        }

        public void SetUp()
        {
            _physicsPiece.OnCollision += ApplyPowersDirectly;
        }

        private void ApplyPowersDirectly(IPhysicsPiece basicPiece)
        {
            var allPieces= _tower.GetPiecesInRadius(_physicsPiece,5);
            foreach (var items in allPieces)
            {
                var direction = (_physicsPiece.GetPhysicsPosition() - items.GetPhysicsPosition()).normalized;
                items.GetRigidBody2D()?.AddRelativeForce(-direction * Time.fixedDeltaTime * 40f, ForceMode2D.Impulse);
            }
            _physicsPiece.OnCollision -= ApplyPowersDirectly;
        }


        public IEnumerator ApplyPowers()
        {
            throw new System.NotImplementedException();
        }
    }
}
