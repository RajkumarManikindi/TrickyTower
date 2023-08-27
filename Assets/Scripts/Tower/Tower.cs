using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using PieceScripts;
using SpecialPowers;
using TMPro;
using UnityEngine;

namespace Tower
{
    public sealed class Tower : MonoBehaviour
    {
        private Animator _animator;
        private IPhysicsPiece _currentPhysicsPiece;
        private IInputController _inputController;
        private float _score;
        private float _finalScore;
        private bool _isAIControlled;
        private float _remainingLives;
        private readonly List<IPhysicsPiece> _towerPieces = new List<IPhysicsPiece>();
        private Vector3 _startPosition;
        private CameraFollow _cameraFollow;
        private TowerGenerator _towerGenerator;
        
        /// <summary>
        /// All Events And their Functions
        /// </summary>
        public event Action OnPieceCreated;
        public event Action OnPieceControlRemoved;
    
        
        [SerializeField]
        private TextMeshPro scoreValueText;
        [SerializeField]
        private TextMeshPro healthValueText;
        [SerializeField]
        private Rigidbody2D baseBuilding;
        [SerializeField]
        private GameObject pieceStartPoint;
        private void Awake()
        {
            _towerGenerator = GetComponentInParent<TowerGenerator>();
            _animator = GetComponent<Animator>();
            _startPosition = pieceStartPoint.transform.localPosition;
            _remainingLives = _towerGenerator.GetTotalLives();
        }
        
        private void Start()
        {
            AddPieceToTower();
        }

        public void SetUpTower(IInputController inputController, CameraFollow cameraFollow, bool isAIControlled)
        {
            _inputController = inputController;
            _cameraFollow = cameraFollow;
            _isAIControlled = isAIControlled;
        }

       

        public IPhysicsPiece GetCurrentPiece()
        {
            return _currentPhysicsPiece;
        }
        
        public List<IPhysicsPiece> GetPiecesInRadius(IPhysicsPiece basicPiece, float radius)
        {
            var listPieces = new Collider2D[10];
            if (!_towerPieces.Contains(basicPiece)) return null;
            Physics2D.OverlapCapsuleNonAlloc(basicPiece.GetPhysicsPosition(),new Vector2(radius, radius), CapsuleDirection2D.Horizontal,0, listPieces);
            return (from item in listPieces where item!=null && item.GetComponent<PieceDestroyer>() != null select item.GetComponent<PieceDestroyer>().PhysicsPiece).ToList();

        }
    
        private void RemovePieceControl(IPhysicsPiece basicPiece)
        {
            PieceControlRemoved();
            AddPieceToTower();
        }

        private void AddPieceToTower()
        {
            // Checks for if Game status is WIN every time we Add piece
            if (!CheckGameStatus()) { return; }
        
        
            _currentPhysicsPiece = GeneratePiece();
            AddPieceToTowerList(_currentPhysicsPiece);
        
        
            ShakeTower();
            PieceCreated();
        }

        private IPhysicsPiece GeneratePiece()
        {
            var piece = _towerGenerator.GetPhysicsPieceObject();
            
            // Setting position to start point
            var pieceTransform = piece.Rigidbody2DObject.transform;
            pieceTransform.parent = this.transform;
            pieceTransform.localPosition = pieceStartPoint.transform.localPosition;
            piece.Rigidbody2DObject.position = pieceStartPoint.transform.position;
        
            //Sending Rigidbody and piece info for creating Physics piece
            return GeneratePieceUsingPieceObject(piece);
        }

        private IPhysicsPiece GeneratePieceUsingPieceObject(PhysicsPieceObject physicsPieceObject)
        {
            var physicsPiece = new PhysicsPieceController(_inputController, physicsPieceObject);
            
            physicsPiece.OnDestroy += OnPieceRemove;
            physicsPiece.OnCollision += RemovePieceControl;
            
            physicsPiece.SetUpPhysics(physicsPieceObject.PieceInfo.piecePhysicsProperties);
            physicsPiece.SetUpBoundariesAndStartPosition(physicsPieceObject.Rigidbody2DObject.position, physicsPieceObject.PieceInfo.limitForMove);
            
            AddCollisionDestroyer(physicsPieceObject.Rigidbody2DObject.gameObject, physicsPiece);
            return physicsPiece;
        }

        private void AddPieceToTowerList(IPhysicsPiece physicsPiece)
        {
            if (physicsPiece!=null && 
                !_towerPieces.Contains(physicsPiece))
            {
                _towerPieces.Add(physicsPiece);
            }
        }
    

