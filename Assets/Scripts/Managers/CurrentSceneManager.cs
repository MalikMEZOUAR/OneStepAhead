using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CurrentSceneManager : MonoBehaviour
{
    public bool isDebugConsoleOpened = false;
    public GameObject GAMEOVER;
    [Header("Listen to events")]
    public StringEventChannel onLevelEnded;
    public VoidEventChannel onPlayerDeath;
    public BoolEventChannel onDebugConsoleOpenEvent;

    private void Start()
    {
        Application.targetFrameRate = 60;
        GAMEOVER.SetActive(false);
        Time.timeScale = 1f;
    }

    private void OnEnable()
    {
        onLevelEnded.OnEventRaised += LoadScene;
        onPlayerDeath.OnEventRaised += Die;
        onDebugConsoleOpenEvent.OnEventRaised += OnDebugConsoleOpen;
    }

    public void LoadScene(string sceneName)
    {
        if (UtilsScene.DoesSceneExist(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.Log($"Unknown scene named {sceneName}. Please add the scene to the build settings.");
        }
    }

    public void LoadScene(int sceneIndex)
    {
        if (UtilsScene.DoesSceneExist(sceneIndex))
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.Log($"Unknown scene with index {sceneIndex}. Please add the scene to the build settings.");
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void RestartLastCheckpoint()
    {
        Debug.Log("RestartLastCheckpoint");
        // Refill life to full
        // Position to last checkpoint
        // Remove menu
        // Reset Rigidbody
        // Reactivate Player movements
        // Reset Player's rotation
    }

    public static void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    private void Die(){
        StartCoroutine(DieAfterDelay(1f));
    }
private IEnumerator DieAfterDelay(float delay)
{
    yield return new WaitForSeconds(delay); 
    GAMEOVER.SetActive(true);
}
    private void OnDisable()
    {
        onLevelEnded.OnEventRaised -= LoadScene;
        onPlayerDeath.OnEventRaised -= Die;
        onDebugConsoleOpenEvent.OnEventRaised -= OnDebugConsoleOpen;
    }

    private void OnDebugConsoleOpen(bool debugConsoleOpened)
    {
        isDebugConsoleOpened = debugConsoleOpened;
    }
}
