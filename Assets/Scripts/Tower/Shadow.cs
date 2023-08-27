using Interfaces;
using PieceScripts;
using UnityEngine;

namespace Tower
{
    public class Shadow : MonoBehaviour
    {
    
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        [SerializeField]
        private Tower tower;
        private IPhysicsPiece _physicsPiece;
        private float _size;
        private void Start()
        {
            tower.OnPieceCreated += ActivateShadow;
            tower.OnPieceControlRemoved += DeActivateShadow;
        }

        private void OnDestroy()
        {
            tower.OnPieceCreated -= ActivateShadow;
            tower.OnPieceControlRemoved -= DeActivateShadow;
        }

        private void ActivateShadow()
        {
            spriteRenderer.gameObject.SetActive(true);
        }

        private void DeActivateShadow()
        {
            spriteRenderer.gameObject.SetActive(false);
        }
        private void FixedUpdate()
        {
            if(!spriteRenderer.gameObject.activeInHierarchy) return;
            _physicsPiece = tower.GetCurrentPiece();
            if (_physicsPiece == null) return;
            _size =  ((BasicPiece)_physicsPiece).GetSize();
            
            spriteRenderer.size = new Vector2(_size, spriteRenderer.size.y);
            var pos = _physicsPiece.GetPhysicsPosition();
            var localTransform = transform;
            var curPos = localTransform.position;
            curPos.x = pos.x;
            localTransform.position = curPos;
        }
    
    
    }
}
