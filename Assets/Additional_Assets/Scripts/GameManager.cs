using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused;
    [SerializeField] private PlayableDirector _introCutScene;

    private void Start()
    {
        isPaused = false;
        if (_introCutScene == null )
        {
            Debug.LogWarning("Playable Director on Game Manager is null");
        }

        if (pauseMenuUI ==  null)
        {
            Debug.LogWarning("PauseMenuUI on Game Manager is null");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
        if (Input.GetKeyDown(KeyCode.Q) && _introCutScene.time < 20.00)
        {
            SkipIntroCutscene();
        }
    }

    public void SkipIntroCutscene()
    {
        _introCutScene.time = 20.00;
    }

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;
        AudioListener.pause = isPaused ? true : false;
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
