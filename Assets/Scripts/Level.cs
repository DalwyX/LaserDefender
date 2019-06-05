using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {

    [SerializeField] float gameOverDelay = 1f;

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadNextLevel()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(DelayCoroutine(nextScene));
    }

    public void LoadGameOver()
    {
        StartCoroutine(DelayCoroutine("Game Over"));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator DelayCoroutine(string sceneName)
    {
        yield return new WaitForSeconds(gameOverDelay);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator DelayCoroutine(int sceneNumber)
    {
        yield return new WaitForSeconds(gameOverDelay);
        SceneManager.LoadScene(sceneNumber);
    }

}
