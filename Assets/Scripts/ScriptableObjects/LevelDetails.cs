using System.Collections.Generic;
using PieceScripts;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "LevelDetails", menuName = "ScriptableObjects/LevelDetails", order = 1)]
    public class LevelDetails : ScriptableObject
    {
        [Header("Piece GamePlay Values")] 
        [Range(0.1f, 1f)]
        public float leftAndRightMoveSpeed;
        [Range(0.01f, 1f)]
        public float fallingDownSpeed;
        [Range(1f, 10f)]
        public float fallingDownBoostSpeed;
        [Range(0f, 360f)]
        public float rotateAngle = 90;

        
        [Header("GamePlay Values")] 
        [Range(1, 200)]
        public int totalHeightTarget;
        
        [Range(1, 10)]
        public int totalLives;

        public List<PieceInfo> pieceInfos;
    }
    
}