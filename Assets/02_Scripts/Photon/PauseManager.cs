using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused = false;

    private void Update()
    {
        if (false)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }

        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true); // 퍼즈메뉴 활성화
        Time.timeScale = 0f; // 게임 멈춤
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false); // 퍼즈메뉴 비활성화
        Time.timeScale = 1f; // 게임 재개
    }

    public void QuitGame()
    {
        Time.timeScale = 1f; // 게임 재개 후 종료
        SceneManager.LoadScene("MainLobby");
    }
}
