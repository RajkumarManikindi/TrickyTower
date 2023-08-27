using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;

namespace PieceScripts
{
    public class PhysicsPiece : BasicPiece, IPhysicsPiece
    {
        private readonly Rigidbody2D _selfRigidBody;
        public event Action<IPhysicsPiece> OnCollision;
        public event Action<IPhysicsPiece> OnDestroy;
        readonly List<RaycastHit2D> _rayCastHits = new List<RaycastHit2D>();
        readonly ContactFilter2D _filterMode = new ContactFilter2D();
        private readonly float _moveSpeed;
        
        protected PhysicsPiece(PhysicsPieceObject physicsPieceObject, float fallingDownSpeed):base( physicsPieceObject.PieceInfo.width, physicsPieceObject.PieceInfo.height)
        {
            _selfRigidBody = physicsPieceObject.Rigidbody2DObject;
            _selfRigidBody.isKinematic = true;
            _moveSpeed = fallingDownSpeed;
        }

        public void SetUpPhysics(PiecePhysicsProperties properties)
        {
            var newPhysics = new PhysicsMaterial2D
            {
                bounciness = properties.bounce,
                friction = properties.friction
            };

            _selfRigidBody.sharedMaterial = newPhysics;
            
            _selfRigidBody.mass = properties.mass;
            _selfRigidBody.gravityScale = properties.gravity;
            _selfRigidBody.angularDrag = properties.angularDrag;
            _selfRigidBody.drag = properties.linearDrag;
            
            DestinationPosition = _selfRigidBody.position;
        }

        public  void StopPhysics()
        {
            _selfRigidBody.useFullKinematicContacts = true;
            _selfRigidBody.WakeUp();
            _selfRigidBody.isKinematic = false;
            _selfRigidBody.gravityScale = 1;
            _selfRigidBody.mass = 10;
        }
        
        public  Vector2 GetPhysicsPosition()
        {
            return _selfRigidBody.position;
        }
        
        public  Rigidbody2D GetRigidBody2D()
        {
            return _selfRigidBody;
        }

        
        public void RunUpdate()
        {
            if (!CanControlThePiece) return;
            MoveVertical(_moveSpeed);
        }

        public bool CanControlPiece()
        {
            return CanControlThePiece;
        }


        public  List<IBasicPiece> GetContactedPieces()
        {
            var filter2D = new ContactFilter2D();
            var results = new List<Collider2D>();
            _selfRigidBody.OverlapCollider(filter2D,results);
            var listOfPieces = results.Select(item => item.GetComponent<IBasicPiece>()).ToList();
            return listOfPieces;
        }
        
        public void MoveHorizontally(float val)
        {
            if (Math.Abs(BoostSpeed - 1) > 0) 
                return;
            var finalVal = DestinationPosition.x + val;
            if (IsDestinationPositionIsInLimits(finalVal))
            {
                DestinationPosition.x = finalVal;    
            }
        }
        
        public void MoveVertical(float val)
        {
            var finalValue = val * BoostSpeed;
            DestinationPosition.y -= finalValue;
             CheckForCollision(DestinationPosition);
        }
        
        private bool IsDestinationPositionIsInLimits(float position)
        {
            return position >= HorizontalLimit.x && position < HorizontalLimit.y;
        }

        
        private bool CheckForPositionAvailability(Vector2 destinationPosition)
        {
            var position = _selfRigidBody.position;
            var direction = (destinationPosition - position).normalized;
            var dis = Vector2.Distance(destinationPosition, position);
            return _selfRigidBody.Cast(direction, _filterMode, _rayCastHits,  dis).Equals(0);
            
        }

        private void CheckForCollision(Vector2 destinationPosition)
        {
            if (CheckForPositionAvailability(destinationPosition))
            {
                MoveObjectAfterConditionsMet(destinationPosition, DestinationAngle);
            }
            else
            {
                OnCollisionToObject();
            }
        }

        private void MoveObjectAfterConditionsMet(Vector2 destinationPosition, float destinationAngle)
        {
            //Debug.Log("Destination Position - "+destinationPosition.x+"__"+destinationPosition.y);
            _selfRigidBody.MovePosition(destinationPosition);
            _selfRigidBody.MoveRotation(destinationAngle);
        }
        
        public void Rotate(float val)
        {
            DestinationAngle += val;
        }

        public void RemoveFromTower()
        {
            OnDestroy?.Invoke(this);
        }

        protected virtual void OnCollisionToObject()
        {
            if (!CanControlThePiece) return;
            StopPhysics();
            CanControlThePiece = false;
            OnCollision?.Invoke(this); 
        }
    }
}
