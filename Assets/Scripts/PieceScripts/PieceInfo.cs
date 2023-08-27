using System;
using UnityEngine;

namespace PieceScripts
{
    [Serializable]
    public class PieceInfo
    {
        [SerializeField]
        private string name;
        public GameObject piecePrefab;
        public float width;
        public float height;

        [Header("Left And Right LimitValue")]
        public float limitForMove;
        
        [Header("Physics Values")] 
        public PiecePhysicsProperties piecePhysicsProperties;

    }

    [Serializable]
    public class PiecePhysicsProperties
    {
        [Range(0, 1)]
        public float friction = 1;
        [Range(0, 1)]
        public float bounce = 0;
        [Range(0, 100)]
        public float mass = 1;
        [Range(0, 10)]
        public float gravity = 0;
        [Range(0, 10)]
        public float angularDrag = 0;
        [Range(0, 10)]
        public float linearDrag = 0;
    }
    
    public class PhysicsPieceObject
    {
        public Rigidbody2D Rigidbody2DObject;
        public PieceInfo PieceInfo;
    }

}