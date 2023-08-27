using Interfaces;
using UnityEngine;
namespace PieceScripts
{
    public class PieceDestroyer : MonoBehaviour
    {
        private const string TagName = "Base";
        public IPhysicsPiece PhysicsPiece { get; set; }
        public void OnCollisionEnter2D(Collision2D col)
        {
            if (!col.transform.CompareTag(TagName)) return;
            PhysicsPiece.RemoveFromTower();
            Destroy(this.gameObject);

        }
    }
}