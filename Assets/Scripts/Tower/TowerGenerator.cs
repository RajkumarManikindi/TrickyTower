using InputManager;
using Interfaces;
using PieceScripts;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Tower
{
    public class TowerGenerator : MonoBehaviour
    {
        public GameObject towerPrefab;
        public bool ai;
        public CameraFollow cameraForTower;
        private LevelDetails levelDetails;
        
        private void Start()
        {
            levelDetails = GameManager.LevelDetails;
            //Creating INPUT SYSTEM according to selection
            IInputController inputController = ai
                ? GameInputManager.GetAIController()
                : GameInputManager.GetController();

            SetUpControllerValues(inputController);
            var currentTower = Instantiate(towerPrefab,transform).GetComponent<Tower>();

            if (currentTower != null)
            {
                currentTower.SetUpTower(inputController, cameraForTower, ai);
            }
        }

        private void SetUpControllerValues(IInputController inputController)
        {
            inputController.LeftAndRightMoveSpeed = levelDetails.leftAndRightMoveSpeed;
            inputController.RotateAngle = levelDetails.rotateAngle;
            inputController.FallDownBoostSpeed = levelDetails.fallingDownBoostSpeed;
            inputController.FallDownSpeed = levelDetails.fallingDownSpeed;
        }

        public float GetTotalLevelHeightTarget()
        {
            return levelDetails.totalHeightTarget;
        }
        
        public int GetTotalLives()
        {
            return levelDetails.totalLives;
        }
        
        public PhysicsPieceObject GetPhysicsPieceObject()
        {
            var piece = GetRandomPieceInfo();
            var pieceGameObject = Instantiate(piece.piecePrefab);
            var rigidBody = pieceGameObject.GetComponent<Rigidbody2D>();
            return new PhysicsPieceObject
            {
                PieceInfo = piece,
                Rigidbody2DObject = rigidBody
            };
        }
        
        private PieceInfo GetRandomPieceInfo()
        {
            var selectedNum = Random.Range(0,levelDetails.pieceInfos.Count);
            return levelDetails.pieceInfos[selectedNum];
        }
    }
}