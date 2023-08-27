using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject levelCompletePage;
        [SerializeField]
        private TextMeshProUGUI headerForLevelComplete;

         

        private void Start()
        {
            GameManager.OnGameWin += OnGameWin;
            GameManager.OnGameFail  += OnGameFail;
        }
        private void OnDestroy()
        {
            GameManager.OnGameWin -= OnGameWin;
            GameManager.OnGameFail  -= OnGameFail;
        }

        private void OnClickPlay()
        {
            SceneManager.LoadScene((int)SceneNames.GameScene);
        }

        public void OnRestart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene((int)SceneNames.GameScene);
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void OnSettingsON()
        {
            Time.timeScale = 0;
        }
        public void OnSettingsOFF()
        {
            Time.timeScale = 1;
        }

        public void LoadMenuScene()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene((int)SceneNames.Menu);
        }

        private void OnGameWin()
        {
            Time.timeScale = 0;
            headerForLevelComplete.text = "Level Complete";
            levelCompletePage.SetActive(true);
        }

        private void OnGameFail()
        {
            Time.timeScale = 0;
            headerForLevelComplete.text = "Level Fail";
            levelCompletePage.SetActive(true);
        }

        public void OnTwoPlayerGame()
        {
            GameManager.IsAIEnabled = true;
            OnClickPlay();
        }
        public void OnSinglePlayerGame()
        {
            GameManager.IsAIEnabled = false;
            OnClickPlay();
        }
    }
}

enum SceneNames
{
    Menu,
    GameScene
}
