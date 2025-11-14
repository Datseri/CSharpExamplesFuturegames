using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Learning.Prototype {


    public class Player : MonoBehaviour {
        public Image healthBar;
        internal Weapons currentWeapon = Weapons.Pistol;
        private InputManager inputManager;

        private void Start() {
            inputManager = FindFirstObjectByType<InputManager>();
            inputManager.onShootPressed += PlayerShoot;
        }

        public readonly Dictionary<Weapons, int> ammoDict = new() {
            { Weapons.None, 0 },
            { Weapons.Pistol, 99 },
            { Weapons.Rifle, 0 },
            { Weapons.RocketLauncher, 0 }
        };

        public void StartMovingTowardsPlayer(Enemy enemy) {
            enemy.StartMovingTowards(transform);
        }

        public void PlayerShoot() {
            if(ammoDict[currentWeapon] <= 0) {
                return;
            }
            ammoDict[currentWeapon]--;
            GameManager.Instance.RefreshUI();
            // spawn bullet
            var go = Instantiate(GameManager.Instance.bulletPrefab, transform.position + transform.forward, transform.rotation);
            var bullet = go.GetComponent<Bullet>();
            GameManager.Instance.activeBullets.Enqueue(bullet);
        }

        public void DamagePlayer(int dmg) {
            if(GameData.godMode) {
                return;
            }
            healthBar.fillAmount -= GetDamagePercent(dmg);
            if(GameData.playerHealth <= 0) {
                KillPlayer();
            }
        }


        public void DamagePlayer(float dmg) {
            if(GameData.godMode) {
                return;
            }
            healthBar.fillAmount -= GetDamagePercent(dmg);
            if(GameData.playerHealth <= 0) {
                KillPlayer();
            }
        }

        private float GetDamagePercent(int dmg) {
            return dmg / 1f;
        }

        private float GetDamagePercent(float dmg) {
            return dmg / 1f;
        }

        private void KillPlayer() {
            GameData.playerIsDead = true;
            GameManager.Instance.lives--;
            if(GameManager.Instance.lives <= 0) {
                GameManager.Instance.GameOver();
            }
            else {
                Invoke("RespawnPlayer", 2f);
            }
        }

        private void RespawnPlayer() {
            healthBar.fillAmount = 1f;
            GameData.playerHealth = 100;
            GameData.playerIsDead = false;
            GameManager.Instance.RefreshUI();
        }
    }
}
