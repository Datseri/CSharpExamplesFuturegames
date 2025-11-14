using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using Random = UnityEngine.Random;

namespace Learning.Prototype {
    public enum Weapons {
        None,
        Pistol,
        Rifle,
        RocketLauncher
    }

    public record PlayerInfo(string Name, int Level, float Health, int HighScore);

    public sealed class GameManager : MonoBehaviour {


        public static GameManager Instance;

        public GameObject bulletPrefab;

        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI livesText;
        public TextMeshProUGUI weaponText;
        public TextMeshProUGUI ammoText;
        public TextMeshProUGUI highScoreText;

        public int lives = 3;
        private static int level = 1;
        public Queue<Bullet> activeBullets;
        private Player player;

        public static float PlayerHealth {
            get => GameData.playerHealth;
            set => GameData.playerHealth = (int)value;
        }


        private void Awake() {
            if(Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
           GameData.highScore = PlayerPrefs.GetInt("HighScore", 0);
            lives = GameData.playerLives;
            level = GameData.currentLevel;

        }

        private void Start() {
            player = FindFirstObjectByType<Player>();
            RefreshUI();
            activeBullets = new Queue<Bullet>();
        }

        private void Update() {
            CheckWinCondition();
        }


        #region UI

        public void RefreshUI() {
            scoreText.text = GameData.score.ToString();
            livesText.text = lives.ToString();
            weaponText.text = player.currentWeapon.ToString();
            ammoText.text = player.ammoDict[player.currentWeapon].ToString();
            highScoreText.text = GameData.highScore.ToString();
        }

        #endregion

        #region GameStates

        public void Pause() {
            GameData.gameIsPaused = true;
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }

        public void UnPause() {
            GameData.gameIsPaused = false;
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void GameOver() {
            GameData.gameIsOver = true;
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }

        private void Victory() {
            GameData.gameIsOver = true;
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }

        public void QuitGame() {
            Application.Quit();
        }

        #endregion

        #region Score
        private void CheckWinCondition() {
            if(GameData.score >= 5000 && !GameData.gameIsOver) {
                Victory();
            }
        }

        public void AddScore(int s) {
            GameData.score += s;
            if(GameData.score > GameData.highScore) {
                GameData.highScore = GameData.score;
                PlayerPrefs.SetInt("HighScore", GameData.highScore);
            }
            RefreshUI();
        }


        #endregion















    }

}
