using System;
using UnityEngine;

namespace Learning.Prototype {
    public class InputManager : MonoBehaviour {

        public Action onShootPressed;
        private void Update() {
            InputHandling();
        }
        private void InputHandling() {
            if(Input.GetKeyDown(KeyCode.Escape)) {
                if(GameData.gameIsPaused) {
                    GameManager.Instance.UnPause();
                }
                else {
                    GameManager.Instance.Pause();
                }
            }

            if(Input.GetKeyDown(KeyCode.Alpha1)) {
                GameData.godMode = !GameData.godMode;
                Debug.Log("GodMode " + GameData.godMode);
            }

            if(Input.GetKeyDown(KeyCode.Alpha2)) {
                GameData.score += 1000;
                GameManager.Instance.RefreshUI();
            }


            if(Input.GetKeyDown(KeyCode.E)) {
                onShootPressed?.Invoke();
            }

            if(!GameData.gameIsPaused && !GameData.playerIsDead) {
                float mx = Input.GetAxis("Mouse X") * GameData.mouseSensitivity;
                float my = Input.GetAxis("Mouse Y") * GameData.mouseSensitivity;
            }
        }

    }
}
