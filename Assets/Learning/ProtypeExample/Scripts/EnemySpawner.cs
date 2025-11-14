using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Learning.Prototype {
    public class EnemySpawner : MonoBehaviour {

        private Player player;
        public Enemy enemyPrefab;
        public Transform[] spawnPoints;
        public List<Enemy> enemies; //DO NOT ASSIGN THIS IN INSPECTOR, USED FOR TRACKING ENEMIES
        private float nextSpawn = 0;

        private void Start() {
            player = FindFirstObjectByType<Player>();
        }
        private void Update() {
            if(player == null) return;
            SpawnEnemy();
        }

        private void SpawnEnemy() {
            if(!GameData.gameIsOver && !GameData.gameIsPaused && Time.time > nextSpawn && enemies.Count < GameData.numMaxEnemies) {
                nextSpawn = Time.time + Random.Range(1f, 3f);
                int r = Random.Range(0, spawnPoints.Length);
                Enemy enemy = Instantiate(enemyPrefab, spawnPoints[r].position, Quaternion.identity);
                enemies.Add(enemy);
                enemy.transform.position = new Vector3(enemy.transform.position.x + Random.Range(1f, 3f), enemy.transform.position.y, enemy.transform.position.z + Random.Range(1f, 3f));
                enemy.transform.LookAt(player.transform);
                player.StartMovingTowardsPlayer(enemy);
            }
        }
    }
}