        private void AddCollisionDestroyer(GameObject pieceGameObject, IPhysicsPiece physicsPiece)
        {
            pieceGameObject.AddComponent<PieceDestroyer>().PhysicsPiece = physicsPiece;
        }

        private bool CheckGameStatus()
        {
            if (CheckGameFailStatus())
            {
                GameFail();
                return false;
            }
            else if (CheckGameWinStatus())
            {
                GameWin();
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool CheckGameWinStatus()
        {
            _finalScore = GetTowerHeight();
            return _finalScore > _towerGenerator.GetTotalLevelHeightTarget();
        }

        private bool CheckGameFailStatus()
        {
            return _remainingLives <= 0;
        }

        private void RemovePieceFromList(IPhysicsPiece physicsPiece)
        {
            physicsPiece.OnDestroy -= OnPieceRemove;

            if (_currentPhysicsPiece == physicsPiece)
            {
                _currentPhysicsPiece = null;
            }
            if (_towerPieces.Contains(physicsPiece))
            {
                _towerPieces.Remove(physicsPiece);
            }
            _finalScore = GetTowerHeight();
        }

        private static readonly int Placed = Animator.StringToHash("Placed");
        private void ShakeTower()
        {
            _animator.SetTrigger(Placed);
        }

        private IPhysicsPiece GetTheTopMostPiece()
        {
            IPhysicsPiece selectedItem = null;
            var heightValue = float.MinValue;
            foreach (var item in _towerPieces.Where(item => !item.CanControlPiece()).Where(item => item.GetPhysicsPosition().y > heightValue))
            {
                heightValue = item.GetPhysicsPosition().y;
                selectedItem = item;
            }
            return selectedItem;
        }
        /// <summary>
        /// Testing With Powers
        /// </summary>
        public void Update()
        {
            // if (Input.GetKeyDown(KeyCode.U))
            // {
            //     CreatePiece();
            // }
        
            /* 
        if (Input.GetKeyDown(KeyCode.J))
        {
           
            if (_currentPhysicsPiece == null) return;
            var bombPower = new BombPower(_currentPhysicsPiece, this);
            bombPower.SetUp();
            

            //_currentPiece.OnCollision +=  bombPower.ApplyPowersDirectly();
            // var sizePower = new SizeIncreasePower(_currentPiece);
            // var func = sizePower.ApplyPower(0.5f);
        }
        */
            
        }

        private void FixedUpdate()
        {
            UpdateScore();
            _currentPhysicsPiece?.RunUpdate();
        }

        

        private void UpdateScore()
        {
            if (_score < _finalScore)
            {
                _score += Time.fixedDeltaTime * 10;
            }
            else
            {
                _score = _finalScore;
            }
            scoreValueText.text = ((int)_score)+"/"+_towerGenerator.GetTotalLevelHeightTarget();
            healthValueText.text = ":"+_remainingLives;
        }

        private float GetTowerHeight()
        {
            float towerHeight = 0;
            var data = baseBuilding.position;
        
            var piece = GetTheTopMostPiece();
            if (piece == null) return towerHeight;
        
            var data1 = piece.GetPhysicsPosition();
            towerHeight = Vector2.Distance(data, data1) ;

            MoveCameraAndStartPointWithHeight(towerHeight);

            return towerHeight * 2;
        
        }

        private void MoveCameraAndStartPointWithHeight(float towerHeight)
        {
            if (towerHeight > 10)
            {
                var val = (towerHeight - 10);
                _cameraFollow.MoveToPosition(val);
                var temp = pieceStartPoint.transform.localPosition;
                temp.y = val;
                pieceStartPoint.transform.localPosition = temp;
            }
            else
            {
                _cameraFollow.KeepToStartPosition();
                pieceStartPoint.transform.localPosition = _startPosition;
            }
        }

        private void OnPieceRemove(IPhysicsPiece basicPiece)
        {
            RemovePieceFromList(basicPiece);
            if (_remainingLives <= 0) return;
            _remainingLives--;
            CheckGameStatus();
        }

    

        private void GameWin()
        {
            if (!_isAIControlled)
            {
                GameManager.GameWin();
            }
        }

        private void GameFail()
        {
            if (!_isAIControlled)
            {
                GameManager.GameFail();
            }
        }

        private void PieceCreated()
        {
            OnPieceCreated?.Invoke();
        }

        private void PieceControlRemoved()
        {
            OnPieceControlRemoved?.Invoke();
        }
    
    
    
    }
}