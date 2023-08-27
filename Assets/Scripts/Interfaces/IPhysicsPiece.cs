using System;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface IPhysicsPiece
    {
        void StopPhysics();
        Vector2 GetPhysicsPosition();
        
       
        List<IBasicPiece> GetContactedPieces();
        Rigidbody2D GetRigidBody2D();
        
        void MoveHorizontally(float val);
        void MoveVertical(float val);
    
        void Rotate(float angle);

        void ChangeBoostValue(float val);
        void RunUpdate();
        void RemoveFromTower();
        bool CanControlPiece();
        event Action<IPhysicsPiece> OnCollision;
        event Action<IPhysicsPiece> OnDestroy;
        
    }
}