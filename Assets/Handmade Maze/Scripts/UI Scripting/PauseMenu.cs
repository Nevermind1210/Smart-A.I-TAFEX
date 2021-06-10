using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIElements
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool isPaused = false;

        public GameObject pausemenuUI;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        public void Resume()
        {
            pausemenuUI.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }

        void Pause()
        {
            pausemenuUI.SetActive(true);
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            isPaused = true;
        }

        public void LoadMenu() // though this is gonna cause a bug, why cause it will null everything on the next time you load the scene
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}