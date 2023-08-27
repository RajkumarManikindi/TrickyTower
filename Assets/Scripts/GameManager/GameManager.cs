using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using Tower;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnGameWin;
    public static event Action OnGameFail;
    public static bool IsAIEnabled;
    
    public GameObject towerGenerator;

    public static LevelDetails LevelDetails;
    public int currentLevel;
    public List<LevelDetails> levels;
    
    [Header("AI SETUP PROPERTIES")]
    public Vector3 aiTowerPosition;
    public RenderTexture renderTexture;
    public GameObject aiDisplayImage;
    public static void GameWin()
    {
        OnGameWin?.Invoke();
    }
    public static void GameFail()
    {
        OnGameFail?.Invoke();
    }
    
    private void Start()
    {
        if (levels == null || levels.Count == 0)
        {
            Debug.LogError("Levels Not setup");
            return;
        }
        
        LevelDetails = currentLevel < levels.Count ? levels[currentLevel] : levels.First();
        
        Instantiate(towerGenerator, Vector3.zero, Quaternion.identity);
        if (IsAIEnabled)
        {
            var towerObject = Instantiate(towerGenerator, aiTowerPosition, Quaternion.identity);
            towerObject.GetComponentInChildren<Camera>().targetTexture = renderTexture;
            towerObject.GetComponent<TowerGenerator>().ai = true;
            aiDisplayImage.SetActive(true);
        }
    }
}